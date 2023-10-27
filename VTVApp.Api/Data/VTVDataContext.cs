using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Models;

namespace VTVApp.Api.Data
{
    public class VTVDataContext : DbContext
    {
        public VTVDataContext(DbContextOptions<VTVDataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<Status> Statuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User to City relationship (with cascading delete)
            modelBuilder.Entity<User>()
                .HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Province relationship (without cascading delete)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Province)
                .WithMany()
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            // City to Province relationship (default behavior can be cascade)
            modelBuilder.Entity<City>()
                .HasOne(c => c.Province)
                .WithMany(p => p.Cities)
                .HasForeignKey(c => c.ProvinceId);

            // Relationship between Appointment and Status
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Status)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.StatusID);

            // Relationship between Check and Appointment
            modelBuilder.Entity<Check>()
                .HasOne(c => c.Appointment)
                .WithMany(a => a.Checks)
                .HasForeignKey(c => c.AppointmentID);

            // Relationship between Check and Checkpoint
            modelBuilder.Entity<Check>()
                .HasOne(c => c.Checkpoint)
                .WithMany(cp => cp.Checks)
                .HasForeignKey(c => c.CheckpointID);

            // Assuming that Vehicle's OwnerID relates to the User entity 
            // (this isn't provided in your tables, but just as an example)
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User) // Assuming Owner is a navigation property of type User in the Vehicle class
                .WithMany() // No collection navigation property provided for this relationship
                .HasForeignKey(v => v.UserId);
        }
    }
}
