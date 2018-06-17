using System;

namespace FileExplorer.Views
{
    internal interface IListView : IView
    {
        event EventHandler<PathEventArgs> SelectedChange;

        void AddFile(string name, string path, string size);
        void AddFolder(string name);
        void AddInaccessibleFolder(string name);
        void ClearList();
    }
}