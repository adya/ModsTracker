using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMT.JsonConverters
{
    class SourcesIDJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType().Equals(typeof(HashSet<Source>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.ReadFrom(reader).ToObject<List<int>>();
            var sources = SourcesManager.Sources.Where(s => array.Contains(s.ID));
            return new HashSet<Source>(sources);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as HashSet<Source>).Select(s => s.ID));
        }
    }
}
