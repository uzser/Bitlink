using Bitlink.Entities;
using System;
using System.Linq;

namespace Bitlink.Data.Repositories.Extensions
{
    public static class UserRepositoryExtension
    {
        public static User GetOrAddUser(this IEntityBaseRepository<User> repository, Guid userUid)
        {
            var user = repository.FindBy(x => x.Uid == userUid).FirstOrDefault();
            if (user != null)
                return user;

            user = new User
            {
                DateCreated = DateTime.Now,
                Uid = userUid,
            };
            repository.Add(user);
            return user;
        }
    }
}
