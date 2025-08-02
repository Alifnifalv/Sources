using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ScreenMetadataMap : EntityTypeConfiguration<ScreenMetadata>
    {
        public ScreenMetadataMap()
        {
            // Primary Key
            this.HasKey(t => t.ScreenID);

            // Properties
            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.ListActionName)
                .HasMaxLength(50);

            this.Property(t => t.ListButtonDisplayName)
                .HasMaxLength(50);

            this.Property(t => t.ModelNamespace)
                .HasMaxLength(1000);

            this.Property(t => t.ModelViewModel)
                .HasMaxLength(1000);

            this.Property(t => t.MasterViewModel)
                .HasMaxLength(1000);

            this.Property(t => t.DetailViewModel)
                .HasMaxLength(1000);

            this.Property(t => t.SummaryViewModel)
                .HasMaxLength(1000);

            this.Property(t => t.DisplayName)
                .HasMaxLength(50);

            this.Property(t => t.JsControllerName)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("ScreenMetadatas", "setting");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ListActionName).HasColumnName("ListActionName");
            this.Property(t => t.ListButtonDisplayName).HasColumnName("ListButtonDisplayName");
            this.Property(t => t.ModelNamespace).HasColumnName("ModelNamespace");
            this.Property(t => t.ModelViewModel).HasColumnName("ModelViewModel");
            this.Property(t => t.MasterViewModel).HasColumnName("MasterViewModel");
            this.Property(t => t.DetailViewModel).HasColumnName("DetailViewModel");
            this.Property(t => t.SummaryViewModel).HasColumnName("SummaryViewModel");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.JsControllerName).HasColumnName("JsControllerName");
            this.Property(t => t.IsCacheable).HasColumnName("IsCacheable");
            this.Property(t => t.IsSavePanelRequired).HasColumnName("IsSavePanelRequired");

            // Relationships
            this.HasOptional(t => t.View)
                .WithMany(t => t.ScreenMetadatas)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
