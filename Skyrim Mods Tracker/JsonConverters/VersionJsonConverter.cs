using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;

namespace SMT.JsonConverters
{
    class VersionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType().Equals(typeof(Models.Version));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string version = (string)JToken.ReadFrom(reader);
            return new Models.Version(version);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as Models.Version).Value);
        }
    }
}
