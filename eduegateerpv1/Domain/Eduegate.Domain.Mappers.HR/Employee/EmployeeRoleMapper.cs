using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Employee;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Enums.Synchronizer;

namespace Eduegate.Domain.Repository.HR.Employee
{
    public class EmployeeRoleMapper : DTOEntityDynamicMapper
    {
        public static EmployeeRoleMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeRoleMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeRoleDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as EmployeeRoleDTO);
        }

        public List<EmployeeRoleDTO> ToDTO(List<EmployeeRole> entities)
        {
            var dtos = new List<EmployeeRoleDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private EmployeeRoleDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeRoles.Where(X => X.EmployeeRoleID == IID)
                 .AsNoTracking()
                 .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private EmployeeRoleDTO ToDTO(EmployeeRole entity)
        {
            var roleDto = new EmployeeRoleDTO()
            {
                EmployeeRoleName = entity.EmployeeRoleName,
                EmployeeRoleID = entity.EmployeeRoleID
            };

            return roleDto;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeRoleDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new EmployeeRole()
            {
                EmployeeRoleID = toDto.EmployeeRoleID,
                EmployeeRoleName = toDto.EmployeeRoleName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.EmployeeRoleID == 0)
                {
                    var maxGroupID = dbContext.EmployeeRoles.Max(a => (int?)a.EmployeeRoleID);

                    entity.EmployeeRoleID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.EmployeeRoles.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.EmployeeRoles.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.EmployeeRoleID));
        }

        public EmployeesDTO GetStaffProfile(long loginID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var staffDetails = new EmployeesDTO();

                var employeeDetails = dbContext.Employees.Where(e => e.LoginID == loginID)
                    .Include(i => i.Gender)
                    .Include(i => i.Departments1)
                    .Include(i => i.Designation)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (employeeDetails != null)
                {
                    staffDetails = new EmployeesDTO()
                    {
                        EmployeeIID = employeeDetails.EmployeeIID,
                        EmployeeName = employeeDetails.EmployeeName == null ? (employeeDetails.FirstName + " " + employeeDetails.MiddleName + " " + employeeDetails.LastName) : employeeDetails.EmployeeName,
                        EmployeeCode = employeeDetails.EmployeeCode != null ? employeeDetails.EmployeeCode : "NA",
                        Age = employeeDetails.Age != null ? employeeDetails.Age : GetAgeByDateOfBirth(employeeDetails.DateOfBirth),
                        WorkEmail = employeeDetails.WorkEmail != null ? employeeDetails.WorkEmail : "NA",
                        WorkMobileNo = employeeDetails.WorkMobileNo != null ? employeeDetails.WorkMobileNo : "NA",
                        WorkPhone = employeeDetails.WorkPhone != null ? employeeDetails.WorkPhone : "NA",
                        DateOfBirthString = employeeDetails.DateOfBirth.HasValue ? employeeDetails.DateOfBirth.Value.ToString("MMMM dd, yyyy") : "NA",
                        JoiningDateString = employeeDetails.DateOfJoining.HasValue ? employeeDetails.DateOfJoining.Value.ToString("MMMM dd, yyyy") : "NA",
                        GenderID = employeeDetails.GenderID,
                        GenderName = employeeDetails.GenderID.HasValue ? employeeDetails.Gender.Description : "NA",
                        DepartmentID = employeeDetails.DepartmentID,
                        DepartmentName = employeeDetails.DepartmentID.HasValue ? employeeDetails.Departments1.DepartmentName : "NA",
                        DesignationID = employeeDetails.DesignationID,
                        DesignationName = employeeDetails.DesignationID.HasValue ? employeeDetails.Designation.DesignationName : "NA",
                        EmployeePhoto = employeeDetails.EmployeePhoto != null ? employeeDetails.EmployeePhoto : null,
                    };
                }
                return staffDetails;
            }
        }

        public int? GetAgeByDateOfBirth(DateTime? dateOfBirth)
        {
            int? age = null;

            if (dateOfBirth.HasValue)
            {
                age = DateTime.Now.Subtract(Convert.ToDateTime(dateOfBirth)).Days;
                age = age / 365;
            }

            return age;
        }

        public string GetDriverDetailsByLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                string driverEmployee = null;

                var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DRIVER_DESIGNATION_ID");

                if (settingValue != null)
                {
                    var designationID = Convert.ToInt32(settingValue);

                    var empDetail = dbContext.Employees.Where(x => x.LoginID == loginID && x.DesignationID == designationID).AsNoTracking().FirstOrDefault();

                    if (empDetail != null)
                    {
                        driverEmployee = "true";
                    }
                }

                return driverEmployee;
            }
        }

        public string GetEmployeeEmailIDByEmployeeCode(string employeeCode)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var employee = dbContext.Employees.Where(s => s.EmployeeCode == employeeCode)
                    .Include(i => i.Login)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (employee != null)
                {
                    return employee.Login.LoginEmailID;
                }
                else
                {
                    return null;
                }
            }
        }

        public EmployeesDTO GetStaffDetailsByLoginID(long loginID)
        {
            var empDto = new EmployeesDTO();
            using (var dbContext = new dbEduegateHRContext())
            {
                var empDetail = dbContext.Employees.Where(x => x.LoginID == loginID).AsNoTracking().FirstOrDefault();

                empDto.EmployeeIID = empDetail.EmployeeIID;
                empDto.BranchID = empDetail.BranchID;
                empDto.FirstName = empDetail.FirstName;
                empDto.MiddleName = empDetail.MiddleName;
                empDto.LastName = empDetail.LastName;

                return empDto;
            }
        }

        public List<KeyValueDTO> GetEmployeesByDepartment(long departmentID)
        {
            var keyValueList = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateHRContext())
            {
                var entities = departmentID > 0 ? dbContext.Employees.Where(x => x.DepartmentID == departmentID && x.IsActive == true).AsNoTracking().ToList() :
                    dbContext.Employees.Where(x => x.IsActive == true).AsNoTracking().ToList();

                foreach (var entity in entities)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = entity.EmployeeIID.ToString(),
                        Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName,
                    });
                }
            }

            return keyValueList;
        }

        public KeyValueDTO GetSchoolPrincipalOrHeadMistress(byte schoolID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var keyValue = new KeyValueDTO();

                var princiDesign = new Domain.Setting.SettingBL(null).GetSettingValue<int>("DESIGNATION_ID_PRINCIPAL", 25);
                var mistrsDesign = new Domain.Setting.SettingBL(null).GetSettingValue<int>("DESIGNATION_ID_HEAD_MISTRESS", 12);

                var entity = dbContext.Employees.Where(x => x.BranchID == schoolID && x.IsActive == true && (x.DesignationID == princiDesign || x.DesignationID == mistrsDesign))
                    .AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    keyValue = new KeyValueDTO()
                    {
                        Key = entity.EmployeeIID.ToString(),
                        Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName,
                    };
                }
                else
                {
                    return null;
                }

                return keyValue;
            }
        }

        public KeyValueDTO GetSchoolWisePrincipal(byte schoolID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var keyValue = new KeyValueDTO();

                var vicePrinciDesign = new Domain.Setting.SettingBL(null).GetSettingValue<int>("DESIGNATION_ID_VICE_PRINCIPAL", 38);

                var entity = dbContext.Employees.Where(x => x.BranchID == schoolID && x.IsActive == true && x.DesignationID == vicePrinciDesign)
                    .AsNoTracking().FirstOrDefault();

                if (entity != null)
                {
                    keyValue = new KeyValueDTO()
                    {
                        Key = entity.EmployeeIID.ToString(),
                        Value = entity.EmployeeCode + " - " + entity.FirstName + " " + (entity.MiddleName != null ? entity.MiddleName + " " : "") + entity.LastName,
                    };
                }
                else
                {
                    return null;
                }

                return keyValue;
            }
        }

    }
}