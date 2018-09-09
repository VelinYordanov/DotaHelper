using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerWinsLossesJsonModel
    {
        [JsonProperty("win")]
        public string Wins { get; set; }

        [JsonProperty("lose")]
        public string Losses { get; set; }
    }
}
