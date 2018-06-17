namespace FileExplorer.Models.Objects.FileSystemEntries.Repositories
{
    internal sealed class LogicalDrive : Repository
    {
        public LogicalDrive(string path, Repository parent) :
            base(path, parent)
        {
            Name = path;
        }

        public override Repository Repository => this;
    }
}