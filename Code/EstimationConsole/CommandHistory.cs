using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class CommandHistory
    {
        private List<string> commandHistoryList;
        private int currentIndex;

        public CommandHistory()
        {
            this.commandHistoryList = new List<string>();
            this.currentIndex = 0;
        }

        public string Previous()
        {
            if (commandHistoryList.Count <= 0)
            {
                return string.Empty;
            }

            if (this.currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = 0;
            }

            return this.Active;
        }

        public string Next()
        {
            if (commandHistoryList.Count <= 0)
            {
                return string.Empty;
            }

            if (this.currentIndex < (this.commandHistoryList.Count - 1))
            {
                currentIndex++;
            }
            else
            {
                currentIndex = this.commandHistoryList.Count - 1;
            }
            
            return this.Active;
        }

        public void Add(string newEntry, bool isNewEntry)
        {
            if (string.IsNullOrWhiteSpace(newEntry))
            {
                return;
            }

            this.commandHistoryList.Add(newEntry);

            if (isNewEntry)
            {
                this.currentIndex = this.LastIndex;
            }
        }

        public void ClearAll()
        {
            this.commandHistoryList.Clear();
            this.currentIndex = 0;
        }

        private int LastIndex
        {
            get
            {
                if (this.commandHistoryList.Count > 0)
                {
                    return this.commandHistoryList.Count - 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Active
        {
            get
            {
                if (currentIndex >= 0 && currentIndex < commandHistoryList.Count)
                {
                    return commandHistoryList[currentIndex];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
