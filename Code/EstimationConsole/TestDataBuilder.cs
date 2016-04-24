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

        public void BuildOutputStructure(RootGroup root, string path)
        {
            ClearDirectory(path);

            string activitiesPath = Path.Combine(path, "activities");
            Directory.CreateDirectory(activitiesPath);

            BuildOutput(root, activitiesPath);
        }

        public void BuildOutput(Group group, string path)
        {
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
                        newActivity.ID = newId;
                        
                        using (FileStream fs = File.Open(Path.Combine(path, newActivity.Name), FileMode.Create))
                        {
                            memoryStream.CopyTo(fs);
                            //CopyStream(memoryStream, fs);
                        }
                    }
                }
            }

            //item.ID = HashGen.Instance.GenerateHash(item.GetDataToHash());
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
                item.ID = HashGen.Instance.GenerateHash(item.GetDataToHash());

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
                item.ID = HashGen.Instance.GenerateHash(item.GetDataToHash());

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

            tree.ID = group.ID;
            tree.Name = group.Name;

            foreach (ActivityObjectBase item in group.Items)
            {
                TreeItem treeItem = new TreeItem();

                treeItem.ItemID = item.ID;
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
