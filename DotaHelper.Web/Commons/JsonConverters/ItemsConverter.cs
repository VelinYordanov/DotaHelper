using DotaHelper.Models.JsonModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons.JsonConverters
{
    public class ItemsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<ItemJsonModel>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var result = new List<ItemJsonModel>();
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject root = JObject.Load(reader);
                var items = root.Properties().First().First().Values();
                foreach (var item in items)
                {
                    var itemData = new ItemJsonModel
                    {
                        ItemId = item["id"].ToString(),
                        Name = item["dname"].ToString(),
                        Attributes = item["attrib"].ToString(),
                        Cost = item["cost"].ToString(),
                        Description = item["desc"].ToString(),
                        Lore = item["lore"].ToString(),
                        Image = item["img"].ToString()
                    };

                    result.Add(itemData);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            // We only need to read the json
        }
    }
}
