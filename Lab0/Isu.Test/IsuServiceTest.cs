using Isu.Entities;
using Isu.Models;
using Isu.MyExceptions;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuTest
{
    private IsuService _isuService = new IsuService();

    private Group _myGroup;

    public IsuTest()
    {
        _myGroup = _isuService.AddGroup(new GroupName("M32081"));
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Student student = _isuService.AddStudent(_myGroup, "Artem Parfenov");
        Assert.Contains(student, _myGroup.Students);
        Assert.Equal(_myGroup.GroupName, student.GroupName);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        for (int i = 0; i < Group.MaxStudentsInGroup; ++i)
        {
            _isuService.AddStudent(_myGroup, "Artem Parfenov");
        }

        Assert.Throws<StudentsLimitException>(() => _isuService.AddStudent(_myGroup, "Artem Parfenov"));
    }

    [Theory]
    [InlineData("G34856")]
    [InlineData("F47833")]
    public void CreateGroupWithInvalidName_ThrowException(string group)
    {
        Assert.Throws<InvalidGroupNameException>(() => _isuService.AddGroup(new GroupName(group)));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group myGroup3 = _isuService.AddGroup(new GroupName("M32101"));
        Group myGroup1 = _isuService.AddGroup(new GroupName("M32091"));
        Group myGroup2 = _isuService.AddGroup(new GroupName("M32071"));
        IReadOnlyCollection<Group> obj = _isuService.FindGroups(new CourseNumber(2));
        Student student = _isuService.AddStudent(_myGroup, "Artem Parfenov");
        _isuService.ChangeStudentGroup(student, myGroup2);
        Assert.Contains(student, myGroup2.Students);
    }
}