using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.JsonModels
{
    public class PlayerSearchJsonModel
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("personaname")]
        public string Name { get; set; }

        [JsonProperty("avatarfull")]
        public string AvatarUrl { get; set; }
    }
}
