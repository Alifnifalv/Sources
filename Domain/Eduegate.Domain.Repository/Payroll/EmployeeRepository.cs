using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.Entity.Models.HR;
using Eduegate.Domain.Entity;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Repository.Payroll
{
    public class EmployeeRepository
    {
        public List<Employee> GetEmployees()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employees = dbContext.Employees.Where(x => x.IsActive == true)
                    .Include(x => x.EmployeeAdditionalInfos)
                    .Include(x => x.PassportVisaDetails)
                    .Include(x => x.Login)
                    .Include(x => x.Nationality)
                    .Include(x => x.EmployeeBankDetails)
                    .Include(x => x.EmployeeRoleMaps).ThenInclude(i => i.EmployeeRole)
                    .AsNoTracking()
                    .ToList();

                return employees.OrderBy(a => a.FirstName +" "+ a.MiddleName +" "+ a.LastName).ToList();
            }
        }

        public List<Employee> GetEmployeesByRolesAndDesignation(int? roleID, int? designationID, string searchText = "")
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                IEnumerable<Employee> employees;

                if (!string.IsNullOrEmpty(searchText))
                {
                    employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where emp.DesignationID == designationID && role.EmployeeRoleID == roleID && emp.FirstName.Contains(searchText)
                                 select emp)
                                  .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                                  .OrderBy(a => a.FirstName);
                }
                else
                {
                    employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where role.EmployeeRoleID == roleID && emp.DesignationID == designationID
                                 select emp)
                                  .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                                  .OrderBy(a => a.FirstName);
                }

                foreach (var employee in employees)
                {
                    foreach (var role in employee.EmployeeRoleMaps)
                    {
                        dbContext.Entry(role).Reference(a => a.EmployeeRole).Load();
                    }

                    dbContext.Entry(employee).Reference(a => a.Login).Load();
                }

                return employees.ToList();
            }
        }
        public Sequence GetNextSequence(string sequenceTypes)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var sequence = db.Sequences.Where(x => x.SequenceType == sequenceTypes)
                    .AsNoTracking()
                    .FirstOrDefault();
                sequence.LastSequence = sequence.LastSequence.HasValue ? sequence.LastSequence + 1 : 1;

                db.Entry(sequence).State = EntityState.Modified;
                db.SaveChanges();
                return sequence;
            }
        }
        public List<Employee> GetEmployeeByBranch(int? branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employees = (from emp in dbContext.Employees                                
                                 where emp.BranchID == branchID
                                 select emp)
                                 .OrderBy(a => a.FirstName)                                
                                 .AsNoTracking()
                                 .ToList();

                return employees;
            }
        }
        public List<Employee> GetEmployeesByRoles(int? roleID, string searchText = "")
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                IEnumerable<Employee> employees;

                if (!string.IsNullOrEmpty(searchText))
                {
                    employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where role.EmployeeRoleID == roleID && emp.FirstName.Contains(searchText)
                                 select emp)
                                  .Include(x => x.EmployeeAdditionalInfos)
                                  .Include(x => x.PassportVisaDetails)
                                  .Include(x => x.EmployeeBankDetails)
                                  .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                                  .AsNoTracking()
                                  .OrderBy(a => a.FirstName);
                }
                else
                {
                    employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where role.EmployeeRoleID == roleID
                                 select emp)
                                  .Include(x=> x.EmployeeAdditionalInfos)
                                  .Include(x => x.PassportVisaDetails)
                                  .Include(x => x.EmployeeBankDetails)
                                  .Take(new Domain.Setting.SettingBL(null).GetSettingValue<int>("MaxFetchCount"))
                                  .AsNoTracking()
                                  .OrderBy(a => a.FirstName);
                }

                foreach (var employee in employees)
                {
                    foreach (var role in employee.EmployeeRoleMaps)
                    {
                        dbContext.Entry(role).Reference(a => a.EmployeeRole).Load();
                    }

                    dbContext.Entry(employee).Reference(a => a.Login).Load();
                }

                return employees.ToList();
            }
        }

        public List<Employee> GetEmployeesBySkus(int roleID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where role.EmployeeRoleID == roleID
                                 select emp)
                                 .OrderBy(a => a.FirstName)
                                 .Include(i => i.EmployeeRole)
                                 .Include(i => i.Login)
                                 .AsNoTracking()
                                 .ToList();

                return employees;
            }
        }


        public List<Employee> GetEmployees(string searchText, int pageSize)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees.Include(i => i.Login).
                    Where(x => x.FirstName +" "+ x.MiddleName +" "+ x.LastName != null && x.FirstName.Contains(searchText)).Take(pageSize).AsNoTracking().ToList();
            }
        }

        public Employee GetEmployee(long employeeID, bool isAdditionalInfoRequired = true)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employee = dbContext.Employees
                    .Where(x => x.EmployeeIID == employeeID)
                    .Include(i => i.Login)
                    .Include(i => i.EmployeeAdditionalInfos)
                    .Include(i => i.EmployeeBankDetails)
                    .Include(i => i.Nationality)
                    .Include(i => i.EmployeeRoleMaps).ThenInclude(i => i.EmployeeRole)
                    .Include(i => i.PassportVisaDetails).ThenInclude(i => i.Country)
                    .Include(i => i.EmployeeLeaveAllocations).ThenInclude(i => i.LeaveType)
                    .Include(i => i.EmployeeQualificationMaps).ThenInclude(i => i.Qualification)
                    .Include(i => i.EmployeeExperienceDetails)
                    .Include(i => i.EmployeeNearestAirport)
                    .Include(i => i.EmployeeCountryAirport)
                    .Include(i => i.TicketEntitilement)
                    .Include(i => i.FlightClass)
                    .Include(i => i.EmployeeRelationsDetails).ThenInclude(i => i.EmployeeRelationType)
                    .AsNoTracking()
                    .FirstOrDefault();

                return employee;
            }
        }

        public Employee SaveEmployeeReq()
        {
            return null;
        }

        public Employee SaveEmployee(Employee entity, CallContext callContext)
        {
            Employee updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Employees.Add(entity);

                    if (entity.EmployeeIID == 0)
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    if (entity.Login != null)
                    {
                        if (entity.Login.LoginIID == 0)
                            dbContext.Entry(entity.Login).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        else
                            dbContext.Entry(entity.Login).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        if (entity.Login.Password == null)
                        {
                            dbContext.Entry(entity.Login).Property(a => a.Password).IsModified = false;
                            dbContext.Entry(entity.Login).Property(a => a.PasswordSalt).IsModified = false;
                        }
                        entity.Login.StatusID = entity.IsActive == false ? (byte?)0 : 1;
                        //Delete contacts
                        var roleMaps = dbContext.EmployeeRoleMaps.Where(x => x.EmployeeID == entity.EmployeeIID).AsNoTracking().ToList();

                        if (roleMaps != null)
                            dbContext.EmployeeRoleMaps.RemoveRange(roleMaps);

                        if (entity.Login.Contacts != null)
                        {
                            foreach (var contact in entity.Login.Contacts)
                            {
                                if (contact.ContactIID == 0)
                                    dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                else
                                    dbContext.Entry(contact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.PassportVisaDetails != null)
                    {
                        foreach (var passportVisaInfo in entity.PassportVisaDetails)
                        {
                            if (passportVisaInfo.PassportVisaIID == 0)
                                dbContext.Entry(passportVisaInfo).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(passportVisaInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeBankDetails != null)
                    {
                        foreach (var bankDetail in entity.EmployeeBankDetails)
                        {
                            if (bankDetail.EmployeeBankIID == 0)
                                dbContext.Entry(bankDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(bankDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeAdditionalInfos != null)
                    {
                        foreach (var additionaInfo in entity.EmployeeAdditionalInfos)
                        {
                            if (additionaInfo.EmployeeAdditionalInfoIID == 0)
                                dbContext.Entry(additionaInfo).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(additionaInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                    if (entity.EmployeeLeaveAllocations != null)
                    {
                        foreach (var leaveAlloc in entity.EmployeeLeaveAllocations)
                        {
                            if (leaveAlloc.LeaveAllocationIID == 0)
                                dbContext.Entry(leaveAlloc).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(leaveAlloc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                    if (entity.EmployeeQualificationMaps != null)
                    {
                        foreach (var academics in entity.EmployeeQualificationMaps)
                        {
                            if (academics.EmployeeQualificationMapIID == 0)
                                dbContext.Entry(academics).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(academics).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeExperienceDetails != null)
                    {
                        foreach (var experience in entity.EmployeeExperienceDetails)
                        {
                            if (experience.EmployeeExperienceDetailIID == 0)
                                dbContext.Entry(experience).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(experience).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeRelationsDetails != null)
                    {
                        foreach (var relations in entity.EmployeeRelationsDetails)
                        {
                            if (relations.EmployeeRelationsDetailIID == 0)
                                dbContext.Entry(relations).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            else
                                dbContext.Entry(relations).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.SaveChanges();
                }
                updatedEntity = GetEmployee(entity.EmployeeIID);

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }




        public EmployeeCatalogRelation AddEmployeeCatalogRelations(EmployeeCatalogRelation entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.EmployeeCatalogRelations.Add(entity);
                db.SaveChanges();
                return entity;
            }
        }

        public bool RemoveEmployeeCatalogRelationsByRelationId(EmployeeCatalogRelation entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<EmployeeCatalogRelation> deleteEntity = db.EmployeeCatalogRelations.Where(x => x.RelationID == entity.RelationID).AsNoTracking().ToList();
                if (deleteEntity != null)
                {
                    db.EmployeeCatalogRelations.RemoveRange(deleteEntity);
                    db.SaveChanges();
                }
                isSuccess = true;
                return isSuccess;
            }
        }

        public List<Employee> GetEmployeeIdNameCatalogRelation(short relationTypeID, long relationID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<Employee> lists = (from e in db.Employees
                                        join r in db.EmployeeCatalogRelations on e.EmployeeIID equals r.EmployeeID
                                        where r.RelationTypeID == relationTypeID && r.RelationID == relationID
                                        select e).AsNoTracking().ToList();
                return lists;
            }
        }

        public string GetEmployeeName(long employeeIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from employee in dbContext.Employees
                        where employee.EmployeeIID == employeeIID
                        select employee.FirstName+" "+employee.MiddleName+" "+employee.LastName)
                        .AsNoTracking()
                        .FirstOrDefault();
            }
        }

        public Employee GetEmployeeByLoginID(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees
                    .Include(a => a.EmployeeRoleMaps)
                    .Include(i => i.EmployeeRoleMaps).ThenInclude(i => i.EmployeeRole)
                    .Where(a => a.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public Employee GetEmployeeByEmployeeID(long? employeeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees
                    .Where(a => a.EmployeeIID == employeeID)
                    .Include(a => a.Department)
                    .Include(a => a.Designation)
                    .Include(a => a.PassportVisaDetails)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Employee> GetEmployeesByDesignation(string designationCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees
                    .Include(x => x.Login)
                    .Include(x => x.EmployeeRoleMaps)
                    .Include(i => i.EmployeeRoleMaps).ThenInclude(i => i.EmployeeRole)
                    .Include(x => x.EmployeeAdditionalInfos)
                    .Include(x => x.PassportVisaDetails)
                    .Include(i => i.PassportVisaDetails).ThenInclude(i => i.Country)
                    .Include(i => i.PassportVisaDetails).ThenInclude(i => i.Employee)
                    .Include(x => x.EmployeeBankDetails)
                    .Include(x => x.Nationality)
                    .Where(e => e.Designation.DesignationCode == designationCode && e.IsActive == true)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Employee GetEmployeeDataByLogin(long? loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employee = dbContext.Employees.Where(e => e.LoginID == loginID)
                    .Include(i => i.Login)
                    .Include(i => i.Nationality)
                    .Include(i => i.EmployeeAdditionalInfos)
                    .Include(i => i.PassportVisaDetails).ThenInclude(i => i.Country)
                    .Include(i => i.EmployeeBankDetails)
                    .Include(i => i.EmployeeRoleMaps).ThenInclude(i => i.EmployeeRole)
                    .Include(i => i.EmployeeLeaveAllocations).ThenInclude(i => i.LeaveType)
                    .OrderByDescending(o => o.EmployeeIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return employee;
            }
        }

        public List<Employee> GetEmployeesByBranch(long? branchID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees.Where(e => e.BranchID == branchID && e.IsActive == true)
                        .Include(i => i.Designation)
                        .AsNoTracking().ToList();
            }
        }


        public EmployeeDTO GetEmployeeDetailsByEmployeeIID(long employeeIID) 
        {
            var empDto = new EmployeeDTO();
            var emp = GetEmployeeByEmployeeID(employeeIID);

            if(emp != null)
            {
                empDto.EmployeeIID = employeeIID;
                empDto.IsActive = emp.IsActive;
                empDto.EmployeeName = emp.FirstName +" "+emp.MiddleName+" "+emp.LastName;
                empDto.EmployeeCode = emp.EmployeeCode;
                empDto.WorkEmail = emp.WorkEmail;
                empDto.DepartmentName = emp.Department.DepartmentName;
                empDto.DesignationName = emp.Designation.DesignationName;
                empDto.NationalIDNo = emp.PassportVisaDetails.FirstOrDefault().NationalIDNo;
            }

            return empDto;
        }

        public EmployeeDTO GetCandidateFromInterviewMap(long? ID)
        {
            var dto = new EmployeeDTO();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.JobInterviewMaps
                    .Where(a => a.MapID == ID)
                    .Include(a => a.Applicant).ThenInclude(c => c.Country)
                    .Include(a => a.Applicant).ThenInclude(c => c.Nationality)
                    .Include(a => a.Interview).ThenInclude(b => b.Job)
                    .AsNoTracking()
                    .FirstOrDefault();

                dto.JobInterviewMapID = ID;
                dto.JobTypeID = entity.Interview?.Job?.JobTypeID;
                dto.IsActive = true;
                dto.FirstName = entity.Applicant?.FirstName;
                dto.MiddleName = entity.Applicant?.MiddleName;
                dto.LastName = entity.Applicant?.LastName;
                dto.GenderID = entity.Applicant?.GenderID;
                dto.DateOfBirth = entity.Applicant?.DateOfBirth;
                dto.Age = entity.Applicant?.Age;
                dto.BloodGroupID = entity.Applicant?.BloodGroupID;
                dto.NationalityID = entity.Applicant?.NationalityID;
                dto.NationalityName = entity.Applicant?.Nationality?.NationalityName;
                dto.WorkEmail = entity.Applicant?.EmailID;
                dto.WorkPhone = entity.Applicant?.PhoneNumber;
                dto.WorkMobileNo = entity.Applicant?.MobileNumber;
                dto.EmergencyContactNo = entity.Applicant?.MobileNumber;
            }

            return dto;
        }

    }
}