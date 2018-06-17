using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Objects.FileSystemEntries.FileSystemFactories
{
    internal sealed class FileSystemFactory : IFileSystemFactory<FileSystemEntry>
    {
        public FileSystemEntry CreateFile(string path, Repository parent)
        {
            return new File(path, parent);
        }

        public FileSystemEntry CreateFolder(string path, Repository parent)
        {
            return new Folder(path, parent);
        }

        public FileSystemEntry CreateLogicalDrive(string path, Repository parent)
        {
            return new LogicalDrive(path, parent);
        }

        public FileSystemEntry CreatePC(string path, Repository parent)
        {
            return new PC(path, parent);
        }
    }
}