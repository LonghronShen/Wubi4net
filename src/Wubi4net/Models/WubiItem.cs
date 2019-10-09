using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wubi4net
{

    internal class WubiItem
    {

        [JsonProperty("unicode")]
        public ulong Unicode { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("wubi_codes")]
        public List<string> WubiCodes { get; set; }

    }

}