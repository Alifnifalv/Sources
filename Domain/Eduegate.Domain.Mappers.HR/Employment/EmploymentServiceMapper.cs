using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.Payroll;


namespace Eduegate.Domain.Mappers.EmploymentService
{
    public class EmploymentServiceMapper : IDTOEntityMapper<EmploymentRequestDTO, HR_EMP_REQ>
    {
        private CallContext _context;
        public static EmploymentServiceMapper Mapper(CallContext context)
        {
            var mapper = new EmploymentServiceMapper();
            mapper._context = context;
            return mapper;
        }

        public EmploymentRequestDTO ToDTO(HR_EMP_REQ entity)
        {
            var repository = new EmploymentServiceRepository();
            var nationality = repository.GetNationality(entity.NAT_CODE).FirstOrDefault();
            var isEmpType = new ReferenceDataBL(_context).IsReplacementorReappointmentEmpType(int.Parse(entity.EMP_TYPE));
            var EmployeeList = isEmpType ? repository.GetEmployeeList(entity.EMP_TYPE == "3" ? entity.REPLACEFOR_EMPNO.Value : entity.OLD_EMPNO.Value).FirstOrDefault() : default(HR_EMP_REQ);
            var payComp = repository.GetPayComMaster(entity.PAYCOMP.HasValue ? entity.PAYCOMP.Value : 0).FirstOrDefault();
            var location = repository.GetLocationMasterByCode(entity.LOC_CODE.HasValue ? entity.LOC_CODE.Value : 0, entity.SHOP.HasValue ? entity.SHOP.Value : 0).FirstOrDefault();
            var shop = repository.HRDepartment(entity.SHOP.HasValue ? entity.SHOP.Value : 0).FirstOrDefault();
            var group = repository.GetGroupDesigMaster(entity.GROUP_CODE.HasValue ? entity.GROUP_CODE.Value : 0).FirstOrDefault();
            var mainDesignation = repository.GetMainDesigMasterByMainCode(entity.MAIN_DESIG_CODE.HasValue ? entity.MAIN_DESIG_CODE.Value : 0, entity.GROUP_CODE.Value).FirstOrDefault();
            var designation = repository.GetDesigMasterByDesigCode(entity.DESIGCD.HasValue ? entity.DESIGCD.Value : 0, entity.MAIN_DESIG_CODE.Value).FirstOrDefault();
            //var doctype = repository.GetDocType(entity.RECRUFRM == "2" ? 2 : 1).FirstOrDefault();
            var productiveType = repository.GetParamters("PR", entity.PRODUCTIVE_TYPE).FirstOrDefault();
            var employmentType = repository.GetParamters("ER", entity.EMP_TYPE).FirstOrDefault();
            var recruitmentType = repository.GetParamters("EL", entity.RECRUFRM).FirstOrDefault();
            var agent = (entity.RECRUFRM != "2" && entity.AGNTCOD.HasValue) ? repository.GetAgents(entity.AGNTCOD.Value).FirstOrDefault() : default(AGENT_MASTER);

            var visaCompany = entity.VISACOMPANY.HasValue ? new Eduegate.Domain.Payroll.EmployeeBL(null).GetCompanyShopLocationByID(entity.EMP_REQ_NO.Value) : default(Eduegate.Services.Contracts.Commons.KeyValueDTO);
            var quotaType = !string.IsNullOrEmpty(entity.QUOTA_TYPE) ? repository.GetQuotaType(int.Parse(entity.QUOTA_TYPE)).FirstOrDefault() : default(HRMD_QUOTA_TYPE);
            var processstatus = repository.GetEmploymentRequestProcessStatusByID(long.Parse(entity.EMP_REQ_NO.ToString()));

            var empStatus = repository.GetEmploymentStatusByID(long.Parse(entity.EMP_REQ_NO.ToString()));

            var contractType = !string.IsNullOrEmpty(entity.CONT_TYPE) ? repository.GetContractType("CNT", entity.CONT_TYPE).FirstOrDefault() : default(CODE_FILE);

            var dto = new EmploymentRequestDTO();
            dto.EMP_REQ_NO = entity.EMP_REQ_NO;
            dto.Nationality = new Services.Contracts.Commons.KeyValueDTO() { Key = nationality.NAT_CODE, Value = nationality.NAT_NAME };
            dto.PASSPORT_NO = entity.PASSPORT_NO;
            dto.PayComp = new Services.Contracts.Commons.KeyValueDTO() { Key = payComp.PAYCOMP_CODE != null ? payComp.PAYCOMP_CODE.ToString() : "", Value = Convert.ToString(payComp.PAYCOMP_NAME) };
            dto.EMP_NO = entity.EMPNO;
            dto.NAME = "";
            dto.F_Name = entity.F_NAME;
            dto.M_Name = !string.IsNullOrEmpty(entity.M_NAME) ? entity.M_NAME : "";
            dto.L_Name = entity.L_NAME;
            dto.Location = new Services.Contracts.Commons.KeyValueDTO() { Key = location.LOC_CODE != null ? location.LOC_CODE.ToString() : "", Value = Convert.ToString(location.LOC_NAME) };
            dto.DOB = entity.DATE_OF_BIRTH.HasValue ? entity.DATE_OF_BIRTH.Value : default(DateTime?);
            dto.PassportExpiryDate = entity.PASSPORT_EXPIRY.HasValue ? entity.PASSPORT_EXPIRY.Value : default(DateTime?);
            dto.PassportIssueDate = entity.PASSPORT_ISSUE.HasValue ? entity.PASSPORT_ISSUE.Value : default(DateTime?);
            dto.Gender = new Services.Contracts.Commons.KeyValueDTO() { Key = entity.GENDER, Value = entity.GENDER == "M" ? "Male" : "Female" };
            dto.Shop = new Services.Contracts.Commons.KeyValueDTO() { Key = shop.DEPT_NO != null ? shop.DEPT_NO.ToString() : "", Value = Convert.ToString(shop.DEPT_NAME) };
            dto.Group = new Services.Contracts.Commons.KeyValueDTO() { Key = group.GROUP_CODE != null ? group.GROUP_CODE.ToString() : "", Value = Convert.ToString(group.GROUP_DESC) };
            dto.MainDesignation = new Services.Contracts.Commons.KeyValueDTO() { Key = mainDesignation.MAIN_DESIG_CODE != null ? mainDesignation.MAIN_DESIG_CODE.ToString() : "", Value = Convert.ToString(mainDesignation.MAIN_DESIG_DESC) };
            dto.Designation = new Services.Contracts.Commons.KeyValueDTO() { Key = designation.DESIG_CODE != null ? designation.DESIG_CODE.ToString() : "", Value = Convert.ToString(designation.DESIG_NAME) };
            dto.BasicSalary = entity.BASIC_SALARY.HasValue ? entity.BASIC_SALARY.Value : 0;
            dto.Reason_Basic = !string.IsNullOrEmpty(entity.REASON_BASIC) ? entity.REASON_BASIC : "";
            dto.ProductiveType = new Services.Contracts.Commons.KeyValueDTO() { Key = productiveType.CODE != null ? productiveType.CODE.ToString() : "", Value = Convert.ToString(productiveType.NAME) };
            dto.EmploymentType = new Services.Contracts.Commons.KeyValueDTO() { Key = employmentType.CODE != null ? employmentType.CODE.ToString() : "", Value = Convert.ToString(employmentType.NAME) };
            dto.EmailID = !string.IsNullOrEmpty(entity.DEPT_EMAIL) ? entity.DEPT_EMAIL : "";
            dto.AlternativeEmailID = !string.IsNullOrEmpty(entity.ALT_DEPT_EMAIL) ? entity.ALT_DEPT_EMAIL : "";
            dto.EmpPersonalRemarks = !string.IsNullOrEmpty(entity.REMARK_EMP_TYPE) ? entity.REMARK_EMP_TYPE : "";
            dto.StaffCar = !string.IsNullOrEmpty(entity.STAFF_CAR_IND) ? entity.STAFF_CAR_IND == "Y" ? true : false : false;
            //dto.ProposedIncrease = entity.BASIC_AFTER_PROBATION.HasValue ? entity.BASIC_AFTER_PROBATION.Value : 0;
            //dto.SalaryChangeAfterPeriod = new Services.Contracts.Commons.KeyValueDTO() { Key = entity.BASIC_PERIOD_AFT_PROB.HasValue ? entity.BASIC_PERIOD_AFT_PROB.Value.ToString() : "", Value = entity.BASIC_PERIOD_AFT_PROB.HasValue ? entity.BASIC_PERIOD_AFT_PROB.Value.ToString() : "" };
            dto.RecuritRemark = !string.IsNullOrEmpty(entity.RECRUITMENT_REMARK) ? entity.RECRUITMENT_REMARK : "";
            dto.RecruitmentType = new Services.Contracts.Commons.KeyValueDTO() { Key = recruitmentType.CODE != null ? recruitmentType.CODE.ToString() : "", Value = Convert.ToString(recruitmentType.NAME) };
            dto.CIVILID = !string.IsNullOrEmpty(entity.CIVILID) ? entity.CIVILID : "";
            dto.CRE_BY = entity.CRE_BY;
            dto.CRE_DT = entity.CRE_DT;
            dto.CRE_IP = entity.CRE_IP;
            dto.CRE_WEBUSER = entity.CRE_WEBUSER;
            dto.REQ_DT = entity.REQ_DT;
            dto.isbudgeted = !string.IsNullOrEmpty(entity.BUDYN) ? entity.BUDYN == "Y" ? true : false : default(bool?);
            if (EmployeeList != null && EmployeeList != default(HR_EMP_REQ))
                dto.replacedEmployee = new Services.Contracts.Commons.KeyValueDTO() { Key = EmployeeList.EMPNO.ToString(), Value = !string.IsNullOrEmpty(EmployeeList.NAME) ? EmployeeList.NAME : string.Concat(EmployeeList.F_NAME, " ", EmployeeList.M_NAME, " ", EmployeeList.L_NAME) };
            else
                dto.replacedEmployee = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };
            dto.Marital_Status = new Services.Contracts.Commons.KeyValueDTO() { Key = !string.IsNullOrEmpty(entity.MARITAL_STATUS) ? entity.MARITAL_STATUS : "", Value = !string.IsNullOrEmpty(entity.MARITAL_STATUS) ? entity.MARITAL_STATUS == "1" ? "Married" : "Unmarried" : "" };
            if (agent != null && agent != default(AGENT_MASTER))
                dto.Agent = new Services.Contracts.Commons.KeyValueDTO() { Key = agent.NO.ToString(), Value = agent.NAME };
            else
                dto.Agent = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };

            dto.Photo = !string.IsNullOrEmpty(entity.PHOTO_FILE_NAME) ? entity.PHOTO_FILE_NAME : "";


            if (contractType != null && contractType != default(CODE_FILE))
                dto.ContractType = new Services.Contracts.Commons.KeyValueDTO() { Key = contractType.CODE, Value = contractType.SHDES };
            else
                dto.ContractType = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };
            dto.Period = entity.CONT_PRDYR;

            if (visaCompany != null && visaCompany != default(Services.Contracts.Commons.KeyValueDTO))
                dto.VisaCompany = visaCompany;
            else
                dto.VisaCompany = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };

            if (quotaType != null && quotaType != default(HRMD_QUOTA_TYPE))
                dto.QuotaType = new Services.Contracts.Commons.KeyValueDTO() { Key = quotaType.QUOTA_TYPEID.ToString(), Value = quotaType.QUOTA_TYPE };
            else
                dto.QuotaType = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };

            if (processstatus != null && processstatus != default(HRMD_EMP_REQ_PROCESSTATUS))
                dto.EmpProcessRequestStatus = new Services.Contracts.Commons.KeyValueDTO() { Key = processstatus.CODE.ToString(), Value = processstatus.STATUSNAME };
            else
                dto.EmpProcessRequestStatus = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };

            if (empStatus != null && empStatus != default(HRMD_EMP_REQ_STATUSES))
                dto.EmpRequestStatus = new Services.Contracts.Commons.KeyValueDTO() { Key = empStatus.CODE.ToString(), Value = empStatus.STATUSNAME };
            else
                dto.EmpRequestStatus = new Services.Contracts.Commons.KeyValueDTO() { Key = "", Value = "" };

            dto.Allowance = new List<EmployementAllowanceDTO>();
            dto.ProposedIncrease = new List<EmploymentProposedIncreaseDTO>();
            dto.documents = new List<Services.Contracts.DocumentFileDTO>();

            
            return dto;
        }

        public HR_EMP_REQ ToEntity(EmploymentServiceDTO employeeDTO)
        {
            return new HR_EMP_REQ();
        }

        public HR_EMP_REQ ToEntity(EmploymentRequestDTO dto)
        {
            try
            {
                var isEmpType = new ReferenceDataBL(_context).IsReplacementorReappointmentEmpType(int.Parse(dto.EmploymentType.Key));
                return new HR_EMP_REQ()
                {
                    CNTRYCD = 1,
                    EMP_REQ_NO = !dto.isNewRequest.HasValue ? int.Parse(new EmploymentServiceRepository().GetNextEmployeeRequestNumber(1, dto.Designation.Key, "ERN", "1")) : dto.EMP_REQ_NO.Value,
                    REQ_LINE_NO = 0,
                    NAT_CODE = dto.Nationality.Key,
                    PASSPORT_NO = dto.PASSPORT_NO,
                    PAYCOMP = dto.PayComp != null ? short.Parse(dto.PayComp.Key) : default(short),
                    EMPNO = !dto.isNewRequest.HasValue ? int.Parse(new EmploymentServiceRepository().GetNextEmployeeRequestNumber(1, dto.Designation.Key, "ENO", "1")) : dto.EMP_NO.Value,
                    //REQ_DT = DateTime.Now,
                    REQ_BATCH_IND = "S",
                    NAME = "",
                    F_NAME = dto.F_Name,
                    M_NAME = !string.IsNullOrEmpty(dto.M_Name) ? dto.M_Name : "",
                    L_NAME = dto.L_Name,
                    LOC_CODE = short.Parse(dto.Location.Key),
                    DATE_OF_BIRTH = dto.DOB,
                    PASSPORT_EXPIRY = dto.PassportExpiryDate,
                    PASSPORT_ISSUE = dto.PassportIssueDate,
                    GENDER = dto.Gender.Key,
                    SHOP = short.Parse(dto.Shop.Key),
                    SHOP_DESC = dto.Shop.Value,
                    GROUP_CODE = short.Parse(dto.Group.Key),
                    GROUP_DESC = dto.Group.Value,
                    MAIN_DESIG_CODE = short.Parse(dto.MainDesignation.Key),
                    MAIN_DESIG_DESC = dto.MainDesignation.Value,
                    DESIGCD = short.Parse(dto.Designation.Key),
                    BASIC_SALARY = dto.BasicSalary,
                    REASON_BASIC = !string.IsNullOrEmpty(dto.Reason_Basic) ? dto.Reason_Basic : "",
                    PRODUCTIVE_TYPE = dto.ProductiveType.Key,
                    EMP_TYPE = dto.EmploymentType.Key,
                    DEPT_EMAIL = dto.EmailID,
                    ALT_DEPT_EMAIL = dto.AlternativeEmailID,
                    REMARK_EMP_TYPE = !string.IsNullOrEmpty(dto.EmpPersonalRemarks) ? dto.EmpPersonalRemarks : "",
                    STAFF_CAR_IND = dto.StaffCar == true ? "Y" : "F",
                    //BASIC_AFTER_PROBATION = dto.ProposedIncrease,
                    //BASIC_PERIOD_AFT_PROB = dto.SalaryChangeAfterPeriod != null && !string.IsNullOrEmpty(dto.SalaryChangeAfterPeriod.Key) ? short.Parse(dto.SalaryChangeAfterPeriod.Key) : default(short),

                    STATUS = "S",

                    REMARK_RECRTMT = !string.IsNullOrEmpty(dto.RecuritRemark) ? dto.RecuritRemark : "",

                    RECRUFRM = dto.RecruitmentType.Key,
                    CIVILID = !string.IsNullOrEmpty(dto.CIVILID) ? dto.CIVILID : "",
                    BUDYN = dto.isbudgeted.HasValue ? dto.isbudgeted == true ? "Y" : "N" : default(string),
                    MARITAL_STATUS = dto.Marital_Status.Key,
                    //REPLACEMENT_ECNO = dto.replacedEmployee.HasValue ? dto.replacedEmployee.Value : 0,
                    REPLACEFOR_EMPNO = isEmpType ? int.Parse(dto.EmploymentType.Key) == 3 ? Convert.ToInt32(dto.replacedEmployee.Key) : default(int?) : default(int?),
                    OLD_EMPNO = isEmpType ? int.Parse(dto.EmploymentType.Key) == 4 ? Convert.ToInt32(dto.replacedEmployee.Key) : default(int?) : default(int?),
                    AGNTCOD = int.Parse(dto.RecruitmentType.Key) != 2 ? short.Parse(dto.Agent.Key) : default(short?),
                    CONT_TYPE = dto.ContractType.Key,
                    CONT_PRDYR = int.Parse(dto.ContractType.Key) == 2 ? short.Parse(dto.Period.ToString()) : default(short?),
                    PHOTO_FILE_NAME = dto.Photo,
                };
            }
            catch
            {
                return new HR_EMP_REQ();
            }

        }
    }
}
