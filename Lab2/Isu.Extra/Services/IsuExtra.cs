using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtra : IIsuService, IIsuExtra
{
    private readonly List<OgnpGroup> _ongpGroups;
    private readonly List<ExtraStudent> _ognpPossibleStudents;
    private readonly List<ExtraGroup> _extraGroups;
    private IsuService _isu;

    public IsuExtra()
    {
        _isu = new IsuService();
        _ongpGroups = new List<OgnpGroup>();
        _ognpPossibleStudents = new List<ExtraStudent>();
        _extraGroups = new List<ExtraGroup>();
    }

    public IReadOnlyCollection<OgnpGroup> Groups => _ongpGroups;
    public OgnpGroup AddOgnpGroup(OgnpGroupName groupName, Timetable timetable)
    {
        ArgumentNullException.ThrowIfNull(groupName, "Null ognp group detected");
        ArgumentNullException.ThrowIfNull(timetable, "Null timetable");
        OgnpGroup ognpGroup = new OgnpGroup(groupName, timetable);
        _ongpGroups.Add(ognpGroup);
        ognpGroup.OgnpGroupName = groupName;
        return ognpGroup;
    }

    public ExtraStudent AddStudentAtOgnpGroup(OgnpGroup ognpGroup, Student student)
    {
        ArgumentNullException.ThrowIfNull(ognpGroup, "Null ognp group detected");
        ArgumentNullException.ThrowIfNull(student, "Null student detected");
        ExtraStudent extraStudent = _ognpPossibleStudents.FirstOrDefault(s => s.Student.Equals(student));
        if (extraStudent is null)
        {
            throw IsuExtraException.ImpossibleAddStudentInOgnpGroup();
        }

        Group group = _isu.FindGroup(student.GroupName);
        Group studentsGroup = _isu.FindGroup(student.GroupName);
        Timetable studentsCurrentTimetable = _extraGroups.FirstOrDefault(g => g.Group.Equals(studentsGroup)).Timetable;
        if (!ognpGroup.Timetable.AreTimetablesIntersect(studentsCurrentTimetable))
            ognpGroup.AddStudentInGroup(extraStudent);
        else throw TimetableException.TimetablesAreIntersect();
        return extraStudent;
    }

    public void RemoveCourseRegistration(ExtraStudent student)
    {
        ArgumentNullException.ThrowIfNull(student, "Null student detected");
        OgnpGroup group = _ongpGroups
            .FirstOrDefault(g => g.OgnpGroupName.Equals(student.OgnpGroupName));
        if (group is null) throw OgnpGroupException.GroupNotFound();
        group.RemoveStudentFromGroup(student);
    }

    public IReadOnlyCollection<ExtraStudent> GetOgnpGroupStudents(OgnpGroupName groupName)
    {
        ArgumentNullException.ThrowIfNull(groupName, "Null student detected");
        return _ongpGroups.FirstOrDefault(g => g.OgnpGroupName.Equals(groupName)).ExtraStudents;
    }

    public IReadOnlyCollection<ExtraStudent> GetStudentsWithoutOgnpGroup()
    {
        return _ognpPossibleStudents.Where(s => s.OgnpGroupName == null).ToList();
    }

    public void SetGroupTimetable(GroupName groupName, Timetable timetable)
    {
        ArgumentNullException.ThrowIfNull(groupName);
        ArgumentNullException.ThrowIfNull(timetable);
        ExtraGroup extraGroup = _extraGroups.FirstOrDefault(g => g.Group.GroupName.Equals(groupName));
        if (extraGroup is null) IsuExtraException.GroupNotFound();
        extraGroup.Timetable = timetable;
    }

    public void SetOgnpGroupTimetable(OgnpGroup ognpGroup, Timetable timetable)
    {
        ArgumentNullException.ThrowIfNull(ognpGroup);
        ArgumentNullException.ThrowIfNull(timetable);
        ognpGroup.Timetable = timetable;
    }

    public Timetable GetGroupTimetable(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);
        return _extraGroups.FirstOrDefault(g => g.Group.Equals(group))?.Timetable ?? throw IsuExtraException.GroupNotFound();
    }

    public Timetable GetOgnpGroupTimetable(OgnpGroup ognpGroup)
    {
        ArgumentNullException.ThrowIfNull(ognpGroup);
        return _ongpGroups.FirstOrDefault(g => g == ognpGroup)?.Timetable ?? throw IsuExtraException.GroupNotFound();
    }

    public IReadOnlyCollection<OgnpGroup> GetOgnpGroups()
    {
        return _ongpGroups;
    }

    public IReadOnlyCollection<OgnpGroup> GetSameCourseGroups(char courseLetter)
    {
        return _ongpGroups.Where(g => g.OgnpGroupName.Name[0] == courseLetter).ToList();
    }

    public Group AddGroup(GroupName name)
    {
        Group group = _isu.AddGroup(name);
        ExtraGroup extraGroup = new ExtraGroup(group);
        _extraGroups.Add(extraGroup);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        Student student = _isu.AddStudent(group, name);
        if (!student.Course.Equals(new CourseNumber(2))) return student;
        ExtraStudent extraStudent = new ExtraStudent(student);
        _ognpPossibleStudents.Add(extraStudent);

        return student;
    }

    public Student GetStudent(int id)
    {
        return _isu.GetStudent(id);
    }

    public Student FindStudent(int id)
    {
        return _isu.FindStudent(id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        return _isu.FindStudents(groupName);
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        return _isu.FindStudents(courseNumber);
    }

    public Group FindGroup(GroupName groupName)
    {
        return _isu.FindGroup(groupName);
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isu.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _isu.ChangeStudentGroup(student, newGroup);
    }
}