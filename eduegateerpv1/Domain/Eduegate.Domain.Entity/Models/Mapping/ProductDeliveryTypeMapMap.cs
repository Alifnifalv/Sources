using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDeliveryTypeMapMap : EntityTypeConfiguration<ProductDeliveryTypeMap>
    {
        public ProductDeliveryTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDeliveryTypeMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductDeliveryTypeMaps", "inventory");
            this.Property(t => t.ProductDeliveryTypeMapIID).HasColumnName("ProductDeliveryTypeMapIID");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.DeliveryCharge).HasColumnName("DeliveryCharge");
            this.Property(t => t.DeliveryChargePercentage).HasColumnName("DeliveryChargePercentage");
            this.Property(t => t.DeliveryDays).HasColumnName("DeliveryDays");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");

            // Relationships
            this.HasOptional(t => t.Product)
                .WithMany(t => t.ProductDeliveryTypeMaps)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductDeliveryTypeMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
            this.HasOptional(t => t.DeliveryTypes1)
                .WithMany(t => t.ProductDeliveryTypeMaps)
                .HasForeignKey(d => d.DeliveryTypeID); 
            this.HasOptional(t => t.Branch)
               .WithMany(t => t.ProductDeliveryTypeMaps)
               .HasForeignKey(d => d.BranchID);
        }
    }
}
