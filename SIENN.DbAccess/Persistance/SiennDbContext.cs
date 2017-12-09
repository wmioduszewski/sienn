﻿namespace SIENN.DbAccess.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using WebApi.Model;

    public class SiennDbContext : DbContext
    {
        public SiennDbContext(DbContextOptions<SiennDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}