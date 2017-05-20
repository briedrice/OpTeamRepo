using LeagueAppReal.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LeagueAppReal.Models.Seed
{
    public class OpTeamSeedData
    {
        private OpTeamContext _context;
        private UserManager<User> _userManager;

        public OpTeamSeedData(OpTeamContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData() {
            if (await _userManager.FindByEmailAsync("briedrice@gmail.com") == null) {
                var user = new User()
                {
                    UserName = "briedrice",
                    Email = "briedrice@gmail.com",
                    SummonerName = "Briedrice"
                };
                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }
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
