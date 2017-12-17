namespace SIENN.DbAccess
{
    using Persistance;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly SiennDbContext siennDbContext;

        public UnitOfWork(SiennDbContext siennDbContext)
        {
            this.siennDbContext = siennDbContext;
        }

        public void Complete()
        {
            siennDbContext.SaveChanges();
        }
    }
}