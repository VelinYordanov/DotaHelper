using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    //This item is being deserialized with a custom ItemsConverter. To change deserialization you need to edit the file.
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
        public string ManaCost { get; set; }

        [JsonProperty("cd")]
        public string Cooldown { get; set; }

        [JsonProperty("img")]
        public string Image { get; set; }

        [JsonProperty("qual")]
        public string Quality { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }
    }
}
