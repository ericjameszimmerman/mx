using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTreeCollection
    {
        public JsonTreeCollection()
        {
            this.Collection = new List<JsonTree>();
        }

        [JsonProperty]
        public List<JsonTree> Collection { get; set; }
    }
}
