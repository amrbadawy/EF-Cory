using Microsoft.EntityFrameworkCore;
using EFCory.Entities.Blogs;
using EFCory.Entities.KPIs;

namespace UPD.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<KPI> KPIs { get; set; }
        public virtual DbSet<KpiAnnualItem> KpiAnnualItems { get; set; }
        //public virtual DbSet<KpiItem> KpiItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
