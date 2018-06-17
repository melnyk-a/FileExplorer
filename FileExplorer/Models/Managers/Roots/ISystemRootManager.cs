using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Managers.Roots
{
    internal interface ISystemRootManager
    {
        Repository Root { get; set; }
    }
}