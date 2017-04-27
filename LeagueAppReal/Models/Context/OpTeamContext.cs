using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LeagueAppReal.Models.Context
{
    public class OpTeamContext :DbContext
    {
        private IConfigurationRoot _config;

        public OpTeamContext(IConfigurationRoot config, DbContextOptions options) :base (options) {
            _config = config;
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer(_config["ConnectionStrings:OpTeamContextConnection"]);
        }
    }
}
