using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PaymentDetailsTheFortMap : EntityTypeConfiguration<PaymentDetailsTheFort>
    {
        public PaymentDetailsTheFortMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TrackID, t.TrackKey });

            // Properties
            this.Property(t => t.TrackID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TrackKey)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            this.Property(t => t.InitStatus)
                .IsRequired();

            this.Property(t => t.InitIP)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.InitLocation)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PShaRequestPhrase)
                .HasMaxLength(100);

            this.Property(t => t.PAccessCode)
                .HasMaxLength(30);

            this.Property(t => t.PMerchantIdentifier)
                .HasMaxLength(30);

            this.Property(t => t.PCommand)
                .HasMaxLength(10);

            this.Property(t => t.PCurrency)
                .HasMaxLength(3);

            this.Property(t => t.PCustomerEmail)
                .HasMaxLength(255);

            this.Property(t => t.PLang)
                .HasMaxLength(2);

            this.Property(t => t.PMerchantReference)
                .HasMaxLength(100);

            this.Property(t => t.PSignatureText)
                .HasMaxLength(1000);

            this.Property(t => t.PSignature)
                .HasMaxLength(250);

            this.Property(t => t.PTransSignature)
                .HasMaxLength(250);

            this.Property(t => t.TransID)
                .HasMaxLength(50);

            this.Property(t => t.PTransCommand)
                .HasMaxLength(10);

            this.Property(t => t.PTransMerchantReference)
                .HasMaxLength(100);

            this.Property(t => t.PTransAccessCode)
                .HasMaxLength(30);

            this.Property(t => t.PTransMerchantIdentifier)
                .HasMaxLength(30);

            this.Property(t => t.PTransCurrency)
                .HasMaxLength(3);

            this.Property(t => t.PTransPaymentOption)
                .HasMaxLength(50);

            this.Property(t => t.PTransEci)
                .HasMaxLength(20);

            this.Property(t => t.PTransAuthorizationCode)
                .HasMaxLength(10);

            this.Property(t => t.PTransOrderDesc)
                .HasMaxLength(35);

            this.Property(t => t.PTransResponseMessage)
                .HasMaxLength(150);

            this.Property(t => t.PTransCustomerIP)
                .HasMaxLength(50);

            this.Property(t => t.PTransCustomerEmail)
                .HasMaxLength(255);

            this.Property(t => t.PTransCardNumber)
                .HasMaxLength(50);

            this.Property(t => t.PTransCustomerName)
                .HasMaxLength(100);

            this.Property(t => t.TServiceCommand)
                .HasMaxLength(15);

            this.Property(t => t.TAccessCode)
                .HasMaxLength(30);

            this.Property(t => t.TSignatureText)
                .HasMaxLength(1000);

            this.Property(t => t.TSignature)
                .HasMaxLength(250);

            this.Property(t => t.TMerchantReference)
                .HasMaxLength(100);

            this.Property(t => t.TLanguage)
                .HasMaxLength(2);

            this.Property(t => t.TShaRequestPhrase)
                .HasMaxLength(100);

            this.Property(t => t.TMerchantIdentifier)
                .HasMaxLength(30);

            this.Property(t => t.AdditionalDetails)
                .HasMaxLength(100);

            this.Property(t => t.CardHolderName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PaymentDetailsTheFort", "payment");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.CustomerID).HasColumnName("CustomerID");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.InitOn).HasColumnName("InitOn");
            this.Property(t => t.InitStatus).HasColumnName("InitStatus");
            this.Property(t => t.InitIP).HasColumnName("InitIP");
            this.Property(t => t.InitLocation).HasColumnName("InitLocation");
            this.Property(t => t.InitAmount).HasColumnName("InitAmount");
            this.Property(t => t.PShaRequestPhrase).HasColumnName("PShaRequestPhrase");
            this.Property(t => t.PAccessCode).HasColumnName("PAccessCode");
            this.Property(t => t.PMerchantIdentifier).HasColumnName("PMerchantIdentifier");
            this.Property(t => t.PCommand).HasColumnName("PCommand");
            this.Property(t => t.PCurrency).HasColumnName("PCurrency");
            this.Property(t => t.PCustomerEmail).HasColumnName("PCustomerEmail");
            this.Property(t => t.PLang).HasColumnName("PLang");
            this.Property(t => t.PMerchantReference).HasColumnName("PMerchantReference");
            this.Property(t => t.PSignatureText).HasColumnName("PSignatureText");
            this.Property(t => t.PSignature).HasColumnName("PSignature");
            this.Property(t => t.PAmount).HasColumnName("PAmount");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.PTransSignature).HasColumnName("PTransSignature");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.POn).HasColumnName("POn");
            this.Property(t => t.PTransCommand).HasColumnName("PTransCommand");
            this.Property(t => t.PTransMerchantReference).HasColumnName("PTransMerchantReference");
            this.Property(t => t.PTransAmount).HasColumnName("PTransAmount");
            this.Property(t => t.PTransAccessCode).HasColumnName("PTransAccessCode");
            this.Property(t => t.PTransMerchantIdentifier).HasColumnName("PTransMerchantIdentifier");
            this.Property(t => t.PTransCurrency).HasColumnName("PTransCurrency");
            this.Property(t => t.PTransPaymentOption).HasColumnName("PTransPaymentOption");
            this.Property(t => t.PTransEci).HasColumnName("PTransEci");
            this.Property(t => t.PTransAuthorizationCode).HasColumnName("PTransAuthorizationCode");
            this.Property(t => t.PTransOrderDesc).HasColumnName("PTransOrderDesc");
            this.Property(t => t.PTransResponseMessage).HasColumnName("PTransResponseMessage");
            this.Property(t => t.PTransStatus).HasColumnName("PTransStatus");
            this.Property(t => t.PTransResponseCode).HasColumnName("PTransResponseCode");
            this.Property(t => t.PTransCustomerIP).HasColumnName("PTransCustomerIP");
            this.Property(t => t.PTransCustomerEmail).HasColumnName("PTransCustomerEmail");
            this.Property(t => t.PTransExpiryDate).HasColumnName("PTransExpiryDate");
            this.Property(t => t.PTransCardNumber).HasColumnName("PTransCardNumber");
            this.Property(t => t.PTransCustomerName).HasColumnName("PTransCustomerName");
            this.Property(t => t.PTransActualAmount).HasColumnName("PTransActualAmount");
            this.Property(t => t.PTransOn).HasColumnName("PTransOn");
            this.Property(t => t.TransOn).HasColumnName("TransOn");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.TServiceCommand).HasColumnName("TServiceCommand");
            this.Property(t => t.TAccessCode).HasColumnName("TAccessCode");
            this.Property(t => t.TSignatureText).HasColumnName("TSignatureText");
            this.Property(t => t.TSignature).HasColumnName("TSignature");
            this.Property(t => t.TMerchantReference).HasColumnName("TMerchantReference");
            this.Property(t => t.TLanguage).HasColumnName("TLanguage");
            this.Property(t => t.TShaRequestPhrase).HasColumnName("TShaRequestPhrase");
            this.Property(t => t.TAmount).HasColumnName("TAmount");
            this.Property(t => t.TMerchantIdentifier).HasColumnName("TMerchantIdentifier");
            this.Property(t => t.AdditionalDetails).HasColumnName("AdditionalDetails");
            this.Property(t => t.CartID).HasColumnName("CartID");
            this.Property(t => t.CardHolderName).HasColumnName("CardHolderName");

            // Relationships
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.PaymentDetailsTheForts)
                .HasForeignKey(d => d.CustomerID);

        }
    }
}
