using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PickOrBanJsonModel
    {
        [JsonProperty("hero_id")]
        public string HeroId { get; set; }

        [JsonProperty("is_pick")]
        public bool IsPicked { get; set; }
    }
}
