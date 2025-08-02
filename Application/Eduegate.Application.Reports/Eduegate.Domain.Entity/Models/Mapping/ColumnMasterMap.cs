using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ColumnMasterMap : EntityTypeConfiguration<ColumnMaster>
    {
        public ColumnMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.ColumnID);

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

            this.Property(t => t.DisplayValue)
                .HasMaxLength(1500);

            this.Property(t => t.FilterType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ColumnNameAr)
                .HasMaxLength(50);

            this.Property(t => t.DisplayValueAr)
                .HasMaxLength(1500);

            // Table & Column Mappings
            this.ToTable("ColumnMaster");
            this.Property(t => t.ColumnID).HasColumnName("ColumnID");
            this.Property(t => t.ColumnCode).HasColumnName("ColumnCode");
            this.Property(t => t.ColumnNameEn).HasColumnName("ColumnNameEn");
            this.Property(t => t.ColumnType).HasColumnName("ColumnType");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.DisplayType).HasColumnName("DisplayType");
            this.Property(t => t.DisplayValue).HasColumnName("DisplayValue");
            this.Property(t => t.FilterType).HasColumnName("FilterType");
            this.Property(t => t.ColumnNameAr).HasColumnName("ColumnNameAr");
            this.Property(t => t.DisplayValueAr).HasColumnName("DisplayValueAr");
        }
    }
}
