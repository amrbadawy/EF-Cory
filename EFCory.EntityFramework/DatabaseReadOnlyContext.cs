using Microsoft.EntityFrameworkCore;
using EFCory.Entities.Blogs;
using EFCory.Entities.KPIs;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace UPD.EntityFramework
{
    public class DatabaseReadOnlyContext : DbContext
    {
        public DatabaseReadOnlyContext(DbContextOptions options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Blogs.Add(null);

        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<KPI> KPIs { get; set; }
        public virtual DbSet<KpiAnnualItem> KpiAnnualItems { get; set; }
        //public virtual DbSet<KpiItem> KpiItems { get; set; }

        public override int SaveChanges()
            => throw new NotImplementedException();

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
            => throw new NotImplementedException();
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}
