using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.Enums;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MissionProcessing", "Model.MasterViewModel")]
    [DisplayName("Transaction Details")]
    public class MissionProcessingHeadViewModel : BaseMasterViewModel
    {
        public MissionProcessingHeadViewModel()
        {
            Documents = new DocumentViewViewModel();
            Address = new MissionProcessingAddressViewModel();
            OrderInfo = new OrderInformationViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job ID")]
        [Order(3)]
        public long HeadIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Job Number")]
        [Order(4)]
        public string JobNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Date")]
        [Order(5)]
        public string JobDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Due Date")]
        [Order(6)]
        public string DueDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Remarks")]
        [Order(7)]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction")]
        [Order(8)]
        public string ReferenceTransaction { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Priority")]
        [Order(9)]
        public string JobPriority { get; set; }

        public int? JobPriorityID { get; set; }

        public Nullable<long> EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Employee Name")]
        [Order(10)]
        public string EmployeeName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Notes")]
        [Order(11)]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "hours-remaining", "ng-bind=\"ShowRemainingHours(Model.MasterViewModel.DueDate)\"")]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Hours Remaining")]
        [Order(12)]
        public string HoursRemaining { get; set; }

        public JobOperationTypes OperationTypes { get; set; }
        public bool IsStatusProver { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Operation Status")]
        [Select2("JobOperationStatus", "Numeric", false, "OnJobOperationStatusChange($event, Model.MasterViewModel)")]
        [LookUp("LookUps.JobOperationStatus")]
        [Order(15)]
        public KeyValueViewModel JobOperationStatus { get; set; }

        public string Started { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "hours-remaining", "ng-bind=\"ShowHoursTaken(Model.MasterViewModel.Started)\"")]
        [DisplayName("Hours Taken")]
        [Order(12)]
        public string HoursTaken { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown,"", " ng-disabled= Model.MasterViewModel.IsStatusProver ")]
        [DisplayName("Reason")]
        [LookUp("LookUps.Reason")]
        public string Reason { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Cash Collected(for COD)")]
        [Order(13)]
        public bool IsCashCollected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GeoLocation, "", "ng-bind=\"GetLocationMap(Model.MasterViewModel.LocationLatitude, Model.MasterViewModel.LocationLongitude)\"")]
        [DisplayName("Location")]
        [Order(14)]
        public string GeoLocation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Amount")]
        [Order(15)]
        public string Amount { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "OrderInfo", "OrderInfo", "", "class='header-list three-column-header'")]
        [DisplayName("Delivery/Payment Details")]
        public OrderInformationViewModel OrderInfo { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents")]
        [DisplayName("Documents")]
        [LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "Model.Document")]
        public DocumentViewViewModel Documents { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "Model.MasterViewModel.Address")]
        [DisplayName("Address")]
        public MissionProcessingAddressViewModel Address { get; set; }

        public long BranchID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Branch")]
        public string BranchName { get; set; }

        public long DocumentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("DocumentType")]
        public string DocumentTypeName { get; set; }

        public long ReferenceTransactionNo { get; set; }

        public long JobStatus { get; set; }

        public long BasketID { get; set; }

        public string ProcessStartDate { get; set; }

        public string ProcessEndDate { get; set; }

        public Nullable<decimal> LocationLatitude { get; set; }

        public Nullable<decimal> LocationLongitude { get; set; }
        public Nullable<long> ParentJobEntryHeadId { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }  

        public bool IsCompletedOrFailed { get; set; }

        public static MissionProcessingViewModel FromJobOperationHeadDTO(JobOperationHeadDTO dto)
        {
            var rootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString();

            var vm = new MissionProcessingViewModel();

            if (dto.IsNotNull())
            {
                vm.MasterViewModel.HeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.BranchID = dto.BranchID;
                vm.MasterViewModel.BranchName = dto.BranchName;
                vm.MasterViewModel.DueDate = dto.DeliveryDate.ToString();
                vm.MasterViewModel.DocumentTypeID = dto.DocumentTypeID;
                vm.MasterViewModel.DocumentTypeName = dto.DocumentTypeName;
                vm.MasterViewModel.Description = dto.Description;
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobDate = dto.JobDate.ToString();
                vm.MasterViewModel.EmployeeID = dto.EmployeeID;
                vm.MasterViewModel.EmployeeName = dto.EmployeeName;
                vm.MasterViewModel.JobNumber = dto.JobNumber;
                vm.MasterViewModel.JobPriority = dto.JobPriority.ToString();
                vm.MasterViewModel.JobPriorityID = dto.JobPriorityID;
                vm.MasterViewModel.IsCashCollected = dto.IsCashCollected;
                vm.MasterViewModel.LocationLatitude = dto.LocationLatitude;
                vm.MasterViewModel.LocationLongitude = dto.LocationLongitude;
                vm.MasterViewModel.TransactionHeadID = dto.TransactionHeadId;
                vm.MasterViewModel.Amount = dto.Amount;
                vm.MasterViewModel.ReferenceTransaction = dto.TransactionNo;
                vm.MasterViewModel.Reason = dto.Reason;

                vm.MasterViewModel.JobOperationStatus = new KeyValueViewModel();

                if (dto.JobOperationStatus.IsNotNullOrEmpty() && dto.JobOperationStatusID.IsNotNull())
                {
                    vm.MasterViewModel.JobOperationStatus.Key = dto.JobOperationStatusID.ToString();
                    vm.MasterViewModel.JobOperationStatus.Value = dto.JobOperationStatus;

                    if (dto.JobOperationStatusID == (int)JobOperationStatusTypes.Completed 
                        || dto.JobOperationStatusID == (int)JobOperationStatusTypes.Failed)
                    {
                        vm.MasterViewModel.IsCompletedOrFailed = true;
                    }
                }
                else
                {
                    vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                    vm.MasterViewModel.JobOperationStatus.Value = "Not Started";
                }

                vm.MasterViewModel.ReferenceTransactionNo = dto.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(dto.ReferenceTransaction) : default(long);
                vm.MasterViewModel.JobStatus = dto.JobStatusID.IsNotNull() ? Convert.ToInt32(dto.JobStatusID) : default(long);
                vm.MasterViewModel.OperationTypes = (JobOperationTypes)dto.OperationTypes;
                vm.MasterViewModel.BasketID = dto.BasketID.IsNotNull() ? Convert.ToInt32(dto.BasketID) : default(long);
                vm.MasterViewModel.ProcessStartDate = dto.ProcessStartDate.ToString();
                vm.MasterViewModel.ProcessEndDate = dto.ProcessEndDate.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;
                vm.MasterViewModel.ParentJobEntryHeadId = dto.ParentJobEntryHeadId > 0 ? dto.ParentJobEntryHeadId : null;

                if (dto.Detail.IsNotNull() && dto.Detail.Count > 0)
                {
                    foreach (var detail in dto.Detail)
                    {
                        vm.DetailViewModel.Add(new MissionProcessingDetailViewModel()
                        {
                            TransactionDetailID = detail.TransactionDetailID,
                            HeadID = detail.JobEntryHeadID,
                            ProductSKUID = detail.ProductSKUID,
                            Description = detail.ProductDescription,
                            Quantity = detail.Quantity,
                            Amount = detail.Amount,
                            LocationID = detail.LocationID.IsNotNull() ? Convert.ToInt32(detail.LocationID) : default(long),
                            BarCode = detail.BarCode,
                            ProductImage = detail.ProductImage.IsNotNull() ? string.Concat(rootUrl, Eduegate.Framework.Enums.EduegateImageTypes.Products.ToString(),"/",detail.ProductImage) : string.Empty,
                            IsQuantiyVerified = detail.IsQuantiyVerified.IsNotNull() ? Convert.ToBoolean(detail.IsQuantiyVerified) : default(bool),
                            IsBarCodeVerified = detail.IsBarCodeVerified.IsNotNull() ? Convert.ToBoolean(detail.IsBarCodeVerified) : default(bool),
                            IsLocationVerified = detail.IsLocationVerified.IsNotNull() ? Convert.ToBoolean(detail.IsLocationVerified) : default(bool),
                            JobStatusID = detail.JobStatusID.IsNotNull() ? Convert.ToInt32(detail.JobStatusID) : default(long),
                            ValidatedQuantity = detail.ValidatedQuantity.IsNotNull() ? Convert.ToDecimal(detail.ValidatedQuantity) : default(decimal),
                            ValidatedLocationBarcode = detail.ValidatedLocationBarcode,
                            ValidatedPartNo = detail.ValidatedPartNo,
                            ValidationBarCode = detail.ValidationBarCode,
                            Remarks = detail.Remarks,
                            CreatedBy = detail.CreatedBy,
                            CreatedDate = detail.CreatedDate,
                            UpdatedBy = detail.UpdatedBy,
                            UpdatedDate = detail.UpdatedDate,
                            TimeStamps = detail.TimeStamps,
                        });
                    }
                }
            }

            return vm;
        }

        public static JobEntryHeadDTO FromVMToJobEntryDTO(MissionProcessingViewModel vm)
        {
            var rootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString();

            var dto = new JobEntryHeadDTO();
            dto.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (dto.IsNotNull())
            {
                dto.JobEntryHeadIID = vm.MasterViewModel.HeadIID;
                dto.BranchID = vm.MasterViewModel.BranchID;
                dto.BranchName = vm.MasterViewModel.BranchName;
                dto.JobEndDate = vm.MasterViewModel.DueDate.IsNotNull() ? Convert.ToDateTime(vm.MasterViewModel.DueDate) : (DateTime?)null;
                dto.DocumentTypeID = Convert.ToInt32(vm.MasterViewModel.DocumentTypeID);
                dto.DocumentTypeName = vm.MasterViewModel.DocumentTypeName;
                dto.Description = vm.MasterViewModel.Description;
                dto.Remarks = vm.MasterViewModel.Remarks;
                dto.JobStartDate = vm.MasterViewModel.JobDate.IsNotNull() ? Convert.ToDateTime(vm.MasterViewModel.JobDate) : (DateTime?)null;
                dto.EmployeeID = vm.MasterViewModel.EmployeeID;
                dto.EmployeeName = vm.MasterViewModel.EmployeeName;
                dto.JobNumber = vm.MasterViewModel.JobNumber;
                dto.PriorityID = Convert.ToByte(vm.MasterViewModel.JobPriorityID);
                dto.JobOperationStatusID = vm.MasterViewModel.JobOperationStatus.Key.IsNotNull() ? Convert.ToByte(vm.MasterViewModel.JobOperationStatus.Key) : (byte?)null;
                dto.IsCashCollected = vm.MasterViewModel.IsCashCollected;
                dto.TransactionHeadID = vm.MasterViewModel.ReferenceTransactionNo != default(long) ? vm.MasterViewModel.ReferenceTransactionNo : (long?)null;
                dto.JobStatusID = Convert.ToInt32(vm.MasterViewModel.JobStatus);
                dto.BasketID = vm.MasterViewModel.BasketID != default(long) ? Convert.ToInt32(vm.MasterViewModel.BasketID) : (int?)null;
                dto.ProcessStartDate = vm.MasterViewModel.ProcessStartDate.IsNotNull() ? Convert.ToDateTime(vm.MasterViewModel.ProcessStartDate) : (DateTime?)null;
                dto.ProcessEndDate = vm.MasterViewModel.ProcessEndDate.IsNotNull() ? Convert.ToDateTime(vm.MasterViewModel.ProcessEndDate) : (DateTime?)null;
                dto.CreatedBy = vm.MasterViewModel.CreatedBy;
                dto.CreatedDate = vm.MasterViewModel.CreatedDate;
                dto.UpdatedBy = vm.MasterViewModel.UpdatedBy;
                dto.UpdatedDate = vm.MasterViewModel.UpdatedDate;
                dto.TimeStamps = vm.MasterViewModel.TimeStamps;
                dto.ParentJobEntryHeadId = vm.MasterViewModel.ParentJobEntryHeadId > 0 ? vm.MasterViewModel.ParentJobEntryHeadId : null;
                dto.Reason = vm.MasterViewModel.Reason;

                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var detail in vm.DetailViewModel)
                    {
                        dto.JobEntryDetails.Add(new JobEntryDetailDTO()
                        {
                            JobEntryDetailIID = detail.TransactionDetailID,
                            JobEntryHeadID = detail.HeadID,
                            ProductSKUID = detail.ProductSKUID,
                            Quantity = detail.Quantity,
                            LocationID = detail.LocationID != default(long) ? Convert.ToInt32(detail.LocationID) : (int?)null,
                            BarCode = detail.BarCode,
                            ProductImage = detail.ProductImage.IsNotNull() ? string.Concat(rootUrl, detail.ProductImage) : string.Empty,
                            IsQuantiyVerified = detail.IsQuantiyVerified,
                            IsBarCodeVerified = detail.IsBarCodeVerified,
                            IsLocationVerified = detail.IsLocationVerified,
                            JobStatusID = detail.JobStatusID != default(long) ? Convert.ToInt32(detail.JobStatusID) : (int?)null,
                            ValidatedQuantity = detail.ValidatedQuantity,
                            ValidatedLocationBarcode = detail.ValidatedLocationBarcode,
                            ValidatedPartNo = detail.ValidatedPartNo,
                            ValidationBarCode = detail.ValidationBarCode,
                            Remarks = detail.Remarks,
                            CreatedBy = detail.CreatedBy,
                            CreatedDate = detail.CreatedDate,
                            UpdatedBy = detail.UpdatedBy,
                            UpdatedDate = detail.UpdatedDate,
                            TimeStamps = detail.TimeStamps,
                        });
                    }
                }
            }

            return dto;
        }

        public static MissionProcessingViewModel FromJobEntryDTOToVM(JobEntryHeadDTO dto)
        {
            var rootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl").ToString();

            var vm = new MissionProcessingViewModel();

            if (dto.IsNotNull())
            {
                vm.MasterViewModel.HeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.BranchID = dto.BranchID.IsNotNull() ? Convert.ToInt32(dto.BranchID) : default(long);
                vm.MasterViewModel.BranchName = dto.BranchName;
                vm.MasterViewModel.DueDate = dto.JobEndDate.ToString();
                vm.MasterViewModel.DocumentTypeID = dto.DocumentTypeID.IsNotNull() ? Convert.ToInt32(dto.DocumentTypeID) : default(long);
                vm.MasterViewModel.DocumentTypeName = dto.DocumentTypeName;
                vm.MasterViewModel.Description = dto.Description;
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobDate = dto.JobStartDate.ToString();
                vm.MasterViewModel.EmployeeID = dto.EmployeeID;
                vm.MasterViewModel.EmployeeName = dto.EmployeeName;
                vm.MasterViewModel.JobNumber = dto.JobNumber;
                vm.MasterViewModel.JobPriority = dto.Priority;
                vm.MasterViewModel.JobPriorityID = dto.PriorityID.IsNotNull() ? Convert.ToByte(dto.PriorityID) : default(int);

                vm.MasterViewModel.JobOperationStatus = new KeyValueViewModel();

                if (dto.JobOperationStatus.IsNotNullOrEmpty() && dto.JobOperationStatusID.IsNotNull())
                {
                    vm.MasterViewModel.JobOperationStatus.Key = dto.JobOperationStatusID.ToString();
                    vm.MasterViewModel.JobOperationStatus.Value = dto.JobOperationStatus;
                }
                else
                {
                    vm.MasterViewModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.NotStarted);
                    vm.MasterViewModel.JobOperationStatus.Value = "Not Started";
                }

                vm.MasterViewModel.ReferenceTransactionNo = dto.TransactionHeadID.IsNotNull() ? Convert.ToInt32(dto.TransactionHeadID) : default(long);
                vm.MasterViewModel.JobStatus = dto.JobStatusID.IsNotNull() ? Convert.ToInt32(dto.JobStatusID) : default(long);
                vm.MasterViewModel.OperationTypes = (JobOperationTypes)Enum.Parse(typeof(JobOperationTypes), dto.JobStatusID.Value.ToString());
                vm.MasterViewModel.BasketID = dto.BasketID.IsNotNull() ? Convert.ToInt32(dto.BasketID) : default(long);
                vm.MasterViewModel.ProcessStartDate = dto.ProcessStartDate.ToString();
                vm.MasterViewModel.ProcessEndDate = dto.ProcessEndDate.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;
                vm.MasterViewModel.ParentJobEntryHeadId = dto.ParentJobEntryHeadId > 0 ? dto.ParentJobEntryHeadId : null;
                vm.MasterViewModel.Reason = dto.Reason;

                if (dto.JobEntryDetails.IsNotNull() && dto.JobEntryDetails.Count > 0)
                {
                    foreach (var detail in dto.JobEntryDetails)
                    {
                        vm.DetailViewModel.Add(new MissionProcessingDetailViewModel()
                        {
                            TransactionDetailID = detail.JobEntryDetailIID,
                            HeadID = detail.JobEntryHeadID.IsNotNull() ? Convert.ToInt32(detail.JobEntryHeadID) : default(long),
                            ProductSKUID = detail.ProductSKUID,
                            Quantity = detail.Quantity,
                            LocationID = detail.LocationID.IsNotNull() ? Convert.ToInt32(detail.LocationID) : default(long),
                            BarCode = detail.BarCode,
                            ProductImage = detail.ProductImage.IsNotNull() ? string.Concat(rootUrl, detail.ProductImage) : string.Empty,
                            IsQuantiyVerified = detail.IsQuantiyVerified.IsNotNull() ? Convert.ToBoolean(detail.IsQuantiyVerified) : default(bool),
                            IsBarCodeVerified = detail.IsBarCodeVerified.IsNotNull() ? Convert.ToBoolean(detail.IsBarCodeVerified) : default(bool),
                            IsLocationVerified = detail.IsLocationVerified.IsNotNull() ? Convert.ToBoolean(detail.IsLocationVerified) : default(bool),
                            JobStatusID = detail.JobStatusID.IsNotNull() ? Convert.ToInt32(detail.JobStatusID) : default(long),
                            ValidatedQuantity = detail.ValidatedQuantity.IsNotNull() ? Convert.ToDecimal(detail.ValidatedQuantity) : default(decimal),
                            ValidatedLocationBarcode = detail.ValidatedLocationBarcode,
                            ValidatedPartNo = detail.ValidatedPartNo,
                            ValidationBarCode = detail.ValidationBarCode,
                            Remarks = detail.Remarks,
                            CreatedBy = detail.CreatedBy,
                            CreatedDate = detail.CreatedDate,
                            UpdatedBy = detail.UpdatedBy,
                            UpdatedDate = detail.UpdatedDate,
                            TimeStamps = detail.TimeStamps,
                        });
                    }
                }
            }

            return vm;
        }

        public static MissionProcessingAddressViewModel FromOrderContactMapDTOToVM(OrderContactMapDTO dto)
        {
            if (dto != null)
            {
                return new MissionProcessingAddressViewModel()
                {
                    ContactPerson = string.Concat(dto.FirstName, dto.MiddleName, " ", dto.LastName),
                    DeliveryAddress = dto.AddressName,
                    MobileNo = dto.MobileNo1,
                    LandLineNo = dto.PhoneNo1,
                    SpecialInstructions = dto.SpecialInstruction,
                    OrderContactMapID = dto.OrderContactMapID
                };
            }
            else return new MissionProcessingAddressViewModel();
        }
    }
}
