using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Services.Contracts.HR;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.Oracle.Models;

namespace Eduegate.Domain.Validators.HR
{
    public class EmploymentRequestValidator
    {
        public EmploymentRequestDTO Model { get; set; }

        public static EmploymentRequestValidator Validator(EmploymentRequestDTO model)
        {
            return  new EmploymentRequestValidator() {Model = model};
        }

        public bool Validate( out string erroMessage){
            return this.ValidateDOB(out erroMessage) ? this.ValidatePassportNo(out erroMessage)
                ? this.ValidateCivilID(out erroMessage) 
                ? this.ValidateBasicSalary(out erroMessage) 
                ? this.ValidateReplacedEmployee(out erroMessage) 
                ? this.ValidateAgent(out erroMessage) 
                ? this.ValidateAllowences(out erroMessage) 
                ? this.ValidateRecruitmentType(out erroMessage) : false : false : false : false : false : false : false;
        }

        public bool ValidateDOB(out string erroMessage)
        {
            int ageLimit = 0;
            erroMessage = string.Empty;

            if (Model.Nationality.Key == null)
            {
                erroMessage = "Please select nationality.";
                return false;
            }

            if (Model.Nationality.Key == "KUW")
            {
                ageLimit = 18;
            }
            else
            {
                ageLimit = 21;
            }

            if (Model.DOB.Value.AddYears(ageLimit) > DateTime.Now)
            {
                erroMessage = "Age should be greater than " + ageLimit + " years";
                return false;
            }

            if (Model.DOB.Value.AddYears(52) < DateTime.Now)
            {
                erroMessage = "The candidate age is crossed the limit, please contact HR";
                return false;
            }

            return true;
        }

        public bool ValidatePassportNo(out string erroMessage)
        {
            erroMessage = string.Empty;

            if (new EmploymentServiceRepository().IsPassportExists(Model.PASSPORT_NO, Model.EMP_REQ_NO))
            {
                erroMessage = "Pasport Number already Exists";
                return false;
            }

            if (Convert.ToInt16(Model.RecruitmentType.Key) == 2 && Model.PassportExpiryDate < DateTime.Now.AddYears(1))
            {
                erroMessage = "Passport should have more than 1 year validity for local Recruitment";
                return false;
            }

            return true;
        }

        public bool ValidateCivilID(out string erroMessage)
        {
            erroMessage = string.Empty;

            if (Convert.ToInt16(Model.RecruitmentType.Key) == 2 && string.IsNullOrEmpty(Model.CIVILID))
            {
                erroMessage = "Civil ID Number is mandatory for Local recruitment";
                return false;
            }

            if (!string.IsNullOrEmpty(Model.CIVILID) && new EmploymentServiceRepository().IsCivilIDExists(Model.CIVILID, Model.EMP_REQ_NO.Value))
            {
                erroMessage = "Civil ID already exists";
                return false;
            }

            return true;
        }

        public bool ValidateBasicSalary(out string erroMessage)
        {
            if (Model.Designation.Key == null)
            {
                erroMessage = "Please select designation.";
                return false;
            }

            var salaryResult = new EmploymentServiceRepository().CheckDesignationSalary(Model.BasicSalary, Convert.ToInt32(Model.Designation.Key));
            decimal basicSalMax = Convert.ToDecimal(salaryResult.MAX_SAL) * Convert.ToDecimal(1.5);
            erroMessage = string.Empty;

            if (Model.BasicSalary < Convert.ToDecimal(salaryResult.MIN_SAL))
            {
                erroMessage = "Basic salary cannot be less than Designation Minimum salary (" + salaryResult.MIN_SAL + ")";
                return false;
            }

            if (Model.BasicSalary < basicSalMax && Model.BasicSalary > salaryResult.MIN_SAL)
            {
                if (!(Model.BasicSalary < Convert.ToDecimal(salaryResult.MAX_SAL) && Model.BasicSalary > Convert.ToDecimal(salaryResult.MIN_SAL)))
                {
                    if (string.IsNullOrEmpty(Model.Reason_Basic))
                    {
                        erroMessage = "Enter reason for Salary greater than Maximum salary + 50% : " + basicSalMax + ".";
                        return false;
                    }
                }

            }

            if (Model.BasicSalary > basicSalMax)
            {
                erroMessage = "Salary cannot be greater Maximum salary + 50% : " + basicSalMax + ".";
                return false;
            }

            return true;
        }

