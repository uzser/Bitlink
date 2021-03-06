﻿using AutoMapper;
using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Data.Repositories.Extensions;
using Bitlink.Entities;
using Bitlink.Web.Infrastructure.Consts;
using Bitlink.Web.Infrastructure.Core;
using Bitlink.Web.Infrastructure.Utils;
using Bitlink.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Web.Controllers
{
    public class LinksController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Link> _linkRepository;
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<Click> _clickRepository;

        public LinksController(
            IEntityBaseRepository<Link> linkRepository,
            IEntityBaseRepository<User> userRepository,
            IEntityBaseRepository<Click> clickRepository,
            IEntityBaseRepository<Error> errorRepository, IUnitOfWork unitOfWork)
            : base(errorRepository, unitOfWork)
        {
            _linkRepository = linkRepository;
            _userRepository = userRepository;
            _clickRepository = clickRepository;
        }

        [Route("{hash}")]
        public HttpResponseMessage Get(string hash)
        {
            return CreateHttpResponse(() =>
            {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                var link = _linkRepository.FindBy(x => x.Hash == hash).FirstOrDefault();
                if (link == null)
                {
                    response.Headers.Location = new Uri(Request.RequestUri, "/Errors/Error404");
                    return response;
                }
                _clickRepository.Add(new Click { Link = link, Date = DateTime.Now });
                response.Headers.Location = new Uri(link.Url);
                return response;
            });
        }

        public HttpResponseMessage Get()
        {
            return CreateHttpResponseUsingUserUid((userUid, isNewUserUid) =>
            {
                var linkViewModels = isNewUserUid
                    ? new List<LinkViewModel>()
                    : _linkRepository.GetLinksByUserUid(userUid).ToList().Select(x => CreateLinkViewModel(x, Request));
                var response = Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<IEnumerable<LinkViewModel>>
                {
                    StatusMessage = UIConstants.StatusMessage.Ok,
                    Data = linkViewModels
                });
                return response;
            });
        }

        public HttpResponseMessage Post([FromBody]string url)
        {
            return CreateHttpResponseUsingUserUid((userUid, isNewUserUid) =>
            {
                string formattedUrl;

                #region Parsing
                if (!UIUtils.TryParseUrl(url, out formattedUrl))
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new ResponseModel<LinkViewModel>
                    {
                        StatusMessage = UIConstants.StatusMessage.InvalidUrl
                    });
                #endregion

                #region Link exists checking

                var hashFromUrl = UIUtils.GetHashFromUrl(formattedUrl);

                var link = _linkRepository
                    .FindBy(x => x.Url == formattedUrl || x.Hash == hashFromUrl)
                    .FirstOrDefault();

                var user = _userRepository.GetOrAddUser(userUid);

                if (link != null)
                {
                    var statusMessage = link.Url == formattedUrl
                        ? UIConstants.StatusMessage.LinkExists
                        : UIConstants.StatusMessage.AlreadyShortenedLink;
                    link.Users.Add(user);
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<LinkViewModel>
                    {
                        StatusMessage = statusMessage,
                        Data = CreateLinkViewModel(link, Request)
                    });
                }
                #endregion

                #region Link creation
                link = new Link
                {
                    Url = formattedUrl,
                    DateCreated = DateTime.Now,
                    Hash = RandomIdGenerator.GetBase62(UIConstants.LinkHashLength)
                };
                link.Users.Add(user);

                var response = Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<LinkViewModel>
                {
                    StatusMessage = UIConstants.StatusMessage.Ok,
                    Data = CreateLinkViewModel(link, Request),
                });
                _linkRepository.Add(link);
                #endregion

                return response;
            });
        }

        private LinkViewModel CreateLinkViewModel(Link link, HttpRequestMessage request)
        {
            var linkViewModel = Mapper.Map<Link, LinkViewModel>(link);
            linkViewModel.ShortUrl = UIUtils.GetShortUrl(request, link.Hash);
            return linkViewModel;
        }
    }
}

