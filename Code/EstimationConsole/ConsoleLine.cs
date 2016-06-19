using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class ConsoleLine
    {
        private StringBuilder sb;
        private int writeIndex;

        public ConsoleLine()
        {
            this.sb = new StringBuilder();
            this.writeIndex = 0;
        }

        public int Length
        {
            get
            {
                return this.sb.Length;
            }
        }

        public void DeleteKey()
        {
            // Remove a character in front of the cursor... nothing if the last character
            if (writeIndex < (this.sb.Length - 1))
            {
                this.sb.Remove(this.writeIndex, 1);
            }
        }

        public void BackspaceKey()
        {
            // Remove a character behind the cursor... nothing if the first character
            if (writeIndex > 0)
            {
                this.sb.Remove(this.writeIndex - 1, 1);
                this.writeIndex--;
            }
        }

        public void Append(char c)
        {
            this.sb.Append(c);
            this.writeIndex++;
        }

        public void Append(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            this.sb.Append(value);
            this.writeIndex += value.Length;
        }

        public void Set(string value)
        {
            this.sb.Clear();
            this.sb.Append(value);
            this.writeIndex = this.sb.Length;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
