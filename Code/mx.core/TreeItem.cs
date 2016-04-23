using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class TreeItem
    {
        public TreeItem()
        {

        }

        public string ItemID { get; set; }

        public string ItemType { get; set; }

        public string Mode { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",
                this.Mode,
                this.ItemType,
                this.ItemID,
                this.Name);
        }
    }
}
