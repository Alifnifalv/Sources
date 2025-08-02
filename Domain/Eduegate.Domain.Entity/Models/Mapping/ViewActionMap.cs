using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ViewActionMap : EntityTypeConfiguration<ViewAction>
    {
        public ViewActionMap()
        {
            // Primary Key
            this.HasKey(t => t.ViewActionID);

            // Properties
            this.Property(t => t.ViewActionID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ViewID);

            this.Property(t => t.ActionCaption)
                .HasMaxLength(100);

            this.Property(t => t.ActionAttribute)
               .HasMaxLength(1000);

            this.Property(t => t.ActionAttribute2)
              .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("ViewActions", "setting");
            this.Property(t => t.ViewActionID).HasColumnName("ViewActionID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.ActionAttribute).HasColumnName("ActionAttribute");
            this.Property(t => t.ActionAttribute2).HasColumnName("ActionAttribute2");
            this.Property(t => t.ActionCaption).HasColumnName("ActionCaption");

            this.HasOptional(t => t.View)
           .WithMany(t => t.ViewActions)
           .HasForeignKey(d => d.ViewID);
        }
    }
}
