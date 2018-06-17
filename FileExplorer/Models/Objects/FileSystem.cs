using FileExplorer.Models.Behaviors;
using FileExplorer.Models.Managers.Roots;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;
using System.Collections.Generic;

namespace FileExplorer.Models
{
    internal sealed class FileSystem
    {
        private readonly BehaviorProvider behaviorProvider;
        private readonly ISystemRootManager systemRootManager;

        public FileSystem(
            BehaviorProvider behaviorProvider,
            ISystemRootManager systemRootManager
            )
        {
            this.behaviorProvider = behaviorProvider;
            this.systemRootManager = systemRootManager;
        }

        public Repository Root => systemRootManager.Root;

        public FileSystemEntry Selected { get; set; }

        public void Expand(Repository repository)
        {
            behaviorProvider.UpdateRepositoryBehavior.Update(repository);
            behaviorProvider.ExpandRepositoryBehavior.Expand(repository);
        }

        public Repository FindDirectory(string path)
        {
            return FindDirectory(path, systemRootManager.Root);
        }

        private Repository FindDirectory(string path, Repository startFrom)
        {
            Repository findedRepository = null;

            foreach (FileSystemEntry entry in startFrom.Entries)
            {
                if (entry is Repository repository)
                {
                    if (repository.Path == path)
                    {
                        findedRepository = repository;
                    }
                    else if (repository.IsExpand)
                    {
                        findedRepository = FindDirectory(path, repository);
                    }
                }
                if (findedRepository != null)
                {
                    break;
                }
            }
            return findedRepository;
        }

        public void InitializeRoot()
        {
            behaviorProvider.InitializeRootBehavior.Initialize();
            behaviorProvider.UpdateRepositoryBehavior.Update(Root);
            behaviorProvider.ExpandRepositoryBehavior.Expand(Root);
        }

        public void Restore(Memento memento)
        {
            foreach (string parent in memento.Parents)
            {
                FindDirectory(parent).IsExpand = true;
            }
            Selected = FindDirectory(memento.Path);
        }

        public Memento Save()
        {
            Memento memento = null;
            if (Selected != null)
            {
                List<string> parents = new List<string>();
                Repository parent = Selected.Parent;
                while (parent != null && parent != Root)
                {
                    parents.Add(parent.Path);
                    parent = parent.Parent;
                }
                memento = new Memento(Selected.Path, parents);
            }
            return memento;
        }
    }
}