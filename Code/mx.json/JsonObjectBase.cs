﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonObjectBase
    {
        public JsonObjectBase()
        {

        }

        [JsonProperty(Order = -2)]
        public virtual string DataType
        {
            get
            {
                return "err";
            }
        }

        /// <summary>
        ///     A unique ID for this element
        ///     TODO: Likely SHA-1
        /// </summary>
        //[JsonProperty(Order = -3)]
        //public string ID { get; set; }
    }
}
