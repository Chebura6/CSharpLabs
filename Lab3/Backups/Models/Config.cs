using Backups.Exceptions;
using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Models;
[JsonObject(MemberSerialization.OptIn)]
public class Config
{
    [JsonConstructor]
    public Config(List<BackupObject> backupObjects)
    {
        BackupObjects = backupObjects;

        // Repository = repository;
        // Algorithm = algorithm;
    }

    internal Config(IAlgorithm algorithm, IRepository repository)
    {
        BackupObjects = new List<BackupObject>();
        Repository = repository;
        Algorithm = algorithm;
    }

    [JsonProperty]
    public List<BackupObject> BackupObjects { get; }

    // [JsonProperty]
    public IAlgorithm Algorithm { get; set; }

    // [JsonProperty]
    public IRepository Repository { get; set; }

    internal void AddFileToConfig(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        BackupObjects.Add(backupObject);
    }

    internal void RemoveFileFromConfig(BackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        var rmFile = BackupObjects.FirstOrDefault(obj => obj.Equals(backupObject));
        if (rmFile is null) throw ConfigException.InvalidObject();
        BackupObjects.Remove(rmFile);
    }
}