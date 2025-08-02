using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentModeMap : EntityTypeConfiguration<PaymentMode>
    {
        public PaymentModeMap()
        {
            // Primary Key
            this.HasKey(t => t.PaymentModeID);

            // Properties
            this.Property(t => t.PaymentModeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PaymentModeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PaymentModes", "account");
            this.Property(t => t.PaymentModeID).HasColumnName("PaymentModeID");
            this.Property(t => t.PaymentModeName).HasColumnName("PaymentModeName");
            this.Property(t => t.AccountId).HasColumnName("AccountId");
        }
    }
}
