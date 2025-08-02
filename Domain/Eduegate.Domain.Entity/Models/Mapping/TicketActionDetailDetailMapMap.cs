using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketActionDetailDetailMapMap : EntityTypeConfiguration<TicketActionDetailDetailMap>
    {
        public TicketActionDetailDetailMapMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketActionDetailDetailMapIID);

            // Properties
            this.Property(t => t.TicketActionDetailDetailMapIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Timestamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("TicketActionDetailDetailMaps", "cs");
            this.Property(t => t.TicketActionDetailDetailMapIID).HasColumnName("TicketActionDetailDetailMapIID");
            this.Property(t => t.TicketActionDetailMapID).HasColumnName("TicketActionDetailMapID");
            this.Property(t => t.Notify).HasColumnName("Notify");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.Timestamps).HasColumnName("Timestamps");

            // Relationships
            this.HasRequired(t => t.TicketActionDetailMap)
                .WithMany(t => t.TicketActionDetailDetailMaps)
                .HasForeignKey(d => d.TicketActionDetailMapID);

        }
    }
}
