using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Entities;
using Bitlink.Web.Infrastructure.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IEntityBaseRepository<Error> ErrorRepository;
        protected readonly IUnitOfWork UnitOfWork;

        public ApiControllerBase(IEntityBaseRepository<Error> errorRepository, IUnitOfWork unitOfWork)
        {
            ErrorRepository = errorRepository;
            UnitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response;

            try
            {
                response = function.Invoke();
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message + ex.StackTrace);
            }

            return response;
        }

        protected HttpResponseMessage CreateHttpResponseUsingUserUid(Func<Guid, bool, HttpResponseMessage> function)
        {
            bool isNewUserUid;
            var userUid = UIUtils.GetUserUid(Request, out isNewUserUid);
            return CreateHttpResponse(() =>
            {
                var response = function(userUid, isNewUserUid);
                if (isNewUserUid)
                    UIUtils.SetUserUid(Request, response);
                return response;
            });
        }

        private void LogError(Exception ex)
        {
            var error = new Error
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                DateCreated = DateTime.Now
            };

            ErrorRepository.Add(error);
            UnitOfWork.Commit();
        }
    }
}