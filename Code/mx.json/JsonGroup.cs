using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonGroup
    {
        public JsonGroup()
        {

        }

        /// <summary>
        ///     An activity short-name or code
        /// </summary>
        [JsonProperty]
        public string GroupCode { get; set; }

        [JsonProperty]
        public string UniqueID { get; set; }
    }
}
