using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;
[JsonObject(MemberSerialization.OptIn)]
public class RestorePoint
{
    [JsonConstructor]
    public RestorePoint(List<Storage> storages, DateTime? timestamp = null)
    {
        ArgumentNullException.ThrowIfNull(storages, "Null List of Backup Objects detected");
        Timestamp = timestamp ?? DateTime.Now;
        Storages = storages;
    }

    [JsonProperty]
    public DateTime Timestamp { get; }
    [JsonProperty]
    public List<Storage> Storages { get; }
}