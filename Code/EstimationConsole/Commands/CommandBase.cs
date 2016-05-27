using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.OnExecute(args);
        }

        public void Execute(string[] args)
        {
            this.OnExecute(args);
        }

        protected virtual void OnExecute(string[] args)
        {

        }
    }
}
