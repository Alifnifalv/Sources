using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherClaimMap : EntityTypeConfiguration<VoucherClaim>
    {
        public VoucherClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimId);

            // Properties
            // Table & Column Mappings
            this.ToTable("VoucherClaims");
            this.Property(t => t.ClaimId).HasColumnName("ClaimId");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.ClaimDate).HasColumnName("ClaimDate");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.ClaimPoint).HasColumnName("ClaimPoint");

            // Relationships
            this.HasOptional(t => t.CustomerMaster)
                .WithMany(t => t.VoucherClaims)
                .HasForeignKey(d => d.RefCustomerID);
            this.HasOptional(t => t.OrderMaster)
                .WithMany(t => t.VoucherClaims)
                .HasForeignKey(d => d.RefOrderID);

        }
    }
}
