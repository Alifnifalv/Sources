using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductFamilyCultureDataMap : EntityTypeConfiguration<ProductFamilyCultureData>
    {
        public ProductFamilyCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.ProductFamilyID });

            // Properties
            this.Property(t => t.ProductFamilyID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FamilyName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductFamilyCultureDatas", "catalog");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.ProductFamilyID).HasColumnName("ProductFamilyID");
            this.Property(t => t.FamilyName).HasColumnName("FamilyName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.ProductFamily)
                .WithMany(t => t.ProductFamilyCultureDatas)
                .HasForeignKey(d => d.ProductFamilyID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductFamilyCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
