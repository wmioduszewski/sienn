namespace SIENN.DbAccess.Persistance
{
    using Microsoft.EntityFrameworkCore;

    public class SiennDbContext : DbContext
    {
        public SiennDbContext(DbContextOptions<SiennDbContext> options)
            : base(options)
        {
        }
    }
}