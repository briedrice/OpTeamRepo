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
using LeagueAppReal.Models;
using RiotSharp.LeagueEndpoint;
using LeagueAppReal.Models.ViewModels;
using Newtonsoft.Json;

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
                GrabMatchHistory(myApi, model, version, champions, summonerSpells);

                model.Champions = champions;
                model.SummonerIcon = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/profileicon/"+ model.SummonerIconId + ".png";
                
               
                
            }
                
        }

        public string ConvertToImage(string imgType, string imagePng, string version) {

            if (imgType == "item")
            {
                var image = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/" + imgType + "/" + imagePng + ".png";
                return image;
            }
            else {
                var image = "http://ddragon.leagueoflegends.com/cdn/" + version + "/img/" + imgType + "/" + imagePng;
                return image;
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

        public void GrabMatchHistory(RiotApi myApi, SummonerViewModel model, string version, Dictionary<string, ChampionStatic>.ValueCollection champions, Dictionary<string, SummonerSpellStatic>.ValueCollection summonerSpells) {

            var matchHistory = myApi.GetRecentGames(Region.na, model.SummonerId);//stats

            var matches = new List<GameEntity>();

            var match = 0;

            foreach (var game in matchHistory)
            {

                var theGame = new GameEntity();
                

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

                theGame.item0 = ConvertToImage("item", item0Id.ToString(), version);
                theGame.item1 = ConvertToImage("item", item1Id.ToString(), version);
                theGame.item2 = ConvertToImage("item", item2Id.ToString(), version);
                theGame.item3 = ConvertToImage("item", item3Id.ToString(), version);
                theGame.item4 = ConvertToImage("item", item4Id.ToString(), version);
                theGame.item5 = ConvertToImage("item", item5Id.ToString(), version);
                theGame.item6 = ConvertToImage("item", item6Id.ToString(), version);

                var players = game.FellowPlayers;
                var theFellowSummoners = new List<FellowSummoner>();

                foreach (var summoner in players)
                {

                    var theTeam = new FellowSummoner();

                    theTeam.teamId = summoner.TeamId;
                    var champPlayedId = summoner.ChampionId;
                    var champ = champions.Where(x => x.Id == champPlayedId).First();
                    var image = champ.Image.Full;

                    theTeam.champImg = ConvertToImage("champion", image, version);
                    theTeam.champName = champ.Name;

                    theFellowSummoners.Add(theTeam);
                }

                theGame.inGameSummonerName = theFellowSummoners;

            var champId = game.ChampionId;
                var champPlayed = champions.Where(x => x.Id == champId).First();
                theGame.ChampName = champPlayed.Name;
                var champImage = champPlayed.Image.Full;
                theGame.ChampPicture = ConvertToImage("champion", champImage, version);

                var spell1Id = game.SummonerSpell1;
                var spell2Id = game.SummonerSpell2;

                var spell1 = summonerSpells.Where(x => x.Id == spell1Id).First().Image.Full;
                var spell2 = summonerSpells.Where(x => x.Id == spell2Id).First().Image.Full;

                theGame.SummonerSpell1 = ConvertToImage("spell", spell1, version);
                theGame.SummonerSpell2 = ConvertToImage("spell", spell2, version);
                matches.Add(theGame);

                match++;
            }

            model.MatchList = matches;
        }
       
    }
}
