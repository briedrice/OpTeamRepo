using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeagueAppReal.Models.Context
{
    public class OpTeamContext :IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public OpTeamContext(IConfigurationRoot config, DbContextOptions options) :base (options) {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer(_config["ConnectionStrings:OpTeamContextConnection"]);
        }

        public DbSet<Person> Person { get; set; }
    }
}
