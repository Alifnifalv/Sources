using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductMasterArabicLogMap : EntityTypeConfiguration<ProductMasterArabicLog>
    {
        public ProductMasterArabicLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductMasterArabicLog1);

            // Properties
            this.Property(t => t.LogKey)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProductMasterArabicLog");
            this.Property(t => t.ProductMasterArabicLog1).HasColumnName("ProductMasterArabicLog");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.LogKey).HasColumnName("LogKey");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.Batch).HasColumnName("Batch");
        }
    }
}
