using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwCustomerOrderMap : EntityTypeConfiguration<vwCustomerOrder>
    {
        public vwCustomerOrderMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OrderID, t.RefCustomerID, t.BillingFirstName, t.BillingLastName, t.BillingTelephone, t.BillingRefAreaID, t.BillingCountryID, t.OrderDate, t.RefOrderStatusID, t.DeliveryPrefrence, t.DeliveryMethod, t.DeliveryCharges, t.DeliveryChargesCancellation, t.ItemAmount, t.OrderAmount, t.TransID, t.TrackID, t.TrackKey, t.SessionID, t.PaymentID, t.IsVoucherPayment, t.CustomerFirstName, t.CustomerLastName, t.EmailID, t.Status, t.BillCountry, t.VoucherAmount, t.MultiPrice, t.RouteID, t.OrderLang, t.OrderSizeID });

            // Properties
            this.Property(t => t.OrderID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RefCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.Block)
                .HasMaxLength(50);

            this.Property(t => t.Street)
                .HasMaxLength(50);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .HasMaxLength(50);

            this.Property(t => t.Mobile)
                .HasMaxLength(50);

            this.Property(t => t.OtherDetails)
                .HasMaxLength(300);

            this.Property(t => t.BillingFirstName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BillingLastName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BillingBlock)
                .HasMaxLength(50);

            this.Property(t => t.BillingStreet)
                .HasMaxLength(50);

            this.Property(t => t.BillingBuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.BillingTelephone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BillingRefAreaID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BillingCountryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryCharges)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryChargesCancellation)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CancelledBy)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CancellationRemarks)
                .HasMaxLength(300);

            this.Property(t => t.InvoiceNo)
                .HasMaxLength(30);

            this.Property(t => t.PaymentRemarks)
                .HasMaxLength(200);

            this.Property(t => t.DeliveryDetails)
                .HasMaxLength(200);

            this.Property(t => t.ItemAmount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderAmount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OrderIP)
                .HasMaxLength(50);

            this.Property(t => t.OrderCountry)
                .HasMaxLength(50);

            this.Property(t => t.TransID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TrackID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TrackKey)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PaymentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransRef)
                .HasMaxLength(50);

            this.Property(t => t.ReceiptNumber)
                .HasMaxLength(50);

            this.Property(t => t.DispatchRemarks)
                .HasMaxLength(300);

            this.Property(t => t.DeliveryRemarks)
                .HasMaxLength(300);

            this.Property(t => t.ReturnedBy)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ReturnRemarks)
                .HasMaxLength(300);

            this.Property(t => t.OrderClosedBy)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CustomerFirstName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CustomerLastName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.EmailID)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CountryNameEn)
                .HasMaxLength(255);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AreaNameEn)
                .HasMaxLength(255);

            this.Property(t => t.BillCountry)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BillArea)
                .HasMaxLength(255);

            this.Property(t => t.VoucherAmount)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DeliveryDriver)
                .HasMaxLength(20);

            this.Property(t => t.OrderLang)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.OrderSizeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwCustomerOrder");
            this.Property(t => t.OrderID).HasColumnName("OrderID");
            this.Property(t => t.RefCustomerID).HasColumnName("RefCustomerID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.RefAreaID).HasColumnName("RefAreaID");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.OtherDetails).HasColumnName("OtherDetails");
            this.Property(t => t.BillingFirstName).HasColumnName("BillingFirstName");
            this.Property(t => t.BillingLastName).HasColumnName("BillingLastName");
            this.Property(t => t.BillingBlock).HasColumnName("BillingBlock");
            this.Property(t => t.BillingStreet).HasColumnName("BillingStreet");
            this.Property(t => t.BillingBuildingNo).HasColumnName("BillingBuildingNo");
            this.Property(t => t.BillingTelephone).HasColumnName("BillingTelephone");
            this.Property(t => t.BillingRefAreaID).HasColumnName("BillingRefAreaID");
            this.Property(t => t.BillingCountryID).HasColumnName("BillingCountryID");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.RefOrderStatusID).HasColumnName("RefOrderStatusID");
            this.Property(t => t.DeliveryPrefrence).HasColumnName("DeliveryPrefrence");
            this.Property(t => t.DeliveryMethod).HasColumnName("DeliveryMethod");
            this.Property(t => t.DeliveryCharges).HasColumnName("DeliveryCharges");
            this.Property(t => t.DeliveryChargesCancellation).HasColumnName("DeliveryChargesCancellation");
            this.Property(t => t.SpecificDeliveryDate).HasColumnName("SpecificDeliveryDate");
            this.Property(t => t.Cancelled).HasColumnName("Cancelled");
            this.Property(t => t.CancelledBy).HasColumnName("CancelledBy");
            this.Property(t => t.CancelledOn).HasColumnName("CancelledOn");
            this.Property(t => t.CancellationRemarks).HasColumnName("CancellationRemarks");
            this.Property(t => t.CancellRefundAmount).HasColumnName("CancellRefundAmount");
            this.Property(t => t.InvoiceNo).HasColumnName("InvoiceNo");
            this.Property(t => t.InvoiceDate).HasColumnName("InvoiceDate");
            this.Property(t => t.DeliveryDate).HasColumnName("DeliveryDate");
            this.Property(t => t.PaymentMethod).HasColumnName("PaymentMethod");
            this.Property(t => t.PaymentRemarks).HasColumnName("PaymentRemarks");
            this.Property(t => t.DeliveryDetails).HasColumnName("DeliveryDetails");
            this.Property(t => t.AdditionalCharges).HasColumnName("AdditionalCharges");
            this.Property(t => t.PromotionalDiscount).HasColumnName("PromotionalDiscount");
            this.Property(t => t.ItemAmount).HasColumnName("ItemAmount");
            this.Property(t => t.OrderAmount).HasColumnName("OrderAmount");
            this.Property(t => t.OrderIP).HasColumnName("OrderIP");
            this.Property(t => t.OrderCountry).HasColumnName("OrderCountry");
            this.Property(t => t.TransID).HasColumnName("TransID");
            this.Property(t => t.TrackID).HasColumnName("TrackID");
            this.Property(t => t.TrackKey).HasColumnName("TrackKey");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.PaymentID).HasColumnName("PaymentID");
            this.Property(t => t.TransRef).HasColumnName("TransRef");
            this.Property(t => t.TransOn).HasColumnName("TransOn");
            this.Property(t => t.ReceiptNumber).HasColumnName("ReceiptNumber");
            this.Property(t => t.DispatchDate).HasColumnName("DispatchDate");
            this.Property(t => t.DispatchRemarks).HasColumnName("DispatchRemarks");
            this.Property(t => t.DeliveryRemarks).HasColumnName("DeliveryRemarks");
            this.Property(t => t.Returned).HasColumnName("Returned");
            this.Property(t => t.ReturnedBy).HasColumnName("ReturnedBy");
            this.Property(t => t.ReturnedOn).HasColumnName("ReturnedOn");
            this.Property(t => t.ReturnedApprovedOn).HasColumnName("ReturnedApprovedOn");
            this.Property(t => t.ReturnRemarks).HasColumnName("ReturnRemarks");
            this.Property(t => t.ReturnAmount).HasColumnName("ReturnAmount");
            this.Property(t => t.ReturnAmountApprove).HasColumnName("ReturnAmountApprove");
            this.Property(t => t.OrderClosed).HasColumnName("OrderClosed");
            this.Property(t => t.LoyaltyPointAssigned).HasColumnName("LoyaltyPointAssigned");
            this.Property(t => t.LotaltyPoints).HasColumnName("LotaltyPoints");
            this.Property(t => t.OrderClosedBy).HasColumnName("OrderClosedBy");
            this.Property(t => t.FinalTotal).HasColumnName("FinalTotal");
            this.Property(t => t.CategorizationPointAssigned).HasColumnName("CategorizationPointAssigned");
            this.Property(t => t.CategorizationPoints).HasColumnName("CategorizationPoints");
            this.Property(t => t.IsVoucherPayment).HasColumnName("IsVoucherPayment");
            this.Property(t => t.ActualDeliveryCost).HasColumnName("ActualDeliveryCost");
            this.Property(t => t.DeliveryDiscount).HasColumnName("DeliveryDiscount");
            this.Property(t => t.cnActualDeliveryCost).HasColumnName("cnActualDeliveryCost");
            this.Property(t => t.cnDeliveryDiscount).HasColumnName("cnDeliveryDiscount");
            this.Property(t => t.CustomerFirstName).HasColumnName("CustomerFirstName");
            this.Property(t => t.CustomerLastName).HasColumnName("CustomerLastName");
            this.Property(t => t.EmailID).HasColumnName("EmailID");
            this.Property(t => t.CountryNameEn).HasColumnName("CountryNameEn");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AreaNameEn).HasColumnName("AreaNameEn");
            this.Property(t => t.BillCountry).HasColumnName("BillCountry");
            this.Property(t => t.BillArea).HasColumnName("BillArea");
            this.Property(t => t.VoucherAmount).HasColumnName("VoucherAmount");
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.ResendEmail).HasColumnName("ResendEmail");
            this.Property(t => t.DeliveryDriver).HasColumnName("DeliveryDriver");
            this.Property(t => t.RouteID).HasColumnName("RouteID");
            this.Property(t => t.OrderLang).HasColumnName("OrderLang");
            this.Property(t => t.OrderSizeID).HasColumnName("OrderSizeID");
        }
    }
}
