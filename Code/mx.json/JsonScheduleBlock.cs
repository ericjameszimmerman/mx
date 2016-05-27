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
    public class JsonScheduleBlock : JsonObjectBase
    {
        public JsonScheduleBlock()
        {
            this.Items = new List<JsonScheduleItem>();
        }

        /// <summary>
        ///     The brief name of the schedule block
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        ///     A longer description of the schedule block
        /// </summary>
        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public List<JsonScheduleItem> Items { get; set; }

        public override string DataType
        {
            get
            {
                return "scheduleblock";
            }
        }
    }
}
