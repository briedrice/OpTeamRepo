using RiotSharp.LeagueEndpoint;
using RiotSharp.StaticDataEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueAppReal.Models
{
    public class SummonerViewModel
    {
        //General Summoner Info
        public string SummonerName { get; set; }
        public long SummonerLevel { get; set; }
        public int SummonerIconId { get; set; }
        public long SummonerId { get; set; }
        public string SummonerIcon { get; set; }
        public RiotSharp.Region SummonerRegion { get; set; }
        public List<LeagueInfo> League { get; set; }

        //Static Data
        public Dictionary<string, ChampionStatic>.ValueCollection Champions { get; set; }
    }

    public class LeagueInfo
    {
        public RiotSharp.LeagueEndpoint.Enums.Tier? Tier { get; set; }
        public string TierName { get; set; }
        public RiotSharp.Queue GameMode { get; set; }
        
        public List<LeagueEntry> AEntry { get; set; }
        public string RankIcon { get; set; }
        public int? Wins { get; set; }
        public int? Losses { get; set; }
        public string Division { get; set; }
        public int LeaguePoints { get; set; }
    }
    //public enum RankIcon {
    //    bronze_1,
    //    bronze_2,
    //    bronze_3,
    //    bronze_4,
    //    bronze_5,
    //    silver_1,
    //    silver_2,
    //    silver_3,
    //    silver_4,
    //    silver_5,
    //    gold_1,
    //    gold_2,
    //    gold_3,
    //    gold_4,
    //    gold_5,
    //    platinum_1,
    //    platinum_2,
    //    platinum_3,
    //    platinum_4,
    //    platinum_5,
    //    diamond_1,
    //    diamond_2,
    //    diamond_3,
    //    diamond_4,
    //    diamond_5,
    //}
    
}
