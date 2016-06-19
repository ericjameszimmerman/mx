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
    public class DeleteActivityCommand : CommandBase
    {
        public DeleteActivityCommand(ProjectModuleBase parent) : base("del", parent)
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

            bool silentMode = false;

            OptionSet option_set = new OptionSet()
                .Add("s|silent", "Deletes without prompt", option => silentMode = option != null);

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
                    ActivityObjectBase itemToDelete = this.ProjectRef.WorkingGroup.FindItemByName(remainingArgs[1]);

                    if (itemToDelete == null)
                    {
                        throw new Exception("Activity not found");
                    }

                    Activity activityToDelete = itemToDelete as Activity;
                    if (activityToDelete == null)
                    {
                        throw new Exception(string.Format("Invalid Activity ({0})", remainingArgs[1]));
                    }

                    if (!silentMode)
                    {
                        bool result = this.ConfirmationPrompt(string.Format("Are you sure you want to delete {0}? (Y/n)", activityToDelete.Name));
                        if (!result)
                        {
                            return;
                        }
                    }

                    this.ExecuteOperation(new DeleteActivityOperation(activityToDelete, this.ProjectRef.Persistor));
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
