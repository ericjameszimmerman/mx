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
            this.AddCommand(new AddActivityCommand(this));
            this.AddCommand(new DeleteActivityCommand(this));
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

        protected override string OnAutoCompleteRequest(string partialEntry)
        {
            List<string> matchingList = new List<string>();

            partialEntry = partialEntry.Replace("\"", "");

            foreach (ActivityObjectBase activity in this.Project.WorkingGroup.Items)
            {
                if (activity.Name.StartsWith(partialEntry, StringComparison.InvariantCultureIgnoreCase))
                {
                    matchingList.Add(activity.Name);
                }
            }

            if (matchingList.Count == 1)
            {
                return this.ResponseHelper(matchingList[0]);
            }
            else if (matchingList.Count > 1)
            {
                string result = matchingList[0];
                int startIndex = partialEntry.Length - 1;

                for (int i = 1; i < matchingList.Count; i++)
                {
                    result = this.StringIntersection(result, matchingList[i], startIndex);
                }

                return this.ResponseHelper(result);
            }
            else
            {
                return string.Empty;
            }
        }

        private string ResponseHelper(string responseInput)
        {
            if (responseInput.Contains(' '))
            {
                // put it in quotes
                return string.Format("\"{0}\"", responseInput);
            }
            else
            {
                return responseInput;
            }
        }

        private string StringIntersection(string s1, string s2, int startIndex)
        {
            int length = Math.Min(s1.Length, s2.Length);

            // Since we are possibly not starting at zero, adjust the length so we don't
            // index past the end of the shortest string.
            length -= startIndex;

            int index;

            for (index = startIndex; index <= length; index++)
            {
                if (Char.ToLower(s1[index]) != Char.ToLower(s2[index]))
                {
                    break;
                }
            }

            return s1.Substring(0, index);
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
