using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ViewFilterMap : EntityTypeConfiguration<ViewFilter>
    {
        public ViewFilterMap()
        {
            // Primary Key
            this.HasKey(t => t.ViewFilterIID);

            // Properties
            this.Property(t => t.PhysicalColumn)
                .HasMaxLength(50);

            this.Property(t => t.ColumnTitle)
                .HasMaxLength(50);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ViewFilters", "setting");
            this.Property(t => t.ViewFilterIID).HasColumnName("ViewFilterIID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.PhysicalColumn).HasColumnName("PhysicalColumn");
            this.Property(t => t.ColumnTitle).HasColumnName("ColumnTitle");
            this.Property(t => t.UIControlTypeID).HasColumnName("UIControlTypeID");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.LookupID).HasColumnName("LookupID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Lookup)
                .WithMany(t => t.ViewFilters)
                .HasForeignKey(d => d.LookupID);
            //this.HasOptional(t => t.UIControlType)
            //    .WithMany(t => t.ViewFilters)
            //    .HasForeignKey(d => d.UIControlTypeID);
            this.HasOptional(t => t.View)
                .WithMany(t => t.ViewFilters)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
