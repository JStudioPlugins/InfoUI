using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoUI
{
    public class StatsDatabase
    {
        private DataStorage<List<Stats>> DataStorage { get; set; }

        public List<Stats> Data { get; private set; }

        public StatsDatabase()
        {
            DataStorage = new DataStorage<List<Stats>>(InfoUI.Instance.Directory, "Stats.json");
        }

        public void Reload()
        {
            Data = DataStorage.Read();
            if (Data == null)
            {
                Data = new List<Stats>();
                DataStorage.Save(Data);
            }
        }

        public void AddKill(CSteamID steamid)
        {
            var stat = Data.FirstOrDefault(x => x.SteamId == steamid);

            int index = Data.FindIndex(x => x.SteamId == steamid);

            if (stat != null)
            {
                Data[index].Kills++;
            }
            else
            {
                Data.Add(new Stats { SteamId = steamid, Deaths = 0, Kills = 1 });
            }
            DataStorage.Save(Data);
        }

        public void AddDeath(CSteamID steamid)
        {
            var stat = Data.FirstOrDefault(x => x.SteamId == steamid);

            int index = Data.FindIndex(x => x.SteamId == steamid);

            if (stat != null)
            {
                Data[index].Deaths++;
            }
            else
            {
                Data.Add(new Stats { SteamId = steamid, Deaths = 1, Kills = 0 });
            }
            DataStorage.Save(Data);
        }

        public int GetKills(CSteamID steamid)
        {
            var stat = Data.FirstOrDefault(x => x.SteamId == steamid);

            if (stat != null)
            {
                return stat.Kills;
            }
            else
            {
                return 0;
            }
        }

        public int GetDeaths(CSteamID steamid)
        {
            var stat = Data.FirstOrDefault(x => x.SteamId == steamid);

            if (stat != null)
            {
                return stat.Deaths;
            }
            else
            {
                return 0;
            }
        }
    }
}
