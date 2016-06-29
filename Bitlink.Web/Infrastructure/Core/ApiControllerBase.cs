using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Entities;
using Bitlink.Web.Infrastructure.Utils;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IEntityBaseRepository<Error> ErrorsRepository;
        protected readonly IUnitOfWork UnitOfWork;

        public ApiControllerBase(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            ErrorsRepository = errorsRepository;
            UnitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
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

            ErrorsRepository.Add(error);
            UnitOfWork.Commit();
        }
    }
}