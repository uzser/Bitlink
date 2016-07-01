using AutoMapper;
using Bitlink.Entities;
using Bitlink.Web.Models;

namespace Bitlink.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        protected override void Configure()
        {
            CreateMap<Link, LinkViewModel>()
                .ForMember(x => x.ShortUrl, x => x.Ignore())
                .ForMember(x => x.ClickCount, x => x.ResolveUsing(l => l.Clicks.Count));
        }
    }
}
