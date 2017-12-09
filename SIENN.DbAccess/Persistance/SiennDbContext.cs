namespace SIENN.DbAccess.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using Services.Model;

    public class SiennDbContext : DbContext
    {
        public SiennDbContext(DbContextOptions<SiennDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new {t.CategoryId, t.ProductId});
        }
    }
}