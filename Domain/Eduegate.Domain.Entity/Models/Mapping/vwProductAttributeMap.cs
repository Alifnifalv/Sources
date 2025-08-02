using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductAttributeMap : EntityTypeConfiguration<vwProductAttribute>
    {
        public vwProductAttributeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ColumnID, t.ColumnCode, t.ColumnNameEn, t.ColumnType, t.DataType, t.DisplayType, t.RefProductCategoryProductID });

            // Properties
            this.Property(t => t.ColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ColumnCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ColumnNameEn)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ColumnType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DataType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DisplayType)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DisplayValue)
                .HasMaxLength(100);

            this.Property(t => t.RefProductCategoryProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductAttributes");
            this.Property(t => t.ColumnID).HasColumnName("ColumnID");
            this.Property(t => t.ColumnCode).HasColumnName("ColumnCode");
            this.Property(t => t.ColumnNameEn).HasColumnName("ColumnNameEn");
            this.Property(t => t.ColumnType).HasColumnName("ColumnType");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.DisplayType).HasColumnName("DisplayType");
            this.Property(t => t.DisplayValue).HasColumnName("DisplayValue");
            this.Property(t => t.RefProductCategoryProductID).HasColumnName("RefProductCategoryProductID");
        }
    }
}
