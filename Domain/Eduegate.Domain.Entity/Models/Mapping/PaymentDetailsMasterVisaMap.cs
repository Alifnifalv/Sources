using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentDetailsMasterVisaMap : EntityTypeConfiguration<PaymentDetailsMasterVisa>
    {
        public PaymentDetailsMasterVisaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TrackID, t.TrackKey });

            // Properties
            this.Property(t => t.TrackID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TrackKey)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InitStatus)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.InitIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InitLocation)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VpcURL)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VpcVersion)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VpcCommand)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AccessCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MerchantID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.VpcLocale)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PaymentCurrency)
                .IsRequired()
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
                .HasMaxLength(2);

            this.Property(t => t.ResponseIP)
                .HasMaxLength(50);

            this.Property(t => t.ResponseLocation)
                .HasMaxLength(100);

            this.Property(t => t.ReturnUrl)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("PaymentDetailsMasterVisa");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
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
            this.Property(t => t.ReturnUrl).HasColumnName("ReturnUrl");

            // Relationships
            this.HasRequired(t => t.CustomerMaster)
                .WithMany(t => t.PaymentDetailsMasterVisas)
                .HasForeignKey(d => d.RefCustomerID);

        }
    }
}
