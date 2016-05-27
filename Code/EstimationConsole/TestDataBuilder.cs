using mx.core;
using mx.core.Utility;
using mx.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class TestDataBuilder
    {
        private JsonObjectFactory factory;

        public TestDataBuilder()
        {
            this.factory = new JsonObjectFactory();
        }

        public ActivityCollection BuildCollection1()
        {
            ActivityCollection collection = new ActivityCollection();

            RootGroup root = collection.Root;
            Group group = new Group() { Name = "Group 1" };
            collection.AddNew(root, group);

            Activity activity1 = new Activity() { Name = "Activity 1", ShortName = "GRP1-1", Description = "The first task" };
            Activity activity2 = new Activity() { Name = "Activity 2", ShortName = "GRP1-2", Description = "The second task" };
            Activity activity3 = new Activity() { Name = "Activity 3", ShortName = "GRP1-3", Description = "The third task" };
            Activity activity4 = new Activity() { Name = "Activity 4", ShortName = "ROOT-1", Description = "The fourth task" };
            Activity activity5 = new Activity() { Name = "Activity 5", ShortName = "ROOT-2", Description = "The fifth task" };

            collection.AddNew(group, activity1);
            collection.AddNew(group, activity2);
            collection.AddNew(group, activity3);
            collection.AddNew(root, activity4);
            collection.AddNew(root, activity5);

            this.BuildOutputStructure(root, GetTestDataPath());
            //UpdateAllIds(root);

            return collection;
        }

        public ActivityCollection BuildMxCollection()
        {
            ActivityCollection collection = new ActivityCollection();
            User userEric = new User("Eric Zimmerman", "ericjzim@gmail.com");

            RootGroup root = collection.Root;
            Group mxSimpleGroup = new Group() { Name = "MxSimple", GroupCode = "MXSE" };
            collection.AddNew(root, mxSimpleGroup);

            Group mxSimpleViewsGroup = new Group() { Name = "Views" };
            collection.AddNew(mxSimpleGroup, mxSimpleViewsGroup);

            Activity treeViewEditor = new Activity() { Name = "Tree View Editor", ShortName = "MXSE-1", Description = "Editor supporting CRUD operations for activities" };
            Activity activityView = new Activity() { Name = "Activity View", ShortName = "MXSE-2", Description = "View for the selected activity" };
            Activity scheduleListView = new Activity() { Name = "Schedule List View", ShortName = "MXSE-3", Description = "Editor supporting add/remove operations for linking activities to an iteration" };
            Activity activityTreeViewSelector = new Activity() { Name = "Activity Tree View Selector", ShortName = "MXSE-4", Description = "Derivative of Tree Editor to select a task to add to iteration list" };
            Activity scheduleView = new Activity() { Name = "Schedule View", ShortName = "MXSE-5", Description = "Editor supporting CRUD operations for schedules / iterations" };
            Activity activityEditDialog = new Activity() { Name = "Activity Edit Dialog", ShortName = "MXSE-6", Description = "Dialog box for editing description, estimate, adding notes, etc." };
            Activity trackingView = new Activity() { Name = "Tracking View", ShortName = "MXSE-7", Description = "View supporting accumulation of time entries by task, group, etc." };
            Activity trackingEditView = new Activity() { Name = "Tracking Edit View", ShortName = "MXSE-8", Description = "View supporting add/edit operations of time entries" };

            collection.AddNew(mxSimpleViewsGroup, treeViewEditor);
            collection.AddNew(mxSimpleViewsGroup, activityView);
            collection.AddNew(mxSimpleViewsGroup, scheduleListView);
            collection.AddNew(mxSimpleViewsGroup, activityTreeViewSelector);
            collection.AddNew(mxSimpleViewsGroup, scheduleView);
            collection.AddNew(mxSimpleViewsGroup, activityEditDialog);
            collection.AddNew(mxSimpleViewsGroup, trackingView);
            collection.AddNew(mxSimpleViewsGroup, trackingEditView);

            Group mxCoreGroup = new Group() { Name = "MxCore", GroupCode = "MXC" };
            collection.AddNew(root, mxCoreGroup);

            Activity scheduleFileFormat = new Activity() { Name = "Schedule File Format", ShortName = "MXC-1", Description = "Format for assigning items in an iteration; how to link / unique ids" };
            Activity trackingFileFormat = new Activity() { Name = "Tracking File Format", ShortName = "MXC-2", Description = "Format for adding time entries for tasks... general scope" };
            Activity activityFileFormat = new Activity() { Name = "Activity File Format", ShortName = "MXC-3", Description = "Format for an activity, including estimate, comments, attachments?, etc." };
            Activity noSqlDemo = new Activity() { Name = "NOSQL Demo", ShortName = "MXC-4", Description = "Do a small demo; perhaps No-SQL will fit rather than loose files or zip of files" };

            scheduleFileFormat.TimeEntries.Add(new TrackingEntry() { Charger = userEric, Date = DateTime.UtcNow, Hours = 13.25 });
            scheduleFileFormat.TimeEntries.Add(new TrackingEntry() { Charger = userEric, Date = DateTime.UtcNow.Subtract(new TimeSpan(24,0,0)), Hours = 5 });

            activityFileFormat.TimeEntries.Add(new TrackingEntry() { Charger = userEric, Date = DateTime.UtcNow, Hours = 3.5 });
            activityFileFormat.TimeEntries.Add(new TrackingEntry() { Charger = userEric, Date = DateTime.UtcNow.Subtract(new TimeSpan(24, 0, 0)), Hours = 1.75 });

            collection.AddNew(mxCoreGroup, scheduleFileFormat);
            collection.AddNew(mxCoreGroup, trackingFileFormat);
            collection.AddNew(mxCoreGroup, activityFileFormat);
            collection.AddNew(mxCoreGroup, noSqlDemo);
            
            Group mxCommandLineGroup = new Group() { Name = "MxConsole", GroupCode = "MXCL" };
            collection.AddNew(root, mxCommandLineGroup);

            Activity defineSupportedCommands = new Activity() { Name = "Define Supported Commands", ShortName = "MXCL-1", Description = "Make a list of commands and supported options" };

            this.BuildOutputStructure(root, GetTestDataPath());
            //UpdateAllIds(root);

            return collection;
        }

        public void BuildOutputStructure(RootGroup root, string path)
        {
            ClearDirectory(path);

            string activitiesPath = Path.Combine(path, "activities");
            Directory.CreateDirectory(activitiesPath);

            BuildOutput(root, activitiesPath);
        }

        public void BuildOutput(Group group, string path)
        {
            this.BuildGroupFileOutput(group, path);

            foreach (ActivityObjectBase item in group.Items)
            {
                if (item is Group)
                {
                    Group newGroup = item as Group;
                    string newPath = Path.Combine(path, newGroup.Name);
                    Directory.CreateDirectory(newPath);
                    BuildOutput(newGroup, newPath);
                }
                else if (item is Activity)
                {
                    Activity newActivity = item as Activity;

                    MemoryStream memoryStream = new MemoryStream(4096);
                    using (StreamWriter sw = new NoCloseStreamWriter(memoryStream))
                    {
                        this.factory.BuildJsonActivityFile(sw, newActivity);
                        //sw.Flush();
                        memoryStream.Position = 0;

                        byte[] myArray = memoryStream.ToArray();
                        string newId = HashGen.Instance.GenerateHash(myArray);
                        newActivity.HashCache = newId;
                        
                        using (FileStream fs = File.Open(Path.Combine(path, newActivity.Name + ".json"), FileMode.Create))
                        {
                            memoryStream.CopyTo(fs);
                            //CopyStream(memoryStream, fs);
                        }
                    }
                }
            }

            //item.ID = HashGen.Instance.GenerateHash(item.GetDataToHash());
        }

        public void BuildGroupFileOutput(Group group, string path)
        {
            MemoryStream memoryStream = new MemoryStream(4096);
            using (StreamWriter sw = new NoCloseStreamWriter(memoryStream))
            {
                this.factory.BuildJsonGroupFile(sw, group);
                //sw.Flush();
                memoryStream.Position = 0;

                byte[] myArray = memoryStream.ToArray();
                string newId = HashGen.Instance.GenerateHash(myArray);
                group.HashCache = newId;

                using (FileStream fs = File.Open(Path.Combine(path, ".group"), FileMode.Create))
                {
                    memoryStream.CopyTo(fs);
                    //CopyStream(memoryStream, fs);
                }
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[4096];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public string GetExecutablePath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public string GetTestDataPath()
        {
            string exePath = GetExecutablePath();
            string path = Path.GetFullPath(Path.Combine(exePath, @"..\..\..\..\TestData\mxproject"));
            return path;
        }

        public void ClearDirectory(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                //ClearDirectory(dir.FullName);
                dir.Delete(true);
            }
        }

        public void UpdateAllIds(RootGroup root)
        {
            foreach (ActivityObjectBase item in root.Items)
            {
                item.HashCache = HashGen.Instance.GenerateHash(item.GetDataToHash());

                if (item is Group)
                {
                    UpdateIds(item as Group);
                }
            }
        }

        public void UpdateIds(Group group)
        {
            foreach (ActivityObjectBase item in group.Items)
            {
                item.HashCache = HashGen.Instance.GenerateHash(item.GetDataToHash());

                if (item is Group)
                {
                    UpdateIds(item as Group);
                }
            }
        }

        public TreeCollection BuildTreeCollection(ActivityCollection collection)
        {
            TreeCollection treeCollection = new TreeCollection();

            this.AddTree(treeCollection, collection.Root);

            return treeCollection;
        }

        public void AddTree(TreeCollection collection, Group group)
        {
            Tree tree = new Tree();
            List<Group> groupList = new List<Group>();

            tree.ID = group.HashCache;
            tree.Name = group.Name;

            foreach (ActivityObjectBase item in group.Items)
            {
                TreeItem treeItem = new TreeItem();

                treeItem.ItemID = item.HashCache;
                treeItem.Name = item.Name;

                if (item is Activity)
                {
                    treeItem.Mode = "1";
                    treeItem.ItemType = "blob";
                }
                else if (item is Group)
                {
                    treeItem.Mode = "2";
                    treeItem.ItemType = "tree";
                    groupList.Add(item as Group);
                }
                else
                {
                    treeItem.Mode = "4";
                    treeItem.ItemType = "err";
                }

                tree.Items.Add(treeItem);
            }

            collection.Add(tree);

            foreach (Group groupItem in groupList)
            {
                this.AddTree(collection, groupItem);
            }
        }

        
    }
}
