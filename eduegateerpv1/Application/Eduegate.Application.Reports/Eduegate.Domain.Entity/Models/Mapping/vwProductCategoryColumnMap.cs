using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductCategoryColumnMap : EntityTypeConfiguration<vwProductCategoryColumn>
    {
        public vwProductCategoryColumnMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CategoryColumnID, t.RefCategoryID, t.RefColumnID, t.ProductDetailsID, t.RefProductID, t.RefCategoryColumnID });

            // Properties
            this.Property(t => t.CategoryColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductDetailsID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCategoryColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryColumnValue)
                .HasMaxLength(255);

            this.Property(t => t.CategoryColumnValueAr)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("vwProductCategoryColumns");
            this.Property(t => t.CategoryColumnID).HasColumnName("CategoryColumnID");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefColumnID).HasColumnName("RefColumnID");
            this.Property(t => t.ProductDetailsID).HasColumnName("ProductDetailsID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.RefCategoryColumnID).HasColumnName("RefCategoryColumnID");
            this.Property(t => t.CategoryColumnValue).HasColumnName("CategoryColumnValue");
            this.Property(t => t.OrderSeq).HasColumnName("OrderSeq");
            this.Property(t => t.CategoryColumnValueRange).HasColumnName("CategoryColumnValueRange");
            this.Property(t => t.CategoryColumnValueAr).HasColumnName("CategoryColumnValueAr");
            this.Property(t => t.CategoryColumnValueRangeAr).HasColumnName("CategoryColumnValueRangeAr");
        }
    }
}
