using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mx.core.Patterns;

namespace EstimationConsole.Commands
{
    public class CommandBase
    {
        private ProjectModuleBase parent;

        public CommandBase(string name, ProjectModuleBase parent)
        {
            this.Name = name;
            this.parent = parent;
        }

        public string Name { get; private set; }

        public Project ProjectRef
        {
            get
            {
                return (this.parent == null) ? null : this.parent.Project;
            }
        }

        public void Execute(string commandLine)
        {
            string[] args = ArgumentsHelper.ConvertStringToArgs(this.Name, commandLine);
            
            this.TryExecute(args);
        }

        public void Execute(string[] args)
        {
            this.TryExecute(args);
        }

        protected void TryExecute(string[] args)
        {
            try
            {
                if (args.Length > 1)
                {
                    if (string.Compare(args[1], "--help", true) == 0)
                    {
                        this.ShowUsage();
                        return;
                    }
                }

                this.OnExecute(args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                this.ShowUsage();
            }
        }

        protected virtual void OnExecute(string[] args)
        {

        }

        protected virtual void ShowUsage()
        {

        }

        protected void ExecuteOperation(IOperation operation)
        {
            try
            {
                operation.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected bool ConfirmationPrompt(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                string inputLower = input.ToLower();

                switch (inputLower)
                {
                    case "y":
                    case "yes":
                        return true;

                    case "n":
                    case "no":
                        return false;

                    default:
                        Console.WriteLine(string.Format("Invalid Entry ({0})", input));
                        break;
                };
            }
        }
    }
}
