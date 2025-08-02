using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DeliveryTypes1Map : EntityTypeConfiguration<DeliveryTypes1>
    {
        public DeliveryTypes1Map()
        {
            // Primary Key
            this.HasKey(t => t.DeliveryTypeID);

            // Properties
            this.Property(t => t.DeliveryTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryTypeName)
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(250);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DeliveryTypes", "inventory");
            this.Property(t => t.DeliveryTypeID).HasColumnName("DeliveryTypeID");
            this.Property(t => t.DeliveryTypeName).HasColumnName("DeliveryTypeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.Days).HasColumnName("Days");

            // Relationships
            this.HasOptional(t => t.DeliveryTypeStatus)
                .WithMany(t => t.DeliveryTypes1)
                .HasForeignKey(d => d.StatusID);
        }
    }
}
