using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class HeroRankingsJsonModel
    {
        [JsonProperty("rankings")]
        public IEnumerable<Ranking> Rankings { get; set; }
    }

    public class Ranking
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("personaname")]
        public string Name { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }

        [JsonProperty("rank_tier")]
        public string RankTier { get; set; }
    }
}
