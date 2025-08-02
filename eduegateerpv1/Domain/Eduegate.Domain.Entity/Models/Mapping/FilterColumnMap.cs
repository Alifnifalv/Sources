using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class FilterColumnMap : EntityTypeConfiguration<FilterColumn>
    {
        public FilterColumnMap()
        {
            // Primary Key
            this.HasKey(t => t.FilterColumnID);

            // Properties
            this.Property(t => t.FilterColumnID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ColumnCaption)
                .HasMaxLength(50);

            this.Property(t => t.ColumnName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FilterColumns", "setting");
            this.Property(t => t.FilterColumnID).HasColumnName("FilterColumnID");
            this.Property(t => t.SequenceNo).HasColumnName("SequenceNo");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ColumnCaption).HasColumnName("ColumnCaption");
            this.Property(t => t.ColumnName).HasColumnName("ColumnName");
            this.Property(t => t.DataTypeID).HasColumnName("DataTypeID");
            this.Property(t => t.UIControlTypeID).HasColumnName("UIControlTypeID");
            this.Property(t => t.DefaultValues).HasColumnName("DefaultValues");
            this.Property(t => t.IsQuickFilter).HasColumnName("IsQuickFilter");
            this.Property(t => t.LookupID).HasColumnName("LookupID");
            this.Property(t => t.IsLookupLazyLoad).HasColumnName("IsLookupLazyLoad");
            this.Property(t => t.Attribute1).HasColumnName("Attribute1");
            this.Property(t => t.Attribute2).HasColumnName("Attribute2");

            // Relationships
            this.HasOptional(t => t.DataType)
                .WithMany(t => t.FilterColumns)
                .HasForeignKey(d => d.DataTypeID);
            this.HasOptional(t => t.UIControlType)
                .WithMany(t => t.FilterColumns)
                .HasForeignKey(d => d.UIControlTypeID);
            this.HasOptional(t => t.View)
                .WithMany(t => t.FilterColumns)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
