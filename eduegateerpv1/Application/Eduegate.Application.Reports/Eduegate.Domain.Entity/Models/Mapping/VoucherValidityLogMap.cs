using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VoucherValidityLogMap : EntityTypeConfiguration<VoucherValidityLog>
    {
        public VoucherValidityLogMap()
        {
            // Primary Key
            this.HasKey(t => t.VoucherValidityLogID);

            // Properties
            this.Property(t => t.Remarks)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("VoucherValidityLog");
            this.Property(t => t.VoucherValidityLogID).HasColumnName("VoucherValidityLogID");
            this.Property(t => t.RefVoucherID).HasColumnName("RefVoucherID");
            this.Property(t => t.ValidDays).HasColumnName("ValidDays");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
