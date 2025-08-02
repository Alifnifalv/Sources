using System.ComponentModel.DataAnnotations.Schema;
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

            this.Property(t => t.PhysicalSchemaName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Views", "setting");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ViewName).HasColumnName("ViewName");
            this.Property(t => t.ViewTitle).HasColumnName("ViewTitle");
            this.Property(t => t.ViewFullPath).HasColumnName("ViewFullPath");
            this.Property(t => t.PhysicalSchemaName).HasColumnName("PhysicalSchemaName");

            this.Property(t => t.HasChild).HasColumnName("HasChild");
            this.Property(t => t.ChildViewID).HasColumnName("ChildViewID");
            this.Property(t => t.ChildFilterField).HasColumnName("ChildFilterField");

            this.Property(t => t.IsRowClickForMultiSelect).HasColumnName("IsRowClickForMultiSelect");

            this.Property(t => t.ControllerName).HasColumnName("ControllerName");
            this.Property(t => t.IsMasterDetail).HasColumnName("IsMasterDetail");
            this.Property(t => t.IsEditable).HasColumnName("IsEditable");
            this.Property(t => t.IsGenericCRUDSave).HasColumnName("IsGenericCRUDSave");
            this.Property(t => t.IsReloadSummarySmartViewAlways).HasColumnName("IsReloadSummarySmartViewAlways");
            this.Property(t => t.JsControllerName).HasColumnName("JsControllerName");
        }
    }
}
