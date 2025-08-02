using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketStatusMap : EntityTypeConfiguration<TicketStatus>
    {
        public TicketStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TicketStatuses", "cs");
            this.Property(t => t.TicketStatusID).HasColumnName("TicketStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
