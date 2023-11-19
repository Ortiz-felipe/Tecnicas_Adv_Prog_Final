using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VTVApp.Api.Models;
using VTVApp.Api.Models.Entities;


namespace VTVApp.Api.Data
{
    public class VTVDataContext : DbContext, IVtvDataContext
    {
        public VTVDataContext(DbContextOptions<VTVDataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Checkpoint> Checkpoints { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new async ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await base.AddAsync(entity, cancellationToken);
        }

        public new async Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
        {
            await base.AddRangeAsync(entities, cancellationToken);
        }

        public new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Update(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to Vehicle relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Vehicles)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId);

            // User to Appointment relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Appointments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // User to City relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.City)
                .WithMany()
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // User to Province relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Province)
                .WithMany()
                .HasForeignKey(u => u.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Vehicle to Appointment relationship
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Appointments)
                .WithOne(a => a.Vehicle)
                .HasForeignKey(a => a.VehicleId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Vehicle)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment to Inspection relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Inspection)
                .WithOne(i => i.Appointment)
                .HasForeignKey<Inspection>(i => i.AppointmentId);

            // Inspection to Checkpoint relationship
            modelBuilder.Entity<Inspection>()
                .HasMany(i => i.Checkpoints)
                .WithOne(c => c.Inspection)
                .HasForeignKey(c => c.InspectionId);

            // City to Province relationship
            modelBuilder.Entity<City>()
                .HasOne(c => c.Province)
                .WithMany(p => p.Cities)
                .HasForeignKey(c => c.ProvinceId);

            // Configure User Role as enum string
            modelBuilder
                .Entity<User>()
                .Property(e => e.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v));
        }
    }
}
