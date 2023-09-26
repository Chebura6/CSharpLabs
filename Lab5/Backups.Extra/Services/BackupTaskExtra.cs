using Backups.Entities;
using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Services;

[JsonObject(MemberSerialization.OptIn)]
public class BackupTaskExtra
{
    public BackupTaskExtra(string backupTaskName, IAlgorithm algorithm, IRepository repository, ILimitAlgo limitAlgo, ILogger logger)
    {
        if (string.IsNullOrEmpty(backupTaskName)) throw new Exception();
        ArgumentNullException.ThrowIfNull(limitAlgo);
        ArgumentNullException.ThrowIfNull(algorithm);
        ArgumentNullException.ThrowIfNull(repository);

        LimitAlgo = limitAlgo;
        MergeAlgo = new Merge(repository);

        BackupTask = new BackupTask(backupTaskName, algorithm, repository);

        StateSaver = new BTStateSaver(repository);
        BackupTask = StateSaver.LoadState();
        if (BackupTask is not null)
        {
            BackupTask.Backup.Config.Algorithm = algorithm;
            BackupTask.Backup.Config.Repository = repository;
        }
        else
        {
            BackupTask = new BackupTask(backupTaskName, algorithm, repository);
        }

        Logger = logger;
    }

    [JsonConstructor]
    internal BackupTaskExtra(BackupTask backupTask)
    {
        BackupTask = backupTask;
    }

    internal ILogger Logger { get; }
    internal Merge MergeAlgo { get; }
    [JsonProperty]
    private BackupTask BackupTask { get; }
    private ILimitAlgo LimitAlgo { get; set; }
    private BTStateSaver StateSaver { get; }

    public void AddObject(string path)
    {
        BackupTask.AddObject(path);
        Logger.MakeLog($"Object {path} successfully added!");
    }

    public void RemoveFile(string path)
    {
        BackupTask.RemoveFile(path);
        Logger.MakeLog($"Object {path} successfully removed!");
    }

    public Backup Execute()
    {
        Backup backup = BackupTask.Execute();
        Logger.MakeLog($"Successfully created {backup.RestorePointsCounter} restore point!");
        LimitAlgo.Backup = backup;

        StateSaver.SaveState(BackupTask);
        List<RestorePoint> pointsToMerge = LimitAlgo.LimitCheck();
        if (pointsToMerge.Count == BackupTask.Backup.GetPoints.Count) throw new Exception();
        if (pointsToMerge.Count != 0)
        {
            MergeAlgo.MergePoints(pointsToMerge);
            Logger.MakeLog($"{pointsToMerge.Count} successfully merged!");
        }

        return backup;
    }

    public void ChangeArchiveAlgorithm(IAlgorithm algorithm)
    {
        BackupTask.ChangeArchiveAlgorithm(algorithm);
        Logger.MakeLog($"Algorithm successfully changed on: {algorithm.Type}");
    }

    public void ChangeRepository(IRepository repository)
    {
        BackupTask.ChangeRepository(repository);
        Logger.MakeLog($"Repository successfully changed on: {repository.Type}");
    }
}