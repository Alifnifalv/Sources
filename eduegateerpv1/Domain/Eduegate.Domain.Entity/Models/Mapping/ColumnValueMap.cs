using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ColumnValueMap : EntityTypeConfiguration<ColumnValue>
    {
        public ColumnValueMap()
        {
            // Primary Key
            this.HasKey(t => t.ColumnValuesID);

            // Properties
            this.Property(t => t.DisplayValue)
                .HasMaxLength(1000);

            this.Property(t => t.DisplayValueAr)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ColumnValues");
            this.Property(t => t.ColumnValuesID).HasColumnName("ColumnValuesID");
            this.Property(t => t.RefColumnID).HasColumnName("RefColumnID");
            this.Property(t => t.DisplayValue).HasColumnName("DisplayValue");
            this.Property(t => t.DisplayPosition).HasColumnName("DisplayPosition");
            this.Property(t => t.DisplayValueAr).HasColumnName("DisplayValueAr");

            // Relationships
            this.HasRequired(t => t.ColumnMaster)
                .WithMany(t => t.ColumnValues)
                .HasForeignKey(d => d.RefColumnID);

        }
    }
}
