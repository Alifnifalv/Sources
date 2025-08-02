using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoOrderMasterMap : EntityTypeConfiguration<BlinkPoOrderMaster>
    {
        public BlinkPoOrderMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoOrderMasterID);

            // Properties
            this.Property(t => t.BlinkPoAwbNo)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.BlinkPoOrderStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Details)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("BlinkPoOrderMaster");
            this.Property(t => t.BlinkPoOrderMasterID).HasColumnName("BlinkPoOrderMasterID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefBlinkVendorMasterID).HasColumnName("RefBlinkVendorMasterID");
            this.Property(t => t.BlinkPoOrderDate).HasColumnName("BlinkPoOrderDate");
            this.Property(t => t.BlinkPoAwbNo).HasColumnName("BlinkPoAwbNo");
            this.Property(t => t.BlinkPoOrderStatus).HasColumnName("BlinkPoOrderStatus");
            this.Property(t => t.Details).HasColumnName("Details");

            // Relationships
            this.HasRequired(t => t.BlinkVendorMaster)
                .WithMany(t => t.BlinkPoOrderMasters)
                .HasForeignKey(d => d.RefBlinkVendorMasterID);

        }
    }
}
