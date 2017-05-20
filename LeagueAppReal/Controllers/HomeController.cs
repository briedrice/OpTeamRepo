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
    public class HomeController : Controller
    {
        private IConfigurationRoot _config;
        private OpTeamContext _context;
        private ISummonInfo _summonerInfo;

        public HomeController(ISummonInfo summonerInfo, IConfigurationRoot config, OpTeamContext context) {
            _summonerInfo = summonerInfo;
            _config = config;
            _context = context;
        }

        public IActionResult Index()
        {
            var people = _context.Person.ToList();
            return View(people);
        }

        public IActionResult LeagueofLegends(SummonerViewModel model)
        {
            if (model.SummonerName != null)
            {
                _summonerInfo.GetSummonerInfo(model.SummonerName, _config["ApiKey:Key"], model);
                return View("LolStats", model);
            }
            else {

                return View(model);
            }
            
        }

        public IActionResult LolStats(SummonerViewModel model) {
            return View(model);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public IActionResult Team()
        {
            return View();
        }
    }
}
