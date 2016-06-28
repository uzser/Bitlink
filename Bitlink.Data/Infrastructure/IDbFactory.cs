using System;

namespace Bitlink.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BitlinkDbContext Init();
    }
}
