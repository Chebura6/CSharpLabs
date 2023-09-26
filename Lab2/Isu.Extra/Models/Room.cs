namespace Isu.Extra.Models;

public class Room : IEquatable<Room>
{
    public Room(string room)
    {
        if (string.IsNullOrWhiteSpace(room)) throw new ArgumentNullException("Null room number detected");
        RoomNumber = room;
    }

    internal string RoomNumber { get; }

    public bool Equals(Room other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RoomNumber == other.RoomNumber;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Room)obj);
    }

    public override int GetHashCode()
    {
        return RoomNumber != null ? RoomNumber.GetHashCode() : 0;
    }
}