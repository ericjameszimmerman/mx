using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class TrackingCollection
    {
        public TrackingCollection()
        {
            this.Collection = new List<TrackingEntry>();
        }

        public List<TrackingEntry> Collection { get; set; }

        public void Add(TrackingEntry entry)
        {
            this.Collection.Add(entry);
        }
    }
}
