using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectNASA.Model
{
    public class Coordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [JsonPropertyName("timezone_id")]
        public string TimezoneId { get; set; }
        public int Offset { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("map_url")]
        public string MapUrl { get; set; }
    }
}
