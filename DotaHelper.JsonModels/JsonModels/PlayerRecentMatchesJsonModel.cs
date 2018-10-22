using DotaHelper.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerRecentMatchesJsonModel
    {
        [JsonProperty("match_id")]
        public string MatchId { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("hero_id")]
        public string HeroId { get; set; }

        [JsonProperty("kills")]
        public string Kills { get; set; }

        [JsonProperty("deaths")]
        public string Deaths { get; set; }

        [JsonProperty("assists")]
        public string Assists { get; set; }

        [JsonProperty("xp_per_min")]
        public string XpPerMin { get; set; }

        [JsonProperty("gold_per_min")]
        public string GoldPerMin { get; set; }

        [JsonProperty("hero_damage")]
        public string HeroDamageDone { get; set; }

        [JsonProperty("tower_damage")]
        public string TowerDamageDone { get; set; }

        [JsonProperty("player_slot")]
        public int PlayerSlot { get; set; }

        [JsonProperty("radiant_win")]
        public bool RadiantWin { get; set; }

        [JsonProperty("lobby_type")]
        public int Lobby { get; set; }

        [JsonProperty("game_mode")]
        public int Game { get; set; }
    }
}
