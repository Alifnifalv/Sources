using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    public class JobOperationViewModel : BaseMasterViewModel
    {
        public JobOperationViewModel()
        {
            MasterViewModel = new JobOperationHeadViewModel();
            DetailViewModel = new List<JobOperationDetailViewModel>();
            Urls = new List<UrlViewModel>();
        }

        public JobOperationHeadViewModel MasterViewModel { get; set; }
        public List<JobOperationDetailViewModel> DetailViewModel { get; set; }
        public List<UrlViewModel> Urls { get; set; }
        public string Type { get; set; }
        public bool IsLoginUserJob { get; set; }

        public static List<JobOperationViewModel> FromJobEntryDTO(List<JobOperationHeadDTO> dtos)
        {
            var heads = new List<JobOperationViewModel>();

            foreach(var dto in dtos) {
                heads.Add(FromJobEntryDTO(dto));
            }

            return heads;
        }

        public static JobOperationViewModel FromJobEntryDTO(JobEntryHeadDTO dto)
        {
            var VM = new JobOperationViewModel()
            {
                MasterViewModel = new JobOperationHeadViewModel()
                {
                    TransactionHeadIID = dto.JobEntryHeadIID,
                    BranchID = dto.BranchID.Value,
                    DueDate = dto.JobEndDate.Value.ToString(),
                    DocumentTypeID = dto.DocumentTypeID.Value,
                    Remarks = dto.Remarks,
                    Description = dto.Description,
                    JobDate = dto.JobStartDate.Value.ToString(),
                },

                DetailViewModel = new List<JobOperationDetailViewModel>()
            };

            foreach (var detail in dto.JobEntryDetails)
            {
                VM.DetailViewModel.Add(new JobOperationDetailViewModel()
                {
                    Quantity = detail.Quantity.Value,
                    TransactionDetailID = detail.JobEntryDetailIID,
                    Description = detail.ProductSKUID.Value.ToString(),
                });
            }
            return VM;
        }

        public static JobOperationViewModel FromJobEntryDTO(JobOperationHeadDTO dto)
        {
            var rootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString();

            var vm = new JobOperationViewModel();
            vm.DetailViewModel = new List<JobOperationDetailViewModel>();

            if (dto.IsNotNull())
            {
                switch (dto.JobStatusID)
                {
                    case (int)JobOperationTypes.Receiving:
                        var receivingHeadModel = new JobReceivingHeadViewModel();
                        vm.MasterViewModel = receivingHeadModel;
                        (vm.MasterViewModel as JobReceivingHeadViewModel).GenerateInvoice = "";
                        break;
                    case (int)JobOperationTypes.Picking:
                        var pickingHeadModel = new JobPickingHeadViewModel();
                        vm.MasterViewModel = pickingHeadModel;
                        (vm.MasterViewModel as JobPickingHeadViewModel).Basket = new KeyValueViewModel();
                        (vm.MasterViewModel as JobPickingHeadViewModel).Basket.Key = dto.BasketID.ToString();
                        if (dto.BasketID > 0)
                        {
                            (vm.MasterViewModel as JobPickingHeadViewModel).Basket.Value = dto.BasketName;
                        }
                        break;
                    case (int)JobOperationTypes.StockOut:
                        var stockoutHeadModel = new JobStockOutHeadViewModel();
                        vm.MasterViewModel = stockoutHeadModel;
                        (vm.MasterViewModel as JobStockOutHeadViewModel).GenerateInvoice = "";
                        break;
                    case (int)JobOperationTypes.Packing:
                    case (int)JobOperationTypes.FailedReceiving:
                        var packingHeadModel = new JobPackingHeadViewModel();
                        vm.MasterViewModel = packingHeadModel;
                        (vm.MasterViewModel as JobPackingHeadViewModel).PacketNo = dto.PacketNo > 0 ? dto.PacketNo.ToString() : "";
                        break;
                    default:
                        break;
                }

                vm.MasterViewModel.TransactionHeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.BranchID = dto.BranchID;
                vm.MasterViewModel.Branch = dto.BranchName;
                vm.MasterViewModel.DueDate = dto.DeliveryDate.IsNotNull() ? dto.DeliveryDate.ToString() : null;
                vm.MasterViewModel.DocumentTypeID = dto.DocumentTypeID;
                vm.MasterViewModel.DocumentType = dto.DocumentTypeName;
                vm.MasterViewModel.Description = dto.Description;
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobDate = dto.JobDate.ToString();
                vm.MasterViewModel.EmployeeID = dto.EmployeeID;
                vm.MasterViewModel.EmployeeName = dto.EmployeeName;
                vm.MasterViewModel.JobNumber = dto.JobNumber;
                vm.MasterViewModel.JobPriority = dto.JobPriority.ToString();
                vm.MasterViewModel.JobPriorityID = dto.JobPriorityID;
                vm.MasterViewModel.LoginID = dto.LoginID;

                vm.MasterViewModel.JobOperationStatus = new KeyValueViewModel();

                if (dto.JobOperationStatus.IsNotNullOrEmpty() && dto.JobOperationStatusID.IsNotNull())
                {
                    vm.MasterViewModel.JobOperationStatus.Key = dto.JobOperationStatusID.ToString();
                    vm.MasterViewModel.JobOperationStatus.Value = dto.JobOperationStatus;
                    vm.MasterViewModel.IsPicked = dto.JobOperationStatusID > 1 ? true : false;
                }

                vm.MasterViewModel.ReferenceTransaction = dto.ReferenceTransaction.ToString();
                vm.MasterViewModel.JobStatusID = dto.JobStatusID;
                vm.MasterViewModel.JobSize = dto.JobSizeID.ToString();
                vm.MasterViewModel.OperationTypes =(JobOperationTypes)dto.OperationTypes;
                vm.MasterViewModel.ProcessStartDate = dto.ProcessStartDate.ToString();
                vm.MasterViewModel.ProcessEndDate = dto.ProcessEndDate.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;
                vm.MasterViewModel.ParentJobEntryHeadId = dto.ParentJobEntryHeadId > 0 ? dto.ParentJobEntryHeadId : null;

                vm = SetDefaultOperationStatus(vm, dto);

                if (dto.Detail.IsNotNull() && dto.Detail.Count > 0)
                {
                    var operationDetail = new JobOperationDetailViewModel();

                    foreach (var detail in dto.Detail)
                    {
                        operationDetail = new JobOperationDetailViewModel();

                        switch (dto.JobStatusID)
                        {
                            case (int)JobOperationTypes.Receiving:
                                var receivingDetailModel = new JobRecevingDetailViewModel();
                                operationDetail = receivingDetailModel;
                                (operationDetail as JobRecevingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity;
                                (operationDetail as JobRecevingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                (operationDetail as JobRecevingDetailViewModel).VerifyPrice = detail.UnitPrice.ToString();
                                (operationDetail as JobRecevingDetailViewModel).VerifyBarCode = detail.BarCode.IsNotNull() ? detail.BarCode.ToString() : null;
                                break;
                            case (int)JobOperationTypes.Picking:
                                var pickingDetailModel = new JobPickingDetailViewModel();
                                operationDetail = pickingDetailModel;
                                (operationDetail as JobPickingDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                (operationDetail as JobPickingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity.IsNull() ? detail.Quantity : null; ;
                                (operationDetail as JobPickingDetailViewModel).VerifyLocation = detail.ValidatedLocationBarcode;
                                (operationDetail as JobPickingDetailViewModel).IsRemainingQuantity = Convert.ToBoolean(detail.IsQuantiyVerified);
                                //(operationDetail as JobPickingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            case (int)JobOperationTypes.PutAway:
                                var putawayDetailModel = new JobPutAwayDetailViewModel();
                                operationDetail = putawayDetailModel;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyQuantity = detail.ValidatedQuantity.IsNull() ? detail.Quantity : detail.ValidatedQuantity;
                                //(operationDetail as JobPutAwayDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyLocation = detail.ValidatedLocationBarcode;
                                break;
                            case (int)JobOperationTypes.StockOut:
                                var stockOutDetailModel = new JobStockoutDetailViewModel();
                                operationDetail = stockOutDetailModel;
                                (operationDetail as JobStockoutDetailViewModel).VerifyQuantity = detail.ValidatedQuantity.IsNull() ? detail.Quantity : detail.ValidatedQuantity;
                                //(operationDetail as JobStockoutDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            case (int)JobOperationTypes.Packing:
                                var packingDetailModel = new JobPackingDetailViewModel();
                                operationDetail = packingDetailModel;
                                (operationDetail as JobPackingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity.IsNull() ? detail.Quantity : detail.ValidatedQuantity;
                                (operationDetail as JobPackingDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                //(operationDetail as JobPackingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            case (int)JobOperationTypes.FailedReceiving:
                                var FailedReceivingDetailModel = new JobPackingDetailViewModel();
                                operationDetail = FailedReceivingDetailModel;
                                (operationDetail as JobPackingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity.IsNull() ? detail.Quantity : detail.ValidatedQuantity;
                                (operationDetail as JobPackingDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                //(operationDetail as JobPackingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            default:
                                break;
                        }

                        operationDetail.TransactionDetailID = detail.TransactionDetailID;
                        operationDetail.JobEntryHeadID = detail.JobEntryHeadID;
                        operationDetail.ProductSKUID = detail.ProductSKUID;
                        operationDetail.Description = detail.ProductDescription;
                        operationDetail.Quantity = detail.Quantity;
                        operationDetail.LocationBarcode = detail.LocationBarcode;
                        operationDetail.LocationID = detail.LocationID;
                        operationDetail.UnitPrice = detail.UnitPrice;
                        operationDetail.BarCode = detail.BarCode;
                        operationDetail.Price = detail.Price;
                        operationDetail.PartNo = detail.PartNo;
                        operationDetail.ProductImage = detail.ProductImage.IsNotNull() ? string.Concat(rootUrl, "Products/", detail.ProductImage) : string.Empty;//detail.ProductIID, "/", (ImageType.SmallImage),
                        operationDetail.IsVerifyQuantity = detail.IsQuantiyVerified;
                        operationDetail.IsVerifyBarCode = detail.BarCode.IsNotNull() ? detail.BarCode == detail.ValidationBarCode ? true : false : false;
                        operationDetail.IsVerifyLocation = detail.LocationBarcode.IsNotNull() ? detail.LocationBarcode == detail.ValidatedLocationBarcode ? true : false : false;
                        operationDetail.IsVerifyPartNo = detail.PartNo.IsNotNull() ? detail.PartNo == detail.ValidatedPartNo ? true : false : false;
                        operationDetail.JobStatusID = detail.JobStatusID;
                        operationDetail.Remarks = detail.Remarks;
                        operationDetail.CreatedBy = detail.CreatedBy;
                        operationDetail.CreatedDate = detail.CreatedDate;
                        operationDetail.UpdatedBy = detail.UpdatedBy;
                        operationDetail.UpdatedDate = detail.UpdatedDate;
                        operationDetail.TimeStamps = detail.TimeStamps;

                        vm.DetailViewModel.Add(operationDetail);
                    }
                }
            }

            return vm;
        }

        public static JobOperationViewModel ToJobOperationVMFromJobEntryDTO(JobEntryHeadDTO dto)
        {
            var rootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString();

            var vm = new JobOperationViewModel();
            vm.DetailViewModel = new List<JobOperationDetailViewModel>();

            if (dto.IsNotNull())
            {
                switch (dto.JobStatusID)
                {
                    case (int)JobOperationTypes.Receiving:
                        var receivingHeadModel = new JobReceivingHeadViewModel();
                        vm.MasterViewModel = receivingHeadModel;
                        (vm.MasterViewModel as JobReceivingHeadViewModel).GenerateInvoice = "";
                        break;
                    case (int)JobOperationTypes.Picking:
                        var pickingHeadModel = new JobPickingHeadViewModel();
                        vm.MasterViewModel = pickingHeadModel;
                        (vm.MasterViewModel as JobPickingHeadViewModel).Basket = new KeyValueViewModel();
                        (vm.MasterViewModel as JobPickingHeadViewModel).Basket.Key = dto.BasketID.ToString();
                        if (dto.BasketID > 0)
                        {
                            (vm.MasterViewModel as JobPickingHeadViewModel).Basket.Value = dto.BasketName;
                        }
                        break;
                    case (int)JobOperationTypes.StockOut:
                        var stockoutHeadModel = new JobStockOutHeadViewModel();
                        vm.MasterViewModel = stockoutHeadModel;
                        (vm.MasterViewModel as JobStockOutHeadViewModel).GenerateInvoice = "";
                        break;
                    case (int)JobOperationTypes.Packing:
                    case (int)JobOperationTypes.FailedReceiving:
                        var packingHeadModel = new JobPackingHeadViewModel();
                        vm.MasterViewModel = packingHeadModel;
                        (vm.MasterViewModel as JobPackingHeadViewModel).PacketNo = dto.PacketNo > 0 ? dto.PacketNo.ToString() : "";
                        break;
                    default:
                        break;
                }

                vm.MasterViewModel.TransactionHeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.BranchID = Convert.ToInt32(dto.BranchID);
                vm.MasterViewModel.Branch = dto.BranchName;
                vm.MasterViewModel.DueDate = dto.JobEndDate.ToString();
                vm.MasterViewModel.DocumentTypeID = Convert.ToInt64(dto.DocumentTypeID);
                vm.MasterViewModel.DocumentType = dto.DocumentTypeName;
                vm.MasterViewModel.Description = dto.Description;
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobDate = dto.JobStartDate.ToString();
                vm.MasterViewModel.EmployeeID = dto.EmployeeID;
                vm.MasterViewModel.EmployeeName = dto.EmployeeName;
                vm.MasterViewModel.JobNumber = dto.JobNumber;
                vm.MasterViewModel.JobPriority = dto.Priority.ToString();
                vm.MasterViewModel.JobPriorityID = Convert.ToInt16(dto.PriorityID);
                vm.MasterViewModel.LoginID = dto.LoginID;

                vm.MasterViewModel.JobOperationStatus = new KeyValueViewModel();

                if (dto.JobOperationStatus.IsNotNullOrEmpty() && dto.JobOperationStatusID.IsNotNull())
                {
                    vm.MasterViewModel.JobOperationStatus.Key = dto.JobOperationStatusID.ToString();
                    vm.MasterViewModel.JobOperationStatus.Value = dto.JobOperationStatus;
                    vm.MasterViewModel.IsPicked = dto.JobOperationStatusID > 1 ? true : false;
                }

                vm.MasterViewModel.ReferenceTransactionNo = dto.TransactionHeadID.ToString();
                vm.MasterViewModel.ReferenceTransaction = dto.TransactionHeadID.ToString();
                vm.MasterViewModel.JobStatusID = dto.JobStatusID;
                vm.MasterViewModel.JobSize = dto.JobSizeID.ToString();
                vm.MasterViewModel.ProcessStartDate = dto.ProcessStartDate.ToString();
                vm.MasterViewModel.ProcessEndDate = dto.ProcessEndDate.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;

                if (dto.JobEntryDetails.IsNotNull() && dto.JobEntryDetails.Count > 0)
                {
                    var operationDetail = new JobOperationDetailViewModel();

                    foreach (var detail in dto.JobEntryDetails)
                    {
                        operationDetail = new JobOperationDetailViewModel();

                        switch (dto.JobStatusID)
                        {
                            case (int)JobOperationTypes.Receiving:
                                operationDetail = new JobRecevingDetailViewModel();
                                var vmJobRec = operationDetail as JobRecevingDetailViewModel;

                                if (vm != null)
                                {
                                    vmJobRec.VerifyQuantity = detail.ValidatedQuantity;
                                    vmJobRec.VerifyPartNo = detail.ValidatedPartNo;
                                    vmJobRec.VerifyPrice = detail.UnitPrice.ToString();
                                    vmJobRec.VerifyBarCode = detail.ValidationBarCode;
                                }
                                break;
                            case (int)JobOperationTypes.Picking:
                                var pickingDetailModel = new JobPickingDetailViewModel();
                                operationDetail = pickingDetailModel;
                                (operationDetail as JobPickingDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                (operationDetail as JobPickingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity;
                                (operationDetail as JobPickingDetailViewModel).VerifyLocation = detail.ValidatedLocationBarcode;
                                (operationDetail as JobPickingDetailViewModel).IsRemainingQuantity = Convert.ToBoolean(detail.IsQuantiyVerified);
                                //(operationDetail as JobPickingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            case (int)JobOperationTypes.PutAway:
                                var putawayDetailModel = new JobPutAwayDetailViewModel();
                                operationDetail = putawayDetailModel;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyQuantity = detail.ValidatedQuantity;
                                //(operationDetail as JobPutAwayDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                (operationDetail as JobPutAwayDetailViewModel).VerifyLocation = detail.ValidatedLocationBarcode;
                                break;
                            case (int)JobOperationTypes.StockOut:
                                var stockOutDetailModel = new JobStockoutDetailViewModel();
                                operationDetail = stockOutDetailModel;
                                (operationDetail as JobPickingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity;
                                //(operationDetail as JobPickingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            case (int)JobOperationTypes.Packing:
                            case (int)JobOperationTypes.FailedReceiving:
                                var packingDetailModel = new JobPackingDetailViewModel();
                                operationDetail = packingDetailModel;
                                (operationDetail as JobPackingDetailViewModel).VerifyQuantity = detail.ValidatedQuantity;
                                (operationDetail as JobPackingDetailViewModel).VerifyBarCode = detail.ValidationBarCode;
                                //(operationDetail as JobPackingDetailViewModel).VerifyPartNo = detail.ValidatedPartNo;
                                break;
                            default:
                                break;
                        }

                        operationDetail.TransactionDetailID = detail.JobEntryDetailIID;
                        operationDetail.JobEntryHeadID = Convert.ToInt32(detail.JobEntryHeadID);
                        operationDetail.ProductSKUID = detail.ProductSKUID;
                        operationDetail.Description = detail.ProductSkuName;
                        operationDetail.UnitPrice = detail.UnitPrice;
                        operationDetail.Quantity = detail.Quantity;
                        operationDetail.LocationID = detail.LocationID;
                        operationDetail.LocationBarcode = detail.LocationBarcode;
                        operationDetail.BarCode = detail.BarCode;
                        operationDetail.Price = detail.ProductPrice;
                        operationDetail.PartNo = detail.PartNo;
                        operationDetail.ProductImage = detail.ProductImage.IsNotNull() ? string.Concat(rootUrl, "Products/", detail.ProductImage) : string.Empty;
                        operationDetail.IsVerifyQuantity = detail.IsQuantiyVerified;
                        operationDetail.IsVerifyBarCode = detail.BarCode.IsNotNull() ? detail.BarCode == detail.ValidationBarCode ? true : false : false;
                        operationDetail.IsVerifyLocation = detail.LocationBarcode.IsNotNull() ? detail.LocationBarcode == detail.ValidatedLocationBarcode ? true : false : false;
                        operationDetail.IsVerifyPartNo = detail.PartNo.IsNotNull() ? detail.PartNo == detail.ValidatedPartNo ? true : false : false;
                        operationDetail.JobStatusID = detail.JobStatusID;
                        operationDetail.Remarks = detail.Remarks;
                        operationDetail.CreatedBy = detail.CreatedBy;
                        operationDetail.CreatedDate = detail.CreatedDate;
                        operationDetail.UpdatedBy = detail.UpdatedBy;
                        operationDetail.UpdatedDate = detail.UpdatedDate;
                        operationDetail.TimeStamps = detail.TimeStamps;

                        vm.DetailViewModel.Add(operationDetail);
                    }
                }
            }

            return vm;
        }

        public static JobEntryHeadDTO ToJobEntryDTOFromOperationHeadAndPutAwayDetailModel(JobOperationHeadViewModel operationHeadModel, List<JobPutAwayDetailViewModel> putAwayDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (operationHeadModel.IsNotNull() && putAwayDetails.Count > 0)
            {
                if (operationHeadModel.IsDoneFlag == true)
                {
                    if (operationHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess))
                    {
                        operationHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        operationHeadModel.JobStatusID = (int)JobOperationTypes.PutAway;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = operationHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = Convert.ToInt32(operationHeadModel.BranchID);
                jobEntryDTO.DocumentTypeID = Convert.ToInt32(operationHeadModel.DocumentTypeID);
                jobEntryDTO.JobNumber = operationHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = operationHeadModel.JobDate != null ? Convert.ToDateTime(operationHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = operationHeadModel.Remarks;
                jobEntryDTO.JobEndDate = operationHeadModel.DueDate != null ? Convert.ToDateTime(operationHeadModel.DueDate) : (DateTime?)null;

                if (operationHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(operationHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = operationHeadModel.EmployeeID;

                //jobEntryDTO.TransactionHeadID = operationHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(operationHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = operationHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(operationHeadModel.ReferenceTransaction) : (int?)null;
                jobEntryDTO.JobStatusID = operationHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = Convert.ToInt16(operationHeadModel.JobSize);
                jobEntryDTO.PriorityID = Convert.ToByte(operationHeadModel.JobPriorityID);
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(operationHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = operationHeadModel.ProcessStartDate != null ? Convert.ToDateTime(operationHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = operationHeadModel.ProcessEndDate != null ? Convert.ToDateTime(operationHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = operationHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = operationHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = operationHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = operationHeadModel.UpdatedDate;
                jobEntryDTO.TimeStamps = operationHeadModel.TimeStamps;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in putAwayDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = operationHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = detail.UnitPrice;
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = detail.LocationID;
                    operationDetail.IsQuantiyVerified = detail.IsVerifyQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    //operationDetail.ValidatedPartNo = detail.VerifyPartNo;
                    operationDetail.ValidationBarCode = detail.VerifyBarCode;
                    operationDetail.ValidatedLocationBarcode = detail.VerifyLocation;
                    operationDetail.CreatedBy = detail.CreatedBy;
                    operationDetail.CreatedDate = detail.CreatedDate.IsNotNull() ? Convert.ToDateTime(detail.CreatedDate) : (DateTime?)null;
                    operationDetail.UpdatedBy = detail.UpdatedBy;
                    operationDetail.UpdatedDate = detail.UpdatedDate.IsNotNull() ? Convert.ToDateTime(detail.UpdatedDate) : (DateTime?)null;
                    operationDetail.TimeStamps = detail.TimeStamps;

                    jobEntryDTO.JobEntryDetails.Add(operationDetail);
                }

                return jobEntryDTO;
            }
            else
            {
                return new JobEntryHeadDTO();
            }
        }

        public static JobEntryHeadDTO ToJobEntryDTOFromOperationHeadAndPackingDetailModel(JobPackingHeadViewModel operationHeadModel, List<JobPackingDetailViewModel> packingDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (operationHeadModel.IsNotNull() && packingDetails.Count > 0)
            {
                if (operationHeadModel.IsDoneFlag == true)
                {
                    if (operationHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess) ||
                        operationHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.Completed))
                    {
                        operationHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        operationHeadModel.JobStatusID = (int)JobOperationTypes.Packed;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = operationHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = operationHeadModel.BranchID > 0 ? Convert.ToInt32(operationHeadModel.BranchID) : (long?)null;
                jobEntryDTO.DocumentTypeID = operationHeadModel.DocumentTypeID > 0 ? Convert.ToInt32(operationHeadModel.DocumentTypeID) : (int?)null;
                jobEntryDTO.JobNumber = operationHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = operationHeadModel.JobDate != null ? Convert.ToDateTime(operationHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = operationHeadModel.Remarks;
                jobEntryDTO.JobEndDate = operationHeadModel.DueDate != null ? Convert.ToDateTime(operationHeadModel.DueDate) : (DateTime?)null;

                if (operationHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(operationHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = operationHeadModel.EmployeeID;

                //jobEntryDTO.TransactionHeadID = operationHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(operationHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = operationHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(operationHeadModel.ReferenceTransaction) : (int?)null;
                jobEntryDTO.JobStatusID = operationHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = operationHeadModel.JobSize.IsNotNull() ? Convert.ToInt16(operationHeadModel.JobSize) : (short?)null;
                jobEntryDTO.PriorityID = operationHeadModel.JobPriorityID.IsNotNull() ? Convert.ToByte(operationHeadModel.JobPriorityID) : (byte?)null;
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(operationHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = operationHeadModel.ProcessStartDate != null ? Convert.ToDateTime(operationHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = operationHeadModel.ProcessEndDate != null ? Convert.ToDateTime(operationHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = operationHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = operationHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = operationHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = operationHeadModel.UpdatedDate;
                jobEntryDTO.PacketNo = string.IsNullOrEmpty(operationHeadModel.PacketNo) ? (byte)0 : Convert.ToByte(operationHeadModel.PacketNo);
                jobEntryDTO.TimeStamps = operationHeadModel.TimeStamps;
                jobEntryDTO.ParentJobEntryHeadId = operationHeadModel.ParentJobEntryHeadId > 0 ? operationHeadModel.ParentJobEntryHeadId : null;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in packingDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = operationHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = detail.UnitPrice;
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = detail.LocationID;
                    operationDetail.IsQuantiyVerified = detail.IsVerifyQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    operationDetail.ValidationBarCode = detail.VerifyBarCode;
                    //operationDetail.ValidatedPartNo = detail.VerifyPartNo;
                    operationDetail.CreatedBy = detail.CreatedBy;
                    operationDetail.CreatedDate = detail.CreatedDate.IsNotNull() ? Convert.ToDateTime(detail.CreatedDate) : (DateTime?)null;
                    operationDetail.UpdatedBy = detail.UpdatedBy;
                    operationDetail.UpdatedDate = detail.UpdatedDate.IsNotNull() ? Convert.ToDateTime(detail.UpdatedDate) : (DateTime?)null;
                    operationDetail.TimeStamps = detail.TimeStamps;

                    jobEntryDTO.JobEntryDetails.Add(operationDetail);
                }

                return jobEntryDTO;
            }
            else
            {
                return new JobEntryHeadDTO();
            }
        }

        public static JobOperationViewModel SetDefaultOperationStatus(JobOperationViewModel vm, JobOperationHeadDTO dto)
        {
            if (dto.JobStatusID == (int)JobOperationTypes.PutAway && dto.JobOperationStatusID == (int)JobOperationStatusTypes.Completed)
            {
                vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                vm.MasterViewModel.JobOperationStatus.Value = JobOperationStatusTypes.NotStarted.ToString();
                vm.MasterViewModel.IsPicked = false;
            }

            //Picking
            if (dto.JobStatusID == (int)JobOperationTypes.Picking && dto.JobOperationStatusID == (int)JobOperationStatusTypes.Completed)
            {
                vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                vm.MasterViewModel.JobOperationStatus.Value = JobOperationStatusTypes.NotStarted.ToString();
                vm.MasterViewModel.IsPicked = false;
            }

            //Stock Out
            if (dto.JobStatusID == (int)JobOperationTypes.StockOut && dto.JobOperationStatusID == (int)JobOperationStatusTypes.Completed)
            {
                vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                vm.MasterViewModel.JobOperationStatus.Value = JobOperationStatusTypes.NotStarted.ToString();
                vm.MasterViewModel.IsPicked = false;
            }

            //Packing
            if (dto.JobStatusID == (int)JobOperationTypes.Packing && dto.JobOperationStatusID == (int)JobOperationStatusTypes.Completed)
            {
                vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                vm.MasterViewModel.JobOperationStatus.Value = JobOperationStatusTypes.NotStarted.ToString();
                vm.MasterViewModel.IsPicked = false;
            }

            return vm;
        }

    }
}
