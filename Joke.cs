using gwc.Interfaces;
using System.Text.Json.Serialization;

namespace gwc
{
    public class Joke : IJoke
    {
        [JsonPropertyName("icon_url")] 
        public string IconUrl { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public override string ToString() {
            return Value;
        }
    }
}