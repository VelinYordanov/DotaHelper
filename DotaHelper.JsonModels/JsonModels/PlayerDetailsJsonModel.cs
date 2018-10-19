using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerDetailsJsonModel
    {
        [JsonProperty("rank_tier")]
        public string RankTier { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profileurl")]
        public string SteamProfile { get; set; }
    }
}
