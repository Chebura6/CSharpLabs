using Isu.Entities;

namespace Isu.Extra.Entities;

public class ExtraGroup
{
    public ExtraGroup(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);
        Group = group;
    }

    internal Group Group { get; }
    internal Timetable Timetable { get; set; }
}