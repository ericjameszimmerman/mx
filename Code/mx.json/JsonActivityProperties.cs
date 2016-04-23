using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json.properties
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonPropertyBase
    {
        public JsonPropertyBase()
        {

        }

        public JsonPropertyBase(string name)
        {
            this.Name = name;
        }

        // We want to show this property first...
        // JSON.Net puts base class properties later than the derived object
        // properties.
        [JsonProperty(Order = -2)]
        public string Name { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class JsonStoryPointsProperty : JsonPropertyBase
    {
        public JsonStoryPointsProperty()
            : base("StoryPoints")
        {
        }

        [JsonProperty]
        public string Points { get; set; }
    }
}
