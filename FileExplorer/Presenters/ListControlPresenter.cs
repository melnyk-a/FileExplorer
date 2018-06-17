using FileExplorer.Models;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Models.Objects.FileSystemEntries.Repositories;
using FileExplorer.Views;

namespace FileExplorer.Presenters
{
    internal sealed class ListControlPresenter
    {
        private readonly FileSystem fileSystem;
        private readonly IListView listView;

        public ListControlPresenter(IListView listView, FileSystem fileSystem)
        {
            this.listView = listView;
            this.fileSystem = fileSystem;

            SubscribeToViewEvents();
        }

        private void ListView_SelectedChange(object sender, PathEventArgs e)
        {
            Repository selected = fileSystem.FindDirectory(e.Path);
            if (selected != null)
            {
                listView.ClearList();
                bool previousExpandCondition = selected.IsExpand;
                selected.IsExpand = true;
                fileSystem.Expand(selected);

                foreach (var item in selected.Entries)
                {
                    if (item is Repository repository)
                    {
                        if (!repository.HasAccessRight)
                        {
                            listView.AddInaccessibleFolder(repository.Name);
                        }
                        else
                        {
                            listView.AddFolder(repository.Name);
                        }
                    }
                    else
                    {
                        listView.AddFile(item.Name, item.Path, Converter.BytesToString(((File)item).Size));
                    }
                }

                selected.IsExpand = previousExpandCondition;
            }
        }

        public void Run()
        {
            listView.Show();
        }

        private void SubscribeToViewEvents()
        {
            listView.SelectedChange += ListView_SelectedChange;
        }
    }
}