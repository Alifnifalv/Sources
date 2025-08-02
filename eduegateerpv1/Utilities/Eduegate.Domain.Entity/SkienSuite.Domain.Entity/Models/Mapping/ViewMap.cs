using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ViewMap : EntityTypeConfiguration<View>
    {
        public ViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ViewID);

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ViewName)
                .HasMaxLength(50);

            this.Property(t => t.ViewFullPath)
                .HasMaxLength(1000);

            this.Property(t => t.PhysicalSchemaName)
                .HasMaxLength(50);

            this.Property(t => t.ChildFilterField)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Views", "setting");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ViewTypeID).HasColumnName("ViewTypeID");
            this.Property(t => t.ViewName).HasColumnName("ViewName");
            this.Property(t => t.ViewFullPath).HasColumnName("ViewFullPath");
            this.Property(t => t.IsMultiLine).HasColumnName("IsMultiLine");
            this.Property(t => t.IsRowCategory).HasColumnName("IsRowCategory");
            this.Property(t => t.PhysicalSchemaName).HasColumnName("PhysicalSchemaName");
            this.Property(t => t.HasChild).HasColumnName("HasChild");
            this.Property(t => t.IsRowClickForMultiSelect).HasColumnName("IsRowClickForMultiSelect");
            this.Property(t => t.ChildViewID).HasColumnName("ChildViewID");
            this.Property(t => t.ChildFilterField).HasColumnName("ChildFilterField");

            // Relationships
            this.HasOptional(t => t.View1)
                .WithMany(t => t.Views1)
                .HasForeignKey(d => d.ChildViewID);
            this.HasOptional(t => t.ViewType)
                .WithMany(t => t.Views)
                .HasForeignKey(d => d.ViewTypeID);

        }
    }
}
