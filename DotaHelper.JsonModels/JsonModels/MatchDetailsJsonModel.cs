using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class MatchDetailsJsonModel
    {
        [JsonProperty("game_mode")]
        public int Game { get; set; }

        [JsonProperty("lobby_type")]
        public int Lobby { get; set; }

        [JsonProperty("radiant_score")]
        public int RadiantScore { get; set; }

        [JsonProperty("last_hits")]
        public int LastHits { get; set; }

        [JsonProperty("dire_score")]
        public int DireScore { get; set; }

        [JsonProperty("radiant_win")]
        public bool RadiantWin { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("picks_bans")]
        public ICollection<PickOrBanJsonModel> PicksAndBans { get; set; }

        [JsonProperty("players")]
        public ICollection<MatchPlayerJsonModel> Players { get; set; }
    }
}
