namespace Bukkit.JSONAPI.Entities.Player
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class PlayerRequest : RequestBase
    {
        public PlayerRequest()
        {
            this.Name = "players.name";
        }

        [JsonProperty("arguments")]
        public override IEnumerable<object> Arguments
        {
            get
            {
                yield return this.PlayerName;
            }
        }

        [JsonIgnore]
        public string PlayerName { get; set; }
    }
}
