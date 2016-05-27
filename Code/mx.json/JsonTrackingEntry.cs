using mx.json.properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTrackingEntry : JsonObjectBase
    {
        public JsonTrackingEntry()
        {
        }
        
        [JsonProperty]
        public string Charger { get; set; }

        /// <summary>
        ///     The date of the time entry
        /// </summary>
        [JsonProperty]
        public string Date { get; set; }

        /// <summary>
        ///     The amount of time charged to the activity
        /// </summary>
        [JsonProperty]
        public string Duration { get; set; }

        public override string DataType
        {
            get
            {
                return "timeentry";
            }
        }
    }
}
