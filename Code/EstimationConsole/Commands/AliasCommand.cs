namespace EstimationConsole.Commands
{
    using mx.core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AliasCommand : CommandBase
    {
        private CommandBase commandReference;

        public AliasCommand(ProjectModuleBase parent, string commandAlias, CommandBase commandReference) : base(commandAlias, parent)
        {
            this.commandReference = commandReference;
        }

        protected override void OnExecute(string[] args)
        {
            this.commandReference.Execute(args);
        }
    }
}
