using System;

namespace FileExplorer.Views
{
    public class PathEventArgs : EventArgs
    {
        public PathEventArgs(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}