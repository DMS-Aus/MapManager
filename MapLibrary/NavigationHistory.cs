using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMS.MapLibrary
{
    public class NavigationHistory
    {
        int current;
        private List<NavigationHistoryItem> history;
        public NavigationHistory()
        {
            history = new List<NavigationHistoryItem>();
            current = -1;
        }

        public void Add(NavigationHistoryItem item)
        {
            if (++current < history.Count)
                history.RemoveRange(current, history.Count - current);
            history.Add(item);
        }

        public void First()
        {
            if (history.Count > 0)
                current = 0;
        }

        public bool HasNext()
        {
            return (history.Count > 0) && (current != history.Count - 1);
        }

        public void Next()
        {
            if (HasNext())
            {
                ++current;
            }
        }

        public bool HasPrevious()
        {
            return current > 0;
        }

        public void Previous()
        {
            if (HasPrevious())
            {
                --current;
            }
        }

        public NavigationHistoryItem Current
        {
            get
            {
                return history[current];
            }
        }

        public NavigationHistoryItem this[int number]
        {
            get
            {
                if (number >= 0 && number < history.Count)
                {
                    return history[number];
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (number >= 0 && number < history.Count)
                {
                    history[number] = value;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Clear()
        {
            history.Clear();
            current = -1;
        }
    }
}
