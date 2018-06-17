using System;

namespace FileExplorer.Views
{
    internal interface IHistoryView
    {
        string Selected { get; set; }

        event EventHandler Back;
        event EventHandler Forward;
        event EventHandler<PathEventArgs> SelectedChange;
        event EventHandler Up;

        void ExpandFolder(string key);
        void SetUpBackMove(bool isEnable);
        void SetUpForwardMove(bool isEnable);
        void SetUpUpMove(bool isEnable);
    }
}