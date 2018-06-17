using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Objects.FileSystemEntries
{
    internal abstract class FileSystemEntry
    {
        public FileSystemEntry(string path, Repository parent)
        {
            Path = path;
            Parent = parent;
        }

        public virtual string Name { get; protected set; }

        public Repository Parent { get; private set; }

        public string Path { get; private set; }

        public abstract Repository Repository { get; }
    }
}