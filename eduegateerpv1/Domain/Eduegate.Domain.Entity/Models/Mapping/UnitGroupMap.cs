using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UnitGroupMap : EntityTypeConfiguration<UnitGroup>
    {
        public UnitGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitGroupID);

            // Properties
            this.Property(t => t.UnitGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnitGroupCode)
                .HasMaxLength(20);

            this.Property(t => t.UnitGroupName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UnitGroups", "catalog");
            this.Property(t => t.UnitGroupID).HasColumnName("UnitGroupID");
            this.Property(t => t.UnitGroupCode).HasColumnName("UnitGroupCode");
            this.Property(t => t.UnitGroupName).HasColumnName("UnitGroupName");
            this.Property(t => t.Fraction).HasColumnName("Fraction");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
