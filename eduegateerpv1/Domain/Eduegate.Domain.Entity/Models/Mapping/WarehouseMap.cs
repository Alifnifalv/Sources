using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            // Primary Key
            this.HasKey(t => t.WarehouseID);

            // Properties
            this.Property(t => t.WarehouseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WarehouseName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Warehouses", "mutual");
            this.Property(t => t.WarehouseID).HasColumnName("WarehouseID");
            this.Property(t => t.WarehouseName).HasColumnName("WarehouseName");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.WarehouseStatuses)
                .WithMany(t => t.Warehouses)
                .HasForeignKey(d => d.StatusID);

        }
    }
}
