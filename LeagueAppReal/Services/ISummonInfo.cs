using LeagueAppReal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueAppReal.Services
{
    public interface ISummonInfo
    {
        void GetSummonerInfo(string summonerInfo, string api, SummonerViewModel model);
    }
}
