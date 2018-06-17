using FileExplorer.Models;
using FileExplorer.Models.Objects.FileSystemEntries;
using FileExplorer.Views;
using System;

namespace FileExplorer.Presenters
{
    internal class HistoryControlPresenter
    {
        private readonly CareTaker careTaker;
        private readonly FileSystem fileSystem;
        private readonly IHistoryView historyView;

        public HistoryControlPresenter(
            IHistoryView historyView,
            FileSystem fileSystem,
            CareTaker careTaker
            )
        {
            this.historyView = historyView;
            this.careTaker = careTaker;
            this.fileSystem = fileSystem;

            SubscribeToViewEvents();
        }

        private FileSystemEntry GetParent(string path)
        {
            FileSystemEntry parent = fileSystem.FindDirectory(path).Parent;
            if (parent == fileSystem.Root)
            {
                parent = null;
            }
            return parent;
        }

        private void HistoryView_Back(object sender, EventArgs e)
        {
            fileSystem.Restore(careTaker.Back());
            historyView.Selected = fileSystem.Selected.Path;
            SetUpMove(careTaker.HasPrevious, careTaker.HasNext, GetParent(historyView.Selected) != null);
        }

        private void HistoryView_Forward(object sender, EventArgs e)
        {
            fileSystem.Restore(careTaker.Forward());
            historyView.Selected = fileSystem.Selected.Path;
            SetUpMove(careTaker.HasPrevious, careTaker.HasNext, GetParent(historyView.Selected) != null);
        }

        private void HistoryView_SelectedChange(object sender, PathEventArgs e)
        {
            fileSystem.Selected = fileSystem.FindDirectory(e.Path);
            careTaker.Add(fileSystem.Save());
            SetUpMove(careTaker.HasPrevious, careTaker.HasNext, GetParent(fileSystem.Selected.Path) != null);
        }

        private void HistoryView_Up(object sender, EventArgs e)
        {
            FileSystemEntry parent = GetParent(historyView.Selected);
            if (parent != null)
            {
                historyView.Selected = parent.Path;
                SetUpMove(careTaker.HasPrevious, careTaker.HasNext, GetParent(parent.Path) != null);
            }
        }

        private void SetUpMove(bool back, bool forward, bool up)
        {
            historyView.SetUpBackMove(back);
            historyView.SetUpForwardMove(forward);
            historyView.SetUpUpMove(up);
        }

        private void SubscribeToViewEvents()
        {
            historyView.SelectedChange += HistoryView_SelectedChange;
            historyView.Back += HistoryView_Back;
            historyView.Forward += HistoryView_Forward;
            historyView.Up += HistoryView_Up;
        }
    }
}