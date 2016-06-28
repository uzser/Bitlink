using Bitlink.Entities.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bitlink.Data.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IQueryable<T> GetAll();

        T GetById(int id);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);
    }
}
