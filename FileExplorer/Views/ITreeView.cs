using System;

namespace FileExplorer.Views
{
    internal interface ITreeView : IView
    {
        bool Loaded(string path, string key);
        string Selected { get; set; }
          
        event EventHandler<PathEventArgs> Expand;
        event EventHandler<PathEventArgs> Collapse;
        event EventHandler LoadTree;

        void AddFolder(string parentKey, string key, string text);
        void AddInaccessibleFolder(string parentKey, string key, string text);
        void AddLogicalDrive(string key, string text);
        void Clear(string key);
        void ExpandFolder(string key);
    }
}