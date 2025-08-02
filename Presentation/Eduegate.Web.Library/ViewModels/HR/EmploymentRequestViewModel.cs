using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Web.Library.ViewModels.HR
{
    public class EmploymentRequestViewModel : BaseMasterViewModel
    {
        public EmploymentRequestViewModel()
        {
            MasterViewModel = new EmploymentRequestMasterViewModel();
        }

        public EmploymentRequestMasterViewModel MasterViewModel { get; set; }

        public static EmploymentRequestViewModel ToVM(EmploymentRequestDTO dto)
        {
            var vm = new EmploymentRequestViewModel()
            {
                MasterViewModel = new EmploymentRequestMasterViewModel()
                {
                    BasicSalary = dto.BasicSalary,
                    //NAME = dto.NAME,
                    F_NAME = dto.F_Name,
                    M_NAME = dto.M_Name,
                    L_NAME = dto.L_Name,
                    PASSPORT_EXPIRY = dto.PassportExpiryDate.HasValue ? dto.PassportExpiryDate.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture) : null,
                    PASSPORT_ISSUE = dto.PassportIssueDate.HasValue ? dto.PassportIssueDate.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture) : null,
                    Location = KeyValueViewModel.ToViewModel(dto.Location),
                    DATE_OF_BIRTH = dto.DOB.HasValue ? dto.DOB.Value.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture) : null,
                    GENDER = dto.Gender.Key,
                    PASSPORT_NO = dto.PASSPORT_NO,
                    // ProposedIncrease = dto.ProposedIncrease,
                    Reason_Basic = dto.Reason_Basic,
                    RecuritRemark = dto.RecuritRemark,
                    StaffCar = dto.StaffCar,
                    Designation = KeyValueViewModel.ToViewModel(dto.Designation),
                    EmploymentType = KeyValueViewModel.ToViewModel(dto.EmploymentType),
                    Group = KeyValueViewModel.ToViewModel(dto.Group),
                    MainDesignation = KeyValueViewModel.ToViewModel(dto.MainDesignation),
                    Nationality = KeyValueViewModel.ToViewModel(dto.Nationality),
                    PayComp = KeyValueViewModel.ToViewModel(dto.PayComp),
                    ProductiveType = KeyValueViewModel.ToViewModel(dto.ProductiveType),
                    RecruitmentType = KeyValueViewModel.ToViewModel(dto.RecruitmentType),
                    //SalaryChangeAfterPeriod = KeyValueViewModel.ToViewModel(dto.SalaryChangeAfterPeriod),
                    Shop = KeyValueViewModel.ToViewModel(dto.Shop),
                    EmailID = dto.EmailID,
                    AlternativeEmailID = dto.AlternativeEmailID,
                    EmpPersonalRemarks = dto.EmpPersonalRemarks,
                    EMP_REQ_NO = dto.EMP_REQ_NO,
                    EMP_NO = dto.EMP_NO,
                    CIVILID = dto.CIVILID,
                    CRE_BY = dto.CRE_BY,
                    CRE_DT = dto.CRE_DT.HasValue ? dto.CRE_DT.Value.ToLongDateString() : null,
                    CRE_IP = dto.CRE_IP,
                    CRE_WEBUSER = dto.CRE_WEBUSER,
                    REQ_DT = dto.REQ_DT.HasValue ? dto.REQ_DT.Value.ToLongDateString() : null,
                    isBudgeted = dto.isbudgeted,
                    ReplacedEmployee = KeyValueViewModel.ToViewModel(dto.replacedEmployee),
                    MaritalStatus = KeyValueViewModel.ToViewModel(dto.Marital_Status),
                    //Designation = KeyValueViewModel.ToDTO(dto.Designation),
                    isNewRequest = dto.isNewRequest,
                    Agent = KeyValueViewModel.ToViewModel(dto.Agent),
                    ContractType = KeyValueViewModel.ToViewModel(dto.ContractType),
                    Period = dto.Period,

                    //ProposedIncreaseAmount = dto.ProposedIncrease.HasValue ? dto.ProposedIncrease.Value > 0 ? dto.ProposedIncrease.Value - dto.BasicSalary : default(decimal?) : default(decimal?),
                    //ProposedIncreasePercent = dto.ProposedIncrease.HasValue ? dto.ProposedIncrease.Value > 0 ? ((dto.ProposedIncrease.Value - dto.BasicSalary) * 100) / dto.BasicSalary : default(decimal?) : default(decimal?),
                    Photo = new Domain.Setting.SettingBL().GetSettingValue<string>("EmploymentRequestPath") + dto.Photo,
                    VisaCompany = KeyValueViewModel.ToViewModel(dto.VisaCompany),
                    QuotaType = KeyValueViewModel.ToViewModel(dto.QuotaType),
                    EmpProcessRequestStatus = KeyValueViewModel.ToViewModel(dto.EmpProcessRequestStatus),
                    EmpRequestStatus = KeyValueViewModel.ToViewModel(dto.EmpRequestStatus)
                },

            };
            var i = 2;
            foreach (var proincreases in dto.ProposedIncrease)
            {
                vm.MasterViewModel.ProposedIncrease.ProposedIncreases.RemoveAt(i);
                i = i - 1;
                vm.MasterViewModel.ProposedIncrease.ProposedIncreases.Add(new ProposedIncreaseViewModel()
                {                    
                    SalaryChangeAfterPeriod = KeyValueViewModel.ToViewModel(proincreases.SalaryChangeAfterPeriod),
                    ProposedIncrease = proincreases.ProposedIncrease.HasValue ? proincreases.ProposedIncrease.Value : default(decimal),
                    ProposedIncreasePercentage = proincreases.ProposedIncrease.HasValue ? proincreases.ProposedIncrease.Value > 0 ? ((proincreases.ProposedIncrease.Value - dto.BasicSalary) * 100) / dto.BasicSalary : default(decimal?) : default(decimal?),
                    ProposedIncreaseAmount = proincreases.ProposedIncrease.HasValue ? proincreases.ProposedIncrease.Value > 0 ? proincreases.ProposedIncrease.Value - dto.BasicSalary : default(decimal?) : default(decimal?),
                    CRE_BY = proincreases.CRE_BY,
                    CRE_DT = proincreases.CRE_DT,
                    CRE_IP = proincreases.CRE_IP,
                    CRE_PROG_ID = proincreases.CRE_PROG_ID,
                    CRE_WEBUSER = proincreases.CRE_WEBUSER,
                    UPD_BY = proincreases.UPD_BY,
                    UPD_DT = proincreases.UPD_DT,
                    UPD_IP = proincreases.UPD_IP,
                    UPD_PROG_ID = proincreases.UPD_PROG_ID,
                    UPD_WEBUSER = proincreases.UPD_WEBUSER,
                    Remarks = proincreases.Remarks
                });
            }

            foreach (var allowances in dto.Allowance)
            {
                vm.MasterViewModel.Allowance.Allowances.Add(new AllowanceViewModel()
                {
                    Allowance = KeyValueViewModel.ToViewModel(allowances.Allowance),
                    Amount = allowances.Amount.HasValue ? allowances.Amount.Value : default(decimal),
                    AmountAfterProbation = allowances.AmountAfterProbation.HasValue ? allowances.AmountAfterProbation.Value : default(decimal),
                    Remark = allowances.Remark,
                    CRE_BY = allowances.CRE_BY,
                    CRE_DT = allowances.CRE_DT,
                    CRE_IP = allowances.CRE_IP,
                    CRE_WEBUSER = allowances.CRE_WEBUSER,
                    REQ_DT = allowances.REQ_DT
                });
            }


            if (dto.documents != null)
            {
                foreach (var document in dto.documents)
                {

                    vm.MasterViewModel.Document.Documents.Add(new EmploymentRequestDocumentFileViewModel()
                    {
                        DocumentFileIID = document.DocumentFileIID,
                        Docfile = new Domain.Setting.SettingBL().GetSettingValue<string>("EmploymentRequestPath") + document.FileName,
                        UploadDocumentType = new KeyValueViewModel() { Key = document.DocumentFileIID.ToString(), Value = document.Description }
                    });
                }
            }

            return vm;
        }

        public static EmploymentRequestViewModel FromDTO(EmploymentRequestDTO dto)
        {

            return null;
        }

        public static EmploymentRequestDTO ToDTO(EmploymentRequestViewModel vm)
        {
            var dto = new EmploymentRequestDTO()
            {
                BasicSalary = vm.MasterViewModel.BasicSalary,
                F_Name = vm.MasterViewModel.F_NAME,
                M_Name = vm.MasterViewModel.M_NAME,
                L_Name = vm.MasterViewModel.L_NAME,
                PassportExpiryDate = string.IsNullOrEmpty(vm.MasterViewModel.PASSPORT_EXPIRY) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.PASSPORT_EXPIRY, "dd-MMM-yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")),//DateTime.Parse(vm.MasterViewModel.PASSPORT_EXPIRY),
                PassportIssueDate = string.IsNullOrEmpty(vm.MasterViewModel.PASSPORT_ISSUE) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.PASSPORT_ISSUE, "dd-MMM-yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")),//DateTime.Parse(vm.MasterViewModel.PASSPORT_ISSUE),
                Location = KeyValueViewModel.ToDTO(vm.MasterViewModel.Location),
                DOB = string.IsNullOrEmpty(vm.MasterViewModel.DATE_OF_BIRTH) ? (DateTime?)null : DateTime.ParseExact(vm.MasterViewModel.DATE_OF_BIRTH, "dd-MMM-yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")),
                Gender = new KeyValueDTO { Key = vm.MasterViewModel.GENDER },
                PASSPORT_NO = string.IsNullOrEmpty(vm.MasterViewModel.PASSPORT_NO) ? null : vm.MasterViewModel.PASSPORT_NO.ToUpper(),
                //ProposedIncrease = vm.MasterViewModel.ProposedIncrease,

                Reason_Basic = vm.MasterViewModel.Reason_Basic,
                RecuritRemark = vm.MasterViewModel.RecuritRemark,
                StaffCar = vm.MasterViewModel.StaffCar,
                Designation = KeyValueViewModel.ToDTO(vm.MasterViewModel.Designation),
                EmploymentType = KeyValueViewModel.ToDTO(vm.MasterViewModel.EmploymentType),
                Group = KeyValueViewModel.ToDTO(vm.MasterViewModel.Group),
                MainDesignation = KeyValueViewModel.ToDTO(vm.MasterViewModel.MainDesignation),
                Nationality = KeyValueViewModel.ToDTO(vm.MasterViewModel.Nationality),
                PayComp = KeyValueViewModel.ToDTO(vm.MasterViewModel.PayComp),
                ProductiveType = KeyValueViewModel.ToDTO(vm.MasterViewModel.ProductiveType),
                RecruitmentType = KeyValueViewModel.ToDTO(vm.MasterViewModel.RecruitmentType),
                //SalaryChangeAfterPeriod = KeyValueViewModel.ToDTO(vm.MasterViewModel.SalaryChangeAfterPeriod),
                Shop = KeyValueViewModel.ToDTO(vm.MasterViewModel.Shop),
                EmailID = vm.MasterViewModel.EmailID,
                AlternativeEmailID = vm.MasterViewModel.AlternativeEmailID,
                EmpPersonalRemarks = vm.MasterViewModel.EmpPersonalRemarks,
                EMP_REQ_NO = vm.MasterViewModel.EMP_REQ_NO,
                EMP_NO = vm.MasterViewModel.EMP_NO,
                CIVILID = vm.MasterViewModel.CIVILID,
                CRE_BY = vm.MasterViewModel.CRE_BY,
                CRE_DT = !string.IsNullOrEmpty(vm.MasterViewModel.CRE_DT) ? DateTime.Parse(vm.MasterViewModel.CRE_DT) : (DateTime?)null,
                CRE_IP = vm.MasterViewModel.CRE_IP,
                CRE_WEBUSER = vm.MasterViewModel.CRE_WEBUSER,
                REQ_DT = !string.IsNullOrEmpty(vm.MasterViewModel.REQ_DT) ? DateTime.Parse(vm.MasterViewModel.REQ_DT) : (DateTime?)null,
                isbudgeted = vm.MasterViewModel.EmploymentType == null ? (bool?)null : vm.MasterViewModel.EmploymentType.Key == "1" ? vm.MasterViewModel.isBudgeted.HasValue ? vm.MasterViewModel.isBudgeted.Value : false : default(bool?),
                replacedEmployee = KeyValueViewModel.ToDTO(vm.MasterViewModel.ReplacedEmployee),
                Marital_Status = KeyValueViewModel.ToDTO(vm.MasterViewModel.MaritalStatus),
                isNewRequest = vm.MasterViewModel.isNewRequest,
                Agent = KeyValueViewModel.ToDTO(vm.MasterViewModel.Agent),
                ContractType = KeyValueViewModel.ToDTO(vm.MasterViewModel.ContractType),
                Period = vm.MasterViewModel.Period,
                Photo = vm.MasterViewModel.Photo,
                VisaCompany = vm.MasterViewModel.VisaCompany.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.VisaCompany.Key) ? KeyValueViewModel.ToDTO(vm.MasterViewModel.VisaCompany) : new KeyValueDTO(),
                QuotaType = vm.MasterViewModel.QuotaType.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.QuotaType.Key) ? KeyValueViewModel.ToDTO(vm.MasterViewModel.QuotaType) : new KeyValueDTO(),
                EmpProcessRequestStatus = vm.MasterViewModel.EmpProcessRequestStatus.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.EmpProcessRequestStatus.Key) ? KeyValueViewModel.ToDTO(vm.MasterViewModel.EmpProcessRequestStatus) : new KeyValueDTO(),
                EmpRequestStatus = vm.MasterViewModel.EmpRequestStatus.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.EmpRequestStatus.Key) ? KeyValueViewModel.ToDTO(vm.MasterViewModel.EmpRequestStatus) : new KeyValueDTO(),
                PersonalRemarks = vm.MasterViewModel.PersonalRemarks
            };

            dto.Allowance = new List<EmployementAllowanceDTO>();

            dto.ProposedIncrease = new List<EmploymentProposedIncreaseDTO>();

            dto.documents = new List<DocumentFileDTO>();

            foreach (var proposedIncre in vm.MasterViewModel.ProposedIncrease.ProposedIncreases)
            {
                if (proposedIncre.ProposedIncrease != null)
                {
                    //if (!(string.IsNullOrEmpty(proposedIncre.SalaryChangeAfterPeriod.Key) || string.IsNullOrEmpty(proposedIncre.SalaryChangeAfterPeriod.Value)))
                    //{
                    dto.ProposedIncrease.Add(new EmploymentProposedIncreaseDTO()
                    {
                        SalaryChangeAfterPeriod = KeyValueViewModel.ToDTO(proposedIncre.SalaryChangeAfterPeriod),
                        ProposedIncrease = proposedIncre.ProposedIncrease,
                        ProposedIncreaseAmount = proposedIncre.ProposedIncreaseAmount,
                        ProposedIncreasePercentage = proposedIncre.ProposedIncreasePercentage,
                        Remarks = proposedIncre.Remarks
                    });
                    //}
                }
            }

            foreach (var allowances in vm.MasterViewModel.Allowance.Allowances)
            {
                if (allowances.Allowance != null)
                {
                    if (!(string.IsNullOrEmpty(allowances.Allowance.Key) || string.IsNullOrEmpty(allowances.Allowance.Value)))
                    {
                        dto.Allowance.Add(new EmployementAllowanceDTO()
                               {
                                   Allowance = KeyValueViewModel.ToDTO(allowances.Allowance),
                                   Amount = allowances.Amount,
                                   AmountAfterProbation = allowances.AmountAfterProbation,
                                   Remark = allowances.Remark,
                                   CRE_BY = allowances.CRE_BY,
                                   CRE_DT = allowances.CRE_DT,
                                   CRE_IP = allowances.CRE_IP,
                                   CRE_WEBUSER = allowances.CRE_WEBUSER,
                                   REQ_DT = allowances.REQ_DT
                               });
                        //}
                    }
                }
            }

            foreach (var document in vm.MasterViewModel.Document.Documents)
            {
                if (!string.IsNullOrEmpty(document.Docfile))
                {
                    dto.documents.Add(new DocumentFileDTO()
                    {
                        FileName = System.IO.Path.GetFileName(document.Docfile),
                        DocumentFileIID = long.Parse(document.UploadDocumentType.Key)
                    });
                }
            }
            return dto;
        }
    }
}
