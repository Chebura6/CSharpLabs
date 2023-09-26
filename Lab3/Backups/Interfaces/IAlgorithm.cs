using Backups.Models;

namespace Backups.Interfaces;

public interface IAlgorithm
{
    string Type { get; }
    List<Storage> ArchiveAlgo(List<BackupObject> backupObjects, IRepository repository, long restorePointsCount);
}