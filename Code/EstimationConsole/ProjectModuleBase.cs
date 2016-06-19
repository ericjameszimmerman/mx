namespace EstimationConsole
{
    using Commands;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectModuleBase
    {
        private Dictionary<string, CommandBase> commandLookup;

        public ProjectModuleBase(string name, ProjectModuleBase parent)
        {
            this.Parent = parent;
            this.Name = name;
            this.commandLookup = new Dictionary<string, CommandBase>();
        }

        protected ProjectModuleBase Parent { get; private set; }

        public virtual Project Project
        {
            get
            {
                return (this.Parent == null) ? null : this.Parent.Project;
            }
        }

        public string Name { get; private set; }

        public virtual string Prompt
        {
            get
            {
                if (this.Parent != null)
                {
                    return string.Format("{0}{1}> ", this.Parent.Prompt, this.Name);
                }
                else
                {
                    return string.Format("{0}> ", this.Name);
                }
            }
        }

        public void RunConsole()
        {
            string command;
            CommandBase commandToExecute;
            ConsoleController console = new ConsoleController();
            console.Initialize(this.OnAutoCompleteRequest);

            while (true)
            {
                Console.Write(string.Format("\n{0}", this.Prompt));
                string line = console.ReadLine();

                int firstSpaceIndex = line.IndexOf(' ');
                if (firstSpaceIndex < 0)
                {
                    command = line;
                    line = string.Empty;
                }
                else
                {
                    command = line.Substring(0, firstSpaceIndex);
                    line = line.Substring(firstSpaceIndex + 1);
                }

                string[] args = ArgumentsHelper.ConvertStringToArgs(command, line);

                if (command == "back")
                {
                    break;
                }
                else if (command == "quit")
                {
                    break;
                }
                else
                {
                    if (commandLookup.TryGetValue(command, out commandToExecute))
                    {
                        commandToExecute.Execute(args);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Command");
                        this.ShowUsage();
                    }
                }
            }
        }

        protected virtual string OnAutoCompleteRequest(string partialEntry)
        {
            return string.Empty;
        }

        //private string ReadLine()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    ConsoleKeyInfo key;
        //    int x = Console.CursorLeft;
        //    int y = Console.CursorTop;
            
        //    Stream stream = Console.OpenStandardInput();

        //    while (true)
        //    {
                
        //        key = Console.ReadKey();
                
        //        if (key.Key == ConsoleKey.Tab)
        //        {
        //            sb.Append("tab");
        //            Console.SetCursorPosition(x, y);
        //            Console.Write(sb.ToString());
        //        }
        //        else if (key.Key == ConsoleKey.Enter)
        //        {
        //            break;
        //        }
        //        else if (key.Key == ConsoleKey.Backspace)
        //        {

        //        }
        //        else
        //        {
        //            sb.Append(key.KeyChar);
        //        }
        //    }

        //    return sb.ToString();
        //}

        protected virtual void ShowUsage()
        {

        }

        protected void AddCommand(CommandBase newCommand)
        {
            this.commandLookup.Add(newCommand.Name, newCommand);
        }
    }
}
