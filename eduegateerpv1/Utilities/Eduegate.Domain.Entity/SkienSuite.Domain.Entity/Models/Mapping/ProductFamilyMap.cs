using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductFamilyMap : EntityTypeConfiguration<ProductFamily>
    {
        public ProductFamilyMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductFamilyIID);

            // Properties
            this.Property(t => t.FamilyName)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductFamilies", "catalog");
            this.Property(t => t.ProductFamilyIID).HasColumnName("ProductFamilyIID");
            this.Property(t => t.FamilyName).HasColumnName("FamilyName");
            this.Property(t => t.ProductFamilyTypeID).HasColumnName("ProductFamilyTypeID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
