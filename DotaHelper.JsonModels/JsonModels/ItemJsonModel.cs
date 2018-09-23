using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class ItemJsonModel
    {
        [JsonProperty("id")]
        public string ItemId { get; set; }

        [JsonProperty("dname")]
        public string Name { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("lore")]
        public string Lore { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("attrib")]
        public string Attributes { get; set; }

        [JsonProperty("mc")]
        public int ManaCost { get; set; }

        [JsonProperty("cd")]
        public int Cooldown { get; set; }

        [JsonProperty("img")]
        public string Image { get; set; }
    }
}
