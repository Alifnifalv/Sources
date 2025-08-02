using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductFamilyTypeMap : EntityTypeConfiguration<ProductFamilyType>
    {
        public ProductFamilyTypeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.FamilyTypeID, t.CultureID });

            // Properties
            this.Property(t => t.FamilyTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FamilyTypeName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductFamilyTypes", "catalog");
            this.Property(t => t.FamilyTypeID).HasColumnName("FamilyTypeID");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.FamilyTypeName).HasColumnName("FamilyTypeName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.ProductFamilyTypes)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
