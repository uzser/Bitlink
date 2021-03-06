using Bitlink.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bitlink.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Uid).HasColumnName("Uid");

            HasMany(u => u.Links)
                .WithMany(l => l.Users)
                .Map(ul =>
                    ul.ToTable("UserLink")
                    .MapLeftKey("UserId")
                    .MapRightKey("LinkId"));
        }
    }
}
