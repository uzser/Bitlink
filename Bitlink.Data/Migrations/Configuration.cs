using Bitlink.Entities;
using System;

namespace Bitlink.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BitlinkDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BitlinkDbContext context)
        {
            base.Seed(context);

            var link = new Link
            {
                DateCreated = DateTime.Now,
                Url = "http://bitlink.azurewebsites.net",
                Hash = "28TkwuI"
            };
            var links = new[]
            {
                link,
                new Link
                {
                    DateCreated = DateTime.Now,
                    Url = "https://www.google.com",
                    Hash = "32Tgend"
                },
            };

            var clicks = new[]
            {
                new Click
                {
                    Date = DateTime.Now,
                    Link = link
                },
                new Click
                {
                    Date = DateTime.Now.AddSeconds(50),
                    Link = link
                },
            };

            foreach (var click in clicks)
            {
                link.Clicks.Add(click);
            }

            var user = new User
            {
                Uid = Guid.Parse("00D84ECC-113E-4D5F-86AD-F67F79F708D0"),
                DateCreated = DateTime.Now
            };
            user.Links.Add(link);

            context.Users.AddOrUpdate(x => x.Uid, user);
            context.Links.AddOrUpdate(x => x.Hash, links);
        }
    }
}
