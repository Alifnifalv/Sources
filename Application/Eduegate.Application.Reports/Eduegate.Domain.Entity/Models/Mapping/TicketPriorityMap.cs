using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketPriorityMap : EntityTypeConfiguration<TicketPriority>
    {
        public TicketPriorityMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketPriorityID);

            // Properties
            this.Property(t => t.PriorityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TicketPriorities", "cs");
            this.Property(t => t.TicketPriorityID).HasColumnName("TicketPriorityID");
            this.Property(t => t.PriorityName).HasColumnName("PriorityName");
        }
    }
}
