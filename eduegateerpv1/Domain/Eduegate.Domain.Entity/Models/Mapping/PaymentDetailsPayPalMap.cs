using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentDetailsPayPalMap : EntityTypeConfiguration<PaymentDetailsPayPal>
    {
        public PaymentDetailsPayPalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TrackID, t.TrackKey });

            // Properties
            this.Property(t => t.TrackID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TrackKey)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BusinessEmail)
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

            this.Property(t => t.InitCurrency)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.TransID)
                .HasMaxLength(50);

            this.Property(t => t.TransCurrency)
                .HasMaxLength(3);

            this.Property(t => t.TransStatus)
                .HasMaxLength(20);

            this.Property(t => t.TransPayerID)
                .HasMaxLength(20);

            this.Property(t => t.TransDateTime)
                .HasMaxLength(30);

            this.Property(t => t.TransPayerStatus)
                .HasMaxLength(20);

            this.Property(t => t.TransPayerEmail)
                .HasMaxLength(30);

            this.Property(t => t.TransPaymentType)
                .HasMaxLength(10);

            this.Property(t => t.TransMessage)
                .HasMaxLength(30);

            this.Property(t => t.TransReason)
                .HasMaxLength(50);

            this.Property(t => t.TransNoOfCart)
                .HasMaxLength(5);

            this.Property(t => t.TransAddressStatus)
                .HasMaxLength(20);

            this.Property(t => t.TransAddressCountryCode)
                .HasMaxLength(10);

            this.Property(t => t.TransAddressZip)
                .HasMaxLength(10);

            this.Property(t => t.TransAddressName)
                .HasMaxLength(30);

            this.Property(t => t.TransAddressStreet)
                .HasMaxLength(30);

            this.Property(t => t.TransAddressCountry)
                .HasMaxLength(20);

            this.Property(t => t.TransAddressCity)
                .HasMaxLength(20);

            this.Property(t => t.TransAddressState)
                .HasMaxLength(20);

            this.Property(t => t.TransResidenceCountry)
                .HasMaxLength(20);

            this.Property(t => t.IpnHandlerTransID)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PaymentDetailsPayPal", "payment");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.BusinessEmail).HasColumnName("BusinessEmail");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.InitOn).HasColumnName("InitOn");
            this.Property(t => t.InitStatus).HasColumnName("InitStatus");
            this.Property(t => t.InitIP).HasColumnName("InitIP");
            this.Property(t => t.InitLocation).HasColumnName("InitLocation");
            this.Property(t => t.InitAmount).HasColumnName("InitAmount");
            this.Property(t => t.InitCurrency).HasColumnName("InitCurrency");
            this.Property(t => t.IpnVerified).HasColumnName("IpnVerified");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.TransAmount).HasColumnName("TransAmount");
            this.Property(t => t.TransCurrency).HasColumnName("TransCurrency");
            this.Property(t => t.TransStatus).HasColumnName("TransStatus");
            this.Property(t => t.TransPayerID).HasColumnName("TransPayerID");
            this.Property(t => t.TransDateTime).HasColumnName("TransDateTime");
            this.Property(t => t.TransPayerStatus).HasColumnName("TransPayerStatus");
            this.Property(t => t.TransPayerEmail).HasColumnName("TransPayerEmail");
            this.Property(t => t.TransPaymentType).HasColumnName("TransPaymentType");
            this.Property(t => t.TransMessage).HasColumnName("TransMessage");
            this.Property(t => t.TransOn).HasColumnName("TransOn");
            this.Property(t => t.TransReason).HasColumnName("TransReason");
            this.Property(t => t.TransNoOfCart).HasColumnName("TransNoOfCart");
            this.Property(t => t.TransAddressStatus).HasColumnName("TransAddressStatus");
            this.Property(t => t.TransAddressCountryCode).HasColumnName("TransAddressCountryCode");
            this.Property(t => t.TransAddressZip).HasColumnName("TransAddressZip");
            this.Property(t => t.TransAddressName).HasColumnName("TransAddressName");
            this.Property(t => t.TransAddressStreet).HasColumnName("TransAddressStreet");
            this.Property(t => t.TransAddressCountry).HasColumnName("TransAddressCountry");
            this.Property(t => t.TransAddressCity).HasColumnName("TransAddressCity");
            this.Property(t => t.TransAddressState).HasColumnName("TransAddressState");
            this.Property(t => t.TransResidenceCountry).HasColumnName("TransResidenceCountry");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.IpnHandlerVerified).HasColumnName("IpnHandlerVerified");
            this.Property(t => t.IpnHandlerTransID).HasColumnName("IpnHandlerTransID");
            this.Property(t => t.IpnHandlerUpdatedOn).HasColumnName("IpnHandlerUpdatedOn");
            this.Property(t => t.ExRateUSD).HasColumnName("ExRateUSD");
            this.Property(t => t.InitAmountUSDActual).HasColumnName("InitAmountUSDActual");
            this.Property(t => t.InitAmountUSD).HasColumnName("InitAmountUSD");
            this.Property(t => t.IpnVerificationRequired).HasColumnName("IpnVerificationRequired");
            this.Property(t => t.InitCartTotalUSD).HasColumnName("InitCartTotalUSD");
            this.Property(t => t.TransAmountActual).HasColumnName("TransAmountActual");
            this.Property(t => t.TransAmountFee).HasColumnName("TransAmountFee");
            this.Property(t => t.TransExchRateKWD).HasColumnName("TransExchRateKWD");
            this.Property(t => t.TransAmountActualKWD).HasColumnName("TransAmountActualKWD");
            this.Property(t => t.TransOn2).HasColumnName("TransOn2");
            this.Property(t => t.PaypalDataTransferData).HasColumnName("PaypalDataTransferData");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.PaymentDetailsPayPals)
                .HasForeignKey(d => d.RefCustomerID);

        }
    }
}
