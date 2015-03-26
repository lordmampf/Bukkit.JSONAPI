namespace Bukkit.JSONAPI.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Bukkit.JSONAPI.Entities.Player;
    using System.Threading.Tasks;

    [TestClass]
    public class Examples
    {
        [TestMethod]
        public void GetPlayerTest()
        {
            var playerName = "d03n3rfr1tz3";

            using (var service = new PlayerService("http://minecraft.example.com:20059/", "admin", "changeme"))
            {
                var player = service.GetPlayer(playerName);

                Assert.IsNotNull(player);
            }
        }

        [TestMethod]
        public async Task GetPlayerAsyncTest()
        {
            var playerName = "d03n3rfr1tz3";

            using (var service = new PlayerService("http://minecraft.example.com:20059/", "admin", "changeme"))
            {
                var player = service.GetPlayerAsync(playerName);

                Assert.IsNotNull(player);

                await player;
                Assert.IsNotNull(player.Result);
            }
        }
    }
}
