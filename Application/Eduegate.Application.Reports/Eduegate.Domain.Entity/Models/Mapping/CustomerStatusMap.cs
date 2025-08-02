using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CustomerStatusMap : EntityTypeConfiguration<CustomerStatus>
    {
        public CustomerStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerStatusID);

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CustomerStatuses", "mutual");
            this.Property(t => t.CustomerStatusID).HasColumnName("CustomerStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
