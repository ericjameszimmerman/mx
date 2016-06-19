using mx.core;
using mx.core.Utility;
using mx.json;
using mx.services.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mx.core.Persistance;

namespace EstimationConsole
{
    public class Project
    {
        private JsonObjectFactory factory;
        private string objectPath;
        private string historyPath;
        private Group activityRoot;
        private JsonPersistor persistor;
        private UserCollection userCollection;

        public Project()
        {
            // TODO: Delete this
            this.factory = new JsonObjectFactory();
        }

        public string ProjectPath { get; set; }
        
        public Group ActivityRoot
        {
            get
            {
                return this.activityRoot;
            }
        }

        public IProjectPersistor Persistor
        {
            get
            {
                return this.persistor;
            }
        }

        public Group WorkingGroup { get; set; }

        public void Load()
        {
            TestDataBuilder builder = new TestDataBuilder();

            this.userCollection = new UserCollection();
            this.persistor = new JsonPersistor(builder.GetTestDataPath(), this.userCollection);
            //ProjectLoader loader = new ProjectLoader();
            //this.activityRoot = loader.LoadProjectTree(builder.GetTestDataPath());

            this.activityRoot = this.persistor.LoadProjectTree();
            this.WorkingGroup = this.activityRoot;
        }

        public void Commit()
        {
            string rootPath = this.ProjectPath;

            this.historyPath = Path.Combine(rootPath, ".history");

            if (!Directory.Exists(historyPath))
                Directory.CreateDirectory(historyPath);

            this.objectPath = Path.Combine(historyPath, "objects");

            if (!Directory.Exists(objectPath))
                Directory.CreateDirectory(objectPath);

            string activityPath = Path.Combine(rootPath, "activities");

            CommitTree(activityPath);

            // Create the current tree
            // Iterate through activities directory.
            // Generate IDs, generate object files

            // Create a commit object
            // Create head object
        }

        private string CommitTree(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            Tree tree = new Tree();
            tree.Name = di.Name;

            foreach (FileInfo file in di.GetFiles())
            {
                TreeItem item = new TreeItem();

                item.ItemID = CommitActivityFile(file.FullName, this.objectPath);
                item.Name = file.Name;
                item.Mode = "1";
                item.ItemType = "blob";

                tree.Items.Add(item);
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                TreeItem item = new TreeItem();

                item.ItemID = CommitTree(dir.FullName);
                item.ItemType = "tree";
                item.Mode = "2";
                item.Name = dir.Name;

                tree.Items.Add(item);
            }

            tree.ID = CommitTreeFile(this.objectPath, tree);

            return tree.ID;
        }

        private string CommitTreeFile(string path, Tree tree)
        {
            MemoryStream memoryStream = new MemoryStream(4096);

            this.factory.BuildJsonTreeFile(memoryStream, tree);
            memoryStream.Position = 0;

            byte[] myArray = memoryStream.ToArray();
            string hash = HashGen.Instance.GenerateHash(myArray);

            string folder = Path.Combine(path, hash.Substring(0, 2));
            string filename = hash.Substring(2);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (FileStream fs = File.Open(Path.Combine(folder, filename), FileMode.Create))
            {
                memoryStream.CopyTo(fs);
            }

            //using (FileStream fs = File.Open(Path.Combine(folder, filename), FileMode.Create))
            //{
            //    factory.BuildJsonTreeFile(fs, tree);
            //}

            return hash;
        }

        private string CommitActivityFile(string path, string targetPath)
        {
            byte[] data = File.ReadAllBytes(path);
            string hash = HashGen.Instance.GenerateHash(data);

            string folder = Path.Combine(targetPath, hash.Substring(0, 2));
            string filename = hash.Substring(2);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllBytes(Path.Combine(folder, filename), data);

            return hash;
        }
    }
}
