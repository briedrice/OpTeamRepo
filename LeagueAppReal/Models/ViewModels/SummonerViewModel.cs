using RiotSharp;
using RiotSharp.GameEndpoint;
using RiotSharp.GameEndpoint.Enums;
using RiotSharp.LeagueEndpoint;
using RiotSharp.MatchEndpoint;
using RiotSharp.StaticDataEndpoint;
using RiotSharp.SummonerEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueAppReal.Models.ViewModels
{
    public class SummonerViewModel
    {
        //General Summoner Info
        public string SummonerName { get; set; }
        public long SummonerLevel { get; set; }
        public int SummonerIconId { get; set; }
        public long SummonerId { get; set; }
        public string SummonerIcon { get; set; }
        public Region SummonerRegion { get; set; }
        public List<LeagueInfo> League { get; set; }
        //Static Data
        public Dictionary<string, ChampionStatic>.ValueCollection Champions { get; set; }

        //matchHistory
        public List<GameEntity> MatchList { get; set; }
    }
    public class GameEntity {
        public int Kills { get; set; }
        public List<FellowSummoner> inGameSummonerName { get; set; }
        public int Assist { get; set; }
        public int Deaths { get; set; }
        public string ChampName {get; set;}
        public string ChampPicture { get; set; }
        public bool win { get; set; }
        public string Kda { get; set; }
        public MapType Map { get; set; }
        public string SummonerSpell1 { get; set; }
        public string SummonerSpell2 { get; set; }
        public GameMode GameMode { get; set; }
        public GameSubType GameSubType { get; set; }
        public GameType GameType { get; set; }
        public int ChampLevel { get; set; }
        public string item0 { get; set; }
        public string item1 { get; set; }
        public string item2 { get; set; }
        public string item4 { get; set; }
        public string item5 { get; set; }
        public string item6 { get; set; }
        public string item3 { get; set; }
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

    public class FellowSummoner
    {
        public string champImg { get; set; }
        public string champName { get; set; }
        public string summonerName { get; set; }
        public int teamId { get; set; }
    }
}
