namespace Bukkit.JSONAPI
{
    using Bukkit.JSONAPI.Entities.Player;
    using System;
    using System.Threading.Tasks;

    public sealed class PlayerService : ServiceBase
    {
        public PlayerService(string uri, string user, string pass)
            : base(uri, user, pass)
        {
        }

        public PlayerService(Uri uri, string user, string pass)
            : base(uri, user, pass)
        {
        }

        public PlayerEntity GetPlayer(string name)
        {
            var request = new PlayerRequest { PlayerName = name };
            return base.Request<PlayerEntity>(request);
        }

        public async Task<PlayerEntity> GetPlayerAsync(string name)
        {
            var request = new PlayerRequest { PlayerName = name };
            return await base.RequestAsync<PlayerEntity>(request);
        }
    }
}
