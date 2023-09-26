using System.Text;
using Backups.Extra.Services;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class BTStateSaver
{
    private const string _pathToJson = "/Users/artemparfenov/BackupState.json";

    public BTStateSaver(IRepository repository)
    {
        Repository = repository;
    }

    internal IRepository Repository { get; }

    public BackupTask LoadState()
    {
        if (!Repository.FileSystem.FileExists(_pathToJson))
        {
            return null;
        }

        BackupTask backupTask = JsonConvert.DeserializeObject<BackupTask>(File.ReadAllText($"{_pathToJson}"));
        return backupTask;
    }

    public void SaveState(BackupTask backupTask)
    {
        string json = JsonConvert.SerializeObject(backupTask, Formatting.Indented);
        var stream = Repository.FileSystem.OpenFile(_pathToJson, FileMode.OpenOrCreate, FileAccess.Write);
        byte[] bytes = Encoding.ASCII.GetBytes(json);
        stream.Write(bytes);
        stream.Close();
    }
}