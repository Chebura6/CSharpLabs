using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IIsuExtra
{
    OgnpGroup AddOgnpGroup(OgnpGroupName groupName, Timetable timetable);
    ExtraStudent AddStudentAtOgnpGroup(OgnpGroup ognpGroup, Student student);
    void RemoveCourseRegistration(ExtraStudent student);
    IReadOnlyCollection<ExtraStudent> GetOgnpGroupStudents(OgnpGroupName groupName);
    IReadOnlyCollection<ExtraStudent> GetStudentsWithoutOgnpGroup();
    void SetGroupTimetable(GroupName groupName, Timetable timetable);
    void SetOgnpGroupTimetable(OgnpGroup ognpGroup, Timetable timetable);
    Timetable GetGroupTimetable(Group group);
    Timetable GetOgnpGroupTimetable(OgnpGroup ognpGroup);
    IReadOnlyCollection<OgnpGroup> GetOgnpGroups();
    IReadOnlyCollection<OgnpGroup> GetSameCourseGroups(char courseLetter);
}