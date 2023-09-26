using Newtonsoft.Json;

namespace Backups.Models;
[JsonObject(MemberSerialization.OptIn)]
public class Storage : IEquatable<Storage>
{
    [JsonConstructor]
    internal Storage(string path, string pathToBackupObject)
    {
        Path = path;
        PathToBackupObject = new List<string>();
        PathToBackupObject.Add(pathToBackupObject);
    }

    internal Storage(string path)
    {
        Path = path;
        PathToBackupObject = new List<string>();
    }

    [JsonProperty]
    public string Path { get; }
    public List<string> PathToBackupObject { get; set; }
    public bool Equals(Storage other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Storage)obj);
    }

    public override int GetHashCode()
    {
        return Path != null ? Path.GetHashCode() : 0;
    }
}