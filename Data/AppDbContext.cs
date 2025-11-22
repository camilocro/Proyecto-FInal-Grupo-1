using Microsoft.EntityFrameworkCore;
using Proyecto_FInal_Grupo_1.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Proyecto_FInal_Grupo_1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<TeamCar> TeamCars => Set<TeamCar>();
        public DbSet<Sponsor> Sponsors => Set<Sponsor>();
        public DbSet<CarSponsor> CarSponsors => Set<CarSponsor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}