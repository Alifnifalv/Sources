using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupportActionMap : EntityTypeConfiguration<SupportAction>
    {
        public SupportActionMap()
        {
            // Primary Key
            this.HasKey(t => t.SupportActionID);

            // Properties
            this.Property(t => t.ActionName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SupportActions", "cs");
            this.Property(t => t.SupportActionID).HasColumnName("SupportActionID");
            this.Property(t => t.ActionName).HasColumnName("ActionName");
            this.Property(t => t.ActionTypeID).HasColumnName("ActionTypeID");
        }
    }
}
