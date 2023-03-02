using Microsoft.EntityFrameworkCore;
using NavalWar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL
{
	public class NavalContext : DbContext
	{
		public NavalContext(DbContextOptions<NavalContext> options) : base(options) { }

		public DbSet<PlayerDAL> Players { get; set; }
		public DbSet<GameDAL> Games { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Ajout player dans BDD
			modelBuilder.Entity<PlayerDAL>()
				.ToTable("Player")
				.Property(p => p.Id)
				.IsRequired();

			modelBuilder.Entity<PlayerDAL>()
				.Property(p => p.Name)
				.IsRequired();

			modelBuilder.Entity<PlayerDAL>()
				.Property(p => p.Map)
				.IsRequired();
            modelBuilder.Entity<PlayerDAL>()
                .Property(p => p.shiplist)
                .IsRequired();
            modelBuilder.Entity<PlayerDAL>()
                .Property(p => p.isready)
                .IsRequired();
            modelBuilder.Entity<PlayerDAL>()
				.HasOne(p => p.GameDAL)
				.WithMany()
				.HasForeignKey(p => p.GameId);

			// Ajout sessions de jeu dans BDD
			modelBuilder.Entity<GameDAL>()
				.ToTable("Game")
				.Property(g => g.GameId)
				.IsRequired();
			modelBuilder.Entity<GameDAL>()
				.Property(g => g.NbrPlayer)
				.IsRequired();

		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
