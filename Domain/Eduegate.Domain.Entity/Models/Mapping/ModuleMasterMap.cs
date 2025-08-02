using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ModuleMasterMap : EntityTypeConfiguration<ModuleMaster>
    {
        public ModuleMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleID);

            // Properties
            this.Property(t => t.ModuleName)
                .HasMaxLength(50);

            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            this.Property(t => t.PageURL)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ModuleMaster");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.PageURL).HasColumnName("PageURL");
        }
    }
}
