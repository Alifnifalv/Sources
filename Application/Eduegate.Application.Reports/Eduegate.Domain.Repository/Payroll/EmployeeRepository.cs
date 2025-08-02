using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using System.Data.Entity;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.Entity.Models.HR;

namespace Eduegate.Domain.Repository.Payroll
{
    public class EmployeeRepository
    {
        public List<Employee> GetEmployees()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employees = dbContext.Employees
                    .Include(x => x.EmployeeAdditionalInfos)
                    .Include(x => x.PassportVisaDetails)
                    .Include(x => x.Login)
                    .Include(x => x.Nationality)
                    .Include(x => x.EmployeeBankDetails)
                    .Include(x => x.EmployeeRoleMaps).Where(x => x.IsActive == true);

                foreach (var employee in employees)
                {
                    foreach (var role in employee.EmployeeRoleMaps)
                    {
                        dbContext.Entry(role).Reference(a => a.EmployeeRole).Load();
                    }
                }

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
                                  .Take(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
                                  .OrderBy(a => a.FirstName);
                }
                else
                {
                    employees = (from emp in dbContext.Employees
                                 join role in dbContext.EmployeeRoleMaps on emp.EmployeeIID equals role.EmployeeID
                                 where role.EmployeeRoleID == roleID && emp.DesignationID == designationID
                                 select emp)
                                  .Take(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
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
                var sequence = db.Sequences.Where(x => x.SequenceType == sequenceTypes).FirstOrDefault();
                sequence.LastSequence = sequence.LastSequence.HasValue ? sequence.LastSequence + 1 : 1;
                db.SaveChanges();
                return sequence;
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
                                  .Take(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
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
                                  .Take(Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue<int>("MaxFetchCount"))
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
                                 select emp).OrderBy(a => a.FirstName);
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


        public List<Employee> GetEmployees(string searchText, int pageSize)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees.Include("Login").
                    Where(x => x.FirstName +" "+ x.MiddleName +" "+ x.LastName != null && x.FirstName.Contains(searchText)).Take(pageSize).ToList();
            }
        }

        public Employee GetEmployee(long employeeID, bool isAdditionalInfoRequired = true)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var employee = dbContext.Employees
                    .Where(x => x.EmployeeIID == employeeID).FirstOrDefault();

