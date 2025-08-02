using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ViewColumnMap : EntityTypeConfiguration<ViewColumn>
    {
        public ViewColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.ViewColumnID);

            // Properties
            this.Property(t => t.ViewColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ColumnName)
                .HasMaxLength(50);

            this.Property(t => t.DataType)
                .HasMaxLength(20);

            this.Property(t => t.PhysicalColumnName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ViewColumns", "setting");
            this.Property(t => t.ViewColumnID).HasColumnName("ViewColumnID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ColumnName).HasColumnName("ColumnName");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.PhysicalColumnName).HasColumnName("PhysicalColumnName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.IsSortable).HasColumnName("IsSortable");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.IsExpression).HasColumnName("IsExpression");
            this.Property(t => t.Expression).HasColumnName("Expression");
            this.Property(t => t.FilterValue).HasColumnName("FilterValue");

            // Relationships
            this.HasOptional(t => t.View)
                .WithMany(t => t.ViewColumns)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
