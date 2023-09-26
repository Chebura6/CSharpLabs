using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;
public class Merge
{
    public Merge(IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        Repository = repository;
    }

    internal IRepository Repository { get; }
    public List<RestorePoint> MergePoints(List<RestorePoint> restorePoints)
    {
        ArgumentNullException.ThrowIfNull(restorePoints);
        ArgumentNullException.ThrowIfNull(Repository);

        var pointsCopy = new List<RestorePoint>(restorePoints);
        for (int i = 0; i < restorePoints.Count - 1; ++i)
        {
            if (restorePoints[i].Storages.Count == 1)
            {
                Repository.FileSystem.DeleteDirectory(Path.GetDirectoryName(restorePoints[i].Storages[0].Path), true);
                restorePoints.Remove(restorePoints[i]);
                continue;
            }

            foreach (Storage storage in restorePoints[i].Storages)
            {
                Storage result = restorePoints[i + 1].Storages
                    .FirstOrDefault(x => GetBackupObjectName(x.Path).Equals(GetBackupObjectName(storage.Path)));
                if (result is null)
                {
                    string destDirPath = Path.GetDirectoryName(restorePoints[i + 1].Storages[0].Path);

                    Repository.FileSystem.CopyFile(storage.Path, Path.Combine(destDirPath, Path.GetFileName(storage.Path)), true);
                }
            }

            Repository.FileSystem.DeleteDirectory(Path.GetDirectoryName(restorePoints[i].Storages[0].Path), true);
            restorePoints.Remove(restorePoints[i]);
        }

        return restorePoints;
    }

    private string GetBackupObjectName(string storagePath)
    {
        storagePath = Path.GetFileName(storagePath);
        while (storagePath.Last() != '(')
        {
            storagePath = storagePath.Remove(storagePath.Length - 1);
        }

        return storagePath.Remove(storagePath.Length - 1);
    }
}