using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LeagueAppReal.Models.Context;

namespace LeagueAppReal.Migrations
{
    [DbContext(typeof(OpTeamContext))]
    partial class OpTeamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LeagueAppReal.Models.Person", b =>
                {
                    b.Property<string>("personId");

                    b.Property<string>("FName");

                    b.Property<string>("LName");

                    b.Property<string>("summonerName");

                    b.HasKey("personId");

                    b.ToTable("Person");
                });
        }
    }
}
