using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CreditNote", "CRUDModel.ViewModel")]
    [DisplayName("Credit Note Details")]
    public class CreditNoteViewModel : BaseMasterViewModel
    {

        public CreditNoteViewModel()
        {
            //Class = new KeyValueViewModel();
            Student = new KeyValueViewModel();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //CreditNoteDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            FeeTypes = new List<CreditNoteFeeTypeViewModel>() { new CreditNoteFeeTypeViewModel() };
        }
        public long CreditNoteIID { get; set; }
        public bool Status { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CreditNoteDate")]
        public string CreditNoteDateString { get; set; }

        public System.DateTime? CreditNoteDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width", "textleft")]
        [CustomDisplay("IsDebitNote")]
        public bool? IsDebitNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CreditNote/DebitNote No.")]
        public string CreditNoteNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }
               

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Student")]
        [Select2("Student", "Numeric", false, "GetGridLookUpsForSchoolCreditNote($event, $element, CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllStudents", "LookUps.AllStudents")]
        public KeyValueViewModel Student { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<CreditNoteFeeTypeViewModel> FeeTypes { get; set; }
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SchoolCreditNoteDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CreditNoteViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            List<KeyValueViewModel> StudentList = new List<KeyValueViewModel>();
            Mapper<SchoolCreditNoteDTO, CreditNoteViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<CreditNoteFeeTypeDTO, CreditNoteFeeTypeViewModel>.CreateMap();
            var feeDto = dto as SchoolCreditNoteDTO;
            var vm = Mapper<SchoolCreditNoteDTO, CreditNoteViewModel>.Map(feeDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.FeeTypes = new List<CreditNoteFeeTypeViewModel>();
            vm.CreditNoteDateString = (vm.CreditNoteDate.HasValue ? vm.CreditNoteDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.CreditNoteIID = feeDto.SchoolCreditNoteIID;
            vm.Description = feeDto.Description;
            vm.CreditNoteNumber = vm.CreditNoteNumber;
            vm.Status = feeDto.Status;
            vm.Student = feeDto.Student.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.Student.Key.ToString(), Value = feeDto.Student.Value };

            //vm.Class = feeDto.Class.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel()
            //{
            //    Key = feeDto.Class.Key.ToString(),
            //    Value = feeDto.Class.Value
            //};
            //vm.Section = feeDto.SectionID == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel()
            //{
            //    Key = feeDto.SectionID.ToString(),
            //    Value = feeDto.Section.Value
            //};
            //if (feeDto.Student != null && feeDto.Student.Count > 0)
            //{
            //    foreach (KeyValueDTO dtoStudent in feeDto.Student)
            //    {
            //        if (dtoStudent.Key != null)
            //            StudentList.Add(new KeyValueViewModel { Key = dtoStudent.Key, Value = dtoStudent.Value }
            //                );
            //    }
            //}
            // vm.Student = StudentList;
            foreach (var creditNoteFeedto in feeDto.CreditNoteFeeTypeMapDTO)
            {
                if (creditNoteFeedto.Amount.HasValue)
                {
                    vm.FeeTypes.Add(new CreditNoteFeeTypeViewModel()
                    {
                        CreditNoteFeeTypeMapIID = creditNoteFeedto.CreditNoteFeeTypeMapIID,
                        SchoolCreditNoteID = creditNoteFeedto.SchoolCreditNoteID,
                        FeeMaster = KeyValueViewModel.ToViewModel(creditNoteFeedto.FeeMaster),
                        FeePeriod = KeyValueViewModel.ToViewModel(creditNoteFeedto.FeePeriod),                      
                        Amount = creditNoteFeedto.Amount,
                        Status = creditNoteFeedto.Status,
                        Months= KeyValueViewModel.ToViewModel(creditNoteFeedto.Months),
                        Years = KeyValueViewModel.ToViewModel(creditNoteFeedto.Years),
                        InvoiceNo= KeyValueViewModel.ToViewModel(creditNoteFeedto.InvoiceNo),
                        FeeDueFeeTypeMapsID= creditNoteFeedto.FeeDueFeeTypeMapsID,
                        FeeDueMonthlySplitID=creditNoteFeedto.FeeDueMonthlySplitID
                    });

                }
            }
            return vm;

        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CreditNoteViewModel, SchoolCreditNoteDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            //Mapper<CreditNoteFeeTypeViewModel, CreditNoteFeeTypeDTO>.CreateMap();
            var dto = Mapper<CreditNoteViewModel, SchoolCreditNoteDTO>.Map(this);
            dto.Description = this.Description;
            dto.SchoolCreditNoteIID = this.CreditNoteIID;
            dto.CreditNoteNumber = this.CreditNoteNumber;
            dto.Status = this.Status;
            dto.CreditNoteFeeTypeMapDTO = new List<CreditNoteFeeTypeDTO>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            //foreach (KeyValueViewModel vm in this.Student)
            //{
            //    studentList.Add(new KeyValueDTO { Key = this.Student.Key, Value = this.Student.Value }
            //        );
            //}
            //dto.Student = studentList;
            dto.Student = string.IsNullOrEmpty(this.Student.Key) ? new KeyValueDTO { Key = null, Value = null } :
            new KeyValueDTO { Key = this.Student.Key, Value = this.Student.Value };
            //dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            //dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.CreditNoteDate = string.IsNullOrEmpty(this.CreditNoteDateString) ? (DateTime?)null : DateTime.ParseExact(this.CreditNoteDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.IsDebitNote = this.IsDebitNote;
            foreach (var feeMasterClass in this.FeeTypes)
            {
                if (feeMasterClass.Amount.HasValue)
                {

                    if (!string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key))
                    {
                        dto.CreditNoteFeeTypeMapDTO.Add(new CreditNoteFeeTypeDTO()
                        {
                            CreditNoteFeeTypeMapIID = feeMasterClass.CreditNoteFeeTypeMapIID,
                            SchoolCreditNoteID = feeMasterClass.SchoolCreditNoteID,
                            FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
                            Amount = feeMasterClass.Amount,
                            MonthID= string.IsNullOrEmpty(feeMasterClass.Months.Key) ? (int?)null : int.Parse(feeMasterClass.Months.Key),
                            YearID= string.IsNullOrEmpty(feeMasterClass.Years.Key) ? (int?)null : int.Parse(feeMasterClass.Years.Key),
                            FeePeriodID= string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),
                            //InvoiceNo = string.IsNullOrEmpty(feeMasterClass.InvoiceNo.Key) ? (int?)null : int.Parse(feeMasterClass.InvoiceNo.Key),
                            FeeDueFeeTypeMapsID = feeMasterClass.FeeDueFeeTypeMapsID,
                            FeeDueMonthlySplitID = feeMasterClass.FeeDueMonthlySplitID
                            //Status= feeMasterClass.Status,

                        });
                    }
                }
            }

            return dto;

        }

        //public static SchoolCreditNoteDTO FromSalesReturnDTO(TransactionDTO transdto)
        //{
        //    if (transdto != null)
        //    {
        //        var dto = new SchoolCreditNoteDTO();
        //        dto.Description ="Credit Note from Sales Retrun :-" +transdto.TransactionHead.TransactionNo;
        //        //dto.SchoolCreditNoteIID = this.CreditNoteIID;
        //        //dto.Status = this.Status;
        //        dto.CreditNoteFeeTypeMapDTO = new List<CreditNoteFeeTypeDTO>();
        //        var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        //        List<KeyValueDTO> studentList = new List<KeyValueDTO>();
        //        //foreach (KeyValueViewModel vm in this.Student)
        //        //{
        //        //    studentList.Add(new KeyValueDTO { Key = this.Student.Key, Value = this.Student.Value }
        //        //        );
        //        //}
        //        //dto.Student = studentList;
        //        dto.Student = !transdto.TransactionHead.StudentID.HasValue ? new KeyValueDTO { Key = null, Value = null } :
        //        new KeyValueDTO { Key = transdto.TransactionHead.StudentID.ToString(), Value = transdto.TransactionHead.StudentName };
        //        //dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
        //        //dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
        //        dto.CreditNoteDate = transdto.TransactionHead.TransactionDate ;
        //        dto.IsDebitNote = false;
        //        if (transdto.TransactionDetails != null && transdto.TransactionDetails.Count > 0)
        //        {
        //            foreach (var transactionDetail in transdto.TransactionDetails)
        //            {
        //                dto.CreditNoteFeeTypeMapDTO.Add(new CreditNoteFeeTypeDTO()
        //                {
        //                    //CreditNoteFeeTypeMapIID = feeMasterClass.CreditNoteFeeTypeMapIID,
        //                    //SchoolCreditNoteID = feeMasterClass.SchoolCreditNoteID,
        //                    //FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
        //                    Amount = transactionDetail.Amount,
        //                    MonthID = transdto.TransactionHead.TransactionDate.Value.Month,
        //                    YearID = transdto.TransactionHead.TransactionDate.Value.Year,
                            

        //                });
        //            }
        //        }
        //            if (feeMasterClass.Amount.HasValue)
        //            {

        //                if (!string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key))
        //                {
        //                    dto.CreditNoteFeeTypeMapDTO.Add(new CreditNoteFeeTypeDTO()
        //                    {
        //                        CreditNoteFeeTypeMapIID = feeMasterClass.CreditNoteFeeTypeMapIID,
        //                        SchoolCreditNoteID = feeMasterClass.SchoolCreditNoteID,
        //                        FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
        //                        Amount = feeMasterClass.Amount,
        //                        MonthID = string.IsNullOrEmpty(feeMasterClass.Months.Key) ? (int?)null : int.Parse(feeMasterClass.Months.Key),
        //                        YearID = string.IsNullOrEmpty(feeMasterClass.Years.Key) ? (int?)null : int.Parse(feeMasterClass.Years.Key),
        //                        FeePeriodID = string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),

        //                        //Status= feeMasterClass.Status,

        //                    });
        //                }
        //            }
        //        }


        //        var salesReturn = new SalesReturnViewModel();
        //        salesReturn.MasterViewModel = new SalesReturnMasterViewModel();
        //        //salesReturn.MasterViewModel.DeliveryDetails = new DeliveryAddressViewModel();
        //        salesReturn.DetailViewModel = new List<SalesReturnDetailViewModel>();

        //        var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        //        salesReturn.MasterViewModel.TransactionHeadIID = dto.TransactionHead.HeadIID;
        //        salesReturn.MasterViewModel.CompanyID = dto.TransactionHead.CompanyID;
        //        salesReturn.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.TransactionHead.DocumentTypeID.ToString(), Value = dto.TransactionHead.DocumentTypeName };
        //        salesReturn.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.TransactionHead.BranchID.ToString(), Value = dto.TransactionHead.BranchName };
        //        salesReturn.MasterViewModel.TransactionNo = dto.TransactionHead.TransactionNo;
        //        salesReturn.MasterViewModel.TransactionDate = dto.TransactionHead.TransactionDate != null ? Convert.ToDateTime(dto.TransactionHead.TransactionDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
        //        salesReturn.MasterViewModel.Remarks = dto.TransactionHead.Description;
        //        salesReturn.MasterViewModel.Reference = dto.TransactionHead.Reference;
        //        salesReturn.MasterViewModel.Currency = new KeyValueViewModel();
        //        salesReturn.MasterViewModel.Currency.Key = dto.TransactionHead.CurrencyID.HasValue ? dto.TransactionHead.CurrencyID.ToString() : null;
        //        salesReturn.MasterViewModel.Currency.Value = dto.TransactionHead.CurrencyName;
        //        salesReturn.MasterViewModel.Customer = new KeyValueViewModel();
        //        // here suplier variable is customer
        //        salesReturn.MasterViewModel.Customer.Key = dto.TransactionHead.CustomerID.HasValue ? dto.TransactionHead.CustomerID.ToString() : null;
        //        salesReturn.MasterViewModel.Customer.Value = dto.TransactionHead.CustomerID + "-" + dto.TransactionHead.CustomerName;

        //        salesReturn.MasterViewModel.Student = new KeyValueViewModel();
        //        salesReturn.MasterViewModel.Student.Key = dto.TransactionHead.StudentID.HasValue ? dto.TransactionHead.StudentID.ToString() : null;
        //        salesReturn.MasterViewModel.Student.Value = dto.TransactionHead.StudentName;
        //        //salesReturn.MasterViewModel.DeliveryType = dto.TransactionHead.DeliveryTypeID.ToString();

        //        //salesReturn.MasterViewModel.Entitlements = dto.TransactionHead.Entitlements.Count > 0
        //        //    ? KeyValueViewModel.FromDTO(dto.TransactionHead.Entitlements) : null;

        //        salesReturn.MasterViewModel.DeliveryDate = dto.TransactionHead.DeliveryDate != null ? Convert.ToDateTime(dto.TransactionHead.DeliveryDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
        //        salesReturn.MasterViewModel.CreatedBy = dto.TransactionHead.CreatedBy;
        //        salesReturn.MasterViewModel.UpdatedBy = Convert.ToInt32(dto.TransactionHead.UpdatedBy);
        //        salesReturn.MasterViewModel.ReferenceTransactionHeaderID = dto.TransactionHead.ReferenceHeadID;
        //        salesReturn.MasterViewModel.ReferenceTransactionNo = dto.TransactionHead.ReferenceTransactionNo;

        //        salesReturn.MasterViewModel.IsTransactionCompleted = dto.TransactionHead.IsTransactionCompleted;

        //        if (dto.TransactionHead.DeliveryMethodID > 0)
        //        {
        //            salesReturn.MasterViewModel.DeliveryMethod.Key = dto.TransactionHead.DeliveryMethodID.ToString();
        //            salesReturn.MasterViewModel.DeliveryMethod.Value = dto.TransactionHead.DeliveryTypeName;
        //        }

        //        if (dto.TransactionHead.EmployeeID > 0)
        //        {
        //            salesReturn.MasterViewModel.SalesMan.Key = dto.TransactionHead.EmployeeID.ToString();
        //            salesReturn.MasterViewModel.SalesMan.Value = dto.TransactionHead.EmployeeName;
        //            //salesInvoice.MasterViewModel.SalesMan = dto.TransactionHead.EmployeeID.ToString();
        //        }

        //        if (dto.TransactionHead.DocumentStatusID.HasValue)
        //        {
        //            salesReturn.MasterViewModel.DocumentStatus.Key = dto.TransactionHead.DocumentStatusID.ToString();
        //            salesReturn.MasterViewModel.DocumentStatus.Value = dto.TransactionHead.DocumentStatusName;
        //        }


        //        // Map TransactionHeadEntitlementMapViewModel
        //        if (dto.TransactionHead.TransactionHeadEntitlementMaps != null && dto.TransactionHead.TransactionHeadEntitlementMaps.Count > 0)
        //        {
        //            salesReturn.MasterViewModel.TransactionHeadEntitlementMaps =
        //                dto.TransactionHead.TransactionHeadEntitlementMaps.Select(x => TransactionHeadEntitlementMapViewModel.ToVm(x)).ToList();
        //        }

        //        if (dto.TransactionDetails != null && dto.TransactionDetails.Count > 0)
        //        {
        //            foreach (var transactionDetail in dto.TransactionDetails)
        //            {
        //                var salesReturnDetail = new SalesReturnDetailViewModel();

        //                salesReturnDetail.TransactionDetailID = transactionDetail.DetailIID;
        //                salesReturnDetail.TransactionHead = Convert.ToInt32(transactionDetail.HeadID);
        //                salesReturnDetail.SKUID = new KeyValueViewModel();
        //                salesReturnDetail.SKUID.Key = transactionDetail.ProductSKUMapID.ToString();
        //                salesReturnDetail.SKUID.Value = transactionDetail.SKU;
        //                salesReturnDetail.Description = transactionDetail.SKU;
        //                salesReturnDetail.Quantity = Convert.ToDouble(transactionDetail.Quantity);
        //                salesReturnDetail.Amount = Convert.ToDouble(transactionDetail.Amount);
        //                salesReturnDetail.UnitPrice = Convert.ToDouble(transactionDetail.UnitPrice);
        //                salesReturnDetail.UnitID = Convert.ToInt32(transactionDetail.UnitID);
        //                salesReturnDetail.CreatedBy = Convert.ToInt32(transactionDetail.CreatedBy);
        //                salesReturnDetail.UpdatedBy = Convert.ToInt32(transactionDetail.UpdatedBy);
        //                salesReturnDetail.PartNo = transactionDetail.PartNo;

        //                salesReturnDetail.CostCenterID = transactionDetail.CostCenterID;

        //                if (transactionDetail.CostCenter != null)
        //                {
        //                    salesReturnDetail.CostCenter = new KeyValueViewModel() { Key = transactionDetail.CostCenter.Key, Value = transactionDetail.CostCenter.Value };

        //                }

        //                salesReturn.DetailViewModel.Add(salesReturnDetail);
        //            }
        //        }

        //        // Map Delivery Detail
        //        if (dto.OrderContactMap.IsNotNull())
        //        {
        //            //salesReturn.MasterViewModel.DeliveryDetails = DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
        //        }

        //        return salesReturn;
        //    }
        //    else
        //    {
        //        return new SalesReturnViewModel();
        //    }
        //}
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolCreditNoteDTO>(jsonString);
        }

    }
}