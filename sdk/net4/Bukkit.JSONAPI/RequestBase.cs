namespace Bukkit.JSONAPI
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class RequestBase
    {
        [JsonProperty("key")]
        public string Key { get; internal set; }

        [JsonProperty("username")]
        public string User { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("arguments")]
        public virtual IEnumerable<object> Arguments { get; set; }
    }
}
