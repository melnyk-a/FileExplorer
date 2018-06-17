using FileExplorer.Models.Objects.FileSystemEntries.Repositories;
using System.IO;

namespace FileExplorer.Models.Objects.FileSystemEntries
{
    internal sealed class File : FileSystemEntry
    {
        public File(string path, Repository parent) :
            base(path, parent)
        {
            Name = System.IO.Path.GetFileName(Path);
        }

        public override Repository Repository => null;

        public long Size => new FileInfo(Path).Length;
    }
}