using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CampaignCustomerMap : EntityTypeConfiguration<CampaignCustomer>
    {
        public CampaignCustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.RowID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CampaignCustomer");
            this.Property(t => t.RowID).HasColumnName("RowID");
            this.Property(t => t.RefCampaignID).HasColumnName("RefCampaignID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.IsCreated).HasColumnName("IsCreated");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.RefVoucherID).HasColumnName("RefVoucherID");
        }
    }
}
