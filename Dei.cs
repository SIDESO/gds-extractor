using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GDSExtractor
{
    internal class Dei
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("plate")]
        public string plate { get; set; }

        [JsonConverter(typeof(DateTimeConverter)), JsonProperty("date")]
        public DateTime date { get; set; }

        [JsonProperty("max_speed")]
        public string max_speed { get; set; }
        

        [JsonProperty("camera")]
        public string camera { get; set; }

        [property: JsonIgnore]
        public string resultado { get; set; }

        public string dateStr => date.ToString("dd/MM/yyyy HH:mm");




       
        public string json()
        {
            return JsonConvert.SerializeObject(this);
        }


        public class DateTimeConverter : JsonConverter<DateTime>
        {
            private readonly string _format = "yyyy-MM-dd HH:mm:ss";

            public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString(_format));
            }

            public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return DateTime.ParseExact((string)reader.Value, _format, null);
            }
        }



    }
}
