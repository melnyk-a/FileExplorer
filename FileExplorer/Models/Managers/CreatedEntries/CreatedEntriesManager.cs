using FileExplorer.Models.Objects.FileSystemEntries;
using System.Collections.Generic;

namespace FileExplorer.Models.Managers.CreatedEntries
{
    internal sealed class CreatedEntriesManager :
        ICreatedEntriesManager,
        ICreatedEntriesProvider
    {
        public ICollection<FileSystemEntry> Items { get; set; }
            = new List<FileSystemEntry>();
    }
}