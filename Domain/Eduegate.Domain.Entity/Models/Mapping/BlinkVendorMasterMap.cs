using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkVendorMasterMap : EntityTypeConfiguration<BlinkVendorMaster>
    {
        public BlinkVendorMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkVendorMasterID);

            // Properties
            this.Property(t => t.BlinkVendorMasterCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.BlinkVendorMasterName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BlinkVendorMaster");
            this.Property(t => t.BlinkVendorMasterID).HasColumnName("BlinkVendorMasterID");
            this.Property(t => t.BlinkVendorMasterCode).HasColumnName("BlinkVendorMasterCode");
            this.Property(t => t.BlinkVendorMasterName).HasColumnName("BlinkVendorMasterName");
            this.Property(t => t.BlinkVendorMasterActive).HasColumnName("BlinkVendorMasterActive");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
