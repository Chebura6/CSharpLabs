using Backups.Entities;
using Backups.Interfaces;
using Newtonsoft.Json;

namespace Backups.Models;

[JsonObject(MemberSerialization.OptIn)]
public class SplitAlgorithm : IAlgorithm
{
    // [JsonConstructor]
    public SplitAlgorithm()
    {
        Archiver = new Archiver();
    }

    [JsonProperty]
    public string Type => nameof(SplitAlgorithm);
    private IArchiver Archiver { get; }

    public List<Storage> ArchiveAlgo(List<BackupObject> backupObjects, IRepository repository, long restorePointsCount)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        List<Storage> storages = new List<Storage>();
        string pathToRestPoint = Path.Combine(repository.PathToTaskDir, "RestorePoint(" + restorePointsCount + ")");
        repository.CreateDirectory(pathToRestPoint);
        foreach (BackupObject backupObject in backupObjects)
        {
            string zipName = repository.GetZipName(backupObject, restorePointsCount);
            var zipArch = Archiver.CreateArchive(pathToRestPoint, zipName, repository);

            storages.Add(new Storage(Path.Combine(pathToRestPoint, zipName) + ".zip", backupObject.Path));
            if (repository.IsDirectory(backupObject.Path))
            {
                Archiver.AddFolderInExistArchive(backupObject.Path, repository, zipArch);
            }
            else
            {
                Archiver.AddFileInExistArchive(backupObject.Path, repository, zipArch);
            }

            Archiver.DeleteArchive(zipArch);
        }

        return storages;
    }
}