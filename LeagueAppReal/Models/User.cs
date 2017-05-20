using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LeagueAppReal.Models
{
    public class User : IdentityUser
    {
        public string SummonerName { get; set; }

    }
}