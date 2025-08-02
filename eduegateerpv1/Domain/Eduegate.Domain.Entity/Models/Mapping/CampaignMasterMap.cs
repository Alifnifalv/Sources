using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CampaignMasterMap : EntityTypeConfiguration<CampaignMaster>
    {
        public CampaignMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CampaignID);

            // Properties
            this.Property(t => t.Campaign)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("CampaignMaster");
            this.Property(t => t.CampaignID).HasColumnName("CampaignID");
            this.Property(t => t.Campaign).HasColumnName("Campaign");
            this.Property(t => t.VoucherAmount).HasColumnName("VoucherAmount");
            this.Property(t => t.VoucherValidity).HasColumnName("VoucherValidity");
            this.Property(t => t.VoucherValidFrom).HasColumnName("VoucherValidFrom");
            this.Property(t => t.VoucherValidTill).HasColumnName("VoucherValidTill");
        }
    }
}
