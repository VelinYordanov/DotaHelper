using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Web.Commons.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, new ItemsConverter());
        }
    }
}
