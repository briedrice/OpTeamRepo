using LeagueAppReal.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueAppReal.Models.Seed
{
    public class OpTeamSeedData
    {
        private OpTeamContext _context;

        public OpTeamSeedData(OpTeamContext context) {
            _context = context;
        }

        public async Task EnsureSeedData() {

            if (!_context.Person.Any()) {

                var person = new Person()
                {
                    FName = "John",
                    LName = "Smith",
                    personId = "e32d",
                    summonerName = "briedrice"
                };
                _context.Person.Add(person);

                await _context.SaveChangesAsync();
            }
        }
    }
}
