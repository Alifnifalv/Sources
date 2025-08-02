using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkPoOrderMasterLogMap : EntityTypeConfiguration<BlinkPoOrderMasterLog>
    {
        public BlinkPoOrderMasterLogMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkPoOrderMasterLogID);

            // Properties
            this.Property(t => t.BlinkPoOrderStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Comment)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BlinkPoOrderMasterLog");
            this.Property(t => t.BlinkPoOrderMasterLogID).HasColumnName("BlinkPoOrderMasterLogID");
            this.Property(t => t.RefBlinkPoOrderMasterID).HasColumnName("RefBlinkPoOrderMasterID");
            this.Property(t => t.BlinkPoOrderStatus).HasColumnName("BlinkPoOrderStatus");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
