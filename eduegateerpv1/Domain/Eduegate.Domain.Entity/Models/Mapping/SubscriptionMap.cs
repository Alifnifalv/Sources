using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SubscriptionMap : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.SubscribeIID);

            // Properties
            this.Property(t => t.SubscribeEmail)
                .HasMaxLength(200);

            this.Property(t => t.VarificationCode)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Subscriptions", "cms");
            this.Property(t => t.SubscribeIID).HasColumnName("SubscriptionIID");
            this.Property(t => t.SubscribeEmail).HasColumnName("SubscriptionEmail");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.VarificationCode).HasColumnName("VarificationCode");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
