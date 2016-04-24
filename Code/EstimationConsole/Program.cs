using mx.core;
using mx.json;
using System;
using System.Collections.Generic;
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
            TestDataBuilder builder = new TestDataBuilder();
            ActivityCollection collection = builder.BuildCollection1();

            TreeCollection treeCollection = builder.BuildTreeCollection(collection);

            JsonObjectFactory factory = new JsonObjectFactory();

            // This is essentially our directory structure and diff list
            string filename = @"D:\Projects\trees.json";
            using (FileStream fs = File.Open(filename, FileMode.Create))
            {
                factory.BuildJsonTreeCollectionFile(fs, treeCollection);
            }

            // This is the details of all activities... estimates not in first rev
            filename = @"D:\Projects\activities.json";
            using (FileStream fs = File.Open(filename, FileMode.Create))
            {
                factory.BuildJsonActivityCollectionFile(fs, collection);
            }

            Project project = new Project();
            project.ProjectPath = builder.GetTestDataPath();
            project.Commit();
            // This is the details of all activities... estimates not in first rev
            //filename = @"D:\Projects\activity.json";
            //using (FileStream fs = File.Open(filename, FileMode.Create))
            //{
            //    factory.BuildJsonActivityFile(fs, collection.Root.Items[1] as Activity);
            //}

            // Next file type is schedule... I should put this object ref into the trees as well
        }
    }
}
