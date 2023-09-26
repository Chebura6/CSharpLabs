using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.MyExceptions;
using Isu.Services;
using Xunit;
using DayOfWeek = Isu.Extra.Models.DayOfWeek;
using Group = Isu.Entities.Group;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private IsuExtra isuExtra = new IsuExtra();
    private OgnpGroupName ognpGroupName;
    private Lesson oop;
    private Lesson foreignLanguage;
    private Lesson cyber;
    private List<Lesson> lessons;
    private List<Lesson> lessons2;
    private List<DayOfWeek> daysOfWeek;
    private List<DayOfWeek> daysOfWeek2;
    private Timetable timetable1;
    private Timetable timetable2;
    private Group myGroup;
    private Student student;
    private OgnpGroup ognpGroup;
    public IsuExtraTest()
    {
        oop = new Lesson("OOP", new StartLessonTime("8:20"), new Teacher("Makarevich Roman"), new Room("1225"));
        foreignLanguage = new Lesson("English", new StartLessonTime("11:40"), new Teacher("Filippovich Ekaterina"), new Room("3219"));
        cyber = new Lesson("Cyber security", new StartLessonTime("13:30"), new Teacher("Noname"), new Room("4303"));
        lessons = new List<Lesson> { oop, foreignLanguage };
        lessons2 = new List<Lesson> { cyber };
        var monday1 = new DayOfWeek(lessons);
        var tuesday1 = new DayOfWeek(lessons);
        var wednesday1 = new DayOfWeek(lessons);
        var thursday1 = new DayOfWeek(lessons);
        var friday1 = new DayOfWeek(lessons);
        var saturday1 = new DayOfWeek(lessons);
        var sunday1 = new DayOfWeek(lessons);
        var monday2 = new DayOfWeek(lessons);
        var tuesday2 = new DayOfWeek(lessons);
        var wednesday2 = new DayOfWeek(lessons);
        var thursday2 = new DayOfWeek(lessons);
        var friday2 = new DayOfWeek(lessons);
        var saturday2 = new DayOfWeek(lessons);
        var sunday2 = new DayOfWeek(lessons);
        var monday3 = new DayOfWeek(lessons2);
        var tuesday3 = new DayOfWeek(lessons2);
        var wednesday3 = new DayOfWeek(lessons2);
        var thursday3 = new DayOfWeek(lessons2);
        var friday3 = new DayOfWeek(lessons2);
        var saturday3 = new DayOfWeek(lessons2);
        var sunday3 = new DayOfWeek(lessons2);
        var monday4 = new DayOfWeek(lessons2);
        var tuesday4 = new DayOfWeek(lessons2);
        var wednesday4 = new DayOfWeek(lessons2);
        var thursday4 = new DayOfWeek(lessons2);
        var friday4 = new DayOfWeek(lessons2);
        var saturday4 = new DayOfWeek(lessons2);
        var sunday4 = new DayOfWeek(lessons2);
        daysOfWeek = new List<DayOfWeek> { monday1, tuesday1, wednesday1, thursday1, friday1, saturday1, sunday1, monday2, tuesday2, wednesday2, thursday2, friday2, saturday2, sunday2 };
        daysOfWeek2 = new List<DayOfWeek> { monday3, tuesday3, wednesday3, thursday3, friday3, saturday3, sunday3, monday4, tuesday4, wednesday4, thursday4, friday4, saturday4, sunday4 };
        timetable1 = new Timetable(daysOfWeek);
        timetable2 = new Timetable(daysOfWeek2);
        ognpGroupName = new OgnpGroupName("K32074");
        myGroup = isuExtra.AddGroup(new GroupName("M32081"));
        student = isuExtra.AddStudent(myGroup, "Artem Parfenov");
        ognpGroup = isuExtra.AddOgnpGroup(ognpGroupName, timetable2);
        isuExtra.SetGroupTimetable(myGroup.GroupName, timetable1);
    }

    [Fact]
    public void AddNewOgnpCourse()
    {
        OgnpGroup ognpGroup = isuExtra.AddOgnpGroup(new OgnpGroupName("H22042"), timetable2);
        Assert.Contains(ognpGroup, isuExtra.GetOgnpGroups());
    }

    [Fact]
    public void AddStudentAtOgnpGroup()
    {
        ExtraStudent extraStudent = isuExtra.AddStudentAtOgnpGroup(ognpGroup, student);

        Assert.Contains(extraStudent, ognpGroup.ExtraStudents);
    }

    [Fact]
    public void RemoveStudentsOgnpRegistration()
    {
        ExtraStudent extraStudent = isuExtra.AddStudentAtOgnpGroup(ognpGroup, student);
        isuExtra.RemoveCourseRegistration(extraStudent);
        Assert.DoesNotContain(extraStudent, ognpGroup.ExtraStudents);
    }

    [Fact]
    public void GetStudentsWithoutOgnpRegistration()
    {
        Student student1 = isuExtra.AddStudent(myGroup, "Mikhno Andrew");
        ExtraStudent extraStudent = isuExtra.AddStudentAtOgnpGroup(ognpGroup, student);
        IReadOnlyCollection<ExtraStudent> extraStudents = isuExtra.GetStudentsWithoutOgnpGroup();
        Assert.Contains(new ExtraStudent(student1), extraStudents);
    }

    [Fact]
    public void GetGetSameCourseGroups()
    {
        OgnpGroupName ognpGroupName1 = new OgnpGroupName("K32084");
        OgnpGroup ognpGroup1 = isuExtra.AddOgnpGroup(ognpGroupName1, timetable2);
        IReadOnlyCollection<OgnpGroup> ognpGroups = new List<OgnpGroup> { ognpGroup, ognpGroup1 };
        Assert.Equal(ognpGroups, isuExtra.GetSameCourseGroups('K'));
    }
}