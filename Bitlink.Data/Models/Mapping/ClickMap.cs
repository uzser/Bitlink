using Bitlink.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Bitlink.Data.Models.Mapping
{
    public class ClickMap : EntityTypeConfiguration<Click>
    {
        public ClickMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Relations
            HasRequired(c => c.Link).WithMany(l => l.Clicks);
        }
    }
}
