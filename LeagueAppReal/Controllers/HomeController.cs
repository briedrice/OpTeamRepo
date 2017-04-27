using System;
using System.Collections.Generic;
using System.Linq;
using LeagueAppReal.Models;
using Microsoft.AspNetCore.Mvc;
using LeagueAppReal.Services;
using Microsoft.Extensions.Configuration;
using LeagueAppReal.Models.Context;

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

        public IActionResult LeagueofLegends()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LeagueofLegends(SummonerViewModel model)
        {
            _summonerInfo.GetSummonerInfo(model.SummonerName, _config["ApiKey:Key"]);

            return View();
        }
    }
}
