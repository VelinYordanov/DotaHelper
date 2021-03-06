﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerHeroesJsonModel
    {
        [JsonProperty("hero_id")]
        public string HeroId { get; set; }

        [JsonProperty("games")]
        public double GamesPlayed { get; set; }

        [JsonProperty("win")]
        public double GamesWon { get; set; }
    }
}
