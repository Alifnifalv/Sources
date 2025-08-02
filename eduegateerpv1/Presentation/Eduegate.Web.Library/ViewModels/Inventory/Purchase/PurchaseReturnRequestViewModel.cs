using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class PurchaseReturnRequestViewModel : BaseMasterViewModel
    {
        public PurchaseReturnRequestViewModel()
        {
            MasterViewModel = new PurchaseReturnRequestMasterViewModel();
            DetailViewModel = new List<PurchaseReturnRequestDetailViewModel>() { new PurchaseReturnRequestDetailViewModel() };
        }

        public PurchaseReturnRequestMasterViewModel MasterViewModel { get; set; }
        public List<PurchaseReturnRequestDetailViewModel> DetailViewModel { get; set; }

        public static TransactionDTO FromVMToTransactionDTO(PurchaseReturnRequestViewModel vm)
        {
            if (vm != null)
            {

                var transaction = new TransactionDTO();
                transaction.TransactionHead = new TransactionHeadDTO();

                transaction.TransactionHead.HeadIID = vm.MasterViewModel.TransactionHeadIID;
                transaction.TransactionHead.CompanyID = vm.MasterViewModel.CompanyID;
                transaction.TransactionHead.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                transaction.TransactionHead.BranchID = Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                transaction.TransactionHead.TransactionNo = vm.MasterViewModel.TransactionNo;
                transaction.TransactionHead.TransactionDate = vm.MasterViewModel.TransactionDate != null ? Convert.ToDateTime(vm.MasterViewModel.TransactionDate) : (DateTime?)null;
                transaction.TransactionHead.DueDate = vm.MasterViewModel.Validity != null ? Convert.ToDateTime(vm.MasterViewModel.Validity) : (DateTime?)null;
                transaction.TransactionHead.Description = vm.MasterViewModel.Remarks;
                transaction.TransactionHead.CurrencyID = Convert.ToInt32(vm.MasterViewModel.Currency.Key);
                transaction.TransactionHead.SupplierID = Convert.ToInt32(vm.MasterViewModel.Supplier.Key);
                //transaction.TransactionHead.DeliveryTypeID = Convert.ToInt32(vm.MasterViewModel.DeliveryType);
                transaction.TransactionHead.IsShipment = vm.MasterViewModel.IsShipment;
                //transaction.TransactionHead.EntitlementID = Convert.ToInt16(vm.MasterViewModel.Entitlements);
                transaction.TransactionHead.ReferenceHeadID = vm.MasterViewModel.ReferenceTransactionHeaderID != null ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionHeaderID) : (long?)null;
                transaction.TransactionHead.CreatedBy = vm.CreatedBy;
                transaction.TransactionHead.UpdatedBy = vm.UpdatedBy;
                //transaction.TransactionHead.TransactionStatusID = Convert.ToByte(vm.MasterViewModel.TransactionStatus);

                transaction.TransactionHead.DeliveryTypeID = vm.MasterViewModel.DeliveryType != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DeliveryType.Key) ? Convert.ToInt32(vm.MasterViewModel.DeliveryType.Key) : (int?)null : (int?)null;

                transaction.TransactionHead.TransactionStatusID = vm.MasterViewModel.TransactionStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.TransactionStatus.Key) ? byte.Parse(vm.MasterViewModel.TransactionStatus.Key) : (byte?)null : (byte?)null;
                transaction.TransactionHead.DocumentStatusID = vm.MasterViewModel.DocumentStatus != null ? !string.IsNullOrEmpty(vm.MasterViewModel.DocumentStatus.Key) ? byte.Parse(vm.MasterViewModel.DocumentStatus.Key) : (byte?)null : (byte?)null;
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

                transaction.TransactionDetails = new List<TransactionDetailDTO>();
                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.SKUID != null && transactionDetail.SKUID != null)
                        {
                            var transactionDetailDTO = new TransactionDetailDTO();

                            transactionDetailDTO.DetailIID = transactionDetail.TransactionDetailID;
                            transactionDetailDTO.HeadID = transactionDetail.TransactionHead;
                            transactionDetailDTO.ProductSKUMapID = Convert.ToInt32(transactionDetail.SKUID.Key);
                            transactionDetailDTO.Quantity = Convert.ToDecimal(transactionDetail.Quantity);
                            transactionDetailDTO.Amount = Convert.ToDecimal(transactionDetail.Amount);
                            transactionDetailDTO.UnitPrice = Convert.ToDecimal(transactionDetail.UnitPrice);
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.CreatedBy = transactionDetail.CreatedBy;
                            transactionDetailDTO.UpdatedBy = transactionDetail.UpdatedBy;
                            transaction.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }
                }
                return transaction;
            }
            else return new TransactionDTO();
        }

        public static PurchaseReturnRequestViewModel FromTransactionDTOToVM(TransactionDTO dto)
        {
            if (dto != null)
            {
                var purchaseReturnRequest = new PurchaseReturnRequestViewModel();
                purchaseReturnRequest.MasterViewModel = new PurchaseReturnRequestMasterViewModel();
                purchaseReturnRequest.DetailViewModel = new List<PurchaseReturnRequestDetailViewModel>();

                var dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

                purchaseReturnRequest.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
                purchaseReturnRequest.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
                purchaseReturnRequest.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
                purchaseReturnRequest.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
                purchaseReturnRequest.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
                purchaseReturnRequest.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString("MM/dd/yyyy") : null;
                purchaseReturnRequest.MasterViewModel.Validity = dto.TransactionHead.DueDate != null ? Convert.ToDateTime(dto.TransactionHead.DueDate).ToString("MM/dd/yyyy") : null;
                purchaseReturnRequest.MasterViewModel.Remarks = dto.TransactionHead.Description;
                purchaseReturnRequest.MasterViewModel.Currency = new KeyValueViewModel();
                purchaseReturnRequest.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.ToString();
                purchaseReturnRequest.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
                purchaseReturnRequest.MasterViewModel.Supplier = new KeyValueViewModel();
                purchaseReturnRequest.MasterViewModel.Supplier.Key = dto.TransactionHead.SupplierID.ToString();
                purchaseReturnRequest.MasterViewModel.Supplier.Value = dto.TransactionHead.SupplierName;
                //purchaseReturnRequest.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();
                purchaseReturnRequest.MasterViewModel.IsShipment = dto.TransactionHead.IsShipment;
                //purchaseReturnRequest.MasterViewModel.Entitlement = dto.TransactionHead.EntitlementID.ToString();
                purchaseReturnRequest.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID.ToString();
                purchaseReturnRequest.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
                purchaseReturnRequest.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
                //purchaseReturnRequest.MasterViewModel.TransactionStatus = dto.TransactionHead.TransactionStatusID.ToString();
                purchaseReturnRequest.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;
                purchaseReturnRequest.MasterViewModel.DocumentStatus = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentStatusID.ToString(), Value = dto.TransactionHead.DocumentStatusName };
                //purchaseReturnRequest.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
                //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

                if (dto.TransactionHead.DeliveryTypeID > 0)
                {
                    purchaseReturnRequest.MasterViewModel.DeliveryType.Key = dto.TransactionHead.DeliveryTypeID.ToString();
                    purchaseReturnRequest.MasterViewModel.DeliveryType.Value = dto.TransactionHead.DeliveryTypeName;
                }

                if (dto.TransactionHead.TransactionStatusID > 0)
                {
                    purchaseReturnRequest.MasterViewModel.TransactionStatus.Key = dto.TransactionHead.TransactionStatusID.ToString();
                    purchaseReturnRequest.MasterViewModel.TransactionStatus.Value = dto.TransactionHead.TransactionStatusName;
                }

                if (dto.TransactionHead.DocumentStatusID > 0)
                {
                    purchaseReturnRequest.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
                    purchaseReturnRequest.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
                }

                // Map TransactionHeadEntitlementMapViewModel
                if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
                {
                    purchaseReturnRequest.MasterViewModel.TransactionHeadEntitlementMaps =
                        dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
                }

                if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
                {
                    foreach (var transactionDetail in dto.TransactionDetails)
                    {
                        var purchaseReturnRequestDetail = new PurchaseReturnRequestDetailViewModel();

                        purchaseReturnRequestDetail.TransactionDetailID = transactionDetail.DetailIID;
                        purchaseReturnRequestDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
                        purchaseReturnRequestDetail.SKUID = new KeyValueViewModel();
                        purchaseReturnRequestDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
                        purchaseReturnRequestDetail.SKUID.Value = transactionDetail.SKU;
                        purchaseReturnRequestDetail.Description = transactionDetail.SKU;
                        purchaseReturnRequestDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
                        purchaseReturnRequestDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
                        purchaseReturnRequestDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
                        purchaseReturnRequestDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
                        purchaseReturnRequestDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
                        purchaseReturnRequestDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
                        purchaseReturnRequestDetail.PartNo = transactionDetail.PartNo;
                        purchaseReturnRequest.DetailViewModel.Add(purchaseReturnRequestDetail);
                    }
                }

                return purchaseReturnRequest;
            }
            else return new PurchaseReturnRequestViewModel();
        }
    }
}
