using mx.core;
using mx.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.services.Data
{
    public class ProjectLoader
    {
        private UserCollection userCollection;
        private JsonObjectFactory factory;

        public ProjectLoader()
        {
            this.userCollection = new UserCollection();
            this.factory = new JsonObjectFactory();
        }

        public void Load(string projectPath)
        {

        }

        public Group LoadProjectTree(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Path.Combine(path, "activities"));

            Group rootGroup = new RootGroup();

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                if (directory.Name.StartsWith("."))
                    continue;

                LoadGroupDirectory(rootGroup, directory);
            }

            foreach (FileInfo file in di.GetFiles("*.json"))
            {
                using (FileStream fs = File.Open(file.FullName, FileMode.Open))
                {
                    Activity activity = new Activity();
                    this.factory.LoadActivityFileProperties(activity, fs, this.userCollection);
                    rootGroup.AddItem(activity);
                }
            }

            return rootGroup;
        }

        public void LoadGroupDirectory(Group parentGroup, DirectoryInfo directoryInfo)
        {
            Group group = new Group();

            using (FileStream fs = File.Open(Path.Combine(directoryInfo.FullName, ".group"), FileMode.Open))
            {
                group.Name = directoryInfo.Name;
                group.Parent = parentGroup;
                this.factory.LoadGroupFileProperties(group, fs);
            }

            foreach (DirectoryInfo di in directoryInfo.GetDirectories())
            {
                LoadGroupDirectory(group, di);
            }

            foreach (FileInfo file in directoryInfo.GetFiles("*.json"))
            {
                using (FileStream fs = File.Open(file.FullName, FileMode.Open))
                {
                    Activity activity = new Activity();
                    this.factory.LoadActivityFileProperties(activity, fs, this.userCollection);
                    activity.Name = Path.GetFileNameWithoutExtension(file.Name);
                    activity.Parent = group;
                    group.AddItem(activity);
                }
            }

            parentGroup.AddItem(group);
        }
    }
}
