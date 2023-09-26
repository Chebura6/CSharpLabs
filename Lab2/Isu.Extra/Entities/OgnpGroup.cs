using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class OgnpGroup
{
    private const int MaxStudentsInGroup = 35; // max on students in group
    private readonly List<ExtraStudent> _extraStudents;
    public OgnpGroup(OgnpGroupName ognpGroupName, Timetable timetable)
    {
        ArgumentNullException.ThrowIfNull(ognpGroupName, "Null ognp group name detected");
        ArgumentNullException.ThrowIfNull(timetable, "Null timetable detected");
        _extraStudents = new List<ExtraStudent>();
        Timetable = timetable;
    }

    public OgnpGroup(OgnpGroupName ognpGroupName, Timetable timetable, List<ExtraStudent> extraStudents)
    {
        _extraStudents = extraStudents;
        ArgumentNullException.ThrowIfNull(ognpGroupName, "Null ognp group name detected");
        ArgumentNullException.ThrowIfNull(timetable, "Null timetable detected");
        _extraStudents = new List<ExtraStudent>();
    }

    public IReadOnlyCollection<ExtraStudent> ExtraStudents => _extraStudents;
    internal OgnpGroupName OgnpGroupName { get; set; }
    internal Timetable Timetable { get; set; }
    internal ExtraStudent AddStudentInGroup(ExtraStudent extraStudent)
    {
        if (_extraStudents.Count == MaxStudentsInGroup) throw OgnpGroupException.GroupOverload();
        _extraStudents.Add(extraStudent);
        extraStudent.OgnpGroupName = OgnpGroupName;
        return extraStudent;
    }

    internal void RemoveStudentFromGroup(ExtraStudent student)
    {
        ArgumentNullException.ThrowIfNull(student, "Null extra student detected");
        _extraStudents.Remove(student);
        student.OgnpGroupName = null;
    }
}