using System.Collections.Generic;

namespace FileExplorer.Models
{
    internal sealed class Memento
    {
        private readonly IEnumerable<string> parents;
        private readonly string path;
       
        public Memento(string path, IEnumerable<string> parents)
        {
            this.path = path;
            this.parents = parents;
        }

        public IEnumerable<string> Parents => parents;

        public string Path => path;
    }
}