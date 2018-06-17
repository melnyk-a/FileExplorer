using FileExplorer.Models.Managers.CreatedEntries;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.FileSystemFactories;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer.Models.Behaviors
{
    using CreateEntry = Func<string, Repository, FileSystemEntry>;

    internal sealed class UpdateRepositoryBehavior
    {
        private readonly ICreatedEntriesManager createdManager;
        private readonly IFileSystemFactory<FileSystemEntry> factory;

        public UpdateRepositoryBehavior(
            IFileSystemFactory<FileSystemEntry> factory,
            ICreatedEntriesManager createdManager
        )
        {    
            this.createdManager = createdManager;
            this.factory = factory;
        }

        private void AddEntries(IEnumerable<FileSystemEntry> children)
        {
            foreach (var child in children)
            {
                createdManager.Items.Add(child);
            }
        }

        private void AddSubItem(IEnumerable<FileSystemEntry> children)
        {
            if (children != null)
            {
                AddEntries(children);
            }
        }

        private IEnumerable<FileSystemEntry> GetChildren(
            Repository parent,
            CreateEntry creator,
            Func<string, string[]> getInner = null
        )
        {
            ICollection<FileSystemEntry> children = null;

            string[] innerEntries = getInner == null ? 
                TryGetInnerEntries() : 
                TryGetInnerEntries(parent.Path, getInner);

            if (innerEntries != null)
            {
                children = new List<FileSystemEntry>();
                for (int i = 0; i < innerEntries.Length; ++i)
                {
                    children.Add(creator(innerEntries[i], parent));
                }
            }

            return children;
        }

        private string[] TryGetInnerEntries(
            string path = null,
            Func<string, string[]> getInner = null
        )
        {
            string[] result = null;
            try
            {
                result = path == null ? Directory.GetLogicalDrives() : getInner(path);
            }
            catch (UnauthorizedAccessException)
            {
            }
            return result;
        }

        public void Update(Repository repository)
        { 
            if (repository != null)
            {
                if (repository.IsExpand && repository.Entries.Count == 0)
                {
                    Repository parent = repository;
                    if (parent is PC)
                    {
                        AddSubItem(GetChildren(parent, factory.CreateLogicalDrive));
                    }
                    else
                    {
                        AddSubItem(GetChildren(parent, factory.CreateFolder, Directory.GetDirectories));
                        AddSubItem(GetChildren(parent, factory.CreateFile, Directory.GetFiles));
                    }
                }
            }
        }
    }
}