using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ModuleMasterErpMap : EntityTypeConfiguration<ModuleMasterErp>
    {
        public ModuleMasterErpMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleMasterErpID);

            // Properties
            this.Property(t => t.ModuleMasterErpName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModuleMasterErpGroupName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModuleMasterErpPageURL)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ModuleMasterErp");
            this.Property(t => t.ModuleMasterErpID).HasColumnName("ModuleMasterErpID");
            this.Property(t => t.ModuleMasterErpName).HasColumnName("ModuleMasterErpName");
            this.Property(t => t.ModuleMasterErpGroupName).HasColumnName("ModuleMasterErpGroupName");
            this.Property(t => t.ModuleMasterErpPageURL).HasColumnName("ModuleMasterErpPageURL");
        }
    }
}
