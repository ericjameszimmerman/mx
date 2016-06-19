using Mono.Options;
using mx.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mx.core.Operations;

namespace EstimationConsole.Commands
{
    public class AddActivityCommand : CommandBase
    {
        public AddActivityCommand(ProjectModuleBase parent) : base("add", parent)
        {

        }

        protected override void ShowUsage()
        {
            // TODO: Look at how Bob structures... I liked that.
        }

        protected override void OnExecute(string[] args)
        {
            if (args == null)
            {
                return;
            }

            if (this.ProjectRef == null || this.ProjectRef.WorkingGroup == null)
            {
                Console.WriteLine("error");
                return;
            }

            bool editMode = false;

            OptionSet option_set = new OptionSet()
                .Add("e|edit", "Opens the edit-mode console for the new item", option => editMode = option != null);
            
            // The name of the new activity will be in the args at a minimum
            // I want to write the activity to file right away following successful creation and update internal cache listing.
            try
            {
                List<string> remainingArgs;
                remainingArgs = option_set.Parse(args);
                
                // We should only have 2 remaining arguments... the command name and target file/activity name
                if (remainingArgs.Count != 2)
                {
                    this.ShowUsage();
                }
                else
                {
                    Activity newActivity = new Activity() { Name = remainingArgs[1] };
                    this.ExecuteOperation(new AddActivityOperation(newActivity, this.ProjectRef.WorkingGroup, this.ProjectRef.Persistor));
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                throw new Exception("Invalid Arguments", ex);
            }
        }
    }
}
