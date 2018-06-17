namespace FileExplorer.Models.Objects.FileSystemEntries.Repositories
{
    internal sealed class PC : Repository
    {
        public PC(string path, Repository parent) :
            base(path, parent)
        {
            Name = PCName;
        }

        private string PCName => "This PC";

        public override Repository Repository => this;
    }
}