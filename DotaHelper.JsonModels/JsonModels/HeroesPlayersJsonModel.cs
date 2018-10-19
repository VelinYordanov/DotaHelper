using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class HeroesPlayersJsonModel
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("games_played")]
        public double GamesPlayed { get; set; }

        [JsonProperty("wins")]
        public double Wins { get; set; } 
    }
}
