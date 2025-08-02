using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models
{
    public class PaymentDetailsKnetMap : EntityTypeConfiguration<PaymentDetailsKnet>
    {
        public PaymentDetailsKnetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TrackID, t.TrackKey });

            // Properties
            //this.Property(t => t.TrackID)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //this.Property(t => t.TrackKey)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //this.Property(t => t.SessionID)
            //    .IsRequired()
            //    .HasMaxLength(50);

            this.Property(t => t.InitStatus)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.InitIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InitLocation)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PaymentAction)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.PaymentCurrency)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.PaymentLang)
                .IsRequired()
                .HasMaxLength(3);

            //this.Property(t => t.InitRawResponse)
            //    .HasMaxLength(100);

            this.Property(t => t.InitPaymentPage)
                .IsRequired();
                //.HasMaxLength(100);

            this.Property(t => t.InitErrorMsg)
                .HasMaxLength(255);

            this.Property(t => t.TransResult)
                .HasMaxLength(100);

            this.Property(t => t.TransPostDate)
                .HasMaxLength(50);

            this.Property(t => t.TransAuth)
                .HasMaxLength(50);

            this.Property(t => t.TransRef)
                .HasMaxLength(50);

            this.Property(t => t.TransErrorMsg)
                .HasMaxLength(255);

            this.Property(t => t.TransIP)
                .HasMaxLength(50);

            this.Property(t => t.TransLocation)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PaymentDetailsKnet", "payment");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.InitOn).HasColumnName("InitOn");
            this.Property(t => t.InitStatus).HasColumnName("InitStatus");
            this.Property(t => t.InitIP).HasColumnName("InitIP");
            this.Property(t => t.InitLocation).HasColumnName("InitLocation");
            this.Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            this.Property(t => t.PaymentAction).HasColumnName("PaymentAction");
            this.Property(t => t.PaymentCurrency).HasColumnName("PaymentCurrency");
            this.Property(t => t.PaymentLang).HasColumnName("PaymentLang");
            this.Property(t => t.InitRawResponse).HasColumnName("InitRawResponse");
            this.Property(t => t.InitPaymentPage).HasColumnName("InitPaymentPage");
            this.Property(t => t.InitErrorMsg).HasColumnName("InitErrorMsg");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.TransOn).HasColumnName("TransOn");
            this.Property(t => t.TransResult).HasColumnName("TransResult");
            this.Property(t => t.TransPostDate).HasColumnName("TransPostDate");
            this.Property(t => t.TransAuth).HasColumnName("TransAuth");
            this.Property(t => t.TransRef).HasColumnName("TransRef");
            this.Property(t => t.TransErrorMsg).HasColumnName("TransErrorMsg");
            this.Property(t => t.TransIP).HasColumnName("TransIP");
            this.Property(t => t.TransLocation).HasColumnName("TransLocation");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.AppKey).HasColumnName("AppKey");
        }
    }
}