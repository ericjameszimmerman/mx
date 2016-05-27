using EstimationConsole.Commands;
using Mono.Options;
using mx.core;
using mx.services.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class ActivitiesConsole : ProjectModuleBase
    {
        public ActivitiesConsole(ProjectConsole project)
            : base("Activities", project)
        {
            this.AddCommand(new ChangeGroup(this));
            this.AddCommand(new ListGroup(this));
        }

        protected override void ShowUsage()
        {
            
        }

        public override string Prompt
        {
            get
            {
                return string.Format("{0}{1}", base.Prompt, this.Pwd);
            }
        }

        public string Pwd
        {
            get
            {
                if (this.Project == null)
                    return string.Empty;

                StringBuilder sb = new StringBuilder();
                GeneratePwd(this.Project.WorkingGroup, sb);
                return sb.ToString();
            }
        }

        private void GeneratePwd(Group group, StringBuilder sb)
        {
            if (group == null)
                return;

            if (group is RootGroup)
                return;

            GeneratePwd(group.Parent, sb);
            sb.Append(string.Format("{0}> ", group.Name));
        }

        //public override void RunConsole()
        //{
        //    bool show_list = false;


        //        string[] args = ArgumentsHelper.ConvertStringToArgs("activities", line);

        //        OptionSet option_set = new OptionSet()
        //            .Add("?|help|h", "Prints out the options.", option => show_help = option != null)
        //            .Add("l|list", "List - lists the activity hierarchy.", option => show_list = option != null)
        //            .Add("b|back", "Back - returns to previous menu.", option => go_back = option != null)
        //            .Add("q|quit",
        //               "Quit - exits the application",
        //               option => quit = option != null);

        //        try
        //        {
        //            option_set.Parse(args);
        //        }
        //        catch (OptionException e)
        //        {
        //            Debug.WriteLine(e.Message);
        //            ShowHelp("Invalid Argument..", option_set);
        //            continue;
        //        }

        //        if (show_help)
        //        {
        //            ShowHelp(this.Name, option_set);
        //        }
        //        else if (show_list)
        //        {
        //            ShowList();
        //        }
        //        else if (go_back)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            ShowHelp(this.Name, option_set);
        //        }
        //    }
        //}

        //public void ShowHelp(string message, OptionSet option_set)
        //{
        //    Console.Error.WriteLine(message);
        //    option_set.WriteOptionDescriptions(Console.Error);
        //}

    }
}