        public bool ValidateReplacedEmployee(out string erroMessage)
        {
            var employeeRepository = new EmploymentServiceRepository();
            erroMessage = string.Empty;

            if (Convert.ToInt32(Model.EmploymentType.Key) == 3)
            {
                if (Model.replacedEmployee.IsNull())
                {
                    erroMessage = "Please Select the Re Appointment for employee name";
                    return false;
                }
                else
                {
                    if (Model.replacedEmployee.Value.IsNull() || Convert.ToInt32(Model.replacedEmployee.Key) == 0)
                    {
                        erroMessage = "Please Select the Replaced employee name";
                        return false;
                    }
                    else
                    {
                        if (employeeRepository.checkEmployeeReplaced(Convert.ToInt32(Model.EMP_NO), Convert.ToInt32(Model.replacedEmployee.Key)))
                        {
                            erroMessage = "This EC # is already in use";
                            return false;
                        }
                    }
                }
            }
            else if (Convert.ToInt32(Model.EmploymentType.Key) == 4)
            {
                if (Model.replacedEmployee.IsNull())
                {
                    erroMessage = "Please Select the Re Appointment for employee name";
                    return false;
                }
                else
                {

                    if (Model.replacedEmployee.Value.IsNull() || Convert.ToInt32(Model.replacedEmployee.Key) == 0)
                    {
                        erroMessage = "Please Select the Re Appointment for employee name";
                        return false;
                    }
                    else
                    {
                        if (employeeRepository.checkEmployeeReAppointed(Convert.ToInt32(Model.EMP_NO), Convert.ToInt32(Model.replacedEmployee.Key)))
                        {
                            erroMessage = "" + Model.replacedEmployee.Value + " has already Reappointed, Please check the Re Appointement employee name.";
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool ValidateAgent(out string erroMessage)
        {
            erroMessage = string.Empty;

            if (Model.RecruitmentType.Key != "2")
            {
                if (Model.Agent.IsNull() || string.IsNullOrEmpty(Model.Agent.Key))
                {
                    erroMessage = "Select Agent";
                    return false;
                }
            }

            return true;
        }

        public bool ValidateAllowences(out string erroMessage)
        {
            erroMessage = string.Empty;

            foreach (var allowance in Model.Allowance)
            {
                var validAllownace = new EmploymentServiceRepository().CheckAllowanceSalary(Convert.ToInt16(allowance.Allowance.Key), Convert.ToDecimal(allowance.Amount));

                if (!(allowance.Amount > validAllownace.MIN_AMT && allowance.Amount < validAllownace.MAX_AMT))
                {
                    erroMessage = allowance.Allowance.Value + " is not between the minimum(" + validAllownace.MIN_AMT + ") and maximum limit(" + validAllownace.MAX_AMT + ")";
                    return false;
                }
            }

            return true;
        }

        public bool ValidateRecruitmentType(out string erroMessage)
        {
            erroMessage = string.Empty;
            var documentList = new EmploymentServiceRepository().GetDocType(Convert.ToInt16(Model.RecruitmentType.Key) == 2 ? "L" : "F");

            switch (Convert.ToInt16(Model.RecruitmentType.Key))
            {
                case 2:
                    if (Model.Nationality.Key != "KUW")
                    {
                        documentList.Remove(new HRMD_FILEUPLOADTYPES() { FILEUPLOADTYPEID = 6 });//Birth Certificate
                        documentList.Remove(new HRMD_FILEUPLOADTYPES() { FILEUPLOADTYPEID = 7 }); //Nationality Certificate
                        var updateddoc = Model.documents.Where(a => a.DocumentFileIID != 6 && a.DocumentFileIID != 7).ToList();
                        var checkDoc = documentList.Where(a => a.FILEUPLOADTYPEID != 6 && a.FILEUPLOADTYPEID != 7).ToList();
                        //updateddoc.Remove(dto.documents.Where(a => a.DocumentFileIID == 6).FirstOrDefault());
                        //updateddoc.Remove(dto.documents.Where(a => a.DocumentFileIID == 7).FirstOrDefault());
                        if (!(updateddoc.Count == checkDoc.Count))
                        {
                            erroMessage = "Upload all the documents";
                            return false;
                        }
                    }
                    else
                    {
                        if (!(Model.documents.Count == documentList.Count))
                        {
                            erroMessage = "Upload all the documents";
                            return false;
                        }
                    }
                    break;
                default:
                    if (!(Model.documents.Count == documentList.Count))
                    {
                        erroMessage = "Upload all the documents";
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
