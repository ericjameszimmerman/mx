using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class TreeCollection
    {

        public TreeCollection()
        {
            this.Collection = new List<Tree>();
        }

        public List<Tree> Collection { get; set; }

        public void Add(Tree tree)
        {
            this.Collection.Add(tree);
        }
    }
}
