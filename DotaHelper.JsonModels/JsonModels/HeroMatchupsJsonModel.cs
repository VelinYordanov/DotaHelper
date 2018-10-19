using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class HeroMatchupsJsonModel
    {
        [JsonProperty("hero_id")]
        public string HeroId { get; set; }

        [JsonProperty("games_played")]
        public double GamesPlayed { get; set; }

        [JsonProperty("wins")]
        public double Wins { get; set; }
    }
}
