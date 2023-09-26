using System.IO.Compression;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Interfaces;

public interface IArchiver
{
    ZipArchive CreateArchive(string pathToDir, string zipName, IRepository repository);
    void AddFolderInExistArchive(string path, IRepository repository, ZipArchive zipArchive);
    void AddFileInExistArchive(string path, IRepository repository, ZipArchive zipArchive);
    void DeleteArchive(ZipArchive zipArchive);
}