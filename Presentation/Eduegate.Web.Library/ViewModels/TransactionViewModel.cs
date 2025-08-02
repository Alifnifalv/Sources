using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels.Inventory;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using System.Globalization;
using Eduegate.Domain;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;
using Eduegate.TransactionEngineCore;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Web.Library.ViewModels
{
    public class TransactionViewModel : BaseMasterViewModel
    {
        public TransactionViewModel()
        {
            this.BillingContact = new ContactsViewModel();
            this.ShippingContact = new ContactsViewModel();
            this.POSSalesReturnTransactionDetails = new List<TransactionDetailViewModel>();
            this.TransactionPaymentMethods = new List<TransactionPaymentMethodViewModel>();
            this.Payment = new PaymentViewModel();
            this.Customer = new CustomerViewModel();
            this.Supplier = new SupplierViewModel();
            this.Payment = new PaymentViewModel();
            TaxDetails = new TaxDetailsViewModel();
            Parameters = new List<KeyValueViewModel>();
        }

        public List<KeyValueViewModel> Parameters { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction ID")]
        public long HeadIID { get; set; }

        public Nullable<long> BranchID { get; set; }

        public Nullable<long> ToBranchID { get; set; }

        public Nullable<int> DocumentTypeID { get; set; }
        public string DocumentName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction No")]
        public string TransactionNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TransactionStatus")]
        [DisplayName("Transaction Status")]
        public string TransactionStatus { get; set; }

        public int? DocumentStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Reason for change")]
        public string DescriptionNew { get; set; }

        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public string TransactionDate { get; set; }

        public List<TransactionDetailViewModel> TransactionDetails { get; set; }
        public List<TransactionPaymentMethodViewModel> TransactionPaymentMethods { get; set; }

        //Extra property from entity which is using in the view
        public string Product { get; set; }
        public CustomerViewModel Customer { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public string SettingCode { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
        public decimal TotalProductDiscount { get; set; }
        public PaymentViewModel Payment { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalQuantity { get; set; }

        public Nullable<decimal> DeliveryCharge { get; set; }
        public Nullable<long> ReferenceHeadID { get; set; }
        public bool IsPIFromPO { get; set; }

        public Nullable<byte> TransactionStatusID
        {
            get
            {
                return string.IsNullOrEmpty(TransactionStatus) ? null : (Nullable<byte>)byte.Parse(TransactionStatus);
            }
            set
            {
                if (string.IsNullOrEmpty(TransactionStatus))
                {
                    TransactionStatus = ((int)Eduegate.Framework.Enums.TransactionStatus.New).ToString();
                }
                else
                {
                    TransactionStatus = ((int)(Framework.Enums.TransactionStatus)value).ToString();
                }
            }
        }

        public string POSStatus
        {
            get { return this.HeadIID == 0 ? null : (this.DocumentStatusID == 1 ? "Draft" : "Completed"); }
        }

        public decimal TransactionLoyaltyPoints { get; set; }

        // Address properties
        public ContactsViewModel ShippingContact { get; set; }
        public ContactsViewModel BillingContact { get; set; }

        public ICollection<TransactionDetailViewModel> POSSalesReturnTransactionDetails { get; set; }
        public TaxDetailsViewModel TaxDetails { get; set; }

        public static TransactionViewModel FromDTO(TransactionHeadDTO dto)
        {
            Mapper<TransactionHeadDTO, TransactionViewModel>.CreateMap();
            Mapper<TransactionDetailDTO, TransactionDetailViewModel>.CreateMap();
            Mapper<Eduegate.Services.Contracts.PaymentGateway.TransactionPaymentMethodDTO, TransactionPaymentMethodViewModel>.CreateMap();
            var tVM = Mapper<TransactionHeadDTO, TransactionViewModel>.Map(dto);

            tVM.TransactionDetails = new List<TransactionDetailViewModel>();

            if (dto.IsNotNull())
            {
                tVM.HeadIID = dto.HeadIID;
                tVM.Description = dto.Description;
                tVM.TransactionNo = dto.TransactionNo;
                tVM.TransactionStatus = dto.TransactionStatusID.ToString();
                tVM.TransactionNo = dto.TransactionNo;
                if (dto.TransactionDetails.IsNotNull() && dto.TransactionDetails.Count > 0)
                {
                    TransactionDetailViewModel tdVM = null;

                    foreach (TransactionDetailDTO tdDTO in dto.TransactionDetails)
                    {
                        tdVM = new TransactionDetailViewModel();
                        tdVM.SKUID = new KeyValueViewModel();

                        tdVM.DetailIID = tdDTO.DetailIID;
                        tdVM.HeadID = tdDTO.HeadID;
                        tdVM.ProductID = tdDTO.ProductID;
                        tdVM.ProductSKUMapID = tdDTO.ProductSKUMapID;
                        tdVM.Quantity = tdDTO.Quantity;
                        tdVM.UnitID = tdDTO.UnitID;
                        tdVM.DiscountPercentage = tdDTO.DiscountPercentage;
                        tdVM.ExchangeRate = tdDTO.ExchangeRate;
                        tdVM.ProductName = tdDTO.ProductName;
                        tdVM.ProductSKU = tdDTO.SKU;
                        tdVM.ImageFile = tdDTO.ImageFile;
                        tdVM.Amount = tdDTO.Amount;
                        tdVM.BarCode = tdDTO.Barcode;

                        tdVM.SKUID.Key = tdDTO.ProductSKUMapID.ToString();
                        tdVM.SKUID.Value = tdDTO.SKU;

                        tVM.TransactionDetails.Add(tdVM);
                    }
                }
            }

            return tVM;
        }

        public static TransactionDTO ToDTO(TransactionViewModel transactionModel)
        {
            var transaction = new TransactionDTO();
            transaction.TransactionHead = new TransactionHeadDTO();
            transaction.TransactionDetails = new List<TransactionDetailDTO>();
            transaction.TransactionHead.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();

            TransactionDetailDTO transactionDetailDTO = null;

            if (transactionModel != null)
            {
                transaction.TransactionHead.HeadIID = transactionModel.HeadIID;
                transaction.TransactionHead.DocumentTypeID = transactionModel.DocumentTypeID;
                transaction.TransactionHead.TransactionDate = transactionModel.TransactionDate != null ? Convert.ToDateTime(transactionModel.TransactionDate) : (DateTime?)null;
                transaction.TransactionHead.CustomerID = transactionModel.CustomerID;

                if (transactionModel.Customer != null && transactionModel.Customer.Customer != null)
                {
                    if (string.IsNullOrEmpty(transactionModel.Customer.Customer.Key) &&
                        !string.IsNullOrEmpty(transactionModel.Customer.Customer.Value))
                    {
                        transactionModel.Customer.CustomerName = transactionModel.Customer.Customer.Value;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(transactionModel.Customer.Customer.Key))
                        {
                            transaction.TransactionHead.CustomerID = long.Parse(transactionModel.Customer.Customer.Key);
                        }
                    }
                }

                transaction.TransactionHead.Description = transactionModel.Description;
                transaction.TransactionHead.SupplierID = transactionModel.SupplierID;

                transaction.TransactionHead.TransactionNo = transactionModel.TransactionNo;
                transaction.TransactionHead.TransactionStatusID = transactionModel.TransactionStatusID;

                transaction.TransactionHead.BranchID = transactionModel.BranchID;
                transaction.TransactionHead.DeliveryCharge = transactionModel.DeliveryCharge;
                transaction.TransactionHead.PaymentMethod = transactionModel.PaymentMethod;
                transaction.TransactionHead.DiscountAmount = Convert.ToInt32(transactionModel.TotalDiscount);
                transaction.TransactionHead.DiscountPercentage = transactionModel.TotalDiscountPercentage;
                transaction.TransactionHead.ReferenceHeadID = transactionModel.ReferenceHeadID;

                if (transactionModel.DocumentStatusID.HasValue)
                {
                    transaction.TransactionHead.DocumentStatusID = transactionModel.DocumentStatusID;
                }

                if (transactionModel.TransactionDetails.IsNotNull())
                {
                    foreach (TransactionDetailViewModel transactionDetail in transactionModel.TransactionDetails)
                    {
                        transactionDetailDTO = new TransactionDetailDTO();

                        transactionDetailDTO.DetailIID = transactionDetail.DetailIID;
                        transactionDetailDTO.HeadID = transactionDetail.HeadID;
                        transactionDetailDTO.ProductID = transactionDetail.ProductID;
                        transactionDetailDTO.ProductSKUMapID = transactionDetail.ProductSKUMapID;

                        transactionDetailDTO.Quantity = transactionDetail.QuantityText;
                        transactionDetailDTO.Amount = transactionDetail.Amount;
                        transactionDetailDTO.UnitPrice = transactionDetail.UnitPrice;
                        transactionDetailDTO.CostPrice = transactionDetail.CostPrice;
                        //transactionDetailDTO.ProductCost = transactionDetail.ProductCost;
                        transactionDetailDTO.UnitID = transactionDetail.UnitID;
                        transactionDetailDTO.DiscountPercentage = transactionDetail.DiscountPercentage;
                        transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                        // transactionDetailDTO.ProductCost = transactionDetail.ProductCost;
                        transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                        transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                        transactionDetailDTO.TaxTemplateID = transactionDetail.TaxTemplate.IsNullOrEmpty() ? (int?)null
                            : int.Parse(transactionDetail.TaxTemplate);
                        transactionDetailDTO.TaxAmount1 = transactionDetail.TaxAmount;
                        transactionDetailDTO.TaxPercentage = transactionDetail.TaxPercentage;
                        transactionDetailDTO.InclusiveTaxAmount = transactionDetail.InclusiveTaxAmount;
                        transactionDetailDTO.ExclusiveTaxAmount = transactionDetail.ExclusiveTaxAmount;
                        transactionDetailDTO.HasTaxInclusive = transactionDetail.HasTaxInclusive;
                        transaction.TransactionDetails.Add(transactionDetailDTO);
                    }
                }

                if (transactionModel.TransactionPaymentMethods != null && transactionModel.TransactionPaymentMethods.Count > 0)
                {
                    if (transaction.TransactionHead.Entitlements == null)
                    {
                        transaction.TransactionHead.Entitlements = new List<KeyValueDTO>();
                    }

                    foreach (TransactionPaymentMethodViewModel payment in transactionModel.TransactionPaymentMethods)
                    {
                        transaction.TransactionHead.Entitlements.Add(new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = payment.PaymentMethodID.ToString(), Value = payment.PaymentMethodName });
                    }

                    if (transaction.TransactionHead.TransactionHeadEntitlementMaps == null)
                    {
                        transaction.TransactionHead.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapDTO>();
                    }

                    foreach (TransactionPaymentMethodViewModel payment in transactionModel.TransactionPaymentMethods)
                    {
                        transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(new TransactionHeadEntitlementMapDTO()
                        {
                            Amount = payment.Amount,
                            EntitlementID = Convert.ToByte(payment.PaymentMethodID),
                            TransactionHeadID = payment.HeadID,
                            EntitlementName = payment.PaymentMethodName,
                            TransactionHeadEntitlementMapID = payment.PaymentMapIID
                        });
                    }
                }

                if (transactionModel.TaxDetails != null)
                {

                    transaction.TransactionHead.TaxDetails = new List<TaxDetailsDTO>();

                    foreach (var tax in transactionModel.TaxDetails.Taxes)
                    {
                        transaction.TransactionHead.TaxDetails.Add(new TaxDetailsDTO()
                        {
                            Amount = tax.ExclusiveTaxAmount,
                            Percentage = tax.TaxPercentage,
                            TaxName = tax.TaxName,
                            TaxID = tax.TaxID,
                            TaxTypeID = tax.TaxTypeID,
                            TaxTemplateID = tax.TaxTemplateID,
                            TaxTemplateItemID = tax.TaxTemplateItemID,
                            HasTaxInclusive = tax.HasTaxInclusive,
                            InclusiveTaxAmount = tax.InclusiveTaxAmount,
                            ExclusiveTaxAmount = tax.ExclusiveTaxAmount,
                        });
                    }
                }
            }

            return transaction;
        }

        public static TransactionViewModel ToVM(TransactionDTO dto)
        {
            var imageHostUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl");
            Mapper<TransactionDTO, TransactionViewModel>.CreateMap();
            Mapper<TransactionHeadDTO, TransactionViewModel>.CreateMap();
            Mapper<TransactionDetailDTO, TransactionDetailViewModel>.CreateMap();
            var vm = Mapper<TransactionDTO, TransactionViewModel>.Map(dto);
            vm.HeadIID = dto.TransactionHead.HeadIID;
            vm.DocumentTypeID = dto.TransactionHead.DocumentTypeID;
            vm.TransactionNo = dto.TransactionHead.TransactionNo;
            vm.TransactionDate = dto.TransactionHead.TransactionDate.Value.ToLongDateString();
            vm.CustomerID = dto.TransactionHead.CustomerID;
            vm.Description = dto.TransactionHead.Description;
            vm.SupplierID = dto.TransactionHead.SupplierID;
            vm.Customer.CustomerName = dto.TransactionHead.CustomerName;

            vm.TransactionNo = dto.TransactionHead.TransactionNo;
            vm.TransactionStatusID = dto.TransactionHead.TransactionStatusID;
            vm.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();

            vm.BranchID = dto.TransactionHead.BranchID;
            vm.DeliveryCharge = dto.TransactionHead.DeliveryCharge;
            vm.PaymentMethod = dto.TransactionHead.PaymentMethod;
            vm.TotalDiscount = Convert.ToInt32(dto.TransactionHead.DiscountAmount);
            vm.TotalDiscountPercentage = dto.TransactionHead.DiscountPercentage.HasValue ? dto.TransactionHead.DiscountPercentage.Value : 0;
            vm.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
            vm.DocumentStatusID = !dto.TransactionHead.DocumentStatusID.HasValue ? (int?)null : int.Parse(dto.TransactionHead.DocumentStatusID.Value.ToString());

            vm.TransactionDetails = new List<TransactionDetailViewModel>();

            foreach (var detail in dto.TransactionDetails)
            {
                vm.TransactionDetails.Add(new TransactionDetailViewModel()
                {
                    DetailIID = detail.DetailIID,
                    HeadID = detail.HeadID,
                    ProductID = detail.ProductID,
                    ProductSKUMapID = detail.ProductSKUMapID,
                    Quantity = detail.Quantity,
                    QuantityText = detail.Quantity.Value,
                    SKUID = new KeyValueViewModel() { Key = detail.ProductSKUMapID.ToString(), Value = detail.SKU },
                    BarCode = detail.Barcode,
                    ProductName = detail.SKU,
                    ProductSKU = detail.SKU,
                    ImageFile = Path.Combine(imageHostUrl, EduegateImageTypes.Products.ToString(),
                    detail.ImageFile.IsNullOrEmpty() ? string.Empty : detail.ImageFile),
                    //TimeStamps = detail.TimeStamps,
                    Amount = detail.Amount,
                    UnitPrice = detail.UnitPrice,
                    CostPrice = detail.CostPrice,
                    UnitID = detail.UnitID,
                    DiscountPercentage = detail.DiscountPercentage,
                    ExchangeRate = detail.ExchangeRate,
                    CreatedBy = detail.CreatedBy,
                    UpdatedBy = detail.UpdatedBy,
                    TaxAmount = detail.TaxAmount1,
                    TaxPercentage = detail.TaxPercentage,
                    TaxTemplateID = detail.TaxTemplateID,
                    TaxTemplate = detail.TaxTemplateID.ToString(),
                    InclusiveTaxAmount = detail.InclusiveTaxAmount,
                    ExclusiveTaxAmount = detail.ExclusiveTaxAmount,
                    HasTaxInclusive = detail.HasTaxInclusive,
                });
            }

            vm.TransactionPaymentMethods = new List<TransactionPaymentMethodViewModel>();

            if (dto.TransactionHead.Entitlements != null)
            {
                foreach (var entitlement in dto.TransactionHead.Entitlements)
                {
                    if (entitlement != null && entitlement.Key.IsNotNullOrEmpty())
                    {
                        vm.TransactionPaymentMethods.Add(new TransactionPaymentMethodViewModel()
                        {
                            PaymentMethodID = short.Parse(entitlement.Key),
                            PaymentMethodName = entitlement.Value
                        });
                    }
                }
            }

            vm.TransactionPaymentMethods = new List<TransactionPaymentMethodViewModel>();

            if (dto.TransactionHead.TransactionHeadEntitlementMaps != null)
            {
                foreach (var entitlement in dto.TransactionHead.TransactionHeadEntitlementMaps)
                {
                    vm.TransactionPaymentMethods.Add(new TransactionPaymentMethodViewModel()
                    {
                        Amount = entitlement.Amount,
                        PaymentMethodID = entitlement.EntitlementID,
                        HeadID = entitlement.TransactionHeadID,
                        PaymentMethodName = entitlement.EntitlementName,
                        PaymentMapIID = entitlement.TransactionHeadEntitlementMapID
                    });
                }
            }

            vm.TaxDetails = new TaxDetailsViewModel() { Taxes = new List<TaxViewModel>() };

            if (dto.TransactionHead.TaxDetails != null)
            {
                foreach (var tax in dto.TransactionHead.TaxDetails)
                {
                    vm.TaxDetails.Taxes.Add(new TaxViewModel()
                    {
                        TaxAmount = tax.Amount,
                        TaxID = tax.TaxID,
                        TaxName = tax.TaxName,
                        TaxPercentage = tax.Percentage,
                        TaxTemplateID = tax.TaxTemplateID,
                        TaxTypeID = tax.TaxTypeID,
                        TaxTemplateItemID = tax.TaxTemplateItemID,
                        HasTaxInclusive = tax.HasTaxInclusive,
                        InclusiveTaxAmount = tax.InclusiveTaxAmount,
                        ExclusiveTaxAmount = tax.ExclusiveTaxAmount
                    });
                }
            }

            return vm;
        }

        public static SalesOrderViewModel FromSalesOrderDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesOrder = new SalesOrderViewModel();
                salesOrder.MasterViewModel = new SalesOrderMasterViewModel();
                //salesOrder.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                ///salesOrder.MasterViewModel.BillingDetails = new BillingAddressViewModel();
                salesOrder.DetailViewModel = new List<SalesOrderDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                salesOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                salesOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesOrder.MasterViewModel.Currency = new KeyValueViewModel();
                salesOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesOrder.MasterViewModel.Customer = new KeyValueViewModel();
                salesOrder.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesOrder.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;
                salesOrder.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                salesOrder.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesOrder.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;

                //salesOrder.MasterViewModel.EntitlementID = (short)dto.TransactionHead.EntitlementID;

                //salesOrder.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID > 0 ? dto.TransactionHead.DeliveryMethodID.Value.ToString() : null;
                    salesOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesOrder.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesOrder.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                salesOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesOrder.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge.HasValue ? dto.TransactionHead.DeliveryCharge.Value : 0;
                salesOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesOrder.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesOrder.MasterViewModel.Student = new KeyValueViewModel();
                salesOrder.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                salesOrder.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                salesOrder.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;
                salesOrder.MasterViewModel.EmailID = dto.TransactionHead.EmailID;
                salesOrder.MasterViewModel.SchoolID = dto.TransactionHead.SchoolID;
                // put delivery option here... express etc
                //if (dto.TransactionHead.DeliveryTypeID > 0)
                //{
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Value = dto.TransactionHead.DeliveryOption;
                //}
                //salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption = dto.TransactionHead.DeliveryTypeID.IsNotNull() ? dto.TransactionHead.DeliveryTypeID.ToString() : null;

                salesOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                // Map TransactionHeadEntitlementMapViewModel
                //if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    salesOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                //        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                //}
                salesOrder.MasterViewModel.SITransactionHeadID = dto.TransactionHead.AgainstReferenceHeadID;

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesOrderDetail = new SalesOrderDetailViewModel();

                        salesOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesOrderDetail.SKUID = new KeyValueViewModel();
                        salesOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesOrderDetail.SKUID.Value = transactionDetail.SKU;
                        salesOrderDetail.Description = transactionDetail.SKU;
                        salesOrderDetail.PartNo = transactionDetail.PartNo;
                        salesOrderDetail.Quantity = transactionDetail.Quantity.Value;
                        salesOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesOrderDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesOrderDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesOrderDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesOrderDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesOrderDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesOrderDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesOrderDetail.ProductCode = transactionDetail.ProductCode;
                        salesOrderDetail.AvailableQuantity = Convert.ToDouble(transactionDetail.AvailableQuantity);
                        salesOrderDetail.UnitDTO = new List<KeyValueViewModel>();

                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesOrderDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesOrderDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesOrderDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }
                        //salesOrderDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture); it takes today's date as default for warranty date
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        salesOrderDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        salesOrderDetail.SKUDetails = null;
                        salesOrder.DetailViewModel.Add(salesOrderDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMaps.IsNotNull() && dto.OrderContactMaps.Count > 0)
                //{
                //    foreach (OrderContactMapDTO ocmDTO in dto.OrderContactMaps)
                //    {
                //        if (ocmDTO.IsShippingAddress == true)
                //            salesOrder.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //        else
                //            salesOrder.MasterViewModel.BillingDetails = BillingAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //    }
                //}

                return salesOrder;
            }
            else
            {
                return new SalesOrderViewModel();
            }
        }

        public static TransactionDTO ToSalesOrderDTO(SalesOrderViewModel vm)
        {
            if (vm != null)
            {
                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }
                if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }
                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }



                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? short.Parse(vm.MasterViewModel.DocumentType.Key) : (int?)null : null;
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption);

                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) ? Convert.ToInt16(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) : (short?)null : (short?)null;

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? long.Parse(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? long.Parse(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus.IsNotNull() && vm.MasterViewModel.DocumentStatus.Key.IsNotNullOrEmpty() ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : default(short) : default(short);

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.IgnoreEntitlementCheck = true;
                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                            //transaction.TransactionHead.DeliveryCharge = transactionDetail.DeliveryCharge.IsNotNull() ? transactionDetail.DeliveryCharge : vm.MasterViewModel.DeliveryCharge.IsNotNull() ? vm.MasterViewModel.DeliveryCharge : (decimal?)null;
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMaps.Add(DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails));
                //}

                //if (vm.MasterViewModel.BillingDetails.IsNotNull())
                //{
                //    if (vm.MasterViewModel.BillingDetails.ContactPerson.IsNull() && vm.MasterViewModel.BillingDetails.BillingAddress.IsNull()
                //        && vm.MasterViewModel.BillingDetails.bMobileNo.IsNull() && vm.MasterViewModel.BillingDetails.LandLineNo.IsNull()
                //        && vm.MasterViewModel.BillingDetails.SpecialInstructions.IsNull() && vm.MasterViewModel.BillingDetails.Area.IsNull())
                //    {
                //        OrderContactMapDTO ocmDTO = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //        ocmDTO.OrderContactMapID = 0;
                //        ocmDTO.IsBillingAddress = true;
                //        ocmDTO.IsShippingAddress = false;

                //        transaction.OrderContactMaps.Add(ocmDTO);
                //    }
                //    else
                //    {
                //        transaction.OrderContactMaps.Add(BillingAddressViewModel.ToDTO(vm.MasterViewModel.BillingDetails));
                //    }
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static SalesInvoiceViewModel FromSalesInvoiceDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesInvoice = new SalesInvoiceViewModel();
                salesInvoice.MasterViewModel = new SalesInvoiceMasterViewModel();
                //salesInvoice.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                salesInvoice.DetailViewModel = new List<SalesInvoiceDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesInvoice.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesInvoice.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesInvoice.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesInvoice.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesInvoice.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesInvoice.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesInvoice.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesInvoice.MasterViewModel.Reference = dto.TransactionHead.Reference;
                salesInvoice.MasterViewModel.Currency = new KeyValueViewModel();
                salesInvoice.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesInvoice.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesInvoice.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                salesInvoice.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesInvoice.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;

                salesInvoice.MasterViewModel.Student = new KeyValueViewModel();
                salesInvoice.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                salesInvoice.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                salesInvoice.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;

                salesInvoice.MasterViewModel.DeliveryMethod = new KeyValueViewModel();
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesInvoice.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    salesInvoice.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                    //salesInvoice.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                }

                //salesInvoice.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                salesInvoice.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesInvoice.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesInvoice.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesInvoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesInvoice.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID;

                salesInvoice.MasterViewModel.SalesMan = new KeyValueViewModel();

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesInvoice.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesInvoice.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                }

                salesInvoice.MasterViewModel.DocumentStatus = new KeyValueViewModel();

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesInvoice.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesInvoice.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                salesInvoice.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                salesInvoice.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesInvoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesInvoice.MasterViewModel.PaidAmount = dto.TransactionHead.PaidAmount;
                salesInvoice.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge > 0 ? dto.TransactionHead.DeliveryCharge.Value : 0;
                salesInvoice.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesInvoice.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesInvoice.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

                salesInvoice.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesInvoice.IsError = dto.TransactionHead.IsError;
                salesInvoice.ErrorCode = dto.TransactionHead.ErrorCode;

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    salesInvoice.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionHead.TaxDetails != null && dto.TransactionHead.TaxDetails.Count > 0)
                {
                    salesInvoice.MasterViewModel.TaxDetails = new TaxDetailsViewModel() { Taxes = dto.TransactionHead.TaxDetails.Select(x => TaxDetailsViewModel.ToVM(x)).ToList() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesInvoiceDetail = new SalesInvoiceDetailViewModel();
                        salesInvoiceDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        salesInvoiceDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesInvoiceDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesInvoiceDetail.SKUID = new KeyValueViewModel();
                        salesInvoiceDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesInvoiceDetail.SKUID.Value = transactionDetail.SKU;
                        salesInvoiceDetail.Description = transactionDetail.SKU;
                        salesInvoiceDetail.PartNo = transactionDetail.PartNo;
                        salesInvoiceDetail.Quantity = transactionDetail.Quantity;
                        //salesInvoiceDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesInvoiceDetail.Amount = transactionDetail.Amount;
                        //salesInvoiceDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesInvoiceDetail.UnitPrice = transactionDetail.UnitPrice;
                        salesInvoiceDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesInvoiceDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesInvoiceDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesInvoiceDetail.CGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount1);
                        salesInvoiceDetail.SGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount2);
                        salesInvoiceDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesInvoiceDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesInvoiceDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesInvoiceDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesInvoiceDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesInvoiceDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesInvoiceDetail.ProductCode = transactionDetail.ProductCode;
                        salesInvoiceDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesInvoiceDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesInvoiceDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesInvoiceDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        salesInvoiceDetail.IsOnEdit = true;

                        if (transactionDetail.WarrantyDate.IsNull())
                        {
                            salesInvoiceDetail.WarrantyDate = Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }

                        salesInvoiceDetail.CostCenterID = transactionDetail.CostCenterID;

                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    salesInvoiceDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}

                        //salesInvoiceDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        salesInvoiceDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        salesInvoiceDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {

                            foreach (var dtoProductSerialMap in transactionDetail.SKUDetails)
                            {
                                var productSerialMap = new ProductSerialMapViewModel();
                                productSerialMap.ProductSerialID = dtoProductSerialMap.ProductSerialID;
                                productSerialMap.SerialNo = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList = new KeyValueViewModel();
                                //productSerialMap.SerialList.Key = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList.Value = dtoProductSerialMap.SerialNo;
                                productSerialMap.ProductSKUMapID = dtoProductSerialMap.ProductSKUMapID;
                                productSerialMap.TimeStamps = dtoProductSerialMap.TimeStamps;
                                salesInvoiceDetail.SKUDetails.Add(productSerialMap);
                            }
                        }
                        else
                            salesInvoiceDetail.SKUDetails.Add(new ProductSerialMapViewModel()
                            {
                                ProductSKUMapID = (long)transactionDetail.ProductSKUMapID,
                                SerialNo = transactionDetail.SerialNumber
                                /*SerialList = new KeyValueViewModel() { Key = transactionDetail.SerialNumber, Value = transactionDetail.SerialNumber }*/
                            });

                        salesInvoice.DetailViewModel.Add(salesInvoiceDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMap.IsNotNull())
                //{
                //    salesInvoice.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                //}

                // Map Delivery Detail
                if (dto.TransactionHead.TaxDetails.IsNotNull())
                {
                    salesInvoice.MasterViewModel.TaxDetails = TaxDetailsViewModel.ToVM(dto.TransactionHead.TaxDetails, null);
                }


                return salesInvoice;
            }
            else
            {
                return new SalesInvoiceViewModel();
            }
        }

        public static TransactionDTO ToSalesInvoiceDTO(SalesInvoiceViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }

                if ((vm.MasterViewModel.Customer == null || vm.MasterViewModel.Customer.Key == null) && (vm.MasterViewModel.Student == null || vm.MasterViewModel.Student.Key == null))
                {
                    throw new Exception("Select Customer or Student!");
                }

                //if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                //{
                //    throw new Exception("Please enter product details");
                //}

                //if (vm.DetailViewModel.FindAll(x => /*(x.Amount == 0) &&*/ !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                //{
                //    throw new Exception("Amount should not be zero for the selected product !");
                //}

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }

                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount) != (vm.DetailViewModel.Sum(x => x.Amount) - (vm.MasterViewModel.Discount ?? 0)))
                {
                    throw new Exception("Payment Amount should be equal to the grand total!");
                }

                if (!string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionNo) && vm.MasterViewModel.ReferenceTransactionNo.Contains("ONL") && (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount) != (vm.MasterViewModel.PaidAmount)))
                {
                    throw new Exception("Amount should match the payment made online!");
                }
                //if (Convert.ToDouble(Math.Round(vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount ?? 0), 3, MidpointRounding.AwayFromZero)) != (Math.Round(vm.DetailViewModel.Sum(y => y.Amount), 3, MidpointRounding.AwayFromZero)))
                //{
                //    throw new Exception(" Payment Amount should be equal to the grand total!");
                //}

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;

                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? Convert.ToInt32(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryMethodID = null;// Convert.ToInt16(vm.MasterViewModel.DeliveryMethod.Key);
                transaction.TransactionHead.DeliveryTypeName = null; //vm.MasterViewModel.DeliveryMethod.Value;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID;
                transaction.TransactionHead.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadID;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.EntitlementID) : (short?)null;

                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;
                            transactionHeadEntitlementMapDTO.ReferenceNo = item.ReferenceNo;
                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.TaxDetails.IsNotNull() && vm.MasterViewModel.TaxDetails.Taxes.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TaxDetails.Taxes)
                //    {
                //        if (item.TaxAmount.HasValue)
                //        {
                //            transaction.TransactionHead.TaxDetails.Add(new Services.Contracts.Inventory.TaxDetailsDTO()
                //            {
                //                TaxID = item.TaxID,
                //                TaxName = item.TaxName,
                //                Amount = item.TaxAmount,
                //                Percentage = item.TaxPercentage,
                //                TaxTemplateID = item.TaxTemplateID,
                //                TaxTemplateItemID = item.TaxTemplateItemID
                //            });
                //        }
                //    }
                //}

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.TaxAmount1 = Convert.ToDecimal(transactionDetail.CGSTAmount);
                            transactionDetailDTO.TaxAmount2 = Convert.ToDecimal(transactionDetail.SGSTAmount);

                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;

                            // transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDetailDTO.CostCenterID = transactionDetail.CostCenterID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate) : (DateTime?)null;

                            if (transactionDetail.SKUDetails != null)
                            {
                                // ProductSerialMapViewModel
                                foreach (var sku in transactionDetail.SKUDetails)
                                {
                                    // if (sku.SerialList.IsNotNull() && sku.SerialList.Value.IsNotNull())
                                    // {
                                    var dtoSKUDetail = new ProductSerialMapDTO();
                                    dtoSKUDetail.SerialNo = sku.SerialNo;
                                    dtoSKUDetail.ProductSKUMapID = sku.ProductSKUMapID;
                                    // add sku detail dto into list
                                    transactionDetailDTO.SKUDetails.Add(dtoSKUDetail);
                                    // }
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transaction.TransactionDetails.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMap = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //}

                if (vm.MasterViewModel.TaxDetails.IsNotNull())
                {
                    transaction.TransactionHead.TaxDetails = TaxDetailsViewModel.ToDTO(vm.MasterViewModel.TaxDetails.Taxes);
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static SalesReturnRequestViewModel FromSalesReturnRequestDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesReturnRequest = new SalesReturnRequestViewModel();
                salesReturnRequest.MasterViewModel = new SalesReturnRequestMasterViewModel();
                salesReturnRequest.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                salesReturnRequest.DetailViewModel = new List<SalesReturnRequestDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesReturnRequest.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesReturnRequest.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesReturnRequest.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesReturnRequest.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesReturnRequest.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesReturnRequest.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesReturnRequest.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesReturnRequest.MasterViewModel.Currency = new KeyValueViewModel();
                salesReturnRequest.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesReturnRequest.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesReturnRequest.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                salesReturnRequest.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesReturnRequest.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;
                //salesReturnRequest.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();

                //            salesReturnRequest.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                salesReturnRequest.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesReturnRequest.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesReturnRequest.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesReturnRequest.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesReturnRequest.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesReturnRequest.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                salesReturnRequest.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesReturnRequest.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    salesReturnRequest.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesReturnRequest.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesReturnRequest.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                }
                if (dto.TransactionHead.StudentID > 0)
                {
                    salesReturnRequest.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                    salesReturnRequest.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                    salesReturnRequest.MasterViewModel.StudentID = dto.TransactionHead.StudentID;
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesReturnRequest.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesReturnRequest.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    salesReturnRequest.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesReturnRequestDetail = new SalesReturnRequestDetailViewModel();

                        salesReturnRequestDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesReturnRequestDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesReturnRequestDetail.SKUID = new KeyValueViewModel();
                        salesReturnRequestDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesReturnRequestDetail.SKUID.Value = transactionDetail.SKU;
                        salesReturnRequestDetail.Description = transactionDetail.SKU;
                        salesReturnRequestDetail.Quantity = transactionDetail.Quantity;
                        salesReturnRequestDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesReturnRequestDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesReturnRequestDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesReturnRequestDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesReturnRequestDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesReturnRequestDetail.PartNo = transactionDetail.PartNo;
                        salesReturnRequestDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesReturnRequestDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesReturnRequestDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesReturnRequestDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesReturnRequestDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesReturnRequestDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesReturnRequestDetail.ProductCode = transactionDetail.ProductCode;
                        salesReturnRequestDetail.ActualQuantity = transactionDetail.Quantity;
                        salesReturnRequestDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesReturnRequestDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesReturnRequestDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesReturnRequestDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        salesReturnRequest.DetailViewModel.Add(salesReturnRequestDetail);
                    }
                }

                // Map Delivery Detail
                if (dto.OrderContactMap.IsNotNull())
                {
                    salesReturnRequest.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                }

                return salesReturnRequest;
            }
            else return new SalesReturnRequestViewModel();
        }

        public static TransactionDTO ToSalesReturnRequestDTO(SalesReturnRequestViewModel vm)
        {
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? Convert.ToDateTime(vm.MasterViewModel.TransactionDate) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);

                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;

                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? Convert.ToDateTime(vm.MasterViewModel.DeliveryDate) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID > 0 ? vm.MasterViewModel.ReferenceTransactionHeaderID : new Nullable<long>();

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;

                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                {
                    transaction.OrderContactMap = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                }


                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static SalesReturnViewModel FromSalesReturnDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesReturn = new SalesReturnViewModel();
                salesReturn.MasterViewModel = new SalesReturnMasterViewModel();
                //salesReturn.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                salesReturn.DetailViewModel = new List<SalesReturnDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesReturn.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesReturn.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesReturn.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesReturn.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesReturn.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesReturn.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesReturn.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesReturn.MasterViewModel.Reference = dto.TransactionHead.Reference;
                salesReturn.MasterViewModel.Currency = new KeyValueViewModel();
                salesReturn.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.HasValue ? dto.TransactionHead.CurrencyID.ToString() : null;
                salesReturn.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesReturn.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                salesReturn.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.HasValue ? dto.TransactionHead.CustomerID.ToString() : null;
                salesReturn.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerID + "-" + dto.TransactionHead.CustomerName;

                salesReturn.MasterViewModel.Student = new KeyValueViewModel();
                salesReturn.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.HasValue ? dto.TransactionHead.StudentID.ToString() : null;
                salesReturn.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                //salesReturn.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();

                //salesReturn.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                salesReturn.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesReturn.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesReturn.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesReturn.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesReturn.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

                salesReturn.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                salesReturn.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesReturn.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesReturn.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesReturn.MasterViewModel.LocalDiscount = dto.TransactionHead.LocalDiscount;
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesReturn.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    salesReturn.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesReturn.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesReturn.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesReturn.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesReturn.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }


                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    salesReturn.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesReturnDetail = new SalesReturnDetailViewModel();

                        salesReturnDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesReturnDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesReturnDetail.SKUID = new KeyValueViewModel();
                        salesReturnDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesReturnDetail.SKUID.Value = transactionDetail.SKU;
                        salesReturnDetail.Description = transactionDetail.SKU;
                        salesReturnDetail.Quantity = transactionDetail.Quantity;
                        salesReturnDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesReturnDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesReturnDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesReturnDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesReturnDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesReturnDetail.PartNo = transactionDetail.PartNo;
                        salesReturnDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesReturnDetail.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                        //salesReturnDetail.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                        salesReturnDetail.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                        salesReturnDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesReturnDetail.CostCenterID = transactionDetail.CostCenterID;
                        salesReturnDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesReturnDetail.UnitDTO = new List<KeyValueViewModel>();
                        salesReturnDetail.ProductCode = transactionDetail.ProductCode;
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesReturnDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesReturnDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesReturnDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        salesReturn.DetailViewModel.Add(salesReturnDetail);
                    }
                }

                // Map Delivery Detail
                if (dto.OrderContactMap.IsNotNull())
                {
                    //salesReturn.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                }

                return salesReturn;
            }
            else
            {
                return new SalesReturnViewModel();
            }
        }

        public static TransactionDTO ToSalesReturnDTO(SalesReturnViewModel vm)
        {
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();
                vm.DetailViewModel = vm.DetailViewModel.Where(x => x.SKUID != null).ToList();
                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }
                if ((vm.MasterViewModel.ReferenceTransactionHeaderID ?? 0) == 0 && vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }
                if ((vm.MasterViewModel.ReferenceTransactionHeaderID ?? 0) == 0 && vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount) != (Convert.ToDecimal(vm.DetailViewModel.Sum(x => x.Amount)) - (vm.MasterViewModel.Discount ?? 0)))
                {
                    throw new Exception("Payment Amount should be equal to the grand total!");
                }

                var mandatoryData = new Domain.Setting.SettingBL(null).GetSettingValue<bool>("SALESRETURNMANDATORYWITHINVOICE", 0, true);

                if (mandatoryData)
                {
                    if (string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionNo))
                    {
                        throw new Exception("Please select invoice for return!");
                    }
                }

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? Convert.ToInt32(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;

                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID > 0 ? vm.MasterViewModel.ReferenceTransactionHeaderID : new Nullable<long>();

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.LocalDiscount = vm.MasterViewModel.LocalDiscount;
                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;

                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDetailDTO.CostCenterID = transactionDetail.CostCenterID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMap = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static TransactionDTO ToDTOFromPurchaseOrderVM(PurchaseOrderViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.ShipmentDetails = new ShipmentDetailDTO();

                //if (vm.MasterViewModel.Branch == null || vm.MasterViewModel.Branch.Key == null)
                //{
                //    throw new Exception("Select Branch!");
                //}

                if (vm.MasterViewModel.DocumentType == null || vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency == null || vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.Supplier == null || vm.MasterViewModel.Supplier.Key == null)
                {
                    throw new Exception("Select Supplier!");
                }

                //if (vm.MasterViewModel.ReceivingMethod == null || vm.MasterViewModel.ReceivingMethod.Key == null)
                //{
                //    throw new Exception("Select Any Receiving Method!");
                //}

                if (vm.DetailViewModel.FindAll(x => x.SKUID != null).Count() <= 0)
                    throw new Exception("Please enter product details!");

                //if (vm.DetailViewModel.Sum(y => y.Amount) <= 0)
                //{
                //    throw new Exception("Amount should not be zero!");
                //}

                //if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                //    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }
                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? short.Parse(vm.MasterViewModel.DocumentType.Key) : (int?)null : null;
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? long.Parse(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? DateTime.ParseExact(vm.MasterViewModel.Validity, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.SupplierID = vm.MasterViewModel.Supplier != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Supplier.Key) ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlement);
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.JobStatusID = Convert.ToInt16(!string.IsNullOrEmpty(vm.MasterViewModel.JobStatus));

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DeliveryTypeName = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Value) ? vm.MasterViewModel.DeliveryMethod.Value.ToString() : null : null;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DocumentStatusName = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Value) ? vm.MasterViewModel.DocumentStatus.Value.ToString() : null : null;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;

                //transaction.TransactionHead.ReceivingMethodID = vm.MasterViewModel.ReceivingMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReceivingMethod.Key) ? short.Parse(vm.MasterViewModel.ReceivingMethod.Key) : (int?)null : null;
                //transaction.TransactionHead.ReceivingMethodName = vm.MasterViewModel.ReceivingMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReceivingMethod.Value) ? vm.MasterViewModel.ReceivingMethod.Value.ToString() : string.Empty : string.Empty;

                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}
                transaction.IgnoreEntitlementCheck = true;

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignAmount = Convert.ToDecimal(transactionDetail.ForeignAmount);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.ExchangeRate = Convert.ToDecimal(transactionDetail.ExchangeRate);
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            //if (vm.MasterViewModel.Allocations.Allocations != null && vm.MasterViewModel.Allocations.Allocations.Count > 0)
                            //{
                            //    QuantityAllocationViewModel allocationToMap = vm.MasterViewModel.Allocations.Allocations.Where(x => x.ProductID == transactionDetailDTO.ProductSKUMapID).FirstOrDefault();
                            //    transactionDetailDTO.TransactionAllocations = new List<TransactionAllocationDTO>();
                            //    if (allocationToMap != null && allocationToMap.BranchIDs != null)
                            //    {
                            //        for (int i = 0; i < allocationToMap.BranchIDs.Count; i++)
                            //        {
                            //            var transactionAllocation = new TransactionAllocationDTO();
                            //            transactionAllocation.TrasactionDetailID = transactionDetail.TransactionDetailID;
                            //            transactionAllocation.BranchID = allocationToMap.BranchIDs[i];
                            //            transactionAllocation.Quantity = allocationToMap.AllocatedQuantity[i];
                            //            transactionDetailDTO.TransactionAllocations.Add(transactionAllocation);
                            //        }
                            //    }
                            //}

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.ShipmentDetails.IsNotNull())
                //{
                //    transaction.ShipmentDetails.TransactionShipmentIID = vm.MasterViewModel.ShipmentDetails.TransactionShipmentIID;
                //    transaction.ShipmentDetails.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //    transaction.ShipmentDetails.SupplierIDFrom = vm.MasterViewModel.ShipmentDetails.SupplierIDFrom;
                //    transaction.ShipmentDetails.SupplierIDTo = vm.MasterViewModel.ShipmentDetails.SupplierIDTo;
                //    transaction.ShipmentDetails.ShipmentReference = vm.MasterViewModel.ShipmentDetails.ShipmentReference;
                //    transaction.ShipmentDetails.FreightCareer = vm.MasterViewModel.ShipmentDetails.FreightCareer;
                //    transaction.ShipmentDetails.ClearanceTypeID = vm.MasterViewModel.ShipmentDetails.ClearanceTypeID;
                //    transaction.ShipmentDetails.AirWayBillNo = vm.MasterViewModel.ShipmentDetails.AirWayBillNo;
                //    transaction.ShipmentDetails.FrieghtCharges = vm.MasterViewModel.ShipmentDetails.FrieghtCharges;
                //    transaction.ShipmentDetails.BrokerCharges = vm.MasterViewModel.ShipmentDetails.BrokerCharges;
                //    transaction.ShipmentDetails.AdditionalCharges = vm.MasterViewModel.ShipmentDetails.AdditionalCharges;
                //    transaction.ShipmentDetails.Weight = vm.MasterViewModel.ShipmentDetails.Weight;
                //    transaction.ShipmentDetails.NoOfBoxes = vm.MasterViewModel.ShipmentDetails.NoOfBoxes;
                //    transaction.ShipmentDetails.BrokerAccount = vm.MasterViewModel.ShipmentDetails.BrokerAccount;
                //    transaction.ShipmentDetails.Remarks = vm.MasterViewModel.ShipmentDetails.Remarks;
                //    transaction.ShipmentDetails.CreatedBy = vm.MasterViewModel.ShipmentDetails.CreatedBy;
                //    transaction.ShipmentDetails.UpdatedBy = vm.MasterViewModel.ShipmentDetails.UpdatedBy;
                //    transaction.ShipmentDetails.CreatedDate = vm.MasterViewModel.ShipmentDetails.CreatedDate;
                //    transaction.ShipmentDetails.UpdatedDate = vm.MasterViewModel.ShipmentDetails.UpdatedDate;
                //    transaction.ShipmentDetails.TimeStamps = vm.MasterViewModel.ShipmentDetails.TimeStamps;
                //}

                return transaction;
            }
            else return new TransactionDTO();
        }

        public static PurchaseOrderViewModel FromDTOToPurchaseOrderVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseOrder = new PurchaseOrderViewModel();
                purchaseOrder.MasterViewModel = new PurchaseOrderMasterViewModel();
                purchaseOrder.DetailViewModel = new List<PurchaseOrderDetailViewModel>();
                //purchaseOrder.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                //purchaseOrder.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                purchaseOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                purchaseOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                //purchaseOrder.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseOrder.MasterViewModel.Remarks = dto.TransactionHead.Reference;
                purchaseOrder.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName.IsNotNull() ? dto.TransactionHead.CurrencyName.ToString() : null;
                purchaseOrder.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseOrder.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //purchaseOrder.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseOrder.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //purchaseOrder.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                purchaseOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseOrder.MasterViewModel.JobStatus = Convert.ToString(dto.TransactionHead.JobStatusID != null ? dto.TransactionHead.JobStatusID : null);
                purchaseOrder.MasterViewModel.InvoiceStatus = dto.TransactionHead.InvoiceStatus.IsNotNull() ? dto.TransactionHead.InvoiceStatus : string.Empty;
                purchaseOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                purchaseOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;
                purchaseOrder.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder;
                purchaseOrder.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                purchaseOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                purchaseOrder.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;
                //purchaseOrder.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements != null && dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;
                //if (dto.TransactionHead.ReceivingMethodID.HasValue)
                //{
                //    purchaseOrder.MasterViewModel.ReceivingMethod = new KeyValueViewModel();
                //    purchaseOrder.MasterViewModel.ReceivingMethod.Key = dto.TransactionHead.ReceivingMethodID.ToString();
                //    purchaseOrder.MasterViewModel.ReceivingMethod.Value = dto.TransactionHead.ReceivingMethodName;
                //}
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    purchaseOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    purchaseOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    purchaseOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    purchaseOrder.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }
                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseOrderDetail = new PurchaseOrderDetailViewModel();

                        purchaseOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseOrderDetail.SKUID = new KeyValueViewModel();
                        purchaseOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseOrderDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseOrderDetail.Description = transactionDetail.SKU;
                        purchaseOrderDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        purchaseOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseOrderDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        //purchaseOrderDetail.SKUDetails = null;
                        purchaseOrderDetail.ProductLength = transactionDetail.ProductLength;
                        purchaseOrderDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        purchaseOrderDetail.PartNo = transactionDetail.PartNo;
                        purchaseOrderDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        purchaseOrderDetail.ForeignRate = transactionDetail.ForeignRate;
                        purchaseOrderDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        purchaseOrderDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        purchaseOrderDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        purchaseOrderDetail.CostCenterID = transactionDetail.CostCenterID;

                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    purchaseOrderDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}
                        purchaseOrderDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        purchaseOrderDetail.ProductCode = transactionDetail.ProductCode;
                        purchaseOrderDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                purchaseOrderDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        purchaseOrderDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                purchaseOrderDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        if (transactionDetail.TransactionAllocations != null && transactionDetail.TransactionAllocations.Count > 0)
                        {
                            var allocation = new QuantityAllocationViewModel();
                            allocation.ProductName = transactionDetail.SKU;
                            allocation.ProductID = (long)transactionDetail.ProductSKUMapID;
                            allocation.Quantity = transactionDetail.Quantity.IsNotNull() ? (decimal)transactionDetail.Quantity : 0;
                            allocation.AllocatedQuantity = new List<decimal>();
                            allocation.BranchIDs = new List<long>();
                            allocation.BranchName = new List<string>();

                            foreach (var transactionDetailAllocation in transactionDetail.TransactionAllocations)
                            {
                                allocation.AllocatedQuantity.Add(transactionDetailAllocation.Quantity != null ? (decimal)transactionDetailAllocation.Quantity : 0);
                                allocation.BranchIDs.Add(transactionDetailAllocation.BranchID);
                            }
                            //purchaseOrder.MasterViewModel.Allocations.Allocations.Add(allocation);
                        }

                        purchaseOrder.DetailViewModel.Add(purchaseOrderDetail);
                    }
                }

                //if (dto.ShipmentDetails.IsNotNull())
                //{
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TransactionShipmentIID = dto.ShipmentDetails.TransactionShipmentIID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TransactionHeadID = dto.TransactionHead.HeadIID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.SupplierIDFrom = dto.ShipmentDetails.SupplierIDFrom;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.SupplierIDTo = dto.ShipmentDetails.SupplierIDTo;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.ShipmentReference = dto.ShipmentDetails.ShipmentReference;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.FreightCareer = dto.ShipmentDetails.FreightCareer;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.ClearanceTypeID = dto.ShipmentDetails.ClearanceTypeID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.AirWayBillNo = dto.ShipmentDetails.AirWayBillNo;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.FrieghtCharges = dto.ShipmentDetails.FrieghtCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.BrokerCharges = dto.ShipmentDetails.BrokerCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.Weight = dto.ShipmentDetails.Weight;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.NoOfBoxes = dto.ShipmentDetails.NoOfBoxes;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.BrokerAccount = dto.ShipmentDetails.BrokerAccount;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.AdditionalCharges = dto.ShipmentDetails.AdditionalCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.Remarks = dto.ShipmentDetails.Remarks;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.CreatedBy = dto.ShipmentDetails.CreatedBy;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.UpdatedBy = dto.ShipmentDetails.UpdatedBy;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.CreatedDate = dto.ShipmentDetails.CreatedDate;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.UpdatedDate = dto.ShipmentDetails.UpdatedDate;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TimeStamps = dto.ShipmentDetails.TimeStamps;
                //}

                return purchaseOrder;
            }
            else return new PurchaseOrderViewModel();
        }

        public static BranchTransferViewModel FromDTOToBranchTransferVM(TransactionDTO dto)
        {

            if (dto.IsNotNull())
            {

                var br = new BranchTransferViewModel();
                br.MasterViewModel = new BranchTransferMasterViewModel();
                br.DetailViewModel = new List<BranchTransferDetailViewModel>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                br.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                br.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                br.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                br.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                br.MasterViewModel.ToBranch = dto.TransactionHead.ToBranchID.ToString();
                br.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                br.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                br.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                br.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                br.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                br.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                br.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                br.MasterViewModel.DocumentStatus = new KeyValueViewModel();
                br.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                br.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                br.MasterViewModel.Reference = dto.TransactionHead.Reference;

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var brDetail = new BranchTransferDetailViewModel();

                        brDetail.TransactionDetailID = transactionDetail.DetailIID;
                        brDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        brDetail.SKUID = new KeyValueViewModel();
                        brDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        brDetail.SKUID.Value = transactionDetail.SKU;
                        brDetail.Description = transactionDetail.SKU;
                        brDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        brDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        brDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        brDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        brDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        brDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };



                        br.DetailViewModel.Add(brDetail);
                    }
                }

                return br;
            }
            else
                return new BranchTransferViewModel();
        }

        public static TransactionDTO ToDTOFromBranchTransferVM(BranchTransferViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm.IsNotNull())
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.TransactionHead = new TransactionHeadDTO();
                transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    if (vm.MasterViewModel.DocumentType == null || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) || string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Value))
                    {
                        throw new Exception("Select Document Type!");
                    }

                    transactionDTO.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                    transactionDTO.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                    transactionDTO.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                    transactionDTO.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                    transactionDTO.TransactionHead.ToBranchID = Convert.ToInt32(vm.MasterViewModel.ToBranch);
                    transactionDTO.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                    transactionDTO.TransactionHead.TransactionDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
                    transactionDTO.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                    transactionDTO.TransactionHead.CreatedBy = vm.CreatedBy;
                    transactionDTO.TransactionHead.UpdatedBy = vm.UpdatedBy;
                    transactionDTO.TransactionHead.ReferenceHeadID = !string.IsNullOrEmpty(vm.MasterViewModel.ReferenceTransactionHeaderID) ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                    transactionDTO.TransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);
                    transactionDTO.TransactionHead.Reference = vm.MasterViewModel.Reference;
                }
                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            //transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);


                            transactionDTO.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transactionDTO.TransactionDetails.Count() == 0)
                {
                    throw new Exception("Select at least one product to initiate a transfer!");
                }

                return transactionDTO;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static TransactionViewModel ToVMReference(TransactionHeadDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            return new TransactionViewModel()
            {
                HeadIID = dto.HeadIID,
                TransactionDate = dto.TransactionDate != null ? Convert.ToDateTime(dto.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                TransactionNo = dto.TransactionNo,
                TransactionLoyaltyPoints = dto.TransactionLoyaltyPoints
            };
        }

        public static List<TransactionViewModel> ToVMReferenceList(List<TransactionHeadDTO> dtoList)
        {
            var list = new List<TransactionViewModel>();
            foreach (var dto in dtoList)
            {
                list.Add(ToVMReference(dto));
            }
            return list;
        }

        #region Branch Transfer Request

        public static BranchTransferRequestViewModel FromDTOToBranchTransferRequestVM(TransactionDTO dto)
        {

            if (dto.IsNotNull())
            {

                var br = new BranchTransferRequestViewModel();
                br.MasterViewModel = new BranchTransferRequestMasterViewModel();
                br.DetailViewModel = new List<BranchTransferRequestDetailViewModel>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                br.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                br.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                br.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                br.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                br.MasterViewModel.ToBranch = dto.TransactionHead.ToBranchID.ToString();
                br.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                br.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                br.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                br.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                br.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                br.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                br.MasterViewModel.DocumentStatus = new KeyValueViewModel();
                br.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                br.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName.ToString();
                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var brDetail = new BranchTransferRequestDetailViewModel();

                        brDetail.TransactionDetailID = transactionDetail.DetailIID;
                        brDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        brDetail.SKUID = new KeyValueViewModel();
                        brDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        brDetail.SKUID.Value = transactionDetail.SKU;
                        brDetail.Description = transactionDetail.SKU;
                        brDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        brDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        brDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);

                        br.DetailViewModel.Add(brDetail);
                    }
                }

                return br;
            }
            else
                return new BranchTransferRequestViewModel();
        }

        public static TransactionDTO ToDTOFromBranchTransferRequestVM(BranchTransferRequestViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm.IsNotNull())
            {
                var transactionDTO = new TransactionDTO();
                transactionDTO.TransactionHead = new TransactionHeadDTO();
                transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    if (vm.MasterViewModel.DocumentType == null || vm.MasterViewModel.DocumentType.Key == null)
                    {
                        throw new Exception("Select Document Type!");
                    }

                    transactionDTO.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                    transactionDTO.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                    transactionDTO.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                    transactionDTO.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                    transactionDTO.TransactionHead.ToBranchID = Convert.ToInt32(vm.MasterViewModel.ToBranch);
                    transactionDTO.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                    transactionDTO.TransactionHead.TransactionDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
                    transactionDTO.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                    transactionDTO.TransactionHead.CreatedBy = vm.CreatedBy;
                    transactionDTO.TransactionHead.UpdatedBy = vm.UpdatedBy;
                    transactionDTO.TransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);
                }
                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDTO.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transactionDTO.TransactionDetails.Count() == 0)
                {
                    throw new Exception("Select at least one product to initiate a transfer request!");
                }

                return transactionDTO;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static TransactionDTO ToDTOFromOrderChangeRequestViewModel(OrderChangeRequestViewModel vm)
        {
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.OrderChangeRequest;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID;
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.BranchID);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? Convert.ToDateTime(vm.MasterViewModel.TransactionDate) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryTypeID);


                //transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) ? Convert.ToInt16(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? long.Parse(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? Convert.ToDateTime(vm.MasterViewModel.DeliveryDate) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus.IsNotNull() && vm.MasterViewModel.DocumentStatus.Key.IsNotNullOrEmpty() ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                //transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;

                transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;



                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {


                            if (transactionDetail.ChildGrid != null && transactionDetail.ChildGrid.Count > 0)
                            {


                                foreach (var child in transactionDetail.ChildGrid)
                                {
                                    var transactionDetailDTO = new TransactionDetailDTO();
                                    transactionDetailDTO.ParentDetailID = transactionDetail.TransactionDetailID;
                                    transactionDetailDTO.Action = child.Action != null ? Convert.ToInt32(child.Action.Key) : (int?)null;

                                    transactionDetailDTO.Remark = child.Remark;
                                    transactionDetailDTO.DetailIID = child.TransactionDetailID;
                                    transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                                    //transactionDetailDTO.ProductID = child.prod;
                                    transactionDetailDTO.ProductSKUMapID = child.SKUID != null ? Convert.ToInt64(child.SKUID.Key) : (long?)null;
                                    transactionDetailDTO.Quantity = Convert.ToDecimal(child.Quantity);
                                    transactionDetailDTO.Amount = Convert.ToDecimal(child.Amount);
                                    transactionDetailDTO.UnitPrice = Convert.ToDecimal(child.UnitPrice);
                                    transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                                    transactionDetailDTO.CreatedBy = child.CreatedBy;
                                    transactionDetailDTO.UpdatedBy = child.UpdatedBy;
                                    transaction.TransactionDetails.Add(transactionDetailDTO);
                                }
                            }

                            //transaction.TransactionHead.DeliveryCharge = transactionDetail.DeliveryCharge.IsNotNull() ? transactionDetail.DeliveryCharge : vm.MasterViewModel.DeliveryCharge.IsNotNull() ? vm.MasterViewModel.DeliveryCharge : (decimal?)null;
                        }
                    }
                }
                if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                {
                    transaction.OrderContactMaps.Add(DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails));
                }
                //if (vm.MasterViewModel.BillingDetails.IsNotNull())
                //{
                //    if (vm.MasterViewModel.BillingDetails.ContactPerson.IsNull() && vm.MasterViewModel.BillingDetails.BillingAddress.IsNull()
                //        && vm.MasterViewModel.BillingDetails.bMobileNo.IsNull() && vm.MasterViewModel.BillingDetails.LandLineNo.IsNull()
                //        && vm.MasterViewModel.BillingDetails.SpecialInstructions.IsNull() && vm.MasterViewModel.BillingDetails.Area.IsNull())
                //    {
                //        OrderContactMapDTO ocmDTO = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //        ocmDTO.OrderContactMapID = 0;
                //        ocmDTO.IsBillingAddress = true;
                //        ocmDTO.IsShippingAddress = false;

                //        transaction.OrderContactMaps.Add(ocmDTO);
                //    }
                //    else
                //    {
                //        transaction.OrderContactMaps.Add(BillingAddressViewModel.ToDTO(vm.MasterViewModel.BillingDetails));
                //    }
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }
        public static OrderChangeRequestViewModel ToOrderChangeRequestViewModel(TransactionDTO parentDTO, TransactionDTO childDTO = null)
        {
            var orderChangeRequestVM = new OrderChangeRequestViewModel();
            orderChangeRequestVM.MasterViewModel = new OrderChangeRequestMasterViewModel();

            //salesReturn.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
            orderChangeRequestVM.DetailViewModel = new List<OrderChangeRequestDetailViewModel>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var headDTO = new TransactionHeadDTO();
            var detailDTO = new List<TransactionDetailDTO>();
            var childGridDTO = new List<TransactionDetailDTO>();

            if (childDTO != null)
            {
                // Create/Edit case
                headDTO = childDTO.TransactionHead;
                detailDTO = parentDTO.TransactionDetails;
                childGridDTO = childDTO.TransactionDetails;
            }
            else
            {
                // Pick case
                headDTO = parentDTO.TransactionHead;
                detailDTO = parentDTO.TransactionDetails;
                childGridDTO = parentDTO.TransactionDetails;
            }


            // bind master view model
            if (headDTO != null)
            {
                orderChangeRequestVM.MasterViewModel.TransactionHeadIID = childDTO != null ? childDTO.TransactionHead.HeadIID : 0;
                orderChangeRequestVM.MasterViewModel.ReferenceTransactionHeaderID = parentDTO.TransactionHead.HeadIID;
                orderChangeRequestVM.MasterViewModel.ReferenceTransactionNo = parentDTO.TransactionHead.TransactionNo;

                //replacementVM.MasterViewModel.DocumentType = childDTO != null ? childDTO.TransactionHead.DocumentTypeName : "Replacement/Exchange/Cancellation";
                //replacementVM.MasterViewModel.DocumentTypeID = childDTO != null ? childDTO.TransactionHead.DocumentTypeID.Value : 793;

                orderChangeRequestVM.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = headDTO.DocumentTypeID.ToString(), Value = headDTO.DocumentTypeName };
                orderChangeRequestVM.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.OrderChangeRequest;

                //orderChangeRequestVM.MasterViewModel.Branch = headDTO.BranchName;
                orderChangeRequestVM.MasterViewModel.BranchID = headDTO.BranchID != null ? (long)headDTO.BranchID : 0;
                orderChangeRequestVM.MasterViewModel.TransactionNo = headDTO.TransactionNo;
                orderChangeRequestVM.MasterViewModel.TransactionDate = headDTO.TransactionDate != null ? Convert.ToDateTime(headDTO.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                orderChangeRequestVM.MasterViewModel.Remarks = headDTO.Description;
                orderChangeRequestVM.MasterViewModel.Currency = new KeyValueViewModel();
                orderChangeRequestVM.MasterViewModel.Currency.Key = headDTO.CurrencyID.HasValue ? headDTO.CurrencyID.ToString() : null;
                orderChangeRequestVM.MasterViewModel.Currency.Value = headDTO.CurrencyName;
                orderChangeRequestVM.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                orderChangeRequestVM.MasterViewModel.Customer.Key = headDTO.CustomerID.HasValue ? headDTO.CustomerID.ToString() : null;
                orderChangeRequestVM.MasterViewModel.Customer.Value = headDTO.CustomerName;
                orderChangeRequestVM.MasterViewModel.CustomerName = headDTO.CustomerName;
                orderChangeRequestVM.MasterViewModel.CustomerID = headDTO.CustomerID != null ? (long)headDTO.CustomerID : 0;
                //salesReturn.MasterViewModel.DeliveryType = headDTO.DeliveryTypeID.ToString();

                //salesReturn.MasterViewModel.Entitlements = headDTO.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(headDTO.Entitlements) : null;

                orderChangeRequestVM.MasterViewModel.DeliveryDate = headDTO.DeliveryDate != null ? Convert.ToDateTime(headDTO.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                orderChangeRequestVM.MasterViewModel.CreatedBy = headDTO.CreatedBy;
                orderChangeRequestVM.MasterViewModel.UpdatedBy = Convert.ToInt32(headDTO.UpdatedBy);
                orderChangeRequestVM.MasterViewModel.IsTransactionCompleted = headDTO.IsTransactionCompleted;

                if (headDTO.DeliveryTypeID > 0)
                {
                    orderChangeRequestVM.MasterViewModel.DeliveryType.Key = headDTO.DeliveryTypeID.ToString();
                    orderChangeRequestVM.MasterViewModel.DeliveryType.Value = headDTO.DeliveryTypeName;
                }

                if (headDTO.EmployeeID > 0)
                {
                    orderChangeRequestVM.MasterViewModel.SalesMan.Key = headDTO.EmployeeID.ToString();
                    orderChangeRequestVM.MasterViewModel.SalesMan.Value = headDTO.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = headDTO.EmployeeID.ToString();
                }

                if (headDTO.DocumentStatusID.HasValue)
                {
                    orderChangeRequestVM.MasterViewModel.DocumentStatus.Key = headDTO.DocumentStatusID.ToString();
                    orderChangeRequestVM.MasterViewModel.DocumentStatus.Value = headDTO.DocumentStatusName;
                }


                // Map TransactionHeadEntitlementMapViewModel
                if (headDTO.TransactionHeadEntitlementMaps != null && headDTO.TransactionHeadEntitlementMaps.Count > 0)
                {
                    orderChangeRequestVM.MasterViewModel.TransactionHeadEntitlementMaps =
                        headDTO.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                // bind detail view model


                if (detailDTO != null && detailDTO.Count > 0)
                {
                    foreach (var transactionDetail in detailDTO)
                    {
                        var orderChangeRequestDetail = new OrderChangeRequestDetailViewModel();

                        orderChangeRequestDetail.TransactionDetailID = transactionDetail.DetailIID;
                        orderChangeRequestDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        orderChangeRequestDetail.SKUID = new KeyValueViewModel();
                        orderChangeRequestDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        orderChangeRequestDetail.SKUID.Value = transactionDetail.SKU;
                        orderChangeRequestDetail.Description = transactionDetail.SKU;
                        orderChangeRequestDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        orderChangeRequestDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        orderChangeRequestDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        orderChangeRequestDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        orderChangeRequestDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        orderChangeRequestDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);


                        // For child grid
                        orderChangeRequestDetail.ChildGrid = new List<OrderChangeRequestDetailChildViewModel>();
                        var child = new OrderChangeRequestDetailChildViewModel();

                        if (childDTO != null)
                        {
                            // Edit-Create
                            var currentChild = childDTO.TransactionDetails.Where(i => i.ParentDetailID == transactionDetail.DetailIID).FirstOrDefault();
                            if (currentChild != null)
                            {
                                child.TransactionDetailID = currentChild.DetailIID;
                                child.ParentDetailID = currentChild.ParentDetailID.Value;
                                if (currentChild.Action != null)

                                    child.Action = new KeyValueViewModel();
                                child.Action.Key = Convert.ToString(currentChild.Action.Value);
                                child.Action.Value = ((Services.Contracts.Enums.ReplacementActions)currentChild.Action.Value).ToString();

                                child.TransactionHead = currentChild.HeadID.Value;
                                child.SKUID = new KeyValueViewModel();
                                child.SKUID.Key = currentChild.ProductSKUMapID.ToString();
                                child.SKUID.Value = currentChild.SKU;
                                child.Description = currentChild.SKU;
                                child.Remark = currentChild.Remark;
                                child.Quantity = Convert.ToDouble(currentChild.Quantity);
                                child.Amount = Convert.ToDouble(currentChild.Amount);
                                child.UnitPrice = Convert.ToDouble(currentChild.UnitPrice);

                                child.UnitID = currentChild.UnitID.HasValue ? currentChild.UnitID.Value : default(long?);
                                child.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                                child.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                            }
                        }
                        else
                        {
                            // Pick
                            //child.TransactionDetailID = transactionDetail.DetailIID;
                            child.ParentDetailID = transactionDetail.DetailIID;
                            child.TransactionHead = transactionDetail.HeadID.Value;
                            child.Action = new KeyValueViewModel();
                            child.SKUID = new KeyValueViewModel();
                            child.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                            child.SKUID.Value = transactionDetail.SKU;
                            child.Description = transactionDetail.SKU;
                            child.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                            child.Amount = Convert.ToDouble(transactionDetail.Amount);
                            child.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                            child.UnitID = transactionDetail.UnitID.IsNotNull() ? (long)transactionDetail.UnitID : 0; // what to be populated when its null
                            child.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                            child.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);

                        }
                        orderChangeRequestDetail.ChildGrid.Add(child);
                        orderChangeRequestVM.DetailViewModel.Add(orderChangeRequestDetail);
                    }
                }

                // Map Delivery Detail
                if (parentDTO.OrderContactMap.IsNotNull())
                {
                    orderChangeRequestVM.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(parentDTO.OrderContactMap);
                }

                return orderChangeRequestVM;
            }
            else
            {
                return new OrderChangeRequestViewModel();
            }
        }

        #endregion


        public static PurchaseTenderViewModel FromDTOToPurchaseTenderVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseOrder = new PurchaseTenderViewModel();
                purchaseOrder.MasterViewModel = new PurchaseTenderMasterViewModel();
                purchaseOrder.DetailViewModel = new List<PurchaseTenderDetailViewModel>();
                //purchaseOrder.MasterViewModel.Allocations = new BranchWiseAllocationViewModel();
                //purchaseOrder.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                purchaseOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                purchaseOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseOrder.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName.IsNotNull() ? dto.TransactionHead.CurrencyName.ToString() : null;
                purchaseOrder.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseOrder.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseOrder.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //purchaseOrder.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseOrder.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //purchaseOrder.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                purchaseOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseOrder.MasterViewModel.JobStatus = Convert.ToString(dto.TransactionHead.JobStatusID != null ? dto.TransactionHead.JobStatusID : null);
                purchaseOrder.MasterViewModel.InvoiceStatus = dto.TransactionHead.InvoiceStatus.IsNotNull() ? dto.TransactionHead.InvoiceStatus : string.Empty;
                purchaseOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                purchaseOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;
                purchaseOrder.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseTender;

                purchaseOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                //purchaseOrder.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements != null && dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;
                if (dto.TransactionHead.ReceivingMethodID.HasValue)
                {
                    purchaseOrder.MasterViewModel.ReceivingMethod = new KeyValueViewModel();
                    purchaseOrder.MasterViewModel.ReceivingMethod.Key = dto.TransactionHead.ReceivingMethodID.ToString();
                    purchaseOrder.MasterViewModel.ReceivingMethod.Value = dto.TransactionHead.ReceivingMethodName;
                }
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    purchaseOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    purchaseOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    purchaseOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseOrderDetail = new PurchaseTenderDetailViewModel();

                        purchaseOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseOrderDetail.SKUID = new KeyValueViewModel();
                        purchaseOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseOrderDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseOrderDetail.Description = transactionDetail.SKU;
                        purchaseOrderDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        purchaseOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseOrderDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        //purchaseOrderDetail.SKUDetails = null;
                        purchaseOrderDetail.ProductLength = transactionDetail.ProductLength;
                        purchaseOrderDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        purchaseOrderDetail.PartNo = transactionDetail.PartNo;
                        //if (transactionDetail.TransactionAllocations != null && transactionDetail.TransactionAllocations.Count > 0)
                        //{
                        //    var allocation = new QuantityAllocationViewModel();
                        //    allocation.ProductName = transactionDetail.SKU;
                        //    allocation.ProductID = (long)transactionDetail.ProductSKUMapID;
                        //    allocation.Quantity = transactionDetail.Quantity.IsNotNull() ? (decimal)transactionDetail.Quantity : 0;
                        //    allocation.AllocatedQuantity = new List<decimal>();
                        //    allocation.BranchIDs = new List<long>();
                        //    allocation.BranchName = new List<string>();

                        //    foreach (var transactionDetailAllocation in transactionDetail.TransactionAllocations)
                        //    {
                        //        allocation.AllocatedQuantity.Add(transactionDetailAllocation.Quantity != null ? (decimal)transactionDetailAllocation.Quantity : 0);
                        //        allocation.BranchIDs.Add(transactionDetailAllocation.BranchID);
                        //    }
                        //    purchaseOrder.MasterViewModel.Allocations.Allocations.Add(allocation);
                        //}

                        purchaseOrder.DetailViewModel.Add(purchaseOrderDetail);
                    }
                }

                //if (dto.ShipmentDetails.IsNotNull())
                //{
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TransactionShipmentIID = dto.ShipmentDetails.TransactionShipmentIID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TransactionHeadID = dto.TransactionHead.HeadIID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.SupplierIDFrom = dto.ShipmentDetails.SupplierIDFrom;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.SupplierIDTo = dto.ShipmentDetails.SupplierIDTo;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.ShipmentReference = dto.ShipmentDetails.ShipmentReference;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.FreightCareer = dto.ShipmentDetails.FreightCareer;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.ClearanceTypeID = dto.ShipmentDetails.ClearanceTypeID;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.AirWayBillNo = dto.ShipmentDetails.AirWayBillNo;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.FrieghtCharges = dto.ShipmentDetails.FrieghtCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.BrokerCharges = dto.ShipmentDetails.BrokerCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.Weight = dto.ShipmentDetails.Weight;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.NoOfBoxes = dto.ShipmentDetails.NoOfBoxes;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.BrokerAccount = dto.ShipmentDetails.BrokerAccount;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.AdditionalCharges = dto.ShipmentDetails.AdditionalCharges;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.Remarks = dto.ShipmentDetails.Remarks;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.CreatedBy = dto.ShipmentDetails.CreatedBy;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.UpdatedBy = dto.ShipmentDetails.UpdatedBy;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.CreatedDate = dto.ShipmentDetails.CreatedDate;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.UpdatedDate = dto.ShipmentDetails.UpdatedDate;
                //    purchaseOrder.MasterViewModel.ShipmentDetails.TimeStamps = dto.ShipmentDetails.TimeStamps;
                //}

                return purchaseOrder;
            }
            else return new PurchaseTenderViewModel();
        }

        public static TransactionDTO ToDTOFromPurchaseTenderVM(PurchaseTenderViewModel vm)
        {
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.ShipmentDetails = new ShipmentDetailDTO();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = long.Parse(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? Convert.ToDateTime(vm.MasterViewModel.TransactionDate) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.SupplierID = vm.MasterViewModel.Supplier != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Supplier.Key) ? Convert.ToInt32(vm.MasterViewModel.Supplier.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlement);
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? Convert.ToDateTime(vm.MasterViewModel.DeliveryDate) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.JobStatusID = Convert.ToInt16(vm.MasterViewModel.JobStatus);

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DeliveryTypeName = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Value) ? vm.MasterViewModel.DeliveryMethod.Value.ToString() : null : null;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DocumentStatusName = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Value) ? vm.MasterViewModel.DocumentStatus.Value.ToString() : null : null;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;


                transaction.TransactionHead.ReceivingMethodID = vm.MasterViewModel.ReceivingMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReceivingMethod.Key) ? short.Parse(vm.MasterViewModel.ReceivingMethod.Key) : (int?)null : null;
                transaction.TransactionHead.ReceivingMethodName = vm.MasterViewModel.ReceivingMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.ReceivingMethod.Value) ? vm.MasterViewModel.ReceivingMethod.Value.ToString() : string.Empty : string.Empty;

                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            //if (vm.MasterViewModel.Allocations.Allocations != null && vm.MasterViewModel.Allocations.Allocations.Count > 0)
                            //{
                            //    QuantityAllocationViewModel allocationToMap = vm.MasterViewModel.Allocations.Allocations.Where(x => x.ProductID == transactionDetailDTO.ProductSKUMapID).FirstOrDefault();
                            //    transactionDetailDTO.TransactionAllocations = new List<TransactionAllocationDTO>();
                            //    if (allocationToMap != null && allocationToMap.BranchIDs != null)
                            //    {
                            //        for (int i = 0; i < allocationToMap.BranchIDs.Count; i++)
                            //        {
                            //            var transactionAllocation = new TransactionAllocationDTO();
                            //            transactionAllocation.TrasactionDetailID = transactionDetail.TransactionDetailID;
                            //            transactionAllocation.BranchID = allocationToMap.BranchIDs[i];
                            //            transactionAllocation.Quantity = allocationToMap.AllocatedQuantity[i];
                            //            transactionDetailDTO.TransactionAllocations.Add(transactionAllocation);
                            //        }
                            //    }
                            //}

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.ShipmentDetails.IsNotNull())
                //{
                //    transaction.ShipmentDetails.TransactionShipmentIID = vm.MasterViewModel.ShipmentDetails.TransactionShipmentIID;
                //    transaction.ShipmentDetails.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //    transaction.ShipmentDetails.SupplierIDFrom = vm.MasterViewModel.ShipmentDetails.SupplierIDFrom;
                //    transaction.ShipmentDetails.SupplierIDTo = vm.MasterViewModel.ShipmentDetails.SupplierIDTo;
                //    transaction.ShipmentDetails.ShipmentReference = vm.MasterViewModel.ShipmentDetails.ShipmentReference;
                //    transaction.ShipmentDetails.FreightCareer = vm.MasterViewModel.ShipmentDetails.FreightCareer;
                //    transaction.ShipmentDetails.ClearanceTypeID = vm.MasterViewModel.ShipmentDetails.ClearanceTypeID;
                //    transaction.ShipmentDetails.AirWayBillNo = vm.MasterViewModel.ShipmentDetails.AirWayBillNo;
                //    transaction.ShipmentDetails.FrieghtCharges = vm.MasterViewModel.ShipmentDetails.FrieghtCharges;
                //    transaction.ShipmentDetails.BrokerCharges = vm.MasterViewModel.ShipmentDetails.BrokerCharges;
                //    transaction.ShipmentDetails.AdditionalCharges = vm.MasterViewModel.ShipmentDetails.AdditionalCharges;
                //    transaction.ShipmentDetails.Weight = vm.MasterViewModel.ShipmentDetails.Weight;
                //    transaction.ShipmentDetails.NoOfBoxes = vm.MasterViewModel.ShipmentDetails.NoOfBoxes;
                //    transaction.ShipmentDetails.BrokerAccount = vm.MasterViewModel.ShipmentDetails.BrokerAccount;
                //    transaction.ShipmentDetails.Remarks = vm.MasterViewModel.ShipmentDetails.Remarks;
                //    transaction.ShipmentDetails.CreatedBy = vm.MasterViewModel.ShipmentDetails.CreatedBy;
                //    transaction.ShipmentDetails.UpdatedBy = vm.MasterViewModel.ShipmentDetails.UpdatedBy;
                //    transaction.ShipmentDetails.CreatedDate = vm.MasterViewModel.ShipmentDetails.CreatedDate;
                //    transaction.ShipmentDetails.UpdatedDate = vm.MasterViewModel.ShipmentDetails.UpdatedDate;
                //    transaction.ShipmentDetails.TimeStamps = vm.MasterViewModel.ShipmentDetails.TimeStamps;
                //}

                return transaction;
            }
            else return new TransactionDTO();
        }

        public static SalesQuotationViewModel FromSalesQuotationDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var SalesQuotation = new SalesQuotationViewModel();
                SalesQuotation.MasterViewModel = new SalesQuotationMasterViewModel();
                //SalesQuotation.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                //SalesQuotation.MasterViewModel.BillingDetails = new BillingAddressViewModel();
                SalesQuotation.DetailViewModel = new List<SalesQuotationDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                SalesQuotation.MasterViewModel.IsError = dto.TransactionHead.IsError;
                SalesQuotation.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                SalesQuotation.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                SalesQuotation.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                SalesQuotation.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                SalesQuotation.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                SalesQuotation.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                SalesQuotation.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                SalesQuotation.MasterViewModel.Remarks = dto.TransactionHead.Description;
                //SalesQuotation.MasterViewModel.Currency = new KeyValueViewModel();
                //SalesQuotation.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                //SalesQuotation.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                SalesQuotation.MasterViewModel.Customer = new KeyValueViewModel();
                SalesQuotation.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                SalesQuotation.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;
                SalesQuotation.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                SalesQuotation.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                SalesQuotation.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;

                //SalesQuotation.MasterViewModel.EntitlementID = (short)dto.TransactionHead.EntitlementID;

                //SalesQuotation.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    SalesQuotation.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID > 0 ? dto.TransactionHead.DeliveryMethodID.Value.ToString() : null;
                    SalesQuotation.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    SalesQuotation.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    SalesQuotation.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    SalesQuotation.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    SalesQuotation.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                SalesQuotation.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                SalesQuotation.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                SalesQuotation.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                SalesQuotation.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge.HasValue ? dto.TransactionHead.DeliveryCharge.Value : 0;
                SalesQuotation.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                SalesQuotation.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;

                // put delivery option here... express etc
                //if (dto.TransactionHead.DeliveryTypeID > 0)
                //{
                //    SalesQuotation.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                //    SalesQuotation.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Value = dto.TransactionHead.DeliveryOption;
                //}
                //SalesQuotation.MasterViewModel.DeliveryPaymentDetail.DeliveryOption = dto.TransactionHead.DeliveryTypeID.IsNotNull() ? dto.TransactionHead.DeliveryTypeID.ToString() : null;

                SalesQuotation.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    SalesQuotation.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    SalesQuotation.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var SalesQuotationDetail = new SalesQuotationDetailViewModel();

                        SalesQuotationDetail.TransactionDetailID = transactionDetail.DetailIID;
                        SalesQuotationDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        SalesQuotationDetail.SKUID = new KeyValueViewModel();
                        SalesQuotationDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        SalesQuotationDetail.SKUID.Value = transactionDetail.SKU;
                        SalesQuotationDetail.Description = transactionDetail.SKU;
                        SalesQuotationDetail.PartNo = transactionDetail.PartNo;
                        SalesQuotationDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        SalesQuotationDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        SalesQuotationDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        SalesQuotationDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        SalesQuotationDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        SalesQuotationDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        //SalesQuotationDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture); it takes today's date as default for warranty date
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        SalesQuotationDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        SalesQuotationDetail.SKUDetails = null;
                        SalesQuotation.DetailViewModel.Add(SalesQuotationDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMaps.IsNotNull() && dto.OrderContactMaps.Count > 0)
                //{
                //    foreach (OrderContactMapDTO ocmDTO in dto.OrderContactMaps)
                //    {
                //        if (ocmDTO.IsShippingAddress == true)
                //            SalesQuotation.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //        else
                //            SalesQuotation.MasterViewModel.BillingDetails = BillingAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //    }
                //}

                return SalesQuotation;
            }
            else
            {
                return new SalesQuotationViewModel();
            }
        }

        public static TransactionDTO ToSalesQuotationDTO(SalesQuotationViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                //transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption);

                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) ? Convert.ToInt16(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) : (short?)null : (short?)null;

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? long.Parse(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus.IsNotNull() && vm.MasterViewModel.DocumentStatus.Key.IsNotNullOrEmpty() ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : default(short) : default(short);

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;

                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}

                transaction.IgnoreEntitlementCheck = true;

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                            //transaction.TransactionHead.DeliveryCharge = transactionDetail.DeliveryCharge.IsNotNull() ? transactionDetail.DeliveryCharge : vm.MasterViewModel.DeliveryCharge.IsNotNull() ? vm.MasterViewModel.DeliveryCharge : (decimal?)null;
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMaps.Add(DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails));
                //}

                //if (vm.MasterViewModel.BillingDetails.IsNotNull())
                //{
                //    if (vm.MasterViewModel.BillingDetails.ContactPerson.IsNull() && vm.MasterViewModel.BillingDetails.BillingAddress.IsNull()
                //        && vm.MasterViewModel.BillingDetails.bMobileNo.IsNull() && vm.MasterViewModel.BillingDetails.LandLineNo.IsNull()
                //        && vm.MasterViewModel.BillingDetails.SpecialInstructions.IsNull() && vm.MasterViewModel.BillingDetails.Area.IsNull())
                //    {
                //        OrderContactMapDTO ocmDTO = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //        ocmDTO.OrderContactMapID = 0;
                //        ocmDTO.IsBillingAddress = true;
                //        ocmDTO.IsShippingAddress = false;

                //        transaction.OrderContactMaps.Add(ocmDTO);
                //    }
                //    else
                //    {
                //        transaction.OrderContactMaps.Add(BillingAddressViewModel.ToDTO(vm.MasterViewModel.BillingDetails));
                //    }
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static DeliveryNoteViewModel FromDeliveryNoteDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var deliveryNote = new DeliveryNoteViewModel();
                deliveryNote.MasterViewModel = new DeliveryNoteMasterViewModel();
                //deliveryNote.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                deliveryNote.DetailViewModel = new List<DeliveryNoteDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                deliveryNote.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                deliveryNote.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                deliveryNote.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                deliveryNote.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                deliveryNote.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                deliveryNote.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                deliveryNote.MasterViewModel.Remarks = dto.TransactionHead.Description;
                deliveryNote.MasterViewModel.Reference = dto.TransactionHead.Reference;
                //deliveryNote.MasterViewModel.Currency = new KeyValueViewModel();
                //deliveryNote.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                //deliveryNote.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                deliveryNote.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                deliveryNote.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                deliveryNote.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;

                deliveryNote.MasterViewModel.DeliveryMethod = new KeyValueViewModel();
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    deliveryNote.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    deliveryNote.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                    //deliveryNote.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                }

                //deliveryNote.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                deliveryNote.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                deliveryNote.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                deliveryNote.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                deliveryNote.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                deliveryNote.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID;

                deliveryNote.MasterViewModel.SalesMan = new KeyValueViewModel();

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    deliveryNote.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    deliveryNote.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                }

                deliveryNote.MasterViewModel.DocumentStatus = new KeyValueViewModel();

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    deliveryNote.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    deliveryNote.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                deliveryNote.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                deliveryNote.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                deliveryNote.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                deliveryNote.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge > 0 ? dto.TransactionHead.DeliveryCharge.Value : 0;
                deliveryNote.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                deliveryNote.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;

                deliveryNote.IsError = dto.TransactionHead.IsError;
                deliveryNote.ErrorCode = dto.TransactionHead.ErrorCode;


                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    deliveryNote.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }
                else
                {
                    deliveryNote.MasterViewModel.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMapViewModel>() { new TransactionHeadEntitlementMapViewModel() };
                }

                if (dto.TransactionHead.TaxDetails != null && dto.TransactionHead.TaxDetails.Count > 0)
                {
                    deliveryNote.MasterViewModel.TaxDetails = new TaxDetailsViewModel() { Taxes = dto.TransactionHead.TaxDetails.Select(x => TaxDetailsViewModel.ToVM(x)).ToList() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var deliveryNoteDetail = new DeliveryNoteDetailViewModel();
                        deliveryNoteDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        deliveryNoteDetail.TransactionDetailID = transactionDetail.DetailIID;
                        deliveryNoteDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        deliveryNoteDetail.SKUID = new KeyValueViewModel();
                        deliveryNoteDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        deliveryNoteDetail.SKUID.Value = transactionDetail.SKU;
                        deliveryNoteDetail.Description = transactionDetail.SKU;
                        deliveryNoteDetail.PartNo = transactionDetail.PartNo;
                        deliveryNoteDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        deliveryNoteDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        deliveryNoteDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        deliveryNoteDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        deliveryNoteDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        deliveryNoteDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        deliveryNoteDetail.CGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount1);
                        deliveryNoteDetail.SGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount2);

                        if (transactionDetail.WarrantyDate.IsNull())
                        {
                            deliveryNoteDetail.WarrantyDate = Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        //deliveryNoteDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        deliveryNoteDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        deliveryNoteDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {

                            foreach (var dtoProductSerialMap in transactionDetail.SKUDetails)
                            {
                                var productSerialMap = new ProductSerialMapViewModel();
                                productSerialMap.ProductSerialID = dtoProductSerialMap.ProductSerialID;
                                productSerialMap.SerialNo = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList = new KeyValueViewModel();
                                //productSerialMap.SerialList.Key = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList.Value = dtoProductSerialMap.SerialNo;
                                productSerialMap.ProductSKUMapID = dtoProductSerialMap.ProductSKUMapID;
                                productSerialMap.TimeStamps = dtoProductSerialMap.TimeStamps;
                                deliveryNoteDetail.SKUDetails.Add(productSerialMap);
                            }
                        }
                        else
                            deliveryNoteDetail.SKUDetails.Add(new ProductSerialMapViewModel()
                            {
                                ProductSKUMapID = (long)transactionDetail.ProductSKUMapID,
                                SerialNo = transactionDetail.SerialNumber
                                /*SerialList = new KeyValueViewModel() { Key = transactionDetail.SerialNumber, Value = transactionDetail.SerialNumber }*/
                            });

                        deliveryNote.DetailViewModel.Add(deliveryNoteDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMap.IsNotNull())
                //{
                //    deliveryNote.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                //}

                // Map Delivery Detail
                if (dto.TransactionHead.TaxDetails.IsNotNull())
                {
                    deliveryNote.MasterViewModel.TaxDetails = TaxDetailsViewModel.ToVM(dto.TransactionHead.TaxDetails, null);
                }


                return deliveryNote;
            }
            else
            {
                return new DeliveryNoteViewModel();
            }
        }

        public static TransactionDTO ToDeliveryNoteDTO(DeliveryNoteViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                //transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryMethodID = Convert.ToInt16(vm.MasterViewModel.DeliveryMethod.Key);
                transaction.TransactionHead.DeliveryTypeName = vm.MasterViewModel.DeliveryMethod.Value;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID;
                transaction.TransactionHead.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadID;

                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.EntitlementID) : (short?)null;

                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;

                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}

                //if (vm.MasterViewModel.TaxDetails.IsNotNull() && vm.MasterViewModel.TaxDetails.Taxes.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TaxDetails.Taxes)
                //    {
                //        if (item.TaxAmount.HasValue)
                //        {
                //            transaction.TransactionHead.TaxDetails.Add(new Services.Contracts.Inventory.TaxDetailsDTO()
                //            {
                //                TaxID = item.TaxID,
                //                TaxName = item.TaxName,
                //                Amount = item.TaxAmount,
                //                Percentage = item.TaxPercentage,
                //                TaxTemplateID = item.TaxTemplateID,
                //                TaxTemplateItemID = item.TaxTemplateItemID
                //            });
                //        }
                //    }
                //}

                transaction.IgnoreEntitlementCheck = true;

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.TaxAmount1 = Convert.ToDecimal(transactionDetail.CGSTAmount);
                            transactionDetailDTO.TaxAmount2 = Convert.ToDecimal(transactionDetail.SGSTAmount);

                            transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;


                            transactionDetailDTO.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate) : (DateTime?)null;

                            if (transactionDetail.SKUDetails != null)
                            {
                                // ProductSerialMapViewModel
                                foreach (var sku in transactionDetail.SKUDetails)
                                {
                                    // if (sku.SerialList.IsNotNull() && sku.SerialList.Value.IsNotNull())
                                    // {
                                    var dtoSKUDetail = new ProductSerialMapDTO();
                                    dtoSKUDetail.SerialNo = sku.SerialNo;
                                    dtoSKUDetail.ProductSKUMapID = sku.ProductSKUMapID;
                                    // add sku detail dto into list
                                    transactionDetailDTO.SKUDetails.Add(dtoSKUDetail);
                                    // }
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMap = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //}

                if (vm.MasterViewModel.TaxDetails.IsNotNull())
                {
                    transaction.TransactionHead.TaxDetails = TaxDetailsViewModel.ToDTO(vm.MasterViewModel.TaxDetails.Taxes);
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static BundleWrapViewModel FromBundleWrapDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var bundleWrap = new BundleWrapViewModel();
                bundleWrap.MasterViewModel = new BundleWrapMasterViewModel();
                bundleWrap.DetailViewModel = new List<BundleWrapDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                bundleWrap.MasterViewModel.IsError = dto.TransactionHead.IsError;
                bundleWrap.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                bundleWrap.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                bundleWrap.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                //bundleWrap.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                bundleWrap.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                bundleWrap.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                bundleWrap.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                //bundleWrap.MasterViewModel.Currency = new KeyValueViewModel();
                //bundleWrap.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                //bundleWrap.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                bundleWrap.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                bundleWrap.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                bundleWrap.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };

                bundleWrap.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                bundleWrap.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);

                bundleWrap.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var bundleWrapDetail = new BundleWrapDetailViewModel();

                        bundleWrapDetail.TransactionDetailID = transactionDetail.DetailIID;
                        bundleWrapDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        bundleWrapDetail.SKUID = new KeyValueViewModel();
                        bundleWrapDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        bundleWrapDetail.SKUID.Value = transactionDetail.SKU;
                        bundleWrapDetail.PartNo = transactionDetail.PartNo;
                        bundleWrapDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        bundleWrapDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        bundleWrapDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        bundleWrapDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        bundleWrapDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        bundleWrapDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        bundleWrap.DetailViewModel.Add(bundleWrapDetail);
                    }
                }

                return bundleWrap;
            }
            else
            {
                return new BundleWrapViewModel();
            }
        }

        public static TransactionDTO ToBundleWrapDTO(BundleWrapViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;

                //transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.DocumentTypeID = 28;
                transaction.TransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;

                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;

                transaction.IgnoreEntitlementCheck = true;

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            //transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.UnitID = Convert.ToInt32(transactionDetail.Unit.Key);
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static BundleUnWrapViewModel FromBundleUnWrapDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var bundleUnWrap = new BundleUnWrapViewModel();
                bundleUnWrap.MasterViewModel = new BundleUnWrapMasterViewModel();
                bundleUnWrap.DetailViewModel = new List<BundleUnWrapDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                bundleUnWrap.MasterViewModel.IsError = dto.TransactionHead.IsError;
                bundleUnWrap.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                bundleUnWrap.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                bundleUnWrap.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                //bundleUnWrap.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                bundleUnWrap.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                bundleUnWrap.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                bundleUnWrap.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                //bundleUnWrap.MasterViewModel.Currency = new KeyValueViewModel();
                //bundleUnWrap.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                //bundleUnWrap.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                bundleUnWrap.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                bundleUnWrap.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                bundleUnWrap.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };


                bundleUnWrap.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                bundleUnWrap.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);

                bundleUnWrap.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var bundleUnWrapDetail = new BundleUnWrapDetailViewModel();

                        bundleUnWrapDetail.TransactionDetailID = transactionDetail.DetailIID;
                        bundleUnWrapDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        bundleUnWrapDetail.SKUID = new KeyValueViewModel();
                        bundleUnWrapDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        bundleUnWrapDetail.SKUID.Value = transactionDetail.SKU;
                        bundleUnWrapDetail.PartNo = transactionDetail.PartNo;
                        bundleUnWrapDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        bundleUnWrapDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        bundleUnWrapDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        bundleUnWrapDetail.Unit = transactionDetail.UnitID.HasValue ? transactionDetail.UnitID.ToString() : null;
                        bundleUnWrapDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        bundleUnWrapDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        bundleUnWrap.DetailViewModel.Add(bundleUnWrapDetail);
                    }
                }

                return bundleUnWrap;
            }
            else
            {
                return new BundleUnWrapViewModel();
            }
        }

        public static TransactionDTO ToBundleUnWrapDTO(BundleUnWrapViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;

                //transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.DocumentTypeID = 29;
                transaction.TransactionHead.DocumentStatusID = Convert.ToInt32(vm.MasterViewModel.DocumentStatus.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;

                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;
                transaction.IgnoreEntitlementCheck = true;


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            /*transactionDetailDTO.UnitID = 1;*///currently hard coding it to 1
                                                                //transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitID = !string.IsNullOrEmpty(transactionDetail.Unit) ? Convert.ToInt64(transactionDetail.Unit) : (long?)null;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }
        public static SalesOrderLiteViewModel FromSalesOrderLiteDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesOrder = new SalesOrderLiteViewModel();
                salesOrder.MasterViewModel = new SalesOrderLiteMasterViewModel();
                //salesOrder.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                ///salesOrder.MasterViewModel.BillingDetails = new BillingAddressViewModel();
                salesOrder.DetailViewModel = new List<SalesOrderLiteDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                salesOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                salesOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesOrder.MasterViewModel.Currency = new KeyValueViewModel();
                salesOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesOrder.MasterViewModel.Customer = new KeyValueViewModel();
                salesOrder.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesOrder.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;
                salesOrder.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                salesOrder.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesOrder.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;

                //salesOrder.MasterViewModel.EntitlementID = (short)dto.TransactionHead.EntitlementID;

                //salesOrder.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID > 0 ? dto.TransactionHead.DeliveryMethodID.Value.ToString() : null;
                    salesOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesOrder.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesOrder.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                salesOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesOrder.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge.HasValue ? dto.TransactionHead.DeliveryCharge.Value : 0;
                salesOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesOrder.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesOrder.MasterViewModel.Student = new KeyValueViewModel();
                salesOrder.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                salesOrder.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                salesOrder.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;
                salesOrder.MasterViewModel.EmailID = dto.TransactionHead.EmailID;
                salesOrder.MasterViewModel.SchoolID = dto.TransactionHead.SchoolID;
                // put delivery option here... express etc
                //if (dto.TransactionHead.DeliveryTypeID > 0)
                //{
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Value = dto.TransactionHead.DeliveryOption;
                //}
                //salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption = dto.TransactionHead.DeliveryTypeID.IsNotNull() ? dto.TransactionHead.DeliveryTypeID.ToString() : null;

                salesOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                // Map TransactionHeadEntitlementMapViewModel
                //if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    salesOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                //        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                //}


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesOrderDetail = new SalesOrderLiteDetailViewModel();

                        salesOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesOrderDetail.SKUID = new KeyValueViewModel();
                        salesOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesOrderDetail.SKUID.Value = transactionDetail.SKU;
                        salesOrderDetail.Description = transactionDetail.SKU;
                        salesOrderDetail.PartNo = transactionDetail.PartNo;
                        salesOrderDetail.Quantity = transactionDetail.Quantity.Value;
                        salesOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesOrderDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesOrderDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesOrderDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesOrderDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesOrderDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesOrderDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesOrderDetail.ProductCode = transactionDetail.ProductCode;
                        salesOrderDetail.UnitDTO = new List<KeyValueViewModel>();

                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesOrderDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesOrderDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesOrderDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }
                        //salesOrderDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture); it takes today's date as default for warranty date
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        salesOrderDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        salesOrderDetail.SKUDetails = null;
                        salesOrder.DetailViewModel.Add(salesOrderDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMaps.IsNotNull() && dto.OrderContactMaps.Count > 0)
                //{
                //    foreach (OrderContactMapDTO ocmDTO in dto.OrderContactMaps)
                //    {
                //        if (ocmDTO.IsShippingAddress == true)
                //            salesOrder.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //        else
                //            salesOrder.MasterViewModel.BillingDetails = BillingAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //    }
                //}

                return salesOrder;
            }
            else
            {
                return new SalesOrderLiteViewModel();
            }
        }

        public static TransactionDTO ToSalesOrderLiteDTO(SalesOrderLiteViewModel vm)
        {
            if (vm != null)
            {
                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }
                if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }
                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }



                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? short.Parse(vm.MasterViewModel.DocumentType.Key) : (int?)null : null;
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption);

                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) ? Convert.ToInt16(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) : (short?)null : (short?)null;

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? long.Parse(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? long.Parse(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus.IsNotNull() && vm.MasterViewModel.DocumentStatus.Key.IsNotNullOrEmpty() ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : default(short) : default(short);

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.IgnoreEntitlementCheck = true;
                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                            //transaction.TransactionHead.DeliveryCharge = transactionDetail.DeliveryCharge.IsNotNull() ? transactionDetail.DeliveryCharge : vm.MasterViewModel.DeliveryCharge.IsNotNull() ? vm.MasterViewModel.DeliveryCharge : (decimal?)null;
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMaps.Add(DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails));
                //}

                //if (vm.MasterViewModel.BillingDetails.IsNotNull())
                //{
                //    if (vm.MasterViewModel.BillingDetails.ContactPerson.IsNull() && vm.MasterViewModel.BillingDetails.BillingAddress.IsNull()
                //        && vm.MasterViewModel.BillingDetails.bMobileNo.IsNull() && vm.MasterViewModel.BillingDetails.LandLineNo.IsNull()
                //        && vm.MasterViewModel.BillingDetails.SpecialInstructions.IsNull() && vm.MasterViewModel.BillingDetails.Area.IsNull())
                //    {
                //        OrderContactMapDTO ocmDTO = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //        ocmDTO.OrderContactMapID = 0;
                //        ocmDTO.IsBillingAddress = true;
                //        ocmDTO.IsShippingAddress = false;

                //        transaction.OrderContactMaps.Add(ocmDTO);
                //    }
                //    else
                //    {
                //        transaction.OrderContactMaps.Add(BillingAddressViewModel.ToDTO(vm.MasterViewModel.BillingDetails));
                //    }
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static SalesInvoiceLiteViewModel FromSalesInvoiceLiteDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesInvoice = new SalesInvoiceLiteViewModel();
                salesInvoice.MasterViewModel = new SalesInvoiceLiteMasterViewModel();
                //salesInvoice.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                salesInvoice.DetailViewModel = new List<SalesInvoiceLiteDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesInvoice.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesInvoice.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                //salesInvoice.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = "54", Value = "Sales Invoice Lite" };
                salesInvoice.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesInvoice.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesInvoice.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesInvoice.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesInvoice.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesInvoice.MasterViewModel.Reference = dto.TransactionHead.Reference;
                salesInvoice.MasterViewModel.Currency = new KeyValueViewModel();
                salesInvoice.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesInvoice.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesInvoice.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                salesInvoice.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesInvoice.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;

                salesInvoice.MasterViewModel.Student = new KeyValueViewModel();
                salesInvoice.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                salesInvoice.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                salesInvoice.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;

                salesInvoice.MasterViewModel.DeliveryMethod = new KeyValueViewModel();
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesInvoice.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    salesInvoice.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                    //salesInvoice.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                }

                //salesInvoice.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                salesInvoice.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesInvoice.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesInvoice.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesInvoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesInvoice.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID;

                salesInvoice.MasterViewModel.SalesMan = new KeyValueViewModel();

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesInvoice.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesInvoice.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                }

                salesInvoice.MasterViewModel.DocumentStatus = new KeyValueViewModel();

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesInvoice.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesInvoice.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                salesInvoice.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                salesInvoice.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesInvoice.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                salesInvoice.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge > 0 ? dto.TransactionHead.DeliveryCharge.Value : 0;
                salesInvoice.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesInvoice.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesInvoice.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

                salesInvoice.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesInvoice.IsError = dto.TransactionHead.IsError;
                salesInvoice.ErrorCode = dto.TransactionHead.ErrorCode;

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    salesInvoice.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionHead.TaxDetails != null && dto.TransactionHead.TaxDetails.Count > 0)
                {
                    salesInvoice.MasterViewModel.TaxDetails = new TaxDetailsViewModel() { Taxes = dto.TransactionHead.TaxDetails.Select(x => TaxDetailsViewModel.ToVM(x)).ToList() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesInvoiceDetail = new SalesInvoiceLiteDetailViewModel();
                        salesInvoiceDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        salesInvoiceDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesInvoiceDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesInvoiceDetail.SKUID = new KeyValueViewModel();
                        salesInvoiceDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesInvoiceDetail.SKUID.Value = transactionDetail.SKU;
                        //salesInvoiceDetail.Description = transactionDetail.SKU;
                        salesInvoiceDetail.PartNo = transactionDetail.PartNo;
                        salesInvoiceDetail.Quantity = transactionDetail.Quantity;
                        //salesInvoiceDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesInvoiceDetail.Amount = transactionDetail.Amount;
                        //salesInvoiceDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        //salesInvoiceDetail.UnitPrice = transactionDetail.UnitPrice;
                        //salesInvoiceDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesInvoiceDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesInvoiceDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesInvoiceDetail.CGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount1);
                        salesInvoiceDetail.SGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount2);
                        //salesInvoiceDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesInvoiceDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesInvoiceDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesInvoiceDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesInvoiceDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesInvoiceDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesInvoiceDetail.ProductCode = transactionDetail.ProductCode;
                        salesInvoiceDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesInvoiceDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesInvoiceDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesInvoiceDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        salesInvoiceDetail.IsOnEdit = true;

                        //if (transactionDetail.WarrantyDate.IsNull())
                        //{
                        //    salesInvoiceDetail.WarrantyDate = Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        //}

                        salesInvoiceDetail.CostCenterID = transactionDetail.CostCenterID;

                        //if (transactionDetail.CostCenter != null)
                        //{
                        //    salesInvoiceDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

                        //}

                        //salesInvoiceDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                        salesInvoiceDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        salesInvoiceDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {

                            foreach (var dtoProductSerialMap in transactionDetail.SKUDetails)
                            {
                                var productSerialMap = new ProductSerialMapViewModel();
                                productSerialMap.ProductSerialID = dtoProductSerialMap.ProductSerialID;
                                productSerialMap.SerialNo = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList = new KeyValueViewModel();
                                //productSerialMap.SerialList.Key = dtoProductSerialMap.SerialNo;
                                //productSerialMap.SerialList.Value = dtoProductSerialMap.SerialNo;
                                productSerialMap.ProductSKUMapID = dtoProductSerialMap.ProductSKUMapID;
                                productSerialMap.TimeStamps = dtoProductSerialMap.TimeStamps;
                                salesInvoiceDetail.SKUDetails.Add(productSerialMap);
                            }
                        }
                        else
                            salesInvoiceDetail.SKUDetails.Add(new ProductSerialMapViewModel()
                            {
                                ProductSKUMapID = (long)transactionDetail.ProductSKUMapID,
                                SerialNo = transactionDetail.SerialNumber
                                /*SerialList = new KeyValueViewModel() { Key = transactionDetail.SerialNumber, Value = transactionDetail.SerialNumber }*/
                            });

                        salesInvoice.DetailViewModel.Add(salesInvoiceDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMap.IsNotNull())
                //{
                //    salesInvoice.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                //}

                // Map Delivery Detail
                if (dto.TransactionHead.TaxDetails.IsNotNull())
                {
                    salesInvoice.MasterViewModel.TaxDetails = TaxDetailsViewModel.ToVM(dto.TransactionHead.TaxDetails, null);
                }


                return salesInvoice;
            }
            else
            {
                return new SalesInvoiceLiteViewModel();
            }
        }

        public static TransactionDTO ToSalesInvoiceLiteDTO(SalesInvoiceLiteViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }

                if ((vm.MasterViewModel.Customer == null || vm.MasterViewModel.Customer.Key == null) && (vm.MasterViewModel.Student == null || vm.MasterViewModel.Student.Key == null))
                {
                    throw new Exception("Select Customer or Student!");
                }

                if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }

                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Amount should not be zero for the selected product !");
                }

                vm.DetailViewModel = vm.DetailViewModel.Where(x => x.SKUID != null && !string.IsNullOrEmpty(x.SKUID.Key)).ToList();

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }

                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount) != (vm.DetailViewModel.Sum(x => x.Amount) - (vm.MasterViewModel.Discount ?? 0)))
                {
                    throw new Exception("Payment Amount should be equal to the grand total!");
                }

                //if (Convert.ToDouble(Math.Round(vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount ?? 0), 3, MidpointRounding.AwayFromZero)) != (Math.Round(vm.DetailViewModel.Sum(y => y.Amount), 3, MidpointRounding.AwayFromZero)))
                //{
                //    throw new Exception(" Payment Amount should be equal to the grand total!");
                //}

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;

                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? Convert.ToInt32(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryMethodID = null;// Convert.ToInt16(vm.MasterViewModel.DeliveryMethod.Key);
                transaction.TransactionHead.DeliveryTypeName = null; //vm.MasterViewModel.DeliveryMethod.Value;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID;
                transaction.TransactionHead.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadID;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;

                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                //transaction.TransactionHead.Entitlements = vm.MasterViewModel.Entitlements.Count > 0 ? KeyValueViewModel.ToDTO(vm.MasterViewModel.Entitlements) : null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.Entitlements[0].Key) : (short?)null;
                //transaction.TransactionHead.EntitlementID = vm.MasterViewModel.Entitlements.Count > 0 ? Convert.ToInt16(vm.MasterViewModel.EntitlementID) : (short?)null;

                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;
                            transactionHeadEntitlementMapDTO.ReferenceNo = item.ReferenceNo;
                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                //if (vm.MasterViewModel.TaxDetails.IsNotNull() && vm.MasterViewModel.TaxDetails.Taxes.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TaxDetails.Taxes)
                //    {
                //        if (item.TaxAmount.HasValue)
                //        {
                //            transaction.TransactionHead.TaxDetails.Add(new Services.Contracts.Inventory.TaxDetailsDTO()
                //            {
                //                TaxID = item.TaxID,
                //                TaxName = item.TaxName,
                //                Amount = item.TaxAmount,
                //                Percentage = item.TaxPercentage,
                //                TaxTemplateID = item.TaxTemplateID,
                //                TaxTemplateItemID = item.TaxTemplateItemID
                //            });
                //        }
                //    }
                //}

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            var price = new ProductDetailBL().GetProductPriceBySKUID(Convert.ToInt32(transactionDetail.SKUID.Key));

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = price;
                            transactionDetailDTO.TaxAmount1 = Convert.ToDecimal(transactionDetail.CGSTAmount);
                            transactionDetailDTO.TaxAmount2 = Convert.ToDecimal(transactionDetail.SGSTAmount);

                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;

                            // transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.UnitID = transactionDetail.UnitID != null ? transactionDetail.UnitID : long.Parse(transactionDetail.Unit.Key);
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDetailDTO.CostCenterID = transactionDetail.CostCenterID;
                            //transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            //transactionDetailDTO.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate) : (DateTime?)null;

                            if (transactionDetail.SKUDetails != null)
                            {
                                // ProductSerialMapViewModel
                                foreach (var sku in transactionDetail.SKUDetails)
                                {
                                    // if (sku.SerialList.IsNotNull() && sku.SerialList.Value.IsNotNull())
                                    // {
                                    var dtoSKUDetail = new ProductSerialMapDTO();
                                    dtoSKUDetail.SerialNo = sku.SerialNo;
                                    dtoSKUDetail.ProductSKUMapID = sku.ProductSKUMapID;
                                    // add sku detail dto into list
                                    transactionDetailDTO.SKUDetails.Add(dtoSKUDetail);
                                    // }
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transaction.TransactionDetails.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMap = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //}

                if (vm.MasterViewModel.TaxDetails.IsNotNull())
                {
                    transaction.TransactionHead.TaxDetails = TaxDetailsViewModel.ToDTO(vm.MasterViewModel.TaxDetails.Taxes);
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        public static SalesOrderLiteViewModel FromSubscriptionSalesOrderDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var salesOrder = new SalesOrderLiteViewModel();
                salesOrder.MasterViewModel = new SalesOrderLiteMasterViewModel();
                //salesOrder.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                ///salesOrder.MasterViewModel.BillingDetails = new BillingAddressViewModel();
                salesOrder.DetailViewModel = new List<SalesOrderLiteDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                salesOrder.MasterViewModel.IsError = dto.TransactionHead.IsError;
                salesOrder.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                salesOrder.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                salesOrder.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                salesOrder.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                salesOrder.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                salesOrder.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                salesOrder.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.Remarks = dto.TransactionHead.Description;
                salesOrder.MasterViewModel.Currency = new KeyValueViewModel();
                salesOrder.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                salesOrder.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                salesOrder.MasterViewModel.Customer = new KeyValueViewModel();
                salesOrder.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                salesOrder.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;
                salesOrder.MasterViewModel.DeliveryTypeID = dto.TransactionHead.DeliveryTypeID > 0 ? dto.TransactionHead.DeliveryTypeID.Value : 0;
                salesOrder.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                salesOrder.MasterViewModel.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;

                //salesOrder.MasterViewModel.EntitlementID = (short)dto.TransactionHead.EntitlementID;

                //salesOrder.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    salesOrder.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID > 0 ? dto.TransactionHead.DeliveryMethodID.Value.ToString() : null;
                    salesOrder.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    salesOrder.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    salesOrder.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                    //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
                }

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    salesOrder.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    salesOrder.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                salesOrder.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                salesOrder.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                salesOrder.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                salesOrder.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge.HasValue ? dto.TransactionHead.DeliveryCharge.Value : 0;
                salesOrder.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                salesOrder.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                salesOrder.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                salesOrder.MasterViewModel.Student = new KeyValueViewModel();
                salesOrder.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                salesOrder.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
                salesOrder.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;
                salesOrder.MasterViewModel.EmailID = dto.TransactionHead.EmailID;
                salesOrder.MasterViewModel.SchoolID = dto.TransactionHead.SchoolID;
                // put delivery option here... express etc
                //if (dto.TransactionHead.DeliveryTypeID > 0)
                //{
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                //    salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Value = dto.TransactionHead.DeliveryOption;
                //}
                //salesOrder.MasterViewModel.DeliveryPaymentDetail.DeliveryOption = dto.TransactionHead.DeliveryTypeID.IsNotNull() ? dto.TransactionHead.DeliveryTypeID.ToString() : null;

                salesOrder.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                // Map TransactionHeadEntitlementMapViewModel
                //if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    salesOrder.MasterViewModel.TransactionHeadEntitlementMaps =
                //        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                //}


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var salesOrderDetail = new SalesOrderLiteDetailViewModel();

                        salesOrderDetail.TransactionDetailID = transactionDetail.DetailIID;
                        salesOrderDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        salesOrderDetail.SKUID = new KeyValueViewModel();
                        salesOrderDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        salesOrderDetail.SKUID.Value = transactionDetail.SKU;
                        salesOrderDetail.Description = transactionDetail.SKU;
                        salesOrderDetail.PartNo = transactionDetail.PartNo;
                        salesOrderDetail.Quantity = transactionDetail.Quantity.Value;
                        salesOrderDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        salesOrderDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        salesOrderDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        salesOrderDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        salesOrderDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        salesOrderDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        //salesOrderDetail.ForeignRate = transactionDetail.ForeignRate;
                        //salesOrderDetail.ForeignAmount = transactionDetail.ForeignAmount;
                        salesOrderDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        salesOrderDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        salesOrderDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        salesOrderDetail.ProductCode = transactionDetail.ProductCode;
                        salesOrderDetail.UnitDTO = new List<KeyValueViewModel>();

                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                salesOrderDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        salesOrderDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                salesOrderDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }
                        //salesOrderDetail.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture) : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture); it takes today's date as default for warranty date
                        if (transactionDetail.WarrantyDate != null)
                        {
                            Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }
                        salesOrderDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        salesOrderDetail.SKUDetails = null;
                        salesOrder.DetailViewModel.Add(salesOrderDetail);
                    }
                }

                // Map Delivery Detail
                //if (dto.OrderContactMaps.IsNotNull() && dto.OrderContactMaps.Count > 0)
                //{
                //    foreach (OrderContactMapDTO ocmDTO in dto.OrderContactMaps)
                //    {
                //        if (ocmDTO.IsShippingAddress == true)
                //            salesOrder.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //        else
                //            salesOrder.MasterViewModel.BillingDetails = BillingAddressViewModel.FromOrderContactDTOToVM(ocmDTO);
                //    }
                //}

                return salesOrder;
            }
            else
            {
                return new SalesOrderLiteViewModel();
            }
        }

        public static TransactionDTO ToSubscriptionSalesOrderDTO(SalesOrderLiteViewModel vm)
        {
            if (vm != null)
            {
                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }
                if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                {
                    throw new Exception("Please enter product details");
                }
                if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                    throw new Exception("Please enter amount for the selected product !");

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }



                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMaps = new List<OrderContactMapDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                //transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.DocumentTypeID = vm.MasterViewModel.DocumentType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? short.Parse(vm.MasterViewModel.DocumentType.Key) : (int?)null : null;
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                transaction.TransactionHead.DeliveryTypeID = new Domain.Setting.SettingBL().GetSettingValue<int>("SUBSCRIPTION_TYPE_ID");

                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) ? Convert.ToInt16(vm.MasterViewModel.DeliveryPaymentDetail.DeliveryOption.Key) : (short?)null : (short?)null;

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? long.Parse(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus.IsNotNull() && vm.MasterViewModel.DocumentStatus.Key.IsNotNullOrEmpty() ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short)Services.Contracts.Enums.DocumentStatuses.Draft;
                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;

                transaction.TransactionHead.DeliveryMethodID = vm.MasterViewModel.DeliveryMethod != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryMethod.Key) ? Convert.ToByte(vm.MasterViewModel.DeliveryMethod.Key) : default(short) : default(short);

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceHeadID;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.IgnoreEntitlementCheck = true;
                // Map TransactionHeadEntitlementMaps
                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                //{
                //    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                //    {
                //        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                //        {
                //            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                //            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                //            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                //            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                //            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                //            transactionHeadEntitlementMapDTO.Amount = item.Amount;

                //            // add into TransactionHeadEntitlementMaps list
                //            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                //        }
                //    }
                //}


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);

                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                            //transaction.TransactionHead.DeliveryCharge = transactionDetail.DeliveryCharge.IsNotNull() ? transactionDetail.DeliveryCharge : vm.MasterViewModel.DeliveryCharge.IsNotNull() ? vm.MasterViewModel.DeliveryCharge : (decimal?)null;
                        }
                    }
                }

                //if (vm.MasterViewModel.DeliveryDetails.IsNotNull())
                //{
                //    transaction.OrderContactMaps.Add(DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails));
                //}

                //if (vm.MasterViewModel.BillingDetails.IsNotNull())
                //{
                //    if (vm.MasterViewModel.BillingDetails.ContactPerson.IsNull() && vm.MasterViewModel.BillingDetails.BillingAddress.IsNull()
                //        && vm.MasterViewModel.BillingDetails.bMobileNo.IsNull() && vm.MasterViewModel.BillingDetails.LandLineNo.IsNull()
                //        && vm.MasterViewModel.BillingDetails.SpecialInstructions.IsNull() && vm.MasterViewModel.BillingDetails.Area.IsNull())
                //    {
                //        OrderContactMapDTO ocmDTO = DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                //        ocmDTO.OrderContactMapID = 0;
                //        ocmDTO.IsBillingAddress = true;
                //        ocmDTO.IsShippingAddress = false;

                //        transaction.OrderContactMaps.Add(ocmDTO);
                //    }
                //    else
                //    {
                //        transaction.OrderContactMaps.Add(BillingAddressViewModel.ToDTO(vm.MasterViewModel.BillingDetails));
                //    }
                //}

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }


        #region FOC Sales
        public static TransactionDTO ToFOCSalesDTO(FOCSalesViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }

                if (vm.MasterViewModel.Currency.Key == null)
                {
                    throw new Exception("Select Any Currency!");
                }

                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }

                if ((vm.MasterViewModel.Customer == null || vm.MasterViewModel.Customer.Key == null) && (vm.MasterViewModel.Student == null || vm.MasterViewModel.Student.Key == null)
                    && (vm.MasterViewModel.Employee == null || vm.MasterViewModel.Employee.Key == null))
                {
                    throw new Exception("Select Customer,Staff or Student!");
                }

                //if (vm.DetailViewModel.Sum(y => y.Amount) == 0)
                //{
                //    throw new Exception("Please enter product details");
                //}

                //if (vm.DetailViewModel.FindAll(x => (x.Amount == 0) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                //{
                //    throw new Exception("Amount should not be zero for the selected product !");
                //}

                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }

                //if (vm.MasterViewModel.TransactionHeadEntitlementMaps.Sum(x => x.Amount) != (vm.DetailViewModel.Sum(x => x.Amount) - (vm.MasterViewModel.Discount ?? 0)))
                //{
                //    throw new Exception("Payment Amount should be equal to the grand total!");
                //}

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;

                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;

                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                //transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.Reference = vm.MasterViewModel.Reference;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                //transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);

                // here suplier variable is customer
                transaction.TransactionHead.CustomerID = vm.MasterViewModel.Customer != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Customer.Key) ? Convert.ToInt32(vm.MasterViewModel.Customer.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StudentID = vm.MasterViewModel.Student != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Student.Key) ? Convert.ToInt32(vm.MasterViewModel.Student.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.StaffID = vm.MasterViewModel.Employee != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Employee.Key) ? Convert.ToInt32(vm.MasterViewModel.Employee.Key) : (long?)null : (long?)null;

                //transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                transaction.TransactionHead.DeliveryMethodID = null;// Convert.ToInt16(vm.MasterViewModel.DeliveryMethod.Key);
                transaction.TransactionHead.DeliveryTypeName = null; //vm.MasterViewModel.DeliveryMethod.Value;
                transaction.TransactionHead.DeliveryDate = vm.MasterViewModel.DeliveryDate != null ? DateTime.ParseExact(vm.MasterViewModel.DeliveryDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID;
                transaction.TransactionHead.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadID;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                //transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.EmployeeID = vm.MasterViewModel.SalesMan != null ? !string.IsNullOrEmpty(vm.MasterViewModel.SalesMan.Key) ? long.Parse(vm.MasterViewModel.SalesMan.Key) : 0 : 0;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.DeliveryCharge = vm.MasterViewModel.DeliveryCharge;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;
                transaction.TransactionHead.ReferenceTransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                // Map TransactionHeadEntitlementMaps
                if (vm.MasterViewModel.TransactionHeadEntitlementMaps.IsNotNull() && vm.MasterViewModel.TransactionHeadEntitlementMaps.Count > 0)
                {
                    foreach (var item in vm.MasterViewModel.TransactionHeadEntitlementMaps)
                    {
                        if (item.Entitlement.IsNotNull() && !string.IsNullOrWhiteSpace(item.Entitlement.Key))
                        {
                            var transactionHeadEntitlementMapDTO = new TransactionHeadEntitlementMapDTO();
                            transactionHeadEntitlementMapDTO.TransactionHeadEntitlementMapID = item.TransactionHeadEntitlementMapID;
                            transactionHeadEntitlementMapDTO.TransactionHeadID = vm.MasterViewModel.TransactionHeadIID;
                            transactionHeadEntitlementMapDTO.EntitlementID = item.Entitlement != null ? Convert.ToByte(item.Entitlement.Key) : default(byte);
                            transactionHeadEntitlementMapDTO.EntitlementName = item.Entitlement != null ? item.Entitlement.Value : null;
                            transactionHeadEntitlementMapDTO.Amount = item.Amount;
                            transactionHeadEntitlementMapDTO.ReferenceNo = item.ReferenceNo;
                            // add into TransactionHeadEntitlementMaps list
                            transaction.TransactionHead.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMapDTO);
                        }
                    }
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            //transactionDetailDTO.ProductID = ;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.CostPrice = Convert.ToDecimal(transactionDetail.CostPrice);
                            transactionDetailDTO.TaxAmount1 = Convert.ToDecimal(transactionDetail.CGSTAmount);
                            transactionDetailDTO.TaxAmount2 = Convert.ToDecimal(transactionDetail.SGSTAmount);
                            transactionDetailDTO.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                            //transactionDetailDTO.CostCenterID = transactionDetail.CostCenter != null ? !string.IsNullOrEmpty(transactionDetail.CostCenter.Key) ? Convert.ToInt32(transactionDetail.CostCenter.Key) : (int?)null : (int?)null;

                            // transactionDetailDTO.UnitID = 1;//currently hard coding it to 1
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;

                            transactionDetailDTO.CostCenterID = transactionDetail.CostCenterID;
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            //transactionDetailDTO.ForeignRate = transactionDetail.ForeignRate;
                            //transactionDetailDTO.ForeignAmount = transactionDetail.ForeignAmount;
                            transactionDetailDTO.ExchangeRate = transactionDetail.ExchangeRate;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.WarrantyDate = transactionDetail.WarrantyDate != null ? Convert.ToDateTime(transactionDetail.WarrantyDate) : (DateTime?)null;

                            if (transactionDetail.SKUDetails != null)
                            {
                                // ProductSerialMapViewModel
                                foreach (var sku in transactionDetail.SKUDetails)
                                {
                                    // if (sku.SerialList.IsNotNull() && sku.SerialList.Value.IsNotNull())
                                    // {
                                    var dtoSKUDetail = new ProductSerialMapDTO();
                                    dtoSKUDetail.SerialNo = sku.SerialNo;
                                    dtoSKUDetail.ProductSKUMapID = sku.ProductSKUMapID;
                                    // add sku detail dto into list
                                    transactionDetailDTO.SKUDetails.Add(dtoSKUDetail);
                                    // }
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                if (transaction.TransactionDetails.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                if (vm.MasterViewModel.TaxDetails.IsNotNull())
                {
                    transaction.TransactionHead.TaxDetails = TaxDetailsViewModel.ToDTO(vm.MasterViewModel.TaxDetails.Taxes);
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }


        public static FOCSalesViewModel FromFOCSalesDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var focSales = new FOCSalesViewModel();
                focSales.MasterViewModel = new FOCSalesMasterViewModel();
                //focSales.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
                focSales.DetailViewModel = new List<FOCSalesDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                focSales.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                focSales.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                focSales.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                focSales.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                focSales.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                focSales.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                focSales.MasterViewModel.Remarks = dto.TransactionHead.Description;
                focSales.MasterViewModel.Reference = dto.TransactionHead.Reference;
                focSales.MasterViewModel.Currency = new KeyValueViewModel();
                focSales.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                focSales.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                focSales.MasterViewModel.Customer = new KeyValueViewModel();
                // here suplier variable is customer
                focSales.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.ToString();
                focSales.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerName;

                focSales.MasterViewModel.Student = new KeyValueViewModel();
                focSales.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.ToString();
                focSales.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;

                focSales.MasterViewModel.ClassSectionDescription = dto.TransactionHead.StudentClassSectionDescription;

                focSales.MasterViewModel.DeliveryMethod = new KeyValueViewModel();
                if (dto.TransactionHead.DeliveryMethodID > 0)
                {
                    focSales.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
                    focSales.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
                    //focSales.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                }

                //focSales.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                focSales.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                focSales.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                focSales.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                focSales.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                focSales.MasterViewModel.JobEntryHeadID = dto.TransactionHead.JobEntryHeadID;

                focSales.MasterViewModel.SalesMan = new KeyValueViewModel();

                if (dto.TransactionHead.EmployeeID > 0)
                {
                    focSales.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
                    focSales.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
                }

                focSales.MasterViewModel.Employee = new KeyValueViewModel();
                if (dto.TransactionHead.StaffID > 0)
                {
                    focSales.MasterViewModel.Employee.Key = dto.TransactionHead.StaffID.ToString();
                    focSales.MasterViewModel.Employee.Value = dto.TransactionHead.StaffName;
                }

                focSales.MasterViewModel.DocumentStatus = new KeyValueViewModel();

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    focSales.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    focSales.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                focSales.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                focSales.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;
                focSales.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
                focSales.MasterViewModel.DeliveryCharge = dto.TransactionHead.DeliveryCharge > 0 ? dto.TransactionHead.DeliveryCharge.Value : 0;
                focSales.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                focSales.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;
                focSales.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

                focSales.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                focSales.IsError = dto.TransactionHead.IsError;
                focSales.ErrorCode = dto.TransactionHead.ErrorCode;

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    focSales.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionHead.TaxDetails != null && dto.TransactionHead.TaxDetails.Count > 0)
                {
                    focSales.MasterViewModel.TaxDetails = new TaxDetailsViewModel() { Taxes = dto.TransactionHead.TaxDetails.Select(x => TaxDetailsViewModel.ToVM(x)).ToList() };
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var focSalesDetail = new FOCSalesDetailViewModel();
                        focSalesDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        focSalesDetail.TransactionDetailID = transactionDetail.DetailIID;
                        focSalesDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        focSalesDetail.SKUID = new KeyValueViewModel();
                        focSalesDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        focSalesDetail.SKUID.Value = transactionDetail.SKU;
                        focSalesDetail.Description = transactionDetail.SKU;
                        focSalesDetail.PartNo = transactionDetail.PartNo;
                        focSalesDetail.Quantity = transactionDetail.Quantity;
                        focSalesDetail.Amount = transactionDetail.Amount;
                        focSalesDetail.UnitPrice = transactionDetail.UnitPrice;
                        focSalesDetail.CostPrice = transactionDetail.CostPrice;
                        focSalesDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        focSalesDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        focSalesDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        focSalesDetail.CGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount1);
                        focSalesDetail.SGSTAmount = Convert.ToInt32(transactionDetail.TaxAmount2);
                        focSalesDetail.Fraction = Convert.ToDouble(transactionDetail.Fraction);
                        focSalesDetail.ExchangeRate = transactionDetail.ExchangeRate;
                        focSalesDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        focSalesDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        focSalesDetail.ProductCode = transactionDetail.ProductCode;
                        focSalesDetail.DocumentTypeID = dto.TransactionHead.DocumentTypeID;

                        focSalesDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                focSalesDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        focSalesDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                focSalesDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        focSalesDetail.IsOnEdit = true;

                        if (transactionDetail.WarrantyDate.IsNull())
                        {
                            focSalesDetail.WarrantyDate = Convert.ToDateTime(transactionDetail.WarrantyDate).ToString(dateFormat, CultureInfo.InvariantCulture);
                        }

                        focSalesDetail.CostCenterID = transactionDetail.CostCenterID;
                        focSalesDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        focSalesDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {

                            foreach (var dtoProductSerialMap in transactionDetail.SKUDetails)
                            {
                                var productSerialMap = new ProductSerialMapViewModel();
                                productSerialMap.ProductSerialID = dtoProductSerialMap.ProductSerialID;
                                productSerialMap.SerialNo = dtoProductSerialMap.SerialNo;
                                productSerialMap.ProductSKUMapID = dtoProductSerialMap.ProductSKUMapID;
                                productSerialMap.TimeStamps = dtoProductSerialMap.TimeStamps;
                                focSalesDetail.SKUDetails.Add(productSerialMap);
                            }
                        }
                        else
                            focSalesDetail.SKUDetails.Add(new ProductSerialMapViewModel()
                            {
                                ProductSKUMapID = (long)transactionDetail.ProductSKUMapID,
                                SerialNo = transactionDetail.SerialNumber
                                /*SerialList = new KeyValueViewModel() { Key = transactionDetail.SerialNumber, Value = transactionDetail.SerialNumber }*/
                            });

                        focSales.DetailViewModel.Add(focSalesDetail);
                    }
                }

                if (dto.TransactionHead.TaxDetails.IsNotNull())
                {
                    focSales.MasterViewModel.TaxDetails = TaxDetailsViewModel.ToVM(dto.TransactionHead.TaxDetails, null);
                }


                return focSales;
            }
            else
            {
                return new FOCSalesViewModel();
            }
        }

        #endregion

        #region Purchase Request -- Start

        #region FromViewModel -- Start

        public static TransactionDTO ToPurchaseRequestDTO(PurchaseRequestViewModel vm)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (vm != null)
            {
                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                transaction.OrderContactMap = new OrderContactMapDTO();

                if (vm.DetailViewModel.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.DocumentType.Key == null)
                {
                    throw new Exception("Select Document Type!");
                }


                if (vm.MasterViewModel.DocumentStatus.Key == null)
                {
                    throw new Exception("Select Any Document Status!");
                }

                if (vm.MasterViewModel.Requisitioned.Key == null)
                {
                    throw new Exception("Please select Requisitioned by!");
                }


                if (vm.DetailViewModel.FindAll(x => (x.Unit == null || x.Unit.Key == null) && !(x.SKUID == null || x.SKUID.Key == null)).Count() > 0)
                {
                    throw new Exception("Please select unit for the selected product !");
                }

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = vm.MasterViewModel.Branch != null && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.ApproverID = vm.MasterViewModel.Approver != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Approver.Key) ? Convert.ToInt32(vm.MasterViewModel.Approver.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.EmployeeID = Convert.ToInt32(vm.MasterViewModel.Requisitioned.Key);
                transaction.TransactionHead.DepartmentID = vm.MasterViewModel.DepartmentID;
                transaction.TransactionHead.ApproverName = vm.MasterViewModel.Requisitioned.Value;
                transaction.TransactionHead.DeliveryMethodID = null;// Convert.ToInt16(vm.MasterViewModel.DeliveryMethod.Key);
                transaction.TransactionHead.DeliveryTypeName = null; //vm.MasterViewModel.DeliveryMethod.Value;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentReferenceTypeID = vm.MasterViewModel.DocumentReferenceTypeID;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;

                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount;


                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.ExpectedUnitPrice);
                            transactionDetailDTO.ActualUnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.Remark = transactionDetail.Remarks;

                            if (transactionDetail.SKUDetails != null)
                            {
                                foreach (var sku in transactionDetail.SKUDetails)
                                {
                                    var dtoSKUDetail = new ProductSerialMapDTO();
                                    dtoSKUDetail.SerialNo = sku.SerialNo;
                                    dtoSKUDetail.ProductSKUMapID = sku.ProductSKUMapID;
                                    transactionDetailDTO.SKUDetails.Add(dtoSKUDetail);
                                }
                            }

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transaction;
            }
            else
            {
                return new TransactionDTO();
            }
        }

        #endregion -- END


        #region FromDTO -- Start

        public static PurchaseRequestViewModel FromPurchaseRequestDTO(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseReq = new PurchaseRequestViewModel();
                purchaseReq.MasterViewModel = new PurchaseRequestMasterViewModel();
                purchaseReq.DetailViewModel = new List<PurchaseRequestDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseReq.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseReq.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseReq.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseReq.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseReq.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseReq.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseReq.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseReq.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = null };

                purchaseReq.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseReq.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);

                purchaseReq.MasterViewModel.Approver = new KeyValueViewModel();
                if (dto.TransactionHead.ApproverID > 0)
                {
                    purchaseReq.MasterViewModel.Approver.Key = dto.TransactionHead.ApproverID.ToString();
                    purchaseReq.MasterViewModel.Approver.Value = dto.TransactionHead.ApproverName;
                }

                purchaseReq.MasterViewModel.Requisitioned = new KeyValueViewModel();
                if (dto.TransactionHead.EmployeeID > 0)
                {
                    purchaseReq.MasterViewModel.Requisitioned.Key = dto.TransactionHead.EmployeeID.ToString();
                    purchaseReq.MasterViewModel.Requisitioned.Value = dto.TransactionHead.EmployeeName;
                }

                purchaseReq.MasterViewModel.RequisitionedDepartment = dto.TransactionHead.Department;

                purchaseReq.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseReq.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseReq.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                purchaseReq.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                purchaseReq.MasterViewModel.RequisitionedDepartment = dto.TransactionHead.Department;

                purchaseReq.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount;
                purchaseReq.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage;

                purchaseReq.IsError = dto.TransactionHead.IsError;
                purchaseReq.ErrorCode = dto.TransactionHead.ErrorCode;


                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var requestDetail = new PurchaseRequestDetailViewModel();
                        requestDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        requestDetail.TransactionDetailID = transactionDetail.DetailIID;
                        requestDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        requestDetail.SKUID = new KeyValueViewModel();
                        requestDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        requestDetail.SKUID.Value = transactionDetail.SKU;
                        requestDetail.Description = transactionDetail.SKU;
                        requestDetail.PartNo = transactionDetail.PartNo;
                        requestDetail.Quantity = transactionDetail.Quantity;
                        requestDetail.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                        requestDetail.ForeignRate = Convert.ToDecimal(transactionDetail.UnitPrice);
                        requestDetail.ExpectedUnitPrice = transactionDetail.UnitPrice;
                        requestDetail.Amount = transactionDetail.Amount;
                        requestDetail.UnitPrice = transactionDetail.ActualUnitPrice;
                        requestDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        requestDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        requestDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        requestDetail.Unit = new KeyValueViewModel() { Key = transactionDetail.UnitID.ToString(), Value = transactionDetail.Unit };
                        requestDetail.UnitGroupID = transactionDetail.UnitGroupID;
                        requestDetail.ProductCode = transactionDetail.ProductCode;
                        requestDetail.DocumentTypeID = dto.TransactionHead.DocumentTypeID;
                        requestDetail.Remarks = transactionDetail.Remark;

                        requestDetail.UnitDTO = new List<KeyValueViewModel>();
                        if (transactionDetail.UnitDTO != null && transactionDetail.UnitDTO.Count > 0)
                        {
                            foreach (KeyValueDTO val in transactionDetail.UnitDTO)
                            {
                                requestDetail.UnitDTO.Add(new KeyValueViewModel()
                                {
                                    Key = val.Key,
                                    Value = val.Value
                                });
                            }
                        }
                        requestDetail.UnitList = new List<UnitsViewModel>();
                        if (transactionDetail.UnitList != null && transactionDetail.UnitList.Count > 0)
                        {
                            foreach (var val in transactionDetail.UnitList)
                            {
                                requestDetail.UnitList.Add(new UnitsViewModel()
                                {
                                    UnitID = val.UnitID,
                                    UnitGroupID = val.UnitGroupID,
                                    UnitCode = val.UnitCode,
                                    UnitName = val.UnitName,
                                    Fraction = val.Fraction
                                });
                            }
                        }

                        requestDetail.IsSerialNumber = transactionDetail.IsSerialNumber;
                        requestDetail.SKUDetails = new List<ProductSerialMapViewModel>();

                        if (transactionDetail.SKUDetails != null && transactionDetail.SKUDetails.Count > 0)
                        {

                            foreach (var dtoProductSerialMap in transactionDetail.SKUDetails)
                            {
                                var productSerialMap = new ProductSerialMapViewModel();
                                productSerialMap.ProductSerialID = dtoProductSerialMap.ProductSerialID;
                                productSerialMap.SerialNo = dtoProductSerialMap.SerialNo;
                                productSerialMap.ProductSKUMapID = dtoProductSerialMap.ProductSKUMapID;
                                productSerialMap.TimeStamps = dtoProductSerialMap.TimeStamps;
                                requestDetail.SKUDetails.Add(productSerialMap);
                            }
                        }
                        else
                            requestDetail.SKUDetails.Add(new ProductSerialMapViewModel()
                            {
                                ProductSKUMapID = (long)transactionDetail.ProductSKUMapID,
                                SerialNo = transactionDetail.SerialNumber
                            });

                        purchaseReq.DetailViewModel.Add(requestDetail);
                    }
                }

                return purchaseReq;
            }
            else
            {
                return new PurchaseRequestViewModel();
            }
        }

        #endregion -- END

        #endregion Purchase Request -- END


        #region Request for Quotation --- RFQ Start

        #region // From dto to viewmodel
        public static PurchaseQuotationViewModel FromDTOToPurchaseQuotationVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseQuotation = new PurchaseQuotationViewModel();
                purchaseQuotation.MasterViewModel = new PurchaseQuotationMasterViewModel();
                purchaseQuotation.DetailViewModel = new List<PurchaseQuotationDetailViewModel>();

                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                purchaseQuotation.MasterViewModel.IsError = dto.TransactionHead.IsError;
                purchaseQuotation.MasterViewModel.ErrorCode = dto.TransactionHead.ErrorCode;
                purchaseQuotation.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseQuotation.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseQuotation.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseQuotation.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseQuotation.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseQuotation.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? dto.TransactionHead.TransactionDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseQuotation.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? dto.TransactionHead.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                purchaseQuotation.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseQuotation.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                purchaseQuotation.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseQuotation.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                purchaseQuotation.MasterViewModel.JobStatus = Convert.ToString(dto.TransactionHead.JobStatusID != null ? dto.TransactionHead.JobStatusID : null);
                purchaseQuotation.MasterViewModel.Discount = dto.TransactionHead.DiscountAmount.IsNotNull() ? dto.TransactionHead.DiscountAmount : null;
                purchaseQuotation.MasterViewModel.DiscountPercentage = dto.TransactionHead.DiscountPercentage.IsNotNull() ? dto.TransactionHead.DiscountPercentage : null;
                purchaseQuotation.MasterViewModel.DocumentReferenceTypeID = Services.Contracts.Enums.DocumentReferenceTypes.PurchaseQuote;
                purchaseQuotation.MasterViewModel.SendMailNotification = dto.TransactionHead.SendMailNotification;

                purchaseQuotation.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseQuotation.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseQuotation.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                purchaseQuotation.MasterViewModel.ExchangeRate = dto.TransactionHead.ExchangeRate;

                purchaseQuotation.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

                if (dto.TransactionHead.DocumentStatusID.HasValue)
                {
                    purchaseQuotation.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseQuotation.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                if (dto.TransactionHead.TenderID.HasValue)
                {
                    purchaseQuotation.MasterViewModel.Tender.Key = dto.TransactionHead.TenderID.ToString();
                    purchaseQuotation.MasterViewModel.Tender.Value = dto.TransactionHead.Tender.Value.ToString() ;
                }

                purchaseQuotation.MasterViewModel.SupplierList = new List<KeyValueViewModel>();
                purchaseQuotation.MasterViewModel.SupplierList = dto.TransactionHead.SupplierList
                                                                .GroupBy(suplr => suplr.Key).Select(group => group.First())  
                                                                .Select(suplr => new KeyValueViewModel()
                                                                {
                                                                    Key = suplr.Key.ToString(),
                                                                    Value = suplr.Value.ToString()
                                                                }).ToList();

                purchaseQuotation.MasterViewModel.PurchaseRequests = new List<KeyValueViewModel>();
                purchaseQuotation.MasterViewModel.PurchaseRequests = dto.TransactionHead.PurchaseRequests
                                                                .GroupBy(req => req.Key).Select(group => group.First())
                                                                .Select(req => new KeyValueViewModel()
                                                                {
                                                                    Key = req.Key.ToString(),
                                                                    Value = req.Value.ToString()
                                                                }).ToList();

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var RFQDetail = new PurchaseQuotationDetailViewModel();

                        RFQDetail.TransactionDetailID = transactionDetail.DetailIID;
                        RFQDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        RFQDetail.SKUID = new KeyValueViewModel();
                        RFQDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        RFQDetail.SKUID.Value = transactionDetail.SKU;
                        RFQDetail.Description = transactionDetail.SKU;
                        RFQDetail.ProductCode = transactionDetail.ProductCode;
                        RFQDetail.Unit = transactionDetail.Unit;
                        RFQDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        RFQDetail.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                        RFQDetail.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                        RFQDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        RFQDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        RFQDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        RFQDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        RFQDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        RFQDetail.IsSerialNumberOnPurchase = transactionDetail.IsSerialNumberOnPurchase;
                        RFQDetail.ProductLength = transactionDetail.ProductLength;
                        RFQDetail.ProductTypeName = transactionDetail.ProductTypeName;
                        RFQDetail.Remark = transactionDetail.Remark;

                        purchaseQuotation.DetailViewModel.Add(RFQDetail);
                    }
                }

                return purchaseQuotation;
            }
            else return new PurchaseQuotationViewModel();
        }
        #endregion

        #region //Data From Viewmodel to dto
        public static TransactionDTO ToDTOFromPurchaseQuotationVM(PurchaseQuotationViewModel vm)
        {
            if (vm != null)
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                if (vm.DetailViewModel.Count == 0)
                {
                    throw new Exception("Select atleast one product!");
                }

                if (vm.MasterViewModel.Branch.Key == null)
                {
                    throw new Exception("Select Branch!");
                }

                if (vm.MasterViewModel.SupplierList == null)
                {
                    throw new Exception("Select Supplier!");
                }

                if (vm.MasterViewModel.PurchaseRequests == null)
                {
                    throw new Exception("Select Purchase request!");
                }

                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();
                transaction.TransactionDetails = new List<TransactionDetailDTO>();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = long.Parse(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = string.IsNullOrEmpty(vm.MasterViewModel.TransactionDate) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.TransactionDate, dateFormat, CultureInfo.InvariantCulture);
                transaction.TransactionHead.DueDate = string.IsNullOrEmpty(vm.MasterViewModel.Validity) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.Validity, dateFormat, CultureInfo.InvariantCulture);
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? short.Parse(vm.MasterViewModel.DocumentStatus.Key) : (short?)null : (short?)null;
                transaction.TransactionHead.DiscountAmount = vm.MasterViewModel.Discount.IsNotNull() ? vm.MasterViewModel.Discount : null;
                transaction.TransactionHead.DiscountPercentage = vm.MasterViewModel.DiscountPercentage.IsNotNull() ? vm.MasterViewModel.DiscountPercentage : null;
                transaction.TransactionHead.TenderID = vm.MasterViewModel.Tender != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Tender.Key) ? long.Parse(vm.MasterViewModel.Tender.Key) : (long?)null : (long?)null;
                transaction.TransactionHead.SendMailNotification = vm.MasterViewModel.SendMailNotification;
                transaction.TransactionHead.CurrencyID = vm.MasterViewModel.Currency != null ? !string.IsNullOrEmpty(vm.MasterViewModel.Currency.Key) ? Convert.ToInt32(vm.MasterViewModel.Currency.Key) : (int?)null : (int?)null;
                transaction.TransactionHead.ExchangeRate = vm.MasterViewModel.ExchangeRate;

                transaction.TransactionHead.SupplierList = new List<KeyValueDTO>();
                foreach (var suplr in vm.MasterViewModel.SupplierList)
                {
                    if (suplr.Key != null)
                    {
                        transaction.TransactionHead.SupplierList.Add(new KeyValueDTO()
                        {
                            Key = suplr.Key.ToString(),
                            Value = suplr.Value.ToString()
                        });
                    }
                }

                transaction.TransactionHead.PurchaseRequests = new List<KeyValueDTO>();
                foreach (var req in vm.MasterViewModel.PurchaseRequests)
                {
                    if (req.Key != null)
                    {
                        transaction.TransactionHead.PurchaseRequests.Add(new KeyValueDTO()
                        {
                            Key = req.Key.ToString(),
                            Value = req.Value.ToString()
                        });
                    }
                }

                transaction.TransactionHead.Tender = vm.MasterViewModel.Tender.Key != null ?
                                                    new KeyValueDTO() { Key = vm.MasterViewModel.Tender.Key.ToString(), Value = vm.MasterViewModel.Tender.Value.ToString() }
                                                    : new KeyValueDTO();

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID.Key != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead <= 0 ? vm.MasterViewModel.TransactionHeadIID : transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = transactionDetail.SKUID != null ? !string.IsNullOrEmpty(transactionDetail.SKUID.Key) ? Convert.ToInt32(transactionDetail.SKUID.Key) : (long?)null : (long?)null;
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Fraction = Convert.ToDecimal(transactionDetail.Fraction);
                            transactionDetailDTO.ForeignRate = Convert.ToDecimal(transactionDetail.ForeignRate);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transactionDetailDTO.UnitGroupID = transactionDetail.UnitGroupID;
                            transactionDetailDTO.Remark = transactionDetail.Remark;

                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }

                return transaction;
            }
            else return new TransactionDTO();
        }
        #endregion

        #endregion ---RFQ End
    }
}