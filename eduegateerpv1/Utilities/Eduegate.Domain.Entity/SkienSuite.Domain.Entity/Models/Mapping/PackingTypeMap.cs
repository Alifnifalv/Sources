using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PackingTypeMap : EntityTypeConfiguration<PackingType>
    {
        public PackingTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PackingTypeIID);

            // Properties
            this.Property(t => t.PackingType1)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("PackingTypes", "catalog");
            this.Property(t => t.PackingTypeIID).HasColumnName("PackingTypeIID");
            this.Property(t => t.PackingType1).HasColumnName("PackingType");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PackingCost).HasColumnName("PackingCost");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
