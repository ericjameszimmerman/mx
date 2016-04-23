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
    public class JsonActivityBase : JsonObjectBase
    {
        public JsonActivityBase()
        {
            this.Properties = new List<JsonPropertyBase>();
        }

        /// <summary>
        ///     An activity short-name or code
        /// </summary>
        [JsonProperty]
        public string ShortName { get; set; }

        /// <summary>
        ///     The brief name of the activity
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        ///     A longer description of the activity
        /// </summary>
        [JsonProperty]
        public string Description { get; set; }

        [JsonProperty]
        public List<JsonPropertyBase> Properties { get; set; }
    }
}
