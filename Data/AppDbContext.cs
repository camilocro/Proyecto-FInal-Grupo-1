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

            // Configuración de Relación 1:1 (Driver - TeamCar)
            // Un Driver tiene un TeamCar, y un TeamCar tiene un Driver.
            modelBuilder.Entity<Driver>()
                .HasOne(d => d.TeamCar)
                .WithOne(tc => tc.Driver)
                .HasForeignKey<Driver>(d => d.TeamCarId)
                .IsRequired(false) // La relación es opcional al crear
                .OnDelete(DeleteBehavior.Restrict); // Evitar borrado en cascada peligroso

            // Configuración de Relación 1:N (Sponsor - Drivers)
            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Sponsor)
                .WithMany(s => s.Drivers)
                .HasForeignKey(d => d.SponsorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Relación N:M (TeamCar - Sponsors mediante CarSponsor)
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