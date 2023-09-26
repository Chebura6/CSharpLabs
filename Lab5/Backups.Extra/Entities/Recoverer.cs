using System.IO.Compression;
using Backups.Interfaces;
using Zio;
using Zio.FileSystems;

namespace Backups.Extra.Entities;

internal class Recoverer
{
    public Recoverer(IRepository repository)
    {
        Repository = repository;
    }

    internal IRepository Repository { get; set; }
    public void RecoverObject(string path, string recoverPath)
    {
        var directoryEntry = Repository.FileSystem.GetDirectoryEntry(recoverPath);
        var files = directoryEntry.EnumerateFiles();
        foreach (var file in files)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (Path.GetFileName(file.Name) == entry.Name)
                    {
                        Repository.FileSystem.DeleteFile(file.Path);
                    }
                }
            }
        }

        var directories = directoryEntry.EnumerateDirectories();
        foreach (var directory in directories)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (Path.GetFileName(directory.Name) == Path.GetFileNameWithoutExtension(entry.Name))
                    {
                        Repository.FileSystem.DeleteDirectory(directory.Path, true);
                    }
                }
            }
        }

        ZipFile.ExtractToDirectory(path, recoverPath);

        var newFiles = directoryEntry.EnumerateFiles();
        foreach (var file in newFiles)
        {
            if (Path.GetExtension(file.Name) == ".zip")
            {
                ZipFile.ExtractToDirectory((string)file.Path, Path.Combine(recoverPath, Path.GetFileNameWithoutExtension((string)file.Path)));
                ExtractDirectory(Path.Combine(recoverPath, Path.GetFileNameWithoutExtension(file.Name)));
                Repository.FileSystem.DeleteFile(file.Path);
            }
        }
    }

    private void ExtractDirectory(string dirPath)
    {
        var directoryEntry = Repository.FileSystem.GetDirectoryEntry(dirPath);
        var files = directoryEntry.EnumerateFiles();
        foreach (var file in files)
        {
            if (Path.GetExtension(file.Name) == ".zip")
            {
                ZipFile.ExtractToDirectory((string)file.Path, Path.Combine(dirPath, Path.GetFileNameWithoutExtension((string)file.Path)));
                ExtractDirectory(Path.Combine(Path.GetDirectoryName((string)file.Path), Path.GetFileNameWithoutExtension(file.Name)));
                Repository.FileSystem.DeleteFile(file.Path);
            }
        }
    }
}