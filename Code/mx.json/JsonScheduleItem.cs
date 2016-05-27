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
    public class JsonScheduleItem : JsonObjectBase
    {
        public JsonScheduleItem()
        {
        }

        /// <summary>
        ///     The unique ID of the activity this pertains to
        /// </summary>
        [JsonProperty]
        public string ActivityID { get; set; }

        [JsonProperty]
        public string Status { get; set; }
        
        public override string DataType
        {
            get
            {
                return "scheduleentry";
            }
        }
    }
}