                if (isAdditionalInfoRequired)
                {
                    dbContext.Entry(employee).Reference(a => a.Login).Load();
                    dbContext.Entry(employee).Collection(a => a.EmployeeRoleMaps).Load();
                    dbContext.Entry(employee).Collection(a => a.EmployeeAdditionalInfos).Load();
                    dbContext.Entry(employee).Collection(a => a.PassportVisaDetails).Load();
                    dbContext.Entry(employee).Collection(a => a.EmployeeBankDetails).Load();
                    dbContext.Entry(employee).Collection(a => a.EmployeeLeaveAllocations).Load();
                    dbContext.Entry(employee).Collection(a => a.EmployeeRelationsDetails).Load();
                    dbContext.Entry(employee).Reference(a => a.Nationality).Load();

                    foreach (var role in employee.EmployeeRoleMaps)
                    {
                        dbContext.Entry(role).Reference(a => a.EmployeeRole).Load();
                    }

                    foreach (var passVisa in employee.PassportVisaDetails)
                    {
                        dbContext.Entry(passVisa).Reference(a => a.Country).Load();
                    }
                    foreach (var leaveAloc in employee.EmployeeLeaveAllocations)
                    {
                        dbContext.Entry(leaveAloc).Reference(a => a.LeaveType).Load();
                    }
                  
                }

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
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    if (entity.Login != null)
                    {
                        if (entity.Login.LoginIID == 0)
                            dbContext.Entry(entity.Login).State = System.Data.Entity.EntityState.Added;
                        else
                            dbContext.Entry(entity.Login).State = System.Data.Entity.EntityState.Modified;

                        if (entity.Login.Password == null)
                        {
                            dbContext.Entry(entity.Login).Property(a => a.Password).IsModified = false;
                            dbContext.Entry(entity.Login).Property(a => a.PasswordSalt).IsModified = false;
                        }
                        entity.Login.StatusID = entity.IsActive == false ? (byte?)0 : 1;
                        //Delete contacts
                        var roleMaps = dbContext.EmployeeRoleMaps.Where(x => x.EmployeeID == entity.EmployeeIID).ToList();

                        if (roleMaps != null)
                            dbContext.EmployeeRoleMaps.RemoveRange(roleMaps);

                        if (entity.Login.Contacts != null)
                        {
                            foreach (var contact in entity.Login.Contacts)
                            {
                                if (contact.ContactIID == 0)
                                    dbContext.Entry(contact).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.PassportVisaDetails != null)
                    {
                        foreach (var passportVisaInfo in entity.PassportVisaDetails)
                        {
                            if (passportVisaInfo.PassportVisaIID == 0)
                                dbContext.Entry(passportVisaInfo).State = System.Data.Entity.EntityState.Added;
                            else
                                dbContext.Entry(passportVisaInfo).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeBankDetails != null)
                    {
                        foreach (var bankDetail in entity.EmployeeBankDetails)
                        {
                            if (bankDetail.EmployeeBankIID == 0)
                                dbContext.Entry(bankDetail).State = System.Data.Entity.EntityState.Added;
                            else
                                dbContext.Entry(bankDetail).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    if (entity.EmployeeAdditionalInfos != null)
                    {
                        foreach (var additionaInfo in entity.EmployeeAdditionalInfos)
                        {
                            if (additionaInfo.EmployeeAdditionalInfoIID == 0)
                                dbContext.Entry(additionaInfo).State = System.Data.Entity.EntityState.Added;
                            else
                                dbContext.Entry(additionaInfo).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    if (entity.EmployeeLeaveAllocations != null)
                    {
                        foreach (var leaveAlloc in entity.EmployeeLeaveAllocations)
                        {
                            if (leaveAlloc.LeaveAllocationIID == 0)
                                dbContext.Entry(leaveAlloc).State = System.Data.Entity.EntityState.Added;
                            else
                                dbContext.Entry(leaveAlloc).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    if (entity.EmployeeRelationsDetails != null)
                    {
                        foreach (var relation in entity.EmployeeRelationsDetails)
                        {
                            if (relation.EmployeeRelationsDetailIID == 0)
                                dbContext.Entry(relation).State = System.Data.Entity.EntityState.Added;
                            else
                                dbContext.Entry(relation).State = System.Data.Entity.EntityState.Modified;
                        }
                    }

                    dbContext.SaveChanges();
                    updatedEntity = GetEmployee(entity.EmployeeIID);
                }
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
                List<EmployeeCatalogRelation> deleteEntity = db.EmployeeCatalogRelations.Where(x => x.RelationID == entity.RelationID).ToList();
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
                                        select e).ToList();
                return lists;
            }
        }

        public string GetEmployeeName(long employeeIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from employee in dbContext.Employees
                        where employee.EmployeeIID == employeeIID
                        select employee.FirstName+" "+employee.MiddleName+" "+employee.LastName).FirstOrDefault();
            }
        }

        public Employee GetEmployeeByLoginID(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees
                    .Include(a => a.EmployeeRoleMaps)
                    .Include(a => a.EmployeeRoleMaps.Select(b => b.EmployeeRole))
                    .Where(a => a.LoginID == loginID)
                    .FirstOrDefault();
            }
        }

        public Employee GetEmployeeByEmployeeID(long? employeeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Employees
                    .Where(a => a.EmployeeIID == employeeID)
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
                     .Include("EmployeeRoleMaps.EmployeeRole")
                     .Include(x => x.EmployeeAdditionalInfos)
                     .Include(x => x.PassportVisaDetails)
                     .Include("PassportVisaDetails.Country")
                     .Include("PassportVisaDetails.Employee")
                     .Include(x => x.EmployeeBankDetails)
                     .Include(x => x.Nationality)
                    .Where(e => e.Designation.DesignationCode == designationCode && e.IsActive == true)
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
                    .Include(i => i.PassportVisaDetails)
                    .Include(i => i.EmployeeBankDetails)
                    .Include(i => i.EmployeeRoleMaps)
                    .Include(i => i.EmployeeLeaveAllocations)
                    .OrderByDescending(o => o.EmployeeIID).FirstOrDefault();

                return employee;
            }
        }

    }
}