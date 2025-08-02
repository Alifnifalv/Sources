using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentMasterVisaMap : EntityTypeConfiguration<PaymentMasterVisa>
    {
        public PaymentMasterVisaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TrackIID });

            // Properties
            this.Property(t => t.TrackIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TrackKey);

            this.Property(t => t.InitStatus)
                .HasMaxLength(1);

            this.Property(t => t.InitIP)
                .HasMaxLength(50);

            this.Property(t => t.InitLocation)
                .HasMaxLength(100);

            this.Property(t => t.VpcVersion)
                .HasMaxLength(50);

            this.Property(t => t.VpcCommand)
                .HasMaxLength(50);

            this.Property(t => t.AccessCode)
                .HasMaxLength(50);

            this.Property(t => t.MerchantID)
                .HasMaxLength(50);

            this.Property(t => t.VpcLocale)
                .HasMaxLength(50);

            this.Property(t => t.PaymentCurrency)
                .HasMaxLength(50);

            this.Property(t => t.ResponseCode)
                .HasMaxLength(50);

            this.Property(t => t.CodeDescription)
                .HasMaxLength(255);

            this.Property(t => t.Message)
                .HasMaxLength(255);

            this.Property(t => t.ReceiptNumber)
                .HasMaxLength(50);

            this.Property(t => t.TransID)
                .HasMaxLength(50);

            this.Property(t => t.AcquireResponseCode)
                .HasMaxLength(50);

            this.Property(t => t.BankAuthorizationID)
                .HasMaxLength(50);

            this.Property(t => t.BatchNo)
                .HasMaxLength(50);

            this.Property(t => t.CardType)
                .HasMaxLength(20);

            this.Property(t => t.ResponseIP)
                .HasMaxLength(50);

            this.Property(t => t.ResponseLocation)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PaymentMasterVisa", "payment");
            this.Property(t => t.TrackIID).HasColumnName("TrackIID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.InitOn).HasColumnName("InitOn");
            this.Property(t => t.InitStatus).HasColumnName("InitStatus");
            this.Property(t => t.InitIP).HasColumnName("InitIP");
            this.Property(t => t.InitLocation).HasColumnName("InitLocation");
            this.Property(t => t.VpcURL).HasColumnName("VpcURL");
            this.Property(t => t.VpcVersion).HasColumnName("VpcVersion");
            this.Property(t => t.VpcCommand).HasColumnName("VpcCommand");
            this.Property(t => t.AccessCode).HasColumnName("AccessCode");
            this.Property(t => t.MerchantID).HasColumnName("MerchantID");
            this.Property(t => t.VpcLocale).HasColumnName("VpcLocale");
            this.Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            this.Property(t => t.VirtualAmount).HasColumnName("VirtualAmount");
            this.Property(t => t.PaymentCurrency).HasColumnName("PaymentCurrency");
            this.Property(t => t.ResponseOn).HasColumnName("ResponseOn");
            this.Property(t => t.ResponseCode).HasColumnName("ResponseCode");
            this.Property(t => t.CodeDescription).HasColumnName("CodeDescription");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.ReceiptNumber).HasColumnName("ReceiptNumber");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.AcquireResponseCode).HasColumnName("AcquireResponseCode");
            this.Property(t => t.BankAuthorizationID).HasColumnName("BankAuthorizationID");
            this.Property(t => t.BatchNo).HasColumnName("BatchNo");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.ResponseAmount).HasColumnName("ResponseAmount");
            this.Property(t => t.ResponseIP).HasColumnName("ResponseIP");
            this.Property(t => t.ResponseLocation).HasColumnName("ResponseLocation");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.Response).HasColumnName("Response");
            this.Property(t => t.LogType).HasColumnName("LogType");
        }
    }
}