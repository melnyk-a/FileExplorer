using FileExplorer.Views;

namespace FileExplorer.Presenters
{
    internal sealed class ExplorerPresenter : IPresenter
    {
        private readonly HistoryControlPresenter historyPresenter;
        private readonly ListControlPresenter listPresenter;
        private readonly TreeControlPresenter treePresenter;
        private readonly ITreeView treeView;

        public ExplorerPresenter(
            ListControlPresenter listPresenter,
            TreeControlPresenter treePresenter,
            HistoryControlPresenter historyPresenter,
            ITreeView treeView
            )
        {
            this.treePresenter = treePresenter;
            this.listPresenter = listPresenter;
            this.historyPresenter = historyPresenter;
            this.treeView = treeView;
        }

        public void Run()
        {
            treeView.Show();
        }
    }
}