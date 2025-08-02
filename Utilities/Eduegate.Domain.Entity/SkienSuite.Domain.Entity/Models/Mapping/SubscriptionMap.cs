using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SubscriptionMap : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionMap()
        {
            // Primary Key
            this.HasKey(t => t.SubscriptionIID);

            // Properties
            this.Property(t => t.SubscriptionEmail)
                .HasMaxLength(200);

            this.Property(t => t.VarificationCode)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Subscriptions", "cms");
            this.Property(t => t.SubscriptionIID).HasColumnName("SubscriptionIID");
            this.Property(t => t.SubscriptionEmail).HasColumnName("SubscriptionEmail");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.VarificationCode).HasColumnName("VarificationCode");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
