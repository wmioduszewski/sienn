namespace SIENN.DbAccess
{
    using System.ComponentModel;

    public interface IUnitOfWork
    {
        void Complete();
    }
}