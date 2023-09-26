using System.IO.Compression;
using System.Windows.Markup;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class Archiver : IArchiver, IDisposable
{
    internal Archiver()
    {
    }

    public ZipArchive CreateArchive(string pathToDir, string zipName, IRepository repository)
    {
        zipName += ".zip";
        string archivePath = Path.Combine(pathToDir.ToString(), zipName);
        var stream = repository.FileSystem.OpenFile(archivePath, FileMode.OpenOrCreate, FileAccess.Write);
        return new ZipArchive(stream, ZipArchiveMode.Create);
    }

    public void AddFileInExistArchive(string path, IRepository repository, ZipArchive zipArchive)
    {
        using var entryStream = zipArchive.CreateEntry(Path.GetFileName(path)).Open();
        using var fileStream = repository.FileSystem.OpenFile(path, FileMode.Open, FileAccess.Read);
        fileStream.CopyTo(entryStream);
    }

    public void AddFolderInExistArchive(string path, IRepository repository, ZipArchive zipArchive)
    {
        var name = Path.GetFileName(path) + ".zip";
        var entryStream = zipArchive.CreateEntry(name).Open();
        var newZipArchive = new ZipArchive(entryStream, ZipArchiveMode.Create);
        foreach (string file in Directory.GetFiles(path))
        {
            if (file.StartsWith('.')) continue;
            AddFileInExistArchive(file, repository, newZipArchive);
        }

        foreach (var directory in Directory.GetDirectories(path))
        {
            AddFolderInExistArchive(directory, repository, newZipArchive);
        }

        newZipArchive.Dispose();
    }

    public void DeleteArchive(ZipArchive zipArchive)
    {
        zipArchive.Dispose();
    }

    public void Dispose()
    {
    }
}