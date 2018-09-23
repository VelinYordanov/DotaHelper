using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class MatchPlayerJsonModel
    {
        [JsonProperty("account_id")]
        public string PlayerId { get; set; }

        [JsonProperty("personaname")]
        public string PlayerName { get; set; }

        [JsonProperty("hero_id")]
        public string HeroId { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("hero_damage")]
        public int HeroDamageDone { get; set; }

        [JsonProperty("hero_healing")]
        public int HealingDone { get; set; }

        [JsonProperty("gold_per_min")]
        public int GoldPerMin { get; set; }

        [JsonProperty("item_0")]
        public string Item1Id { get; set; }

        [JsonProperty("item_1")]
        public string Item2Id { get; set; }

        [JsonProperty("item_2")]
        public string Item3Id { get; set; }

        [JsonProperty("item_3")]
        public string Item4Id { get; set; }

        [JsonProperty("item_4")]
        public string Item5Id { get; set; }

        [JsonProperty("item_5")]
        public string Item6Id { get; set; }

        [JsonProperty("kills")]
        public int Kills { get; set; }

        [JsonProperty("deaths")]
        public int Deaths { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("denies")]
        public int Denies { get; set; }

        [JsonProperty("isRadiant")]
        public bool IsRadiant { get; set; }
    }
}
