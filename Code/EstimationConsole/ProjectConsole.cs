using EstimationConsole.Commands;
using Mono.Options;
using mx.core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class ProjectConsole : ProjectModuleBase
    {
        private Project project;

        public ProjectConsole() : base("Project", null)
        {
            ActivitiesConsole activitiesModule = new ActivitiesConsole(this);
            ScheduleConsole scheduleModule = new ScheduleConsole(this);
            TrackingConsole trackingModule = new TrackingConsole(this);

            this.AddCommand(new RunConsoleCommand("act", activitiesModule));
            this.AddCommand(new RunConsoleCommand("sched", scheduleModule));
            this.AddCommand(new RunConsoleCommand("track", trackingModule));
        }

        public string ProjectPath { get; set; }

        public override string Prompt
        {
            get
            {
                return "Project> ";
            }
        }
        
        public void SetProject(Project project)
        {
            this.project = project;
        }

        public override Project Project
        {
            get
            {
                return this.project;
            }
        }

        //public void RunConsole()
        //{
        //    bool show_help = false;
        //    bool activities = false;
        //    bool schedule = false;
        //    bool tracking = false;
        //    bool generate = false;
        //    bool quit = false;


        //    while (true)
        //    {
        //        if (quit)
        //            break;

        //        show_help = false;
        //        activities = false;
        //        schedule = false;
        //        tracking = false;
        //        generate = false;

        //        // TODO: Reads
        //        Console.Write(string.Format("\n{0}", this.Prompt));
        //        string line = Console.ReadLine();

        //        string[] args = ArgumentsHelper.ConvertStringToArgs("project", line);

        //        OptionSet option_set = new OptionSet()
        //            .Add("?|help|h", "Prints out the options.", option => show_help = option != null)
        //            .Add("gen|generate", "Generates test data", option => generate = option != null)
        //            .Add("act|activities", "Selects activities module", option => activities = option != null)
        //            .Add("sch|schedule", "Selects scheduling module", option => schedule = option != null)
        //            .Add("track|tracking", "Selects tracking module", option => tracking = option != null)
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
        //            ShowHelp("project", option_set);
        //        }
        //        else if (generate)
        //        {
        //            TestDataBuilder builder = new TestDataBuilder();
        //            ActivityCollection collection = builder.BuildMxCollection();

        //            TreeCollection treeCollection = builder.BuildTreeCollection(collection);

        //            Project project = new Project();
        //            project.ProjectPath = builder.GetTestDataPath();
        //            project.Commit();
        //        }
        //        else if (activities)
        //        {
        //            activitiesModule.RunConsole();
        //        }
        //        else if (tracking)
        //        {
        //            trackingModule.RunConsole();
        //        }
        //        else if (schedule)
        //        {
        //            scheduleModule.RunConsole();
        //        }
        //        else
        //        {
        //            ShowHelp("project", option_set);
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
