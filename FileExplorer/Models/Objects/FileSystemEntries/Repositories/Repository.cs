using System;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer.Models.Objects.FileSystemEntries.Repositories
{
    internal abstract class Repository : FileSystemEntry
    {
        public Repository(string path, Repository parent) :
            base(path, parent)
        {
            Name = System.IO.Path.GetFileName(Path);
        }

        public IList<FileSystemEntry> Entries { get; private set; }
            = new List<FileSystemEntry>();

        public bool HasAccessRight
        {
            get
            {
                bool hasAccessRight = true;

                try
                {
                    Directory.EnumerateDirectories(Path);
                }
                catch (UnauthorizedAccessException)
                {
                    hasAccessRight = false;
                }

                return hasAccessRight;
            }
        }

        public bool HasChildDirectories
        {
            get
            {
                bool hasChildDirectories = false;

                if (HasAccessRight)
                {
                    foreach (string childDirectory in Directory.EnumerateDirectories(Path))
                    {
                        hasChildDirectories = true;
                        break;
                    }
                }

                return hasChildDirectories;
            }
        }

        public bool IsExpand { get; set; }
    }
}