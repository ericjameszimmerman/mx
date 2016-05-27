namespace EstimationConsole
{
    using Commands;
    using System;
    using System.Collections.Generic;
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

            while (true)
            {
                // TODO: Reads
                Console.Write(string.Format("\n{0}", this.Prompt));
                string line = Console.ReadLine();

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

        protected virtual void ShowUsage()
        {

        }

        protected void AddCommand(CommandBase newCommand)
        {
            this.commandLookup.Add(newCommand.Name, newCommand);
        }
    }
}
