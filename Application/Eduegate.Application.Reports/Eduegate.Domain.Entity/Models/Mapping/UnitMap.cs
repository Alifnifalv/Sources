using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitID);

            // Properties
            this.Property(t => t.UnitID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnitCode)
                .HasMaxLength(20);

            this.Property(t => t.UnitName)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Units", "catalog");
            this.Property(t => t.UnitID).HasColumnName("UnitID");
            //this.Property(t => t.UnitGroupID).HasColumnName("UnitGroupID");
            this.Property(t => t.UnitCode).HasColumnName("UnitCode");
            this.Property(t => t.UnitName).HasColumnName("UnitName");
            this.Property(t => t.Fraction).HasColumnName("Fraction");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            //this.HasOptional(t => t.UnitGroup)
            //    .WithMany(t => t.Units)
            //    .HasForeignKey(d => d.UnitGroupID);

        }
    }
}
