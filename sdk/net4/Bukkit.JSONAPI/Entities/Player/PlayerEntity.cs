namespace Bukkit.JSONAPI.Entities.Player
{
    using Newtonsoft.Json;
    using System;

    public class PlayerEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ip")]
        public string IP { get; set; }

        [JsonProperty("level")]
        public int? Level { get; set; }

        [JsonProperty("experience")]
        public int? Experience { get; set; }

        [JsonProperty("health")]
        public int? Health { get; set; }

        [JsonProperty("foodLevel")]
        public int? Food { get; set; }
    }
}
