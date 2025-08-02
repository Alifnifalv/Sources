using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ModuleMasterErpRightMap : EntityTypeConfiguration<ModuleMasterErpRight>
    {
        public ModuleMasterErpRightMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleMasterErpRightsID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ModuleMasterErpRights");
            this.Property(t => t.ModuleMasterErpRightsID).HasColumnName("ModuleMasterErpRightsID");
            this.Property(t => t.RefModuleMasterErpID).HasColumnName("RefModuleMasterErpID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");

            // Relationships
            this.HasRequired(t => t.ModuleMasterErp)
                .WithMany(t => t.ModuleMasterErpRights)
                .HasForeignKey(d => d.RefModuleMasterErpID);
            this.HasRequired(t => t.UserMaster)
                .WithMany(t => t.ModuleMasterErpRights)
                .HasForeignKey(d => d.RefUserID);

        }
    }
}
