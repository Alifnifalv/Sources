using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductTypeDeliveryTypeMapMap : EntityTypeConfiguration<ProductTypeDeliveryTypeMap>
    {
        public ProductTypeDeliveryTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductTypeDeliveryTypeMapIID);

            // Properties
            this.Property(t => t.ProductTypeDeliveryTypeMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductTypeDeliveryTypeMaps", "inventory");
            this.Property(t => t.ProductTypeDeliveryTypeMapIID).HasColumnName("ProductTypeDeliveryTypeMapIID");
            this.Property(t => t.ProductTypeID).HasColumnName("ProductTypeID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ProductType)
                .WithMany(t => t.ProductTypeDeliveryTypeMaps)
                .HasForeignKey(d => d.ProductTypeID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.ProductTypeDeliveryTypeMaps)
                .HasForeignKey(d => d.DeliveryTypeID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.ProductTypeDeliveryTypeMaps)
                .HasForeignKey(d => d.CompanyID);

        }
    }
}
