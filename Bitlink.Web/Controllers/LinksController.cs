using AutoMapper;
using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
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
            return CreateHttpResponse(Request, () =>
            {
                var links = _linksRepository.GetAll().ToList();
                var linkViewModels = Mapper.Map<IEnumerable<Link>, IEnumerable<LinkViewModel>>(links);
                var response = Request.CreateResponse(HttpStatusCode.OK, linkViewModels);

                return response;
            });
        }
    }
}

