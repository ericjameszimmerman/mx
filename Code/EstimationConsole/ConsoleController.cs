using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class ConsoleController
    {
        public delegate string AutoCompleteDelegate(string partialEntry);

        private int startX;
        private int startY;
        private int currentX;
        private int currentY;
        private ConsoleLine line;
        private AutoCompleteDelegate autoCompleteDelegate;
        private CommandHistory history;

        public ConsoleController()
        {
            this.history = new CommandHistory();
        }

        public void Initialize(AutoCompleteDelegate autoCompleteDelegate)
        {
            this.autoCompleteDelegate = autoCompleteDelegate;
        }

        public void GetCursorPos()
        {
            this.currentX = Console.CursorLeft;
            this.currentY = Console.CursorTop;
        }

        public void SetCursorPos()
        {
            Console.CursorLeft = this.currentX;
            Console.CursorTop = this.currentY;
        }

        public void SaveStartPos()
        {
            this.startY = Console.CursorTop;
            this.startX = Console.CursorLeft;
        }

        /// <summary>
        /// We want to print spaces to the area we've written to so we can write over
        /// without stale output getting in the way.
        /// </summary>
        public void ClearCharacters(int x, int y, int numberOfCharacters)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(new string(' ', numberOfCharacters));
        }

        public void CursorLeft(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CursorLeft();
            }
        }

        public void CursorLeft()
        {
            if (currentY > startY)
            {
                if (currentX <= 0)
                {
                    currentX = Console.WindowWidth - 1;
                    currentY--;
                }
                else
                {
                    currentX--;
                }
            }
            else if (currentY < startY)
            {
                // Do Nothing
            }
            else if (currentX <= startX)
            {
                // Do Nothing... don't allow previous line
            }
            else
            {
                currentX--;
            }
        }

        public void CursorRight(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CursorRight();
            }
        }

        /// <summary>
        ///     TODO: This is broken; handle line wrap
        /// </summary>
        public void CursorRight()
        {
            if (currentX < (this.line.Length))
            {
                currentX++;
            }
            //else
            //{ 
            //    if (currentY > startY)
            //    {
            //        currentX = Console.WindowWidth;
            //        currentY--;
            //    }
            //}
        }

        private void ReplaceLine(int x, int y, string newLine)
        {
            this.ClearCharacters(x, y, line.Length);

            line.Set(newLine);
            Console.SetCursorPosition(x, y);
            Console.Write(line.ToString());
        }

        /// <summary>
        ///     NOTE: To deal with caveats of terminal junk, I'm maintaining the active console
        ///           line independently and then re-writing it when a large change occurs.
        ///           All of the SetCursorPosition calls are for calls that need to re-write the entire
        ///           line.
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            ConsoleKeyInfo key;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            this.line = new ConsoleLine();
            bool firstHistoryRequest = true;
            bool customChanges = false;

            this.SaveStartPos();

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Tab)
                {
                    this.HandleAutoCompleteRequest(this.line, x, y);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    this.history.Add(line.ToString(), customChanges);
                    Console.Write(Environment.NewLine);
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    line.BackspaceKey();
                    GetCursorPos();
                    Console.SetCursorPosition(x, y);
                    Console.Write(line.ToString());
                    Console.Write(' ');
                    CursorLeft();
                    SetCursorPos();
                    customChanges = true;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    GetCursorPos();
                    CursorLeft();
                    SetCursorPos();
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    GetCursorPos();
                    CursorRight();
                    SetCursorPos();
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    string previousCommand = (firstHistoryRequest) ? this.history.Active : this.history.Previous();
                    if (!string.IsNullOrEmpty(previousCommand))
                    {
                        this.ReplaceLine(x, y, previousCommand);
                        customChanges = false;
                    }
                    firstHistoryRequest = false;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    string nextCommand = this.history.Next();
                    if (!string.IsNullOrEmpty(nextCommand))
                    {
                        this.ReplaceLine(x, y, nextCommand);
                        customChanges = false;
                    }
                }
                else if (key.Key == ConsoleKey.Delete)
                {
                    line.DeleteKey();
                    GetCursorPos();
                    Console.SetCursorPosition(x, y);
                    Console.Write(line.ToString());
                    Console.Write(' ');
                    SetCursorPos();
                    customChanges = true;
                }
                else
                {
                    line.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                    customChanges = true;
                }
            }

            return line.ToString();
        }

        private void HandleAutoCompleteRequest(ConsoleLine line, int x, int y)
        {
            // Find the term to auto-complete first
            string lineToParse = line.ToString();
            int index = lineToParse.LastIndexOf(' ');
            string baseCommand;

            if (index < 0)
            {
                if (lineToParse.Length <= 0)
                {
                    // Nothing to auto-complete. Just ignore.
                    return;
                }

                // Must be the whole thing
                index = 0;
                baseCommand = string.Empty;
            }
            else
            {
                // We found a delimiter, so save up through and including the delimiter
                baseCommand = lineToParse.Substring(0, index + 1);
            }

            string partialEntry = lineToParse.Substring(index).Trim();

            // The overloaded class or delegate is in charge of doing the actual auto-complete
            string autoCompleteResult = this.OnAutoComplete(partialEntry);

            if (!string.IsNullOrEmpty(autoCompleteResult))
            {
                // We need to keep everything before the auto-complete token, but overwrite the partial
                this.ReplaceLine(x, y, baseCommand + autoCompleteResult);
            }
        }

        protected virtual string OnAutoComplete(string partialEntry)
        {
            if (this.autoCompleteDelegate != null)
            {
                return this.autoCompleteDelegate(partialEntry);
            }

            return string.Empty;
        }
    }
}
