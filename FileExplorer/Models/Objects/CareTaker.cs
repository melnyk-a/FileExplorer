﻿using System.Collections.Generic;

namespace FileExplorer.Models
{
    internal sealed class CareTaker
    {
        private int current = -1;
        private readonly IList<Memento> history = new List<Memento>();

        public bool HasNext
        {
            get => current < history.Count - 1;
        }

        public bool HasPrevious
        {
            get => current > 0;
        }

        public void Add(Memento memento)
        {
            if (memento != null)
            {
                if (current == -1)
                {
                    history.Add(memento);
                    ++current;
                }
                else if (history[current].Path != memento.Path)
                {
                    if (current < history.Count - 1)
                    {
                        for(int i= history.Count - current - 1;i!=current+1;--i)
                        {
                            history.RemoveAt(i);
                        }
                    }
                    history.Add(memento);
                    ++current;
                }
            }
        }

        public Memento Back()
        {
            return history[--current];
        }
        
        public Memento Forward()
        {
            return history[++current];
        }
    }
}