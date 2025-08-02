using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDigitalRevertLogMap : EntityTypeConfiguration<ProductDigitalRevertLog>
    {
        public ProductDigitalRevertLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDigitalRevertID);

            // Properties
            this.Property(t => t.DigitalKey)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProductDigitalRevertLog");
            this.Property(t => t.ProductDigitalRevertID).HasColumnName("ProductDigitalRevertID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.DigitalKey).HasColumnName("DigitalKey");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ProductDigitalID).HasColumnName("ProductDigitalID");
        }
    }
}
