using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoGrnLogMap : EntityTypeConfiguration<BlinkPoGrnLog>
    {
        public BlinkPoGrnLogMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoGrnLogID);

            // Properties
            this.Property(t => t.GrnStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BlinkPoGrnLog");
            this.Property(t => t.BlinkPoGrnLogID).HasColumnName("BlinkPoGrnLogID");
            this.Property(t => t.RefBlinkPoGrnMasterID).HasColumnName("RefBlinkPoGrnMasterID");
            this.Property(t => t.GrnStatus).HasColumnName("GrnStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");

            // Relationships
            this.HasRequired(t => t.BlinkPoGrnMaster)
                .WithMany(t => t.BlinkPoGrnLogs)
                .HasForeignKey(d => d.RefBlinkPoGrnMasterID);

        }
    }
}
