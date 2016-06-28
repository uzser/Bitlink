using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Bitlink.Data.Mapping;
using Bitlink.Entities;

namespace Bitlink.Data
{
    public class BitlinkDbContext : DbContext
    {
        static BitlinkDbContext()
        {
            Database.SetInitializer<BitlinkDbContext>(null);
        }

        public BitlinkDbContext()
            : base("Name=BitlinkDbContext")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<Click> Clicks { get; set; }

        public DbSet<Error> Errors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new LinkMap());
            modelBuilder.Configurations.Add(new ClickMap());
            modelBuilder.Configurations.Add(new ErrorMap());
        }
    }
}
