using FileExplorer.Models.Managers.CreatedEntries;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;

namespace FileExplorer.Models.Behaviors
{
    internal sealed class ExpandRepositoryBehavior
    {
        private readonly ICreatedEntriesProvider createdEntries;

        public ExpandRepositoryBehavior(ICreatedEntriesProvider createdEntries)
        {
            this.createdEntries = createdEntries;
        }

        public void Expand(Repository repository)
        {
            if (repository != null)
            {
                foreach (FileSystemEntry entry in createdEntries.Items)
                {
                    repository.Entries.Add(entry);
                }
                if (repository.Entries.Count == 0)
                {
                    repository.IsExpand = false;
                }
                createdEntries.Items.Clear();
            }
        }
    }
}