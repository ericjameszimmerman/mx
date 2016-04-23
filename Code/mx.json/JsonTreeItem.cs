using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonTreeItem
    {
        public JsonTreeItem()
        {

        }

        /// <summary>
        ///     A unique ID for this element
        ///     TODO: Likely SHA-1
        /// </summary>
        [JsonProperty(Order = -2)]
        public string ItemID { get; set; }

        [JsonProperty]
        public string ItemType { get; set; }

        [JsonProperty]
        public string Mode { get; set; }

        [JsonProperty]
        public string Name { get; set; }
    }
}
