using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductSerialMapMap : EntityTypeConfiguration<ProductSerialMap>
    {
        public ProductSerialMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSerialIID);

            // Properties
            this.Property(t => t.SerialNo)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ProductSerialMaps", "inventory");
            this.Property(t => t.ProductSerialIID).HasColumnName("ProductSerialIID");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.DetailID).HasColumnName("DetailID");
            this.Property(t => t.ProductSKUMapID).HasColumnName("ProductSKUMapID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            

            // Relationships
            this.HasOptional(t => t.TransactionDetail)
                .WithMany(t => t.ProductSerialMaps)
                .HasForeignKey(d => d.DetailID);

            this.HasOptional(t => t.ProductSKUMap)
                .WithMany(t => t.ProductSerialMaps)
                .HasForeignKey(d => d.ProductSKUMapID);
        }
    }
}
