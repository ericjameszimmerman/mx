using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonCommit
    {
        public JsonCommit()
        {

        }

        [JsonProperty]
        public string Tree { get; set; }

        [JsonProperty]
        public string Parent { get; set; }

        [JsonProperty]
        public string Commiter { get; set; }

        [JsonProperty]
        public string CommitDate { get; set; }

        [JsonProperty]
        public string CommitMessage { get; set; }
    }
}
