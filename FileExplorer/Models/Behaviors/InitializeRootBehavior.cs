using FileExplorer.Models.Managers.Roots;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.FileSystemFactories;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Behaviors
{
    internal sealed class InitializeRootBehavior
    {
        private readonly IFileSystemFactory<FileSystemEntry> factory;
        private readonly ISystemRootManager systemRoot;

        public InitializeRootBehavior(
            IFileSystemFactory<FileSystemEntry> factory,
            ISystemRootManager systemRoot
        )
        {
            this.factory = factory;
            this.systemRoot = systemRoot;
        }

        public void Initialize()
        {
            systemRoot.Root = factory.CreatePC(null, null) as Repository;
            systemRoot.Root.IsExpand = true; 
        }
    }
}