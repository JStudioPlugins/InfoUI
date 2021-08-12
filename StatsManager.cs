using Rocket.Core.Logging;
using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace InfoUI
{
    public static class StatsManager
    {
        public static Timer timer;
        public static StatsDatabase database = new StatsDatabase();

        public static void Start()
        {
            timer = new Timer(7000);
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
            Logger.Log("STATS MANAGER START CALLED, WILL RELOAD DATABASE");
            database.Reload();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (SteamPlayer player in Provider.clients)
            {
                UnturnedPlayer uplay = UnturnedPlayer.FromCSteamID(player.playerID.steamID);
                EffectManager.sendUIEffectText(134, player.transportConnection, true, "Rep", $"Reputation: {uplay.Reputation}");
                EffectManager.sendUIEffectText(134, player.transportConnection, true, "Kills", $"Kills: {database.GetKills(player.playerID.steamID)}");
                EffectManager.sendUIEffectText(134, player.transportConnection, true, "Deaths", $"Deaths: {database.GetDeaths(player.playerID.steamID)}");
            }
        }
    }
}
