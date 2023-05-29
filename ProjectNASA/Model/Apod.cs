using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ProjectNASA.Model
{
    public class Apod : IEquatable<Apod>
    {
        public string Copyright { get; set; }
        public DateOnly Date { get; set; }
        public string Explanation { get; set; }
        public string Hdurl { get; set; }
        [JsonProperty("media_type")]
        public string MediaType { get; set; }
        [JsonProperty("service_version")]
        public string ServiceVersion { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

        public bool Equals(Apod other)
        {
            if (Date == other.Date)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Apod)) 
                return false;

            return Equals(obj as Apod);
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
    }
}
