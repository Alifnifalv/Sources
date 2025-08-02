using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDigitalMap : EntityTypeConfiguration<ProductDigital>
    {
        public ProductDigitalMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductDigitalID);

            // Properties
            this.Property(t => t.DigitalKey)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SupplierInvoice)
                .HasMaxLength(20);

            this.Property(t => t.DigitalKeyStatus)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ProductDigital");
            this.Property(t => t.ProductDigitalID).HasColumnName("ProductDigitalID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.DigitalKey).HasColumnName("DigitalKey");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.DigitalKeySold).HasColumnName("DigitalKeySold");
            this.Property(t => t.SupplierInvoice).HasColumnName("SupplierInvoice");
            this.Property(t => t.DigitalKeyStatus).HasColumnName("DigitalKeyStatus");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.ProductDigitals)
            //    .HasForeignKey(d => d.RefProductID);

        }
    }
}
