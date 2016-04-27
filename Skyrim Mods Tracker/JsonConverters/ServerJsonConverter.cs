using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.JsonConverters
{
    class ServerIDJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType().Equals(typeof(Server));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int server = (int)JToken.ReadFrom(reader);
            return ServersManager.ServerWithID(server);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as Server).ID);
        }
    }
}
