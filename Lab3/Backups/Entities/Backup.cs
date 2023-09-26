using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;
[JsonObject(MemberSerialization.OptIn)]
public class Backup
{
    [JsonProperty]
    private readonly List<RestorePoint> _restorePoints;

    [JsonConstructor]
    public Backup(Config config, List<RestorePoint> restorePoints)
    {
        Config = config;
        _restorePoints = restorePoints;
    }

    internal Backup(Config config)
    {
        Config = config;
        _restorePoints = new List<RestorePoint>();
        RestorePointsCounter = 0;
    }

    [JsonProperty]
    public Config Config { get; }
    public IReadOnlyCollection<RestorePoint> GetPoints => _restorePoints;
    [JsonProperty]
    public long RestorePointsCounter { get; private set; }
    internal void AddRestorePoint(List<Storage> storages)
    {
        ArgumentNullException.ThrowIfNull(storages);
        var restorePoint = new RestorePoint(storages);
        _restorePoints.Add(restorePoint);
        RestorePointsCounter += 1;
    }
}