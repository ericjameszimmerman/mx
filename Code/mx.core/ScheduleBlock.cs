using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class ScheduleBlock
    {
        public ScheduleBlock()
        {
            this.Collection = new List<ScheduleItem>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ScheduleItem> Collection { get; set; }
    }
}
