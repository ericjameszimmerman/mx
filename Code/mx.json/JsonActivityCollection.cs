using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonActivityCollection
    {
        public JsonActivityCollection()
        {
            this.Collection = new List<JsonActivityBase>();
        }

        [JsonProperty]
        public List<JsonActivityBase> Collection { get; set; }
    }
}
