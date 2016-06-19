using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mx.core;
using mx.core.Persistance;
using mx.core.Utility;

namespace mx.json
{
    public class JsonPersistor : IProjectPersistor
    {
        private string projectRootPath;
        private JsonObjectFactory factory;
        private UserCollection userCollection;

        private string activityRootPath;

        public JsonPersistor(string projectRootPath, UserCollection collection)
        {
            this.factory = new JsonObjectFactory();
            this.userCollection = collection;
            this.projectRootPath = projectRootPath;
            this.activityRootPath = Path.Combine(projectRootPath, "activities");
        }

        private string ConvertProjectToFilesystemPath(string projectPath)
        {
            if (projectPath.StartsWith("/"))
            {
                projectPath = projectPath.Substring(1);
            }

            return projectPath.Replace("/", @"\");
        }

        public bool ActivityExists(string path)
        {
            string filepath = Path.Combine(activityRootPath, this.ConvertProjectToFilesystemPath(path) + ".json");
            return File.Exists(filepath);
        }

        public bool GroupExists(string path)
        {
            string grouppath = Path.Combine(activityRootPath, this.ConvertProjectToFilesystemPath(path));
            return Directory.Exists(grouppath);
        }

        public void CreateActivity(Activity activity)
        {
            string activityPath;

            activityPath = this.ConvertProjectToFilesystemPath(activity.Path);
            string path = Path.Combine(this.activityRootPath, activityPath);

            MemoryStream memoryStream = new MemoryStream(4096);
            using (StreamWriter sw = new NoCloseStreamWriter(memoryStream))
            {
                this.factory.BuildJsonActivityFile(sw, activity);
                
                memoryStream.Position = 0;

                byte[] myArray = memoryStream.ToArray();
                string newId = HashGen.Instance.GenerateHash(myArray);
                activity.HashCache = newId;

                using (FileStream fs = File.Open(path + ".json", FileMode.Create))
                {
                    memoryStream.CopyTo(fs);
                }
            }
        }

        public void CreateGroup(Group group)
        {
            string path = Path.Combine(this.activityRootPath, this.ConvertProjectToFilesystemPath(group.Path));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            MemoryStream memoryStream = new MemoryStream(4096);
            using (StreamWriter sw = new NoCloseStreamWriter(memoryStream))
            {
                this.factory.BuildJsonGroupFile(sw, group);
                memoryStream.Position = 0;

                byte[] myArray = memoryStream.ToArray();
                string newId = HashGen.Instance.GenerateHash(myArray);
                group.HashCache = newId;

                using (FileStream fs = File.Open(Path.Combine(path, ".group"), FileMode.Create))
                {
                    memoryStream.CopyTo(fs);
                }
            }
        }

        public void UpdateActivity(Activity activity)
        {
            string path = Path.Combine(this.activityRootPath, this.ConvertProjectToFilesystemPath(activity.Path));
        }

        public void UpdateGroup(Group group)
        {
            string path = Path.Combine(this.activityRootPath, this.ConvertProjectToFilesystemPath(group.Path));
        }

        public void DeleteActivity(Activity activity)
        {
            string path = Path.Combine(this.activityRootPath, this.ConvertProjectToFilesystemPath(activity.Path) + ".json");

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void DeleteGroup(Group group)
        {
            string path = Path.Combine(this.activityRootPath, this.ConvertProjectToFilesystemPath(group.Path));
        }

        //public void LoadGroup(string path, out Group group, bool recursive)
        //{

        //}

        //public void LoadActivity(string path, out Activity activity)
        //{

        //}

        public Group LoadProjectTree()
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(this.activityRootPath);

            Group rootGroup = new RootGroup();

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                if (directory.Name.StartsWith("."))
                {
                    continue;
                }

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

        private void LoadGroupDirectory(Group parentGroup, DirectoryInfo directoryInfo)
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
