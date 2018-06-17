using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Managers.Roots
{
    internal interface ISystemRootProvider
    {
        Repository Root { get; }
    }
}