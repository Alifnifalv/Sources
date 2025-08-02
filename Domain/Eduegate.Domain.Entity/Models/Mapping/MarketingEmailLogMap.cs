using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class MarketingEmailLogMap : EntityTypeConfiguration<MarketingEmailLog>
    {
        public MarketingEmailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CampaignName)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MarketingEmailLog");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.CampaignName).HasColumnName("CampaignName");
            this.Property(t => t.OpenedOn).HasColumnName("OpenedOn");
        }
    }
}
