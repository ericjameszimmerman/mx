using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    /// <summary>
    /// I think this is transatory only...
    /// The activity collection will have a dictionary + hierarchical references.
    /// You will build a tree from the hierarchy.
    /// </summary>
    public class Tree : ActivityObjectBase
    {
        public Tree()
        {
            this.Items = new List<TreeItem>();
        }

        public List<TreeItem> Items { get; set; }

        public override string GetDataToHash()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Name);

            foreach (TreeItem item in Items)
            {
                sb.Append(item.ItemID);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                this.ID,
                this.Name);
        }

        
    }
}
