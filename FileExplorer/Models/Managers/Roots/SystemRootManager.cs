using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Managers.Roots
{
    internal sealed class SystemRootManager :
        ISystemRootProvider,
        ISystemRootManager
    {
        public Repository Root { get; set; }
    }
}