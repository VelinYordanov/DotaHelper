using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class HeroesJsonModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("localized_name")]
        public string Name { get; set; }

        [JsonProperty("name")]
        public string FullName { get; set; }
    }
}
