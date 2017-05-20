﻿using RiotSharp;
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
using LeagueAppReal.Models;
using RiotSharp.LeagueEndpoint;
using LeagueAppReal.Models.ViewModels;

namespace LeagueAppReal.Services
{
    public class SummonerInfoService : ISummonInfo
    {
        public void GetSummonerInfo(string summonerName, string api, SummonerViewModel model)
        {

            var myApi = RiotApi.GetInstance(api);
            var staticApi = StaticRiotApi.GetInstance(api);

            if (summonerName != null) {

                GrabSummoner(myApi, summonerName, model);

                var champions = staticApi.GetChampions(Region.na, ChampionData.image).Champions.Values;
                var summonerSpells = staticApi.GetSummonerSpells(Region.na, SummonerSpellData.image).SummonerSpells.Values;
                var version = staticApi.GetVersions(Region.na).FirstOrDefault();
                var rankedStats = myApi.GetStatsRanked(Region.na, model.SummonerId);
                var summonerIdList = new List<long> { model.SummonerId };
                var leagues = myApi.GetLeagues(Region.na, summonerIdList).FirstOrDefault().Value;
                
                GrabEntries(leagues, model);
                
                model.Champions = champions;
                
                model.SummonerIcon = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/profileicon/"+ model.SummonerIconId + ".png";
                
               
                var matchHistory = myApi.GetRecentGames(Region.na, model.SummonerId);//stats
                var matchHistory2 = myApi.GetRecentGamesAsync(Region.na, model.SummonerId);

                var matches = new List<GameEntity>();

                foreach (var game in matchHistory) {

                    var theGame = new GameEntity();

                    theGame.Players = game.FellowPlayers.ToList();
                    theGame.Kills = game.Statistics.ChampionsKilled;
                    theGame.Assist = game.Statistics.Assists;
                    theGame.Deaths = game.Statistics.NumDeaths;
                    theGame.win = game.Statistics.Win;
                    double kda = (double)(game.Statistics.ChampionsKilled + game.Statistics.Assists) / (double)game.Statistics.NumDeaths;
                    theGame.Kda = kda.ToString("0.##");
                    theGame.Map = game.MapType;
                    theGame.ChampLevel = game.Level;
                    theGame.GameMode = game.GameMode;
                    theGame.GameType = game.GameType;
                    theGame.GameSubType = game.GameSubType;

                    var item0Id = game.Statistics.Item0;
                    var item1Id = game.Statistics.Item1;
                    var item2Id = game.Statistics.Item2;
                    var item3Id = game.Statistics.Item3;
                    var item4Id = game.Statistics.Item4;
                    var item5Id = game.Statistics.Item5;
                    var item6Id = game.Statistics.Item6;

                    theGame.item0 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item0Id + ".png";
                    theGame.item1 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item1Id + ".png";
                    theGame.item2 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item2Id + ".png";
                    theGame.item3 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item3Id + ".png";
                    theGame.item4 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item4Id + ".png";
                    theGame.item5 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item5Id + ".png";
                    theGame.item6 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/item/" + item6Id + ".png";

                    var champId = game.ChampionId;
                    var champPlayed = champions.Where(x => x.Id == champId).First();
                    theGame.ChampName = champPlayed.Name;
                    var champImage = champPlayed.Image.Full;
                    theGame.ChampPicture = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/champion/" + champImage;
                   
                    var spell1Id = game.SummonerSpell1;
                    var spell2Id = game.SummonerSpell2;

                    var spell1 = summonerSpells.Where(x => x.Id == spell1Id).First();
                    var spell2 = summonerSpells.Where(x => x.Id == spell2Id).First();

                    theGame.SummonerSpell1 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/spell/" + spell1.Image.Full;
                    theGame.SummonerSpell2 = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/spell/" + spell2.Image.Full;
                    matches.Add(theGame);
                }

                model.MatchList = matches;
                var matchHistory23 = myApi.GetMatchListAsync(Region.na, model.SummonerId).Result;//player info
                //var certainMatch = myApi.GetMatch(Region.na, 2489910377);
                
            }
                
        }

        public void GrabSummoner(IRiotApi myApi, string summonerName, SummonerViewModel model) {
            try
            {
                var summoner = myApi.GetSummoner(Region.na, summonerName);
                model.SummonerName = summoner.Name;
                model.SummonerLevel = summoner.Level;
                model.SummonerRegion = summoner.Region;
                model.SummonerIconId = summoner.ProfileIconId;
                model.SummonerId = summoner.Id;
            }

            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                Console.WriteLine("Could not get summoner or summoner does not exist");
                return;
            } 
        }

        public void GrabEntries(List<League> leagues, SummonerViewModel model) {
            var allLeagues = new List<LeagueInfo>();

            foreach (var league in leagues)
            {
                var leagueInfo = new LeagueInfo();

                leagueInfo.Tier = league.Tier;
                leagueInfo.TierName = league.Name;
                leagueInfo.GameMode = league.Queue;

                leagueInfo.Wins = league.Entries.FirstOrDefault().Wins;
                leagueInfo.Losses = league.Entries.FirstOrDefault().Losses;
                leagueInfo.Division = league.Entries.FirstOrDefault().Division;
                leagueInfo.LeaguePoints = league.Entries.FirstOrDefault().LeaguePoints;
                leagueInfo.RankIcon = "/img/tier-icons/" + leagueInfo.Tier + "_" + leagueInfo.Division + ".png";


                allLeagues.Add(leagueInfo);
            }

            model.League = allLeagues;
        }
        
    }
}
