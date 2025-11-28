using Microsoft.EntityFrameworkCore;
using Proyecto_FInal_Grupo_1.Models;

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

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.TeamCar)
                .WithOne(tc => tc.Driver)
                .HasForeignKey<Driver>(d => d.TeamCarId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Sponsor)
                .WithMany(s => s.Drivers)
                .HasForeignKey(d => d.SponsorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarSponsor>()
                .HasOne(cs => cs.TeamCar)
                .WithMany(tc => tc.CarSponsors)
                .HasForeignKey(cs => cs.TeamCarId);

            modelBuilder.Entity<CarSponsor>()
                .HasOne(cs => cs.Sponsor)
                .WithMany(s => s.CarSponsors)
                .HasForeignKey(cs => cs.SponsorId);
        }
    }
}