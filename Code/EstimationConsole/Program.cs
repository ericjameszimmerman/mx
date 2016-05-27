using Mono.Options;
using mx.core;
using mx.json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool show_help = false;
            ProjectConsole projectConsole = new ProjectConsole();

            OptionSet option_set = new OptionSet()
                .Add("?|help|h", "Prints out the options.", option => show_help = option != null)
                .Add("p=|proj=|project=|projectname=",
                   "REQUIRED: ProjectName - The project path to add or edit.",
                   option => projectConsole.ProjectPath = option);

            try
            {
                option_set.Parse(args);
            }
            catch (OptionException e)
            {
                Debug.WriteLine(e.Message);
                ShowHelp("Invalid Argument..", option_set);
            }

            if (show_help)
            {
                ShowHelp("mxconsole.exe", option_set);
            }

            Project project = new Project();
            project.Load();

            projectConsole.SetProject(project);
            projectConsole.RunConsole();

            //TestDataBuilder builder = new TestDataBuilder();
            //ActivityCollection collection = builder.BuildMxCollection();

            //TreeCollection treeCollection = builder.BuildTreeCollection(collection);

            //JsonObjectFactory factory = new JsonObjectFactory();

            //// This is essentially our directory structure and diff list
            //string filename = @"D:\Projects\trees.json";
            //using (FileStream fs = File.Open(filename, FileMode.Create))
            //{
            //    factory.BuildJsonTreeCollectionFile(fs, treeCollection);
            //}

            //// This is the details of all activities... estimates not in first rev
            //filename = @"D:\Projects\activities.json";
            //using (FileStream fs = File.Open(filename, FileMode.Create))
            //{
            //    factory.BuildJsonActivityCollectionFile(fs, collection);
            //}

            //Project project = new Project();
            //project.ProjectPath = builder.GetTestDataPath();
            //project.Commit();
        }

        public static void ShowHelp(string message, OptionSet option_set)
        {
            Console.Error.WriteLine(message);
            option_set.WriteOptionDescriptions(Console.Error);
            Environment.Exit(-1);
        }
    }
}
