using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Objects.FileSystemEntries.FileSystemFactories
{
    internal interface IFileSystemFactory<T>
    {
        T CreateFile(string path, Repository parent);
        T CreateFolder(string path, Repository parent);
        T CreateLogicalDrive(string path, Repository parent);
        T CreatePC(string path, Repository parent);
    }
}