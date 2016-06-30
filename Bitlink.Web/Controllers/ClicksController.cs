using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Entities;
using Bitlink.Web.Infrastructure.Consts;
using Bitlink.Web.Infrastructure.Core;
using Bitlink.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Web.Controllers
{
    public class ClicksController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Click> _clickRepository;

        public ClicksController(
            IEntityBaseRepository<Click> clickRepository,
            IEntityBaseRepository<Error> errorRepository, IUnitOfWork unitOfWork)
            : base(errorRepository, unitOfWork)
        {
            _clickRepository = clickRepository;
        }

        public HttpResponseMessage Get([FromUri]long[] linkIds)
        {
            return CreateHttpResponse(() =>
            {
                var clickCounts = new ClickViewModel[] { };

                if (linkIds != null && linkIds.Any())
                    clickCounts = _clickRepository
                        .FindBy(x => linkIds.Contains(x.Link.Id))
                        .GroupBy(x => x.Link.Id)
                        .Select(x =>
                            new ClickViewModel
                            {
                                LinkId = x.Key,
                                ClickCount = x.Count()
                            })
                        .ToArray();

                var response = Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<IEnumerable<ClickViewModel>>
                {
                    StatusMessage = UIConstants.StatusMessage.Ok,
                    Data = clickCounts
                });
                return response;
            });
        }
    }
}

