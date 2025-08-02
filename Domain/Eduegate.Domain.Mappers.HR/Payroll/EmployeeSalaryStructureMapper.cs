using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeSalaryStructureMapper : DTOEntityDynamicMapper
    {
        public static EmployeeSalaryStructureMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeSalaryStructureMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeSalaryStructureDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entityList = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeSalaryStructureIID == IID)
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Include(x => x.SalaryStructure)
                    .Include(x => x.SalaryPaymentMode)
                    .Include(x => x.PayrollFrequency)
                    .Include(x => x.SalaryComponent)
                    .Include(x => x.Account)
                    .Include(x => x.EmployeeSalaryStructureComponentMaps).ThenInclude(x => x.SalaryComponent)
                    .Include(x => x.EmployeeSalaryStructureComponentMaps).ThenInclude(x => x.EmployeeSalaryStructureVariableMaps)
                    .Include(x => x.EmployeeSalaryStructureLeaveSalaryMaps).ThenInclude(x => x.SalaryComponent)
                    .Include(x => x.LeaveSalaryStructure)
                    .AsNoTracking()
                    .ToList();

                var employee = new List<KeyValueDTO>();
                EmployeeSalaryStructure employeeMap = null;
                foreach (var emp in entityList)
                {
                    employee.Add(new KeyValueDTO()
                    {
                        Value = emp.Employee.EmployeeCode + " - " + emp.Employee.FirstName + " " + emp.Employee.MiddleName + " " + emp.Employee.LastName,
                        Key = emp.EmployeeID.ToString()
                    });
                    employeeMap = emp;
                }

                var entity = entityList.Count > 0 ? entityList.FirstOrDefault() : null;
                var structure = new EmployeeSalaryStructureDTO();
                if (entity != null)
                {
                    structure = new EmployeeSalaryStructureDTO()
                    {
                        EmployeeSalaryStructureIID = entity.EmployeeSalaryStructureIID,
                        EmployeeID = entity.EmployeeID,
                        Employee = employee,
                        SalaryStructureID = entity.SalaryStructureID,
                        EmployeeSalaryStructure = new KeyValueDTO()
                        {
                            Key = entity.SalaryStructureID.ToString(),
                            Value = entity.SalaryStructure.StructureName
                        },
                        FromDate = entity.FromDate,
                        IsActive = entity.IsActive,
                        Amount = entity.Amount,
                        AccountID = entity.AccountID,
                        PayrollFrequencyID = entity.PayrollFrequencyID,
                        IsSalaryBasedOnTimeSheet = entity.IsSalaryBasedOnTimeSheet,
                        PaymentModeID = entity.PaymentModeID,
                        TimeSheetSalaryComponentID = entity.TimeSheetSalaryComponentID,
                        TimeSheetHourRate = entity.TimeSheetHourRate,
                        TimeSheetLeaveEncashmentPerDay = entity.TimeSheetLeaveEncashmentPerDay,
                        TimeSheetMaximumBenefits = entity.TimeSheetMaximumBenefits,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        PaymentMode = entity.PaymentModeID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.PaymentModeID.ToString(),
                            Value = entity.SalaryPaymentMode.PaymentName
                        }: new KeyValueDTO(),
                        PayrollFrequency = entity.PayrollFrequencyID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.PayrollFrequencyID.ToString(),
                            Value = entity.PayrollFrequency.FrequencyName
                        }: new KeyValueDTO(),
                        TimeSheetSalaryComponent = entity.TimeSheetSalaryComponentID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.TimeSheetSalaryComponentID.HasValue ? Convert.ToString(entity.TimeSheetSalaryComponentID) : null,
                            Value = entity.SalaryComponent.Description
                        } : new KeyValueDTO(),

                        Account = entity.AccountID.HasValue? new KeyValueDTO()
                        {
                            Key = entity.AccountID.ToString(),
                            Value = entity.Account.AccountName
                        }: new KeyValueDTO(),
                        LeaveSalaryStructureID = entity.LeaveSalaryStructureID,
                        EmployeeLeaveSalaryStructure = entity.LeaveSalaryStructureID.HasValue ? new KeyValueDTO()
                        {
                            Key = entity.LeaveSalaryStructureID.ToString(),
                            Value = entity.LeaveSalaryStructure.StructureName
                        } : new KeyValueDTO(),
                    };

                    structure.SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();

                    foreach (var salaryComponent in entity.EmployeeSalaryStructureComponentMaps)
                    {
                        var listcomponentVariableMapDTO = new List<EmployeeSalaryStructureVariableDTO>();
                        foreach (var variablemap in salaryComponent.EmployeeSalaryStructureVariableMaps)
                        {
                            var componentVariableMapDTO = new EmployeeSalaryStructureVariableDTO()
                            {
                                EmployeeSalaryStructureVariableMapIID = variablemap.EmployeeSalaryStructureVariableMapIID,
                                EmployeeSalaryStructureComponentMapID = salaryComponent.EmployeeSalaryStructureComponentMapIID,
                                VariableValue = variablemap.VariableValue,
                                VariableKey = variablemap.VariableKey
                            };
                            listcomponentVariableMapDTO.Add(componentVariableMapDTO);
                        }

                        structure.SalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                        {
                            EmployeeSalaryStructureComponentMapIID = salaryComponent.EmployeeSalaryStructureComponentMapIID,
                            Amount = salaryComponent.Amount,
                            EmployeeSalaryStructureID = salaryComponent.EmployeeSalaryStructureID,
                            SalaryComponentID = salaryComponent.SalaryComponentID,
                            Formula = salaryComponent.Formula,
                            SalaryComponent = new KeyValueDTO()
                            {
                                Key = salaryComponent.SalaryComponentID.ToString(),
                                Value = salaryComponent.SalaryComponent.Description
                            },
                            EmployeeSalaryStructureVariableMap = listcomponentVariableMapDTO
                        });
                    }

                    foreach (var leaveSalaryComponent in entity.EmployeeSalaryStructureLeaveSalaryMaps)
                    {
                        structure.LeaveSalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                        {
                            EmployeeSalaryStructureComponentMapIID = leaveSalaryComponent.EmployeeSalaryStructureLeaveSalaryMapIID,
                            Amount = leaveSalaryComponent.Amount,
                            EmployeeSalaryStructureID = leaveSalaryComponent.EmployeeSalaryStructureID,
                            SalaryComponentID = leaveSalaryComponent.SalaryComponentID,
                            Formula = leaveSalaryComponent.Formula,
                            SalaryComponent = new KeyValueDTO()
                            {
                                Key = leaveSalaryComponent.SalaryComponentID.ToString(),
                                Value = leaveSalaryComponent.SalaryComponent.Description
                            }
                        });
                    }

                }

                return ToDTOString(structure);
            }
        }
        public EmployeeSalaryStructureDTO GetEmployeesSalaryStructure(long employeeID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var maxID = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeID == employeeID && a.IsActive == true).AsNoTracking().Max(y => (long?)y.EmployeeSalaryStructureIID);

                var entity = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeSalaryStructureIID == maxID)
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Include(x => x.SalaryStructure)
                    .Include(x => x.SalaryPaymentMode)
                    .Include(x => x.PayrollFrequency)
                    .Include(x => x.SalaryComponent)
                    .Include(x => x.Account)
                    .AsNoTracking()
                    .FirstOrDefault();

                var structure = new EmployeeSalaryStructureDTO()
                {
                    EmployeeSalaryStructureIID = entity.EmployeeSalaryStructureIID,
                    EmployeeID = entity.EmployeeID,
                    SalaryStructureID = entity.SalaryStructureID,
                    EmployeeSalaryStructure = new KeyValueDTO()
                    {
                        Key = entity.SalaryStructureID.ToString(),
                        Value = entity.SalaryStructure.StructureName
                    },
                    FromDate = entity.FromDate,
                    Amount = entity.Amount,
                    AccountID = entity.AccountID,
                    PayrollFrequencyID = entity.PayrollFrequencyID,
                    IsSalaryBasedOnTimeSheet = entity.IsSalaryBasedOnTimeSheet,
                    PaymentModeID = entity.PaymentModeID,
                    TimeSheetSalaryComponentID = entity.TimeSheetSalaryComponentID,
                    TimeSheetHourRate = entity.TimeSheetHourRate,
                    TimeSheetLeaveEncashmentPerDay = entity.TimeSheetLeaveEncashmentPerDay,
                    TimeSheetMaximumBenefits = entity.TimeSheetMaximumBenefits,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,

                    PaymentMode = new KeyValueDTO()
                    {
                        Key = entity.PaymentModeID.ToString(),
                        Value = entity.SalaryPaymentMode.PaymentName
                    },
                    PayrollFrequency = new KeyValueDTO()
                    {

                        Key = entity.PayrollFrequencyID.ToString(),
                        Value = entity.PayrollFrequency.FrequencyName
                    },
                    TimeSheetSalaryComponent = entity.TimeSheetSalaryComponentID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TimeSheetSalaryComponentID.HasValue ? Convert.ToString(entity.TimeSheetSalaryComponentID) : null,
                        Value = entity.SalaryComponent.Description
                    } : new KeyValueDTO(),
                    Account = new KeyValueDTO()
                    {
                        Key = entity.AccountID.ToString(),
                        Value = entity.Account.AccountName
                    }
                };

                structure.SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
                var entitySalaryComponentMaps = dbContext.EmployeeSalaryStructureComponentMaps.Where(X => X.EmployeeSalaryStructureID == entity.EmployeeSalaryStructureIID)
                    .Include(i => i.SalaryComponent)
                    .AsNoTracking()
                    .ToList();

                foreach (var salaryComponent in entitySalaryComponentMaps)
                {
                    structure.SalaryComponents.Add(new EmployeeSalaryStructureComponentMapDTO()
                    {
                        EmployeeSalaryStructureComponentMapIID = salaryComponent.EmployeeSalaryStructureComponentMapIID,
                        Amount = salaryComponent.Amount,
                        Deduction = salaryComponent.Amount.Value < 0 ? salaryComponent.Amount.Value * -1 : (decimal?)null,
                        Earnings = salaryComponent.Amount.Value > 0 ? salaryComponent.Amount.Value * 1 : (decimal?)null,
                        EmployeeSalaryStructureID = salaryComponent.EmployeeSalaryStructureID,
                        SalaryComponentID = salaryComponent.SalaryComponentID,
                        Formula = salaryComponent.Formula,
                        SalaryComponent = new KeyValueDTO()
                        {
                            Key = salaryComponent.SalaryComponentID.ToString(),
                            Value = salaryComponent.SalaryComponent.Description
                        }
                    });
                }

                return structure;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeSalaryStructureDTO;
            //convert the dto to entity and pass to the repository.

            if (toDto.Employee.Count == 0)
            {
                throw new Exception("Please Select an Employee!");
            }

            if (toDto.SalaryStructureID == null || toDto.SalaryStructureID == 0)
            {
                throw new Exception("Please Select a Salary Structure Name!");
            }
            if (toDto.SalaryComponents.Count == 0)
            {
                throw new Exception("Please Select any Salary Component!");
            }

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                EmployeeSalaryStructure employeeMap = null;
                if (toDto.Employee.Count > 0)
                {
                    foreach (KeyValueDTO keyval in toDto.Employee)
                    {
                        var entity = new EmployeeSalaryStructure()
                        {
                            EmployeeSalaryStructureIID = toDto.EmployeeSalaryStructureIID,
                            //EmployeeID = toDto.EmployeeID,
                            EmployeeID = int.Parse(keyval.Key),
                            SalaryStructureID = toDto.SalaryStructureID,
                            FromDate = toDto.FromDate,
                            Amount = toDto.Amount,
                            AccountID = toDto.AccountID,
                            IsSalaryBasedOnTimeSheet = toDto.IsSalaryBasedOnTimeSheet,
                            PaymentModeID = toDto.PaymentModeID,
                            PayrollFrequencyID = toDto.PayrollFrequencyID,
                            TimeSheetSalaryComponentID = toDto.TimeSheetSalaryComponentID,
                            TimeSheetHourRate = toDto.TimeSheetHourRate,
                            TimeSheetLeaveEncashmentPerDay = toDto.TimeSheetLeaveEncashmentPerDay,
                            TimeSheetMaximumBenefits = toDto.TimeSheetMaximumBenefits,
                            LeaveSalaryStructureID = toDto.LeaveSalaryStructureID,
                        };
                        if (toDto.EmployeeSalaryStructureIID == 0)
                        {
                            entity.IsActive = toDto.IsActive;
                            entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                            entity.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            entity.IsActive = toDto.IsActive;
                            entity.CreatedBy = toDto.CreatedBy;
                            entity.CreatedDate = toDto.CreatedDate;
                            entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                            entity.UpdatedDate = DateTime.Now;
                        }
                        employeeMap = entity;

                        var IIDs = toDto.SalaryComponents
                        .Select(a => a.EmployeeSalaryStructureComponentMapIID).ToList();
                        //delete maps
                        var entities = dbContext.EmployeeSalaryStructureComponentMaps.Where(x =>
                            x.EmployeeSalaryStructureID == entity.EmployeeSalaryStructureIID &&
                            !IIDs.Contains(x.EmployeeSalaryStructureComponentMapIID)).AsNoTracking().ToList();

                        if (entities.IsNotNull())
                            dbContext.EmployeeSalaryStructureComponentMaps.RemoveRange(entities);

                        entity.EmployeeSalaryStructureComponentMaps = new List<EmployeeSalaryStructureComponentMap>();

                        foreach (var salaryComp in toDto.SalaryComponents)
                        {
                            var listcomponentVariableMap = new List<EmployeeSalaryStructureVariableMap>();
                            foreach (var variablemap in salaryComp.EmployeeSalaryStructureVariableMap)
                            {
                                var componentVariableMap = new EmployeeSalaryStructureVariableMap()
                                {
                                    EmployeeSalaryStructureVariableMapIID = variablemap.EmployeeSalaryStructureVariableMapIID,
                                    EmployeeSalaryStructureComponentMapID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                                    VariableValue = variablemap.VariableValue,
                                    VariableKey = variablemap.VariableKey
                                };
                                listcomponentVariableMap.Add(componentVariableMap);
                            }


                            entity.EmployeeSalaryStructureComponentMaps.Add(new EmployeeSalaryStructureComponentMap()
                            {
                                EmployeeSalaryStructureComponentMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                                EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                                SalaryComponentID = salaryComp.SalaryComponentID,
                                Amount = salaryComp.Amount,
                                EmployeeSalaryStructureVariableMaps = listcomponentVariableMap
                            });
                        }

                        var leaveIIDs = toDto.LeaveSalaryComponents
                        .Select(a => a.EmployeeSalaryStructureComponentMapIID).ToList();
                        //delete maps
                        var leaveEntities = dbContext.EmployeeSalaryStructureLeaveSalaryMaps.Where(x =>
                            x.EmployeeSalaryStructureID == entity.EmployeeSalaryStructureIID &&
                            !leaveIIDs.Contains(x.EmployeeSalaryStructureLeaveSalaryMapIID)).AsNoTracking().ToList();

                        if (leaveEntities.IsNotNull())
                            dbContext.EmployeeSalaryStructureLeaveSalaryMaps.RemoveRange(leaveEntities);

                        entity.EmployeeSalaryStructureLeaveSalaryMaps = new List<EmployeeSalaryStructureLeaveSalaryMap>();

                        foreach (var salaryComp in toDto.LeaveSalaryComponents)
                        {
                            entity.EmployeeSalaryStructureLeaveSalaryMaps.Add(new EmployeeSalaryStructureLeaveSalaryMap()
                            {
                                EmployeeSalaryStructureLeaveSalaryMapIID = salaryComp.EmployeeSalaryStructureComponentMapIID,
                                EmployeeSalaryStructureID = salaryComp.EmployeeSalaryStructureID,
                                SalaryComponentID = salaryComp.SalaryComponentID,
                                Amount = salaryComp.Amount,
                            });
                        }

                        if (toDto.IsActive == true)
                        {
                            var existingSalaryStructure = dbContext.EmployeeSalaryStructures.Where(s => s.EmployeeID == int.Parse(keyval.Key) && s.IsActive == true && s.EmployeeSalaryStructureIID != toDto.EmployeeSalaryStructureIID)
                            .AsNoTracking().ToList();

                            foreach (var salary in existingSalaryStructure)
                            {
                                salary.IsActive = false;
                                dbContext.Entry(salary).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }


                        dbContext.EmployeeSalaryStructures.Add(entity);

                        if (entity.EmployeeSalaryStructureIID == 0)
                        {
                            foreach (var compnent in entity.EmployeeSalaryStructureComponentMaps)
                            {
                                foreach (var variableMap in compnent.EmployeeSalaryStructureVariableMaps)
                                {

                                    dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                                }

                                dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                            }
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            foreach (var compnent in entity.EmployeeSalaryStructureComponentMaps)
                            {
                                foreach (var variableMap in compnent.EmployeeSalaryStructureVariableMaps)
                                {
                                    if (variableMap.EmployeeSalaryStructureVariableMapIID == 0)
                                    {
                                        dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(variableMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }

                                if (compnent.EmployeeSalaryStructureComponentMapIID == 0)
                                {
                                    dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            foreach (var compnent in entity.EmployeeSalaryStructureLeaveSalaryMaps)
                            {
                                if (compnent.EmployeeSalaryStructureLeaveSalaryMapIID == 0)
                                {
                                    dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                            }

                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        dbContext.SaveChanges();
                    }
                }
                return GetEntity(employeeMap.EmployeeSalaryStructureIID);
            }
        }

        public List<EmployeesDTO> GetEmployeesByCalendarID(long calendarID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var employeeList = new List<EmployeesDTO>();

                var empDatas = dbContext.Employees.Where(x => x.AcademicCalendarID == calendarID && x.IsActive == true && x.BranchID == _context.SchoolID).OrderBy(x => x.EmployeeCode).AsNoTracking().ToList();

                foreach (var emp in empDatas)
                {
                    employeeList.Add(new EmployeesDTO()
                    {
                        EmployeeIID = emp.EmployeeIID,
                        EmployeeCode = emp.EmployeeCode,
                        EmployeeName = emp.FirstName + " " + emp.MiddleName + " " + emp.LastName,
                        EmployeePhoto = emp.EmployeePhoto,
                        DateOfJoining = emp.DateOfJoining,
                        DateOfBirth = emp.DateOfBirth
                    });
                }

                return employeeList;
            }
        }

    }
}