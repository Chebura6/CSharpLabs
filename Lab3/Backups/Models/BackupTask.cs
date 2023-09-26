using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Models;
[JsonObject(MemberSerialization.OptIn)]
public class BackupTask
{
    public BackupTask(string backupTaskName, IAlgorithm algorithm, IRepository repository)
    {
        if (string.IsNullOrEmpty(backupTaskName)) throw BackupTaskException.InvalidPath();
        Backup = new Backup(new Config(algorithm, repository));
        BackupTaskName = backupTaskName;
    }

    [JsonConstructor]
    public BackupTask(string backupTaskName, Backup backup)
    {
        BackupTaskName = backupTaskName;
        Backup = backup;
    }

    [JsonProperty]
    public Backup Backup { get; }
    [JsonProperty]
    private string BackupTaskName { get; }
    public void AddObject(string path)
    {
        if (string.IsNullOrEmpty(path)) throw BackupTaskException.InvalidPath();
        var obj = new BackupObject(path);
        Backup.Config.AddFileToConfig(obj);
    }

    public void RemoveFile(string path)
    {
        if (string.IsNullOrEmpty(path)) throw BackupTaskException.InvalidPath();
        var obj = new BackupObject(path);
        Backup.Config.RemoveFileFromConfig(obj);
    }

    public Backup Execute()
    {
        if (Backup.Config.BackupObjects.Count == 0) throw BackupTaskException.CountOfObjectsException();
        Backup.Config.Repository.PathToTaskDir = Path.Combine(Backup.Config.Repository.PathToRepo, BackupTaskName);
        Backup.Config.Repository.CreateDirectory(Backup.Config.Repository.PathToTaskDir);
        var storages = Backup.Config.Algorithm.ArchiveAlgo(Backup.Config.BackupObjects, Backup.Config.Repository, Backup.RestorePointsCounter);
        Backup.AddRestorePoint(storages);
        return Backup;
    }

    public void ChangeArchiveAlgorithm(IAlgorithm algorithm)
    {
        ArgumentNullException.ThrowIfNull(algorithm, "Null argument algo detected");
        Backup.Config.Algorithm = algorithm;
    }

    public void ChangeRepository(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, "Null repo object!");
        Backup.Config.Repository = repository;
    }
}