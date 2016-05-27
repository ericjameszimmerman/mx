using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class TrackingEntry
    {
        public TrackingEntry()
        {

        }
        
        public User Charger { get; set; }

        public double Hours { get; set; }

        public DateTime Date { get; set; }
    }
}
