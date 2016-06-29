using AutoMapper;
using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Data.Repositories.Extensions;
using Bitlink.Entities;
using Bitlink.Web.Infrastructure.Core;
using Bitlink.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Web.Controllers
{
    [AllowAnonymous]
    public class LinksController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Link> _linksRepository;

        public LinksController(IEntityBaseRepository<Link> linksRepository,
             IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
            : base(errorsRepository, unitOfWork)
        {
            _linksRepository = linksRepository;
        }

        public HttpResponseMessage Get()
        {
            return CreateHttpResponseUsingUserUid((userUid, isNewUserUid) =>
            {
                var links = isNewUserUid
                    ? new List<Link>()
                    : _linksRepository.GetLinksByUserUid(userUid).ToList();
                var linkViewModels = Mapper.Map<IEnumerable<Link>, IEnumerable<LinkViewModel>>(links);
                var response = Request.CreateResponse(HttpStatusCode.OK, linkViewModels);
                return response;
            });
        }
    }
}

