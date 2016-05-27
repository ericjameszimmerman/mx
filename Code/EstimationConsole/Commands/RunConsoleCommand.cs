
namespace EstimationConsole.Commands
{
    using System;

    public class RunConsoleCommand : CommandBase
    {
        private ProjectModuleBase module;

        public RunConsoleCommand(string name, ProjectModuleBase module) : base(name, module)
        {
            this.module = module;
        }

        protected override void OnExecute(string[] args)
        {
            try
            {
                module.RunConsole();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
