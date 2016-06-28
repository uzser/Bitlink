namespace Bitlink.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private BitlinkDbContext _dbContext;

        public BitlinkDbContext Init()
        {
            return _dbContext ?? (_dbContext = new BitlinkDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
