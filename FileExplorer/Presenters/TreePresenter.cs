using FileExplorer.Models;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;
using FileExplorer.Views;
using System;

namespace FileExplorer.Presenters
{
    internal sealed class TreeControlPresenter
    {
        private readonly string fakeFolder = "Fake";
        private readonly FileSystem fileSystem;
        private readonly ITreeView treeView;

        public TreeControlPresenter(ITreeView treeView, FileSystem fileSystem)
        {
            this.treeView = treeView;
            this.fileSystem = fileSystem;

            SubscribeToViewEvents();
        }

        private void AddFakeFolder(FileSystemEntry entry)
        {
            treeView.AddFolder(entry.Path, fakeFolder, fakeFolder);
        }

        private void ExpandTreeView(Repository expanded)
        {
            foreach (FileSystemEntry entry in expanded.Entries)
            {
                if (entry is Repository repository)
                {
                    if (!repository.HasAccessRight)
                    {
                        treeView.AddInaccessibleFolder(expanded.Path, entry.Path, entry.Name);
                    }
                    else
                    {
                        treeView.AddFolder(expanded.Path, entry.Path, entry.Name);
                        if (repository.HasChildDirectories)
                        {
                            AddFakeFolder(repository);
                        }
                    }
                    if (repository.IsExpand)
                    {
                        treeView.ExpandFolder(repository.Path);
                    }
                }
            }
        }

        public void Run()
        {
            treeView.Show();
        }

        private void SubscribeToViewEvents()
        {
            treeView.LoadTree += TreeView_LoadTree;
            treeView.Expand += TreeView_Expand;
            treeView.Collapse += TreeView_Collapse;
        }

        private void TreeView_Collapse(object sender, PathEventArgs e)
        {
            Repository repository = fileSystem.FindDirectory(e.Path);
            repository.IsExpand = false;
        }

        private void TreeView_Expand(object sender, PathEventArgs e)
        {
            Repository repository = fileSystem.FindDirectory(e.Path);
            repository.IsExpand = true;
            fileSystem.Expand(repository);
            if (!treeView.Loaded(e.Path, fakeFolder))
            {
                treeView.Clear(e.Path);
            }
            ExpandTreeView(repository);

            if (fileSystem.Selected != null)
            {
                treeView.Selected = fileSystem.Selected.Path;
            }
        }

        private void TreeView_LoadTree(object sender, EventArgs e)
        {
            fileSystem.InitializeRoot();
            foreach (FileSystemEntry entry in fileSystem.Root.Entries)
            {
                treeView.AddLogicalDrive(entry.Path, entry.Name);
                AddFakeFolder(entry);
            }
        }
    }
}