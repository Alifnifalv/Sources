using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoRequestActionMap : EntityTypeConfiguration<IntlPoRequestAction>
    {
        public IntlPoRequestActionMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoRequestActionID);

            // Properties
            this.Property(t => t.RequestAction)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IntlPoRequestAction");
            this.Property(t => t.IntlPoRequestActionID).HasColumnName("IntlPoRequestActionID");
            this.Property(t => t.RequestAction).HasColumnName("RequestAction");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
