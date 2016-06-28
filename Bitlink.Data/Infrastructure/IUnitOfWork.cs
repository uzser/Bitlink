namespace Bitlink.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
