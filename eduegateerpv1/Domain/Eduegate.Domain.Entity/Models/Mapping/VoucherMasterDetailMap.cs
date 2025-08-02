using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherMasterDetailMap : EntityTypeConfiguration<VoucherMasterDetail>
    {
        public VoucherMasterDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherMasterDetailsID);

            // Properties
            this.Property(t => t.Remarks)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("VoucherMasterDetails");
            this.Property(t => t.VoucherMasterDetailsID).HasColumnName("VoucherMasterDetailsID");
            this.Property(t => t.RefVoucherID).HasColumnName("RefVoucherID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.UserMaster)
                .WithMany(t => t.VoucherMasterDetails)
                .HasForeignKey(d => d.RefUserID);
            this.HasRequired(t => t.VoucherMaster)
                .WithMany(t => t.VoucherMasterDetails)
                .HasForeignKey(d => d.RefVoucherID);

        }
    }
}
