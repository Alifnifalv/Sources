using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierMasterTypeMap : EntityTypeConfiguration<SupplierMasterType>
    {
        public SupplierMasterTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierMasterTypeID);

            // Properties
            this.Property(t => t.SupplierText)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.SupplierType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("SupplierMasterType");
            this.Property(t => t.SupplierMasterTypeID).HasColumnName("SupplierMasterTypeID");
            this.Property(t => t.SupplierText).HasColumnName("SupplierText");
            this.Property(t => t.SupplierType).HasColumnName("SupplierType");
        }
    }
}
