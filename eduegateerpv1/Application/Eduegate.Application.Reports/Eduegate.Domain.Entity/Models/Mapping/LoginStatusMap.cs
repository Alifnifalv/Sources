using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LoginStatusMap : EntityTypeConfiguration<LoginStatus>
    {
        public LoginStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.LoginStatusID);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LoginStatuses", "admin");
            this.Property(t => t.LoginStatusID).HasColumnName("LoginStatusID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
