using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketReasonMap : EntityTypeConfiguration<TicketReason>
    {
        public TicketReasonMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketReasonID);

            // Properties
            this.Property(t => t.TicketReasonID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TicketReasonName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TicketReasons", "cs");
            this.Property(t => t.TicketReasonID).HasColumnName("TicketReasonID");
            this.Property(t => t.TicketReasonName).HasColumnName("TicketReasonName");
        }
    }
}
