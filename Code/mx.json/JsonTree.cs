using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTree : JsonObjectBase
    {
        public JsonTree()
        {
            Items = new List<JsonTreeItem>();
        }

        [JsonProperty]
        public List<JsonTreeItem> Items { get; set; }
    }
}
