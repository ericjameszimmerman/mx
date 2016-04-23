using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class RootGroup : Group
    {
        public RootGroup()
        {
            this.Name = "Root";
            this.Parent = null;
            this.ID = "0000000000000000000000000000000000000000";    
        }
    }
}
