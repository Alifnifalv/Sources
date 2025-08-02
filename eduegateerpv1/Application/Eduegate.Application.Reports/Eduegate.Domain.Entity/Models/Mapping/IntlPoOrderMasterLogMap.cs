using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoOrderMasterLogMap : EntityTypeConfiguration<IntlPoOrderMasterLog>
    {
        public IntlPoOrderMasterLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoOrderMasterLogID);

            // Properties
            this.Property(t => t.Status)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoOrderMasterLog");
            this.Property(t => t.IntlPoOrderMasterLogID).HasColumnName("IntlPoOrderMasterLogID");
            this.Property(t => t.RefIntlPoOrderMasterID).HasColumnName("RefIntlPoOrderMasterID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");

            // Relationships
            this.HasRequired(t => t.IntlPoOrderMaster)
                .WithMany(t => t.IntlPoOrderMasterLogs)
                .HasForeignKey(d => d.RefIntlPoOrderMasterID);

        }
    }
}
