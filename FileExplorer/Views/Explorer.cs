using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileExplorer.Views
{
    public partial class Explorer : Form, ITreeView, IListView, IHistoryView
    {
        public Explorer()
        {
            InitializeComponent();
        }

        public string Selected
        {
            get => treeView.SelectedNode.Name;
            set => treeView.SelectedNode = Find(value);
        }

        public event EventHandler Back;
        public event EventHandler<PathEventArgs> Expand;
        public event EventHandler<PathEventArgs> Collapse;
        public event EventHandler Forward;
        public event EventHandler LoadTree
        {
            add { Load += value; }
            remove { Load -= value; }
        }
        public event EventHandler<PathEventArgs> SelectedChange;
        public event EventHandler Up;

        void IListView.AddFolder(string name)
        {
            listView.Items.Add(name, "ClosedFolder.png");
        }

        void ITreeView.AddFolder(string parentKey, string key, string text)
        {
            TreeNode node = Find(parentKey);
            node.Nodes.Add(key, text, "ClosedFolder.png", "ClosedFolder.png");
        }

        void IListView.AddInaccessibleFolder(string name)
        {
            listView.Items.Add(name, "InaccessibleFolder.png");
        }

        void ITreeView.AddInaccessibleFolder(string parentKey, string key, string text)
        {
            TreeNode node = Find(parentKey);
            node.Nodes.Add(key, text, "InaccessibleFolder.png", "InaccessibleFolder.png");
        }

        void IListView.AddFile(string name, string path, string size)
        {
            Icon icon = Icon.ExtractAssociatedIcon(path);
            smallImageList.Images.Add(path, icon);
            largeImageList.Images.Add(path, icon);
            ListViewItem item = listView.Items.Add(name, path);
            item.SubItems.Add(size);
        }

        void ITreeView.AddLogicalDrive(string key, string text)
        {
            treeView.Nodes.Add(key, text, "HardDrive.png", "HardDrive.png");
        }

        private void BackToolStripButton_Click(object sender, EventArgs e)
        {
            Back?.Invoke(this, EventArgs.Empty);
        }

        public void Clear(string key)
        {
            TreeNode node = Find(key);
            node.Nodes.Clear();
        }

        public void ClearList()
        {
            listView.Items.Clear();
        }

        private void DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.Details;
        }

        public void ExpandFolder(string key)
        {
            TreeNode node = Find(key);
            node.Expand();
        }

        private TreeNode Find(string key)
        {
            TreeNode finded = null;
            TreeNode[] node = treeView.Nodes.Find(key, true);
            if (node.Length > 0)
            {
                // Значение ключа уникально, 
                // поэтому мы всегда обращаемся к 0 элементу.
                finded = node[0];
            }
            return finded;
        }

        private void ForwardToolStripButton_Click(object sender, EventArgs e)
        {
            Forward?.Invoke(this, EventArgs.Empty);
        }

        private void LargeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.LargeIcon;
        }

        private void ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.List;
        }

        public bool Loaded(string path, string key)
        {
            bool isLoaded = false;
            TreeNode node = Find(path);
            if (node.FirstNode.Name != key)
            {
                isLoaded = true;
            }
            return isLoaded;
        }

        private void TitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.Tile;
        }

        private void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            Collapse?.Invoke(this, new PathEventArgs(e.Node.Name));
        }

        private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            Expand?.Invoke(this, new PathEventArgs(e.Node.Name));
        }

        private void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            SelectedChange?.Invoke(this, new PathEventArgs(e.Node.Name));
        }

        public void SetUpBackMove(bool isEnable)
        {
            backToolStripButton.Enabled = isEnable;
        }

        public void SetUpForwardMove(bool isEnable)
        {
            forwardToolStripButton.Enabled = isEnable;
        }

        public void SetUpUpMove(bool isEnable)
        {
            upToolStripButton.Enabled = isEnable;
        }

        private void SmallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.SmallIcon;
        }

        public new void Show()
        {
            System.Windows.Forms.Application.Run(this);
        }

        private void UpToolStripButton_Click(object sender, EventArgs e)
        {
            Up?.Invoke(this, EventArgs.Empty);
        }
    }
}