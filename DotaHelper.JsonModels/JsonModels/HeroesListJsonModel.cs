using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class HeroesListJsonModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("localized_name")]
        public string Name { get; set; }

        [JsonProperty("pro_win")]
        public int ProWins { get; set; }

        [JsonProperty("pro_pick")]
        public double ProPick { get; set; }

        [JsonProperty("1_pick")]
        public double Pick1 { get; set; }

        [JsonProperty("1_win")]
        public double Win1 { get; set; }

        [JsonProperty("2_pick")]
        public double Pick2 { get; set; }

        [JsonProperty("2_win")]
        public double Win2 { get; set; }

        [JsonProperty("3_pick")]
        public double Pick3 { get; set; }

        [JsonProperty("3_win")]
        public double Win3 { get; set; }

        [JsonProperty("4_pick")]
        public double Pick4 { get; set; }

        [JsonProperty("4_win")]
        public double Win4 { get; set; }

        [JsonProperty("5_pick")]
        public double Pick5 { get; set; }

        [JsonProperty("5_win")]
        public double Win5 { get; set; }

        [JsonProperty("6_pick")]
        public double Pick6 { get; set; }

        [JsonProperty("6_win")]
        public double Win6 { get; set; }

        [JsonProperty("7_pick")]
        public double Pick7 { get; set; }

        [JsonProperty("7_win")]
        public double Win7 { get; set; }

        [JsonProperty("8_pick")]
        public double Pick8 { get; set; }

        [JsonProperty("8_win")]
        public double Win8 { get; set; }
    }
}
