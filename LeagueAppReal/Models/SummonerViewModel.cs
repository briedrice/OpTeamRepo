using RiotSharp.GameEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.MatchEndpoint;
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

        //matchHistory
        public List<GameEntity> MatchList { get; set; }
    }
    public class GameEntity {
        public int Kills { get; set; }
        public List<RiotSharp.GameEndpoint.Player> Players { get; set; }
        public int Assist { get; set; }
        public int Deaths { get; set; }
        public string ChampName {get; set;}
        public string ChampPicture { get; set; }
        public bool win { get; set; }
        public string Kda { get; set; }
        public RiotSharp.MapType Map { get; set; }
        public string SummonerSpell1 { get; set; }
        public string SummonerSpell2 { get; set; }
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
   
    
}
