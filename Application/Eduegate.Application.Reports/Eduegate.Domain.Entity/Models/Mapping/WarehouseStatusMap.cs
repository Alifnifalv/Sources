using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class WarehouseStatusMap : EntityTypeConfiguration<WarehouseStatus>
    {
        public WarehouseStatusMap() 
        {
            // Primary Key
            this.HasKey(t => t.WarehouseStatusID); 

            // Properties
            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings 
            this.ToTable("WarehouseStatuses", "mutual");
            this.Property(t => t.WarehouseStatusID).HasColumnName("WarehouseStatusID");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
        }
    }
}
