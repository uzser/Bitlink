using Bitlink.Entities;
using System;
using System.Linq;

namespace Bitlink.Data.Repositories.Extensions
{
    public static class LinkRepositoryExtension
    {
        public static IQueryable<Link> GetLinksByUserUid(this IEntityBaseRepository<Link> repository, Guid userUid)
        {
            return repository.FindBy(x => x.Users.Any(u => u.Uid == userUid));
        }
    }
}
