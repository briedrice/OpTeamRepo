using RiotSharp;
using RiotSharp.ChampionEndpoint;
using RiotSharp.ChampionMasteryEndpoint;
using RiotSharp.CurrentGameEndpoint;
using RiotSharp.GameEndpoint;
using RiotSharp.StaticDataEndpoint;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;

namespace LeagueAppReal.Services
{
    public class SummonerInfoService : ISummonInfo
    {
        public void GetSummonerInfo(string summonerName, string api)
        {
            var myApi = RiotApi.GetInstance(api);
            var staticApi = StaticRiotApi.GetInstance(api);
            var statusApi = StatusRiotApi.GetInstance();

            var summoner = myApi.GetSummoner(Region.na, summonerName);

            var champions = staticApi.GetChampions(Region.na, ChampionData.image).Champions.Values;
            foreach (var champion in champions)
            {
                Console.WriteLine(champion.Name);
                
            }




            var varusRanked = summoner.GetStatsRanked(RiotSharp.StatsEndpoint.Season.Season2017);

            Console.WriteLine(varusRanked);
            Console.ReadLine();
        }
    }
}
