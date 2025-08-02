using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class VehicleOwnershipTypeMap : EntityTypeConfiguration<VehicleOwnershipType>
    {
        public VehicleOwnershipTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.VehicleOwnershipTypeID);

            // Properties
            this.Property(t => t.VehicleOwnershipTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OwnershipTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("VehicleOwnershipTypes", "mutual");
            this.Property(t => t.VehicleOwnershipTypeID).HasColumnName("VehicleOwnershipTypeID");
            this.Property(t => t.OwnershipTypeName).HasColumnName("OwnershipTypeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
