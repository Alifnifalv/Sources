
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Inventory;

namespace Eduegate.Web.Library.ViewModels
{
    public class OrderDeliveryDetailViewModel : BaseMasterViewModel
    {
        public OrderDeliveryDetailViewModel()
        {
            DeliveryDetails = new DeliveryAddressViewModel();
            BillingDetails = new BillingAddressViewModel();
        }

        public long HeadIID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> ToBranchID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<byte> EntitlementID { get; set; }
        public Nullable<long> ReferenceHeadID { get; set; }
        public Nullable<long> JobEntryHeadID { get; set; }
        public Nullable<short> DeliveryMethodID { get; set; }
        public Nullable<int> DeliveryDays { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<bool> IsShipment { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<int> DeliveryTypeID { get; set; }
        public Nullable<int> JobStatusID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> TransactionRole { get; set; }
        public string DeliveryType { get; set; }

        public DeliveryAddressViewModel DeliveryDetails { get; set; }

        public BillingAddressViewModel BillingDetails { get; set; }

        // Mappers
        public static OrderDetailDTO ToDTO(OrderDeliveryDetailViewModel vm)
        {
            Mapper<OrderDeliveryDetailViewModel, OrderDetailDTO>.CreateMap();
            Mapper<DeliveryAddressViewModel, OrderContactMapDTO>.CreateMap();
            var mapper = Mapper<OrderDeliveryDetailViewModel, OrderDetailDTO>.Map(vm);
            var order = new OrderDetailDTO();
            order.orderDetails = new List<OrderContactMapDTO>();
            order.HeadIID = vm.HeadIID;
            order.CompanyID = vm.CompanyID;
            order.DocumentTypeID = (int)vm.DocumentTypeID;
            order.TransactionDate = vm.TransactionDate.IsNotNull() ? Convert.ToDateTime(vm.TransactionDate) : DateTime.Now;
            order.CustomerID = vm.CustomerID;
            order.Description = vm.Description;
            order.TransactionNo = vm.TransactionNo;
            order.SupplierID = vm.SupplierID;
            order.TransactionStatusID = vm.TransactionStatusID;
            order.DiscountAmount = vm.DiscountAmount;
            order.DiscountPercentage = vm.DiscountPercentage;
            order.BranchID = vm.BranchID != default(long) ? vm.BranchID : (long?)null;
            order.ToBranchID = vm.ToBranchID != default(long) ? vm.ToBranchID : (long?)null;
            order.DueDate = vm.DueDate;
            order.DeliveryDate = vm.DeliveryDate;
            order.DeliveryDays = vm.DeliveryDays;
            order.CurrencyID = vm.CurrencyID != default(long) && vm.CurrencyID != null ? Convert.ToInt32(vm.CurrencyID) : (int?)null;
            order.DeliveryMethodID = vm.DeliveryMethodID > 0 ? (short)vm.DeliveryMethodID : (short?)null; // we are converting becuase in DB we have short and we are passing DeliveryTypeID
            order.IsShipment = (bool)vm.IsShipment;
            order.EmployeeID = vm.EmployeeID.HasValue ? vm.EmployeeID.Value : default(long) ;
            order.EntitlementID = vm.EntitlementID;
            order.UpdatedBy = vm.UpdatedBy;
            order.UpdatedDate = DateTime.Now;
            order.CreatedDate = vm.CreatedDate;
            order.CreatedBy = vm.CreatedBy;
            order.ReferenceHeadID = vm.ReferenceHeadID.HasValue ? vm.ReferenceHeadID.Value : default(long?) ;
            order.JobEntryHeadID = vm.JobEntryHeadID.HasValue ? vm.JobEntryHeadID.Value : default(long?); ;
            order.DeliveryTypeID = vm.DeliveryTypeID.HasValue ? vm.DeliveryTypeID.Value : default(int?); ;
            order.DeliveryCharge = vm.DeliveryCharge;
            order.JobStatusID = vm.JobStatusID > 0 ? vm.JobStatusID : null;
            order.DocumentStatusID = vm.DocumentStatusID > 0 ? vm.DocumentStatusID : null;
            order.TransactionRole = vm.TransactionRole > 0 ? vm.TransactionRole : null;

            if (vm.DeliveryDetails.IsNotNull())
            {
                order.orderDetails.Add(DeliveryAddressViewModel.ToDTO(vm.DeliveryDetails));
            }
            return order;
        }

        public static OrderDeliveryDetailViewModel ToVM(OrderDetailDTO dto)
        {
            Mapper<OrderDetailDTO, OrderDeliveryDetailViewModel>.CreateMap();
            var vm = new OrderDeliveryDetailViewModel();
            vm.HeadIID = dto.HeadIID;
            vm.CompanyID = dto.CompanyID;
            vm.DocumentTypeID = (int)dto.DocumentTypeID;
            vm.TransactionDate = dto.TransactionDate.IsNotNull() ? Convert.ToDateTime(dto.TransactionDate) : DateTime.Now;
            vm.CustomerID = dto.CustomerID;
            vm.Description = dto.Description;
            vm.TransactionNo = dto.TransactionNo;
            vm.SupplierID = dto.SupplierID;
            vm.TransactionStatusID = dto.TransactionStatusID;
            vm.DiscountAmount = dto.DiscountAmount;
            vm.DiscountPercentage = dto.DiscountPercentage;
            vm.BranchID = dto.BranchID != default(long) ? dto.BranchID : (long?)null;
            vm.ToBranchID = dto.ToBranchID != default(long) ? dto.ToBranchID : (long?)null;
            vm.DueDate = dto.DueDate;
            vm.DeliveryDate = dto.DeliveryDate;
            vm.DeliveryDays = dto.DeliveryDays;
            vm.CurrencyID = dto.CurrencyID != default(long) && dto.CurrencyID != null ? Convert.ToInt32(dto.CurrencyID) : (int?)null;
            vm.DeliveryMethodID = dto.DeliveryMethodID > 0 ? (short)dto.DeliveryMethodID : (short?)null; // we are converting becuase in DB we have short and we are passing DeliveryTypeID
            vm.IsShipment = dto.IsShipment;
            vm.EmployeeID = dto.EmployeeID != default(long) ? Convert.ToInt64(dto.EmployeeID) : (long?)null;
            vm.EntitlementID = dto.EntitlementID;
            vm.UpdatedBy = dto.UpdatedBy;
            vm.UpdatedDate = DateTime.Now;
            vm.CreatedDate = dto.CreatedDate;
            vm.CreatedBy = dto.CreatedBy;
            vm.ReferenceHeadID = dto.ReferenceHeadID;
            vm.JobEntryHeadID = dto.JobEntryHeadID;
            vm.DeliveryTypeID = dto.DeliveryTypeID;
            vm.DeliveryType = dto.DeliveryType;
            vm.DeliveryCharge = dto.DeliveryCharge;
            vm.JobStatusID = dto.JobStatusID > 0 ? dto.JobStatusID : null;
            vm.DocumentStatusID = dto.DocumentStatusID > 0 ? dto.DocumentStatusID : null;
            vm.TransactionRole = dto.TransactionRole > 0 ? dto.TransactionRole : null;
            vm.DeliveryDetails = new DeliveryAddressViewModel();
            if (dto.orderDetails.IsNotNull() && dto.orderDetails.Count > 0)
            {
                foreach (OrderContactMapDTO ocmDTO in dto.orderDetails)
                {
                    if (ocmDTO.IsShippingAddress == true)
                        vm.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(ocmDTO);

                }
            }
            return vm;
        }
    }
}
