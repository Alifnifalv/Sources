using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class InventoryTypeMap : EntityTypeConfiguration<InventoryType>
    {
        public InventoryTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.InventoryTypeID);

            // Properties
            this.Property(t => t.InventoryTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.InventoryTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("InventoryTypes", "inventory");
            this.Property(t => t.InventoryTypeID).HasColumnName("InventoryTypeID");
            this.Property(t => t.InventoryTypeName).HasColumnName("InventoryTypeName");
        }
    }
}
