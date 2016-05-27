using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTrackingCollection
    {
        public JsonTrackingCollection()
        {
            this.Collection = new List<JsonTrackingEntry>();
        }

        [JsonProperty]
        public List<JsonTrackingEntry> Collection { get; set; }
    }
}
