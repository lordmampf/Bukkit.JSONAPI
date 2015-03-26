namespace Bukkit.JSONAPI
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ResponseBase
    {
        [JsonProperty("is_success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("success")]
        public JArray Success { get; set; }

        [JsonProperty("error")]
        public JObject Error { get; set; }
    }
}
