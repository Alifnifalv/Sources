using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMasterMap : EntityTypeConfiguration<OrderMaster>
    {
        public OrderMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderID);

            // Properties
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

            this.Property(t => t.OrderIP)
                .HasMaxLength(50);

            this.Property(t => t.OrderCountry)
                .HasMaxLength(50);

            this.Property(t => t.TransID)
                .HasMaxLength(30);

            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(50);

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

            this.Property(t => t.PartialDeliveryRemarks)
                .HasMaxLength(300);

            this.Property(t => t.Floor)
                .HasMaxLength(20);

            this.Property(t => t.Flat)
                .HasMaxLength(20);

            this.Property(t => t.BillingFloor)
                .HasMaxLength(20);

            this.Property(t => t.BillingFlat)
                .HasMaxLength(20);

            this.Property(t => t.DeliveryDriver)
                .HasMaxLength(20);

            this.Property(t => t.OrderLang)
                .HasMaxLength(2);

            this.Property(t => t.Jadda)
                .HasMaxLength(10);

            this.Property(t => t.Telephone2)
                .HasMaxLength(20);

            this.Property(t => t.BillingJadda)
                .HasMaxLength(10);

            this.Property(t => t.BillingTelephone2)
                .HasMaxLength(20);

            this.Property(t => t.RefIntlCity)
                .HasMaxLength(35);

            this.Property(t => t.RefIntlArea)
                .HasMaxLength(50);

            this.Property(t => t.OrderType)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("OrderMaster");
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
            this.Property(t => t.MultiPrice).HasColumnName("MultiPrice");
            this.Property(t => t.PartialDeliveryDate).HasColumnName("PartialDeliveryDate");
            this.Property(t => t.PartialDeliveryRemarks).HasColumnName("PartialDeliveryRemarks");
            this.Property(t => t.ExpressDeliveryCharges).HasColumnName("ExpressDeliveryCharges");
            this.Property(t => t.NextDayDeliveryCharges).HasColumnName("NextDayDeliveryCharges");
            this.Property(t => t.ExpressDeliveryDiscount).HasColumnName("ExpressDeliveryDiscount");
            this.Property(t => t.NextDayDeliveryDiscount).HasColumnName("NextDayDeliveryDiscount");
            this.Property(t => t.cnExpressDeliveryCharges).HasColumnName("cnExpressDeliveryCharges");
            this.Property(t => t.cnNextDayDeliveryCharges).HasColumnName("cnNextDayDeliveryCharges");
            this.Property(t => t.cnExpressDeliveryDiscount).HasColumnName("cnExpressDeliveryDiscount");
            this.Property(t => t.cnNextDayDeliveryDiscount).HasColumnName("cnNextDayDeliveryDiscount");
            this.Property(t => t.cnActualNextdayDelivery).HasColumnName("cnActualNextdayDelivery");
            this.Property(t => t.cnActualExpressDeliveryCharge).HasColumnName("cnActualExpressDeliveryCharge");
            this.Property(t => t.ResendEmail).HasColumnName("ResendEmail");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Flat).HasColumnName("Flat");
            this.Property(t => t.BillingFloor).HasColumnName("BillingFloor");
            this.Property(t => t.BillingFlat).HasColumnName("BillingFlat");
            this.Property(t => t.DeliveryDriver).HasColumnName("DeliveryDriver");
            this.Property(t => t.OrderLang).HasColumnName("OrderLang");
            this.Property(t => t.Jadda).HasColumnName("Jadda");
            this.Property(t => t.Telephone2).HasColumnName("Telephone2");
            this.Property(t => t.BillingJadda).HasColumnName("BillingJadda");
            this.Property(t => t.BillingTelephone2).HasColumnName("BillingTelephone2");
            this.Property(t => t.RefIntlCity).HasColumnName("RefIntlCity");
            this.Property(t => t.RefIntlArea).HasColumnName("RefIntlArea");
            this.Property(t => t.OrderType).HasColumnName("OrderType");

            // Relationships
            this.HasRequired(t => t.OrderStatusMaster)
                .WithMany(t => t.OrderMasters)
                .HasForeignKey(d => d.RefOrderStatusID);

        }
    }
}
