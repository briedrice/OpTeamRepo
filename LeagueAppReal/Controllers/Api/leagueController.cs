using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LeagueAppReal.Services;
using Microsoft.Extensions.Configuration;
using LeagueAppReal.Models.Context;
using Microsoft.AspNetCore.Authorization;
using LeagueAppReal.Models.ViewModels;
using LeagueAppReal.Models;

namespace LeagueAppReal.Controllers
{
    /// <summary>
    /// A controller intercepts the incoming browser request and returns
    /// an HTML view (.cshtml file) or any other type of data.
    /// </summary>
    public class LeagueController : Controller
    {
        private IConfigurationRoot _config;
        private OpTeamContext _context;
        private ISummonInfo _summonerInfo;

        public LeagueController(ISummonInfo summonerInfo, IConfigurationRoot config, OpTeamContext context) {
            _summonerInfo = summonerInfo;
            _config = config;
            _context = context;
        }

        [HttpGet("/api/league")]
        public IActionResult Get(SummonerViewModel model, string summonerName) {
            _summonerInfo.GetSummonerInfo(summonerName, _config["ApiKey:Key"], model);
            return Ok(model);
        }
    }
}
