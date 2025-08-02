using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCategoryColumnFilterMap : EntityTypeConfiguration<vwCategoryColumnFilter>
    {
        public vwCategoryColumnFilterMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ColumnCode, t.ColumnNameEn, t.ColumnType, t.DataType, t.DisplayType, t.Position, t.RefCategoryID, t.RefColumnID, t.CategoryColumnID, t.ColumnNameAr });

            // Properties
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

            this.Property(t => t.FilterType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RefCategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ColumnNameAr)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("vwCategoryColumnFilters");
            this.Property(t => t.ColumnCode).HasColumnName("ColumnCode");
            this.Property(t => t.ColumnNameEn).HasColumnName("ColumnNameEn");
            this.Property(t => t.ColumnType).HasColumnName("ColumnType");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.DisplayType).HasColumnName("DisplayType");
            this.Property(t => t.FilterType).HasColumnName("FilterType");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.RefCategoryID).HasColumnName("RefCategoryID");
            this.Property(t => t.RefColumnID).HasColumnName("RefColumnID");
            this.Property(t => t.CategoryColumnID).HasColumnName("CategoryColumnID");
            this.Property(t => t.ColumnNameAr).HasColumnName("ColumnNameAr");
        }
    }
}
