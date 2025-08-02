using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoGrnMasterMap : EntityTypeConfiguration<BlinkPoGrnMaster>
    {
        public BlinkPoGrnMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoGrnMasterID);

            // Properties
            this.Property(t => t.GrnStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("BlinkPoGrnMaster");
            this.Property(t => t.BlinkPoGrnMasterID).HasColumnName("BlinkPoGrnMasterID");
            this.Property(t => t.RefBlinkLocationMasterID).HasColumnName("RefBlinkLocationMasterID");
            this.Property(t => t.GrnDate).HasColumnName("GrnDate");
            this.Property(t => t.GrnStatus).HasColumnName("GrnStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.BlinkLocationMaster)
                .WithMany(t => t.BlinkPoGrnMasters)
                .HasForeignKey(d => d.RefBlinkLocationMasterID);

        }
    }
}
