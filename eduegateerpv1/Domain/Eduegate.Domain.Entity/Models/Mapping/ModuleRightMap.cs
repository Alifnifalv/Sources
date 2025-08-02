using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ModuleRightMap : EntityTypeConfiguration<ModuleRight>
    {
        public ModuleRightMap()
        {
            // Primary Key
            this.HasKey(t => new { t.RefUserID, t.RefModuleID });

            // Properties
            this.Property(t => t.RefUserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefModuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ModuleRights");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.RefModuleID).HasColumnName("RefModuleID");

            // Relationships
            this.HasRequired(t => t.UserMaster)
                .WithMany(t => t.ModuleRights)
                .HasForeignKey(d => d.RefUserID);

        }
    }
}
