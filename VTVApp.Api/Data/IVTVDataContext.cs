using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Data
{
    public interface IVtvDataContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Inspection> Inspections { get; set; }
        DbSet<Checkpoint> Checkpoints { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Province> Provinces { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    }

}
