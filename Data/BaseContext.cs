using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
namespace Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
        }

        public override EntityEntry Entry(object entity)
        {
            UpdateLastChangeDate(entity);

            return base.Entry(entity);
        }

        private void UpdateLastChangeDate(object entity)
        {
            var property = entity.GetType().GetProperty("LastChangeDate");

            if (property != null)
                property.SetValue(entity, DateTime.Now);
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        {
            UpdateLastChangeDate(entity);

            return base.Entry(entity);
        }

        public override EntityEntry Update(object entity)
        {
            UpdateLastChangeDate(entity);

            return base.Update(entity);
        }

        public override void UpdateRange(IEnumerable<object> entities)
        {
            foreach (object item in entities)
                UpdateLastChangeDate(item);

            base.UpdateRange(entities);
        }

        public override void UpdateRange(params object[] entities)
        {
            foreach (object item in entities)
                UpdateLastChangeDate(item);

            base.UpdateRange(entities);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            UpdateLastChangeDate(entity);

            return base.Update(entity);
        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedBaseEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedBaseEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

    }
}