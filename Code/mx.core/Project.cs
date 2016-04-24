using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class fffProject
    {
        public fffProject()
        {

        }

        public string ProjectPath { get; set; }

        public void Commit()
        {
            string rootPath = this.ProjectPath;

            string projectPath = Path.Combine(rootPath, ".history");

            if (!Directory.Exists(projectPath))
                Directory.CreateDirectory(projectPath);

            string objectPath = Path.Combine(projectPath, "objects");

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

                item.ItemID = CommitActivityFile(file.FullName);
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

            tree.ID = HashGen.Instance.GenerateHash(tree.GetDataToHash());

            return tree.ID;
        }

        private string CommitActivityFile(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            string hash = HashGen.Instance.GenerateHash(data);

            string folder = Path.Combine(path, hash.Substring(0, 2));
            string filename = hash.Substring(2);

            if (!Directory.Exists(folder))
            {
                File.WriteAllBytes(Path.Combine(folder, filename), data);
            }

            return hash;
        }
    }
}
