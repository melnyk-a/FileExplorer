using FileExplorer.Models.Objects.FileSystemEntries;
using System.Collections.Generic;

namespace FileExplorer.Models.Managers.CreatedEntries
{
    internal interface ICreatedEntriesManager
    {
        ICollection<FileSystemEntry> Items { get; set; }
    }
}