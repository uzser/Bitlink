using Bitlink.Data.Repositories;
using Bitlink.Entities.Common;
using System.Net.Http;

namespace Bitlink.Web.Infrastructure.Extensions
{
    public static class RequestMessageExtensions
    {
        internal static IEntityBaseRepository<T> GetDataRepository<T>(this HttpRequestMessage request) where T : class, IEntityBase, new()
        {
            return request.GetService<IEntityBaseRepository<T>>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            var dependencyScope = request.GetDependencyScope();
            var service = (TService)dependencyScope.GetService(typeof(TService));
            return service;
        }
    }
}