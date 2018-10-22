using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerWinsLossesJsonModel
    {
        [JsonProperty("win")]
        public double Wins { get; set; }

        [JsonProperty("lose")]
        public double Losses { get; set; }
    }
}
