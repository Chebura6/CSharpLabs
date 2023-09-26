using System.Collections.ObjectModel;
using Isu.Entities;
using Isu.Models;
using Isu.MyExceptions;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    public IsuService()
    {
        _groups = new List<Group>();
    }

    public IReadOnlyCollection<Group> Groups => _groups;
    public Group AddGroup(GroupName name)
    {
        if (name is null) throw new NullGroupNameException();
        if (_groups.Any(currentGroup => currentGroup.GroupName.Equals(name)))
        {
            throw new AlreadyExistGroupException(); // if group with this name already exists
        }

        var addingGroup = new Group(name);
        _groups.Add(addingGroup);
        return addingGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group is null) throw new NullGroupException();
        if (string.IsNullOrWhiteSpace(name)) throw new NullOrWhitespaceStringException();
        Group findingGroup = _groups.FirstOrDefault(currentGroup => currentGroup.Equals(group));
        if (findingGroup is null)
        {
            throw new GroupIsNotFoundException();
        }

        var addingStudent = new Student(group, name);
        findingGroup.AddStudentInGroup(addingStudent);
        return addingStudent;
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new NotValidStudentIDException();
    }

    public Student FindStudent(int id)
    {
        return _groups
            .SelectMany(x => x.Students)
            .FirstOrDefault(student => student.IsuId == id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        if (groupName is null) throw new NullGroupNameException();
        return _groups.Where(g => g.GroupName.Equals(groupName)).SelectMany(g => g.Students).ToList();
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        if (courseNumber is null) throw new NullCourseNumberException();
        return _groups
            .SelectMany(group => group.Students)
            .Where(student => student.Course.Equals(courseNumber))
            .ToList();
    }

    public Group FindGroup(GroupName groupName)
    {
        if (groupName is null) throw new NullGroupNameException();
        return _groups.FirstOrDefault(currentGroup => currentGroup.GroupName.Equals(groupName));
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        if (courseNumber is null) throw new NullCourseNumberException();
        return _groups.Where(currentGroup => currentGroup.CourseNumber.Equals(courseNumber)).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (student is null) throw new NullStudentException();
        if (newGroup is null) throw new NullGroupException();
        Group oldGroup = _groups
            .FirstOrDefault(g => g.GroupName.Equals(student.GroupName));
        if (oldGroup is null)
        {
            throw new GroupIsNotFoundException();
        }

        oldGroup.RemoveStudentFromGroup(student);
        newGroup.AddStudentInGroup(student);
    }
}