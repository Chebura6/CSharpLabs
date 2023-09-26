using Backups.Exceptions;
using Newtonsoft.Json;
using Zio;

namespace Backups.Models;
[JsonObject(MemberSerialization.OptIn)]
public class BackupObject : IEquatable<BackupObject>
{
    [JsonConstructor]
    public BackupObject(string path)
    {
        if (string.IsNullOrEmpty(path)) throw BackupObjectException.InvalidPath();
        Path = path;
    }

    [JsonProperty]
    internal string Path { get; }

    public bool Equals(BackupObject other)
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
        return Equals((BackupObject)obj);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
}