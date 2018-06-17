using FileExplorer.Models.Objects.FileSystemEntries;
using System.Collections.Generic;

namespace FileExplorer.Models.Managers.CreatedEntries
{
    internal interface ICreatedEntriesProvider
    {
        ICollection<FileSystemEntry> Items { get; }
    }
}