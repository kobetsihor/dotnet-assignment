using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=app.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    Name = "Dog",
                    BirthDate = new DateTime(2022, 8, 8),
                    OwnerId = new Guid("02806643-41b8-4891-afaf-e73e145c717b"),
                    OwnerName = "Dog Owner",
                    OwnerEmail = "dogowner@example.com"
                },
                new Animal
                {
                    Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d477"),
                    Name = "Cat",
                    BirthDate = new DateTime(2023, 8, 8),
                    OwnerId = new Guid("193d0469-0245-4a28-985d-685cc879b0f5"),
                    OwnerName = "Cat Owner",
                    OwnerEmail = "catowner@example.com"
                },
                new Animal
                {
                    Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"),
                    Name = "Rabbit",
                    BirthDate = new DateTime(2024, 8, 8),
                    OwnerId = new Guid("becaab5e-3f90-4bb0-b623-8257ebed9e98"),
                    OwnerName = "Rabbit Owner",
                    OwnerEmail = "rabbitsowner@example.com"
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = new Guid("f47ac10b-58cc-4372-a567-0e0232c3d479"),
                    AnimalId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    CustomerId = new Guid("02806643-41b8-4891-afaf-e73e145c717b"),
                    StartTime = new DateTime(2025, 8, 12, 16, 0, 0),
                    EndTime = new DateTime(2025, 8, 12, 17, 0, 0),
                    Notes = "Vet appointment"
                },
                new Appointment
                {
                    Id = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"),
                    AnimalId = new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"),
                    CustomerId = new Guid("02806643-41b8-4891-afaf-e73e145c717b"),
                    StartTime = new DateTime(2025, 8, 13, 16, 0, 0),
                    EndTime = new DateTime(2025, 8, 13, 17, 0, 0),
                    Notes = "Follow-up check"
                }
            );
        }
    }
}