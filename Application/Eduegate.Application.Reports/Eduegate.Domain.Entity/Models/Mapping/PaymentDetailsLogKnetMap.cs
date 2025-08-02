using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentDetailsLogKnetMap : EntityTypeConfiguration<PaymentDetailsLogKnet>
    {
        public PaymentDetailsLogKnetMap()
        {
            // Primary Key
            this.HasKey(t => t.LogID);

            // Properties
            this.Property(t => t.CustomerSessionID)
                .HasMaxLength(50);

            this.Property(t => t.TransResult)
                .HasMaxLength(100);

            this.Property(t => t.TransPostDate)
                .HasMaxLength(50);

            this.Property(t => t.TransAuth)
                .HasMaxLength(50);

            this.Property(t => t.TransRef)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PaymentDetailsLogKnet", "payment");
            this.Property(t => t.LogID).HasColumnName("LogID");
            this.Property(t => t.CustomerSessionID).HasColumnName("CustomerSessionID");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.TransResult).HasColumnName("TransResult");
            this.Property(t => t.TransPostDate).HasColumnName("TransPostDate");
            this.Property(t => t.TransAuth).HasColumnName("TransAuth");
            this.Property(t => t.TransRef).HasColumnName("TransRef");
        }
    }
}
