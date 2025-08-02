using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository.Frameworks;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using Eduegate.Services.Contracts.HR.Leaves;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Entity.HR.Leaves;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeePromotionMapper : DTOEntityDynamicMapper

    {
        public static EmployeePromotionMapper Mapper(CallContext context)
        {
            var mapper = new EmployeePromotionMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeePromotionDTO>(entity);
        }
  
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public List<EmployeePromotionDTO> GetEmployeeDetailsByEmployeeID(long employeeID)
        {
            var Empdet = new EmployeePromotionDTO();
            var EmpdetList = new List<EmployeePromotionDTO>();
            var keyValueDto = new KeyValueDTO()
            {
                Key = null,
                Value = null
            };
            using (var dbContext = new dbEduegateHRContext())
            {
                var employeeDet = dbContext.Employees.Where(e => e.EmployeeIID == employeeID)
                    .Include(i => i.Branch)
                    .Include(i => i.Designation)
                    .Include(i => i.LeaveGroup)
                    .AsNoTracking()
                    .FirstOrDefault();

                Empdet = new EmployeePromotionDTO()
                {
                    NewBranch = new KeyValueDTO()
                    {
                        Key = employeeDet.BranchID.ToString(),
                        Value = employeeDet.Branch?.BranchName
                    },
                    NewDesignation = new KeyValueDTO()
                    {
                        Key = employeeDet.DesignationID.ToString(),
                        Value = employeeDet.Designation.DesignationName
                    },
                    NewLeaveGroupID = employeeDet.LeaveGroupID,
                    NewLeaveGroup = employeeDet.LeaveGroupID.HasValue ? new KeyValueDTO()
                    {
                        Key = employeeDet.LeaveGroupID.ToString(),
                        Value = employeeDet.LeaveGroup?.LeaveGroupName
                    } : new KeyValueDTO(),
                };

                //if (Empdet.IsNull() && Empdet.NewLeaveGroupID.IsNull())
                //    keyValueDto = dbContext.LeaveGroups.Where(x => x.LeaveGroupID == Empdet.NewLeaveGroupID)
                //        .Select(x => new KeyValueDTO() { Key = x.LeaveGroupID.ToString(), Value = x.LeaveGroupName })
                //        .FirstOrDefault();
                //Empdet.NewLeaveGroup = keyValueDto;

                Empdet.EmployeePromotionLeaveAllocs = new List<Services.Contracts.HR.Leaves.EmployeePromotionLeaveAllocDTO>();
                var employeeLeaveAllocations = dbContext.EmployeeLeaveAllocations.Where(x => x.EmployeeID == employeeID)
                    .Include(i => i.LeaveType)
                    .AsNoTracking()
                    .ToList();

                if (employeeLeaveAllocations.Count != 0)
                {
                    foreach (var leaveInfo in employeeLeaveAllocations)
                    {
                        Empdet.EmployeePromotionLeaveAllocs.Add(new Services.Contracts.HR.Leaves.EmployeePromotionLeaveAllocDTO()
                        {
                            AllocatedLeaves = leaveInfo.AllocatedLeaves,
                            LeaveTypeID = leaveInfo.LeaveTypeID,
                            LeaveType = leaveInfo.LeaveTypeID.HasValue ? new KeyValueDTO()
                            {
                                Key = leaveInfo.LeaveTypeID.ToString(),
                                Value = leaveInfo.LeaveType?.Description
                            } : new KeyValueDTO(),
                        });
                    }
                }

                var employeecomponentDTO = new EmployeeSalaryStructureMapper().GetEmployeesSalaryStructure(employeeID);
                Empdet.SalaryStructure = employeecomponentDTO;
                EmpdetList.Add(Empdet);

            }
            return EmpdetList;
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeePromotions.Where(X => X.EmployeePromotionIID == IID)
                   .Include(i => i.Employee)
                   .Include(i => i.SalaryStructure)
                   .Include(i => i.NewDesignation)
                   .Include(i => i.OldDesignation)
                   .Include(i => i.OldDesignation)
                   .Include(i => i.NewBranch)
                   .Include(i => i.OldBranch)
                   .Include(i => i.NewLeaveGroup)
                   .Include(i => i.OldLeaveGroup)
                   .Include(i => i.PaymentMode)
                   .Include(i => i.TimeSheetSalaryComponent)
                   .Include(i => i.Account)
                   .Include(i => i.PayrollFrequency)
                   .Include(i => i.EmployeePromotionSalaryComponentMaps).ThenInclude(i => i.SalaryComponent)
                   .Include(i => i.EmployeePromotionLeaveAllocations).ThenInclude(i => i.LeaveType)
                   .AsNoTracking()
                   .FirstOrDefault();

                var structure = new EmployeePromotionDTO()
                {
                    EmployeePromotionIID = entity.EmployeePromotionIID,
                    EmployeeID = entity.EmployeeID,
                    Employee = new KeyValueDTO()
                    {
                        Value = entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName,
                        Key = entity.EmployeeID.ToString()
                    },
                    SalaryStructureID = entity.SalaryStructureID,
                    EmployeeSalaryStructure = new KeyValueDTO()
                    {
                        Key = entity.SalaryStructureID.ToString(),
                        Value = entity.SalaryStructure.StructureName
                    },
                    OldDesignation = new KeyValueDTO()
                    {
                        Value = entity.OldDesignation.DesignationName,
                        Key = entity.OldDesignationID.ToString()
                    },
                    OldBranch = new KeyValueDTO()
                    {
                        Value = entity.OldBranch.BranchName,
                        Key = entity.OldBranchID.ToString()
                    },
                    OldLeaveGroup = entity.OldLeaveGroupID.HasValue ? new KeyValueDTO()
                    {
                        Value = entity.OldLeaveGroup.LeaveGroupName,
                        Key = entity.OldLeaveGroupID.ToString()
                    } : new KeyValueDTO(),
                    NewDesignation = new KeyValueDTO()
                    {
                        Value = entity.NewDesignation.DesignationName,
                        Key = entity.NewDesignationID.ToString()
                    },
                    NewBranch = new KeyValueDTO()
                    {
                        Value = entity.NewBranch.BranchName,
                        Key = entity.NewBranchID.ToString()
                    },
                    NewLeaveGroup = entity.NewLeaveGroupID.HasValue ?  new KeyValueDTO()
                    {
                        Value = entity.NewLeaveGroup.LeaveGroupName,
                        Key = entity.NewLeaveGroupID.ToString()
                    }: new KeyValueDTO(),

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
                    IsApplyImmediately = entity.IsApplyImmediately,
                    PaymentMode = new KeyValueDTO()
                    {
                        Key = entity.PaymentModeID.ToString(),
                        Value = entity.PaymentMode.PaymentName
                    },
                    PayrollFrequency = new KeyValueDTO()
                    {

                        Key = entity.PayrollFrequencyID.ToString(),
                        Value = entity.PayrollFrequency.FrequencyName
                    },
                    TimeSheetSalaryComponent = entity.TimeSheetSalaryComponentID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.TimeSheetSalaryComponentID.HasValue ? Convert.ToString(entity.TimeSheetSalaryComponentID) : null,
                        Value = entity.TimeSheetSalaryComponent.Description
                    } : new KeyValueDTO(),

                    Account = new KeyValueDTO()
                    {
                        Key = entity.AccountID.ToString(),
                        Value = entity.Account.AccountName
                    }
                };

                structure.SalaryComponents = new List<EmployeePromotionComponentMapDTO>();
                foreach (var salaryComponent in entity.EmployeePromotionSalaryComponentMaps)
                {
                    structure.SalaryComponents.Add(new EmployeePromotionComponentMapDTO()
                    {
                        EmployeePromotionSalaryComponentMapIID = salaryComponent.EmployeePromotionSalaryComponentMapIID,
                        EmployeePromotionID = salaryComponent.EmployeePromotionID,
                        Amount = salaryComponent.Amount,

                        SalaryComponentID = salaryComponent.SalaryComponentID,
                        Formula = salaryComponent.Formula,
                        SalaryComponent = new KeyValueDTO()
                        {
                            Key = salaryComponent.SalaryComponentID.ToString(),
                            Value = salaryComponent.SalaryComponent.Description
                        }
                    });
                }

                structure.EmployeePromotionLeaveAllocs = new List<EmployeePromotionLeaveAllocDTO>();
                foreach (var leaveTypes in entity.EmployeePromotionLeaveAllocations)
                {
                    structure.EmployeePromotionLeaveAllocs.Add(new EmployeePromotionLeaveAllocDTO()
                    {
                        EmployeePromotionLeaveAllocationIID = leaveTypes.EmployeePromotionLeaveAllocationIID,
                        EmployeePromotionID = entity.EmployeePromotionIID,
                        AllocatedLeaves = leaveTypes.AllocatedLeaves,
                        LeaveTypeID = leaveTypes.LeaveTypeID,
                        LeaveType = leaveTypes.LeaveTypeID.HasValue ? new KeyValueDTO()
                        {
                            Key = leaveTypes.LeaveTypeID.ToString(),
                            Value = leaveTypes.LeaveType.Description
                        } : new KeyValueDTO()
                    }) ;
                }

                return ToDTOString(structure);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeePromotionDTO;
            //convert the dto to entity and pass to the repository.


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
                var entity = new EmployeePromotion()
                {
                    EmployeePromotionIID = toDto.EmployeePromotionIID,
                    SalaryStructureID = toDto.SalaryStructureID,
                    EmployeeID = toDto.EmployeeID,
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
                    // OldRoleID = toDto.OldRoleID,
                    OldDesignationID = toDto.OldDesignationID,
                    OldBranchID = toDto.OldBranchID,
                    // NewRoleID = toDto.NewRoleID,
                    NewDesignationID = toDto.NewDesignationID,
                    NewBranchID = toDto.NewBranchID,
                    OldLeaveGroupID = toDto.OldLeaveGroupID,
                    NewLeaveGroupID = toDto.NewLeaveGroupID,
                    IsApplyImmediately = toDto.IsApplyImmediately,

                };
                if (toDto.EmployeePromotionIID == 0)
                {
                    entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = toDto.CreatedDate;
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;
                }

                var IIDs = toDto.SalaryComponents
                .Select(a => a.EmployeePromotionSalaryComponentMapIID).ToList();

                //delete maps
                var entities = dbContext.EmployeePromotionSalaryComponentMaps.Where(x =>
                    x.EmployeePromotionID == entity.EmployeePromotionIID &&
                    !IIDs.Contains(x.EmployeePromotionSalaryComponentMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.EmployeePromotionSalaryComponentMaps.RemoveRange(entities);

                entity.EmployeePromotionSalaryComponentMaps = new List<EmployeePromotionSalaryComponentMap>();

                foreach (var salaryComp in toDto.SalaryComponents)
                {
                    entity.EmployeePromotionSalaryComponentMaps.Add(new EmployeePromotionSalaryComponentMap()
                    {
                        // EmployeeSalaryStructureComponentMapID = salaryComp.EmployeeSalaryStructureComponentMapID,
                        EmployeePromotionSalaryComponentMapIID = salaryComp.EmployeePromotionSalaryComponentMapIID,
                        EmployeePromotionID = toDto.EmployeePromotionIID,
                        SalaryComponentID = salaryComp.SalaryComponentID,
                        Amount = salaryComp.Amount,
                    });
                }

                var leaveIIDs = toDto.EmployeePromotionLeaveAllocs
               .Select(a => a.EmployeePromotionLeaveAllocationIID).ToList();
                //delete maps
                var leveentities = dbContext.EmployeePromotionLeaveAllocations.Where(x =>
                    x.EmployeePromotionID == entity.EmployeePromotionIID &&
                    !leaveIIDs.Contains(x.EmployeePromotionLeaveAllocationIID)).AsNoTracking().ToList();

                if (leveentities.IsNotNull())
                    dbContext.EmployeePromotionLeaveAllocations.RemoveRange(leveentities);

                entity.EmployeePromotionLeaveAllocations = new List<EmployeePromotionLeaveAllocation>();

                foreach (var leaveTypes in toDto.EmployeePromotionLeaveAllocs)
                {
                    entity.EmployeePromotionLeaveAllocations.Add(new EmployeePromotionLeaveAllocation()
                    {
                        EmployeePromotionLeaveAllocationIID = leaveTypes.EmployeePromotionLeaveAllocationIID,
                        EmployeePromotionID = toDto.EmployeePromotionIID,
                        AllocatedLeaves = leaveTypes.AllocatedLeaves,
                        LeaveTypeID = leaveTypes.LeaveTypeID,
                    });
                }

                dbContext.EmployeePromotions.Add(entity);
                if (entity.EmployeePromotionIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var leavealloc in entity.EmployeePromotionLeaveAllocations)
                    {
                        if (leavealloc.EmployeePromotionLeaveAllocationIID != 0)
                        {
                            dbContext.Entry(leavealloc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(leavealloc).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }
                    foreach (var compnent in entity.EmployeePromotionSalaryComponentMaps)
                    {
                        if (compnent.EmployeePromotionSalaryComponentMapIID != 0)
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(compnent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                toDto.EmployeePromotionIID = entity.EmployeePromotionIID;

                if (toDto.IsApplyImmediately == true)
                {
                    #region Filteration

                    //string _employee_IDs = string.Empty;
                    //if (toDto.EmployeeID.HasValue)
                    //    _employee_IDs = string.Join(",", toDto.EmployeeID);

                    //string _promotion_IDs = string.Empty;
                    //if (toDto.EmployeePromotionIID != 0)
                    //    _promotion_IDs = string.Join(",", toDto.EmployeePromotionIID);

                    #endregion

                    #region OLD_METHOD PROCEDURE 
                    //string message = string.Empty;
                    //SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                    //_sBuilder.ConnectTimeout = 30; // Set Timedout
                    //using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                    //{
                    //    try { conn.Open(); }
                    //    catch (Exception ex)
                    //    {
                    //        throw ex;
                    //    }

                    //    using (SqlCommand sqlCommand = new SqlCommand("[payroll].[SP_AUTO_EMPLOYEE_PROMOTION]", conn))
                    //    {
                    //        sqlCommand.CommandType = CommandType.StoredProcedure;

                    //        sqlCommand.Parameters.Add(new SqlParameter("@EFFECTIVEDATE", SqlDbType.DateTime));
                    //        sqlCommand.Parameters["@EFFECTIVEDATE"].Value = System.DateTime.Now;

                    //        sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
                    //        sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

                    //        sqlCommand.Parameters.Add(new SqlParameter("@EMPLOYEEIDs", SqlDbType.VarChar));
                    //        sqlCommand.Parameters["@EMPLOYEEIDs"].Value = _employee_IDs;

                    //        sqlCommand.Parameters.Add(new SqlParameter("@PROMOTIONIDs", SqlDbType.VarChar));
                    //        sqlCommand.Parameters["@PROMOTIONIDs"].Value = _promotion_IDs;

                    //        try
                    //        {
                    //            // Run the stored procedure.
                    //            Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);  
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            message = message.Length > 0 ? message : "0#Error on Saving";
                    //            throw new Exception(message);  
                    //        }
                    //        finally
                    //        {
                    //            conn.Close();
                    //        }
                    //    }
                    //}
                    #endregion

                    try
                    {
                        // Step 1: Retrieve promotions for the specified date
                        var promotions = dbContext.EmployeePromotions
                            .Where(p => p.FromDate.Value.Date == DateTime.Now.Date && p.EmployeeID == toDto.EmployeeID).ToList();


                        // Step 2: Update employee details
                        foreach (var promotion in promotions)
                        {
                            var employee = dbContext.Employees.FirstOrDefault(e => e.EmployeeIID == promotion.EmployeeID);
                            if (employee != null)
                            {
                                employee.DesignationID = promotion.NewDesignationID;
                                employee.BranchID = promotion.NewBranchID;
                                employee.LeaveGroupID = promotion.NewLeaveGroupID;
                            }

                            //dbContext.SaveChanges();

                            // Step 3: Update EmployeeSalaryStructures
                            var existingStructures = dbContext.EmployeeSalaryStructures
                                .Where(es => es.EmployeeID == promotion.EmployeeID && es.FromDate != promotion.FromDate)
                                .ToList();

                            //foreach (var structure in existingStructures)
                            //{
                            //    structure.IsActive = false;
                            //}

                            existingStructures.ForEach(structure => structure.IsActive = false);

                            var matchedStructure = dbContext.EmployeeSalaryStructures
                                .FirstOrDefault(es => es.EmployeeID == promotion.EmployeeID && es.FromDate == promotion.FromDate);

                            // Fetch relevant Salary Structure IDs from promotions                 
                            var salaryStructureIds = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeID == promotion.EmployeeID)
                                .Select(a => a.EmployeeSalaryStructureIID).ToList();

                            // Delete existing components mapped to these salary structures
                            if (salaryStructureIds.Any())
                            {
                                var existingComponents = dbContext.EmployeeSalaryStructureComponentMaps
                                    .Where(map => salaryStructureIds.Contains(map.EmployeeSalaryStructureID ?? 0))
                                    .ToList();

                                dbContext.EmployeeSalaryStructureComponentMaps.RemoveRange(existingComponents);
                            }

                            var newComponents = new List<EmployeeSalaryStructureComponentMap>();

                            // Insert updated components based on EmployeePromotionSalaryComponentMaps
                            var newPromComponents = dbContext.EmployeePromotionSalaryComponentMaps
                                .Where(epc => epc.EmployeePromotionID == promotion.EmployeePromotionIID).ToList();

                            foreach (var component in newPromComponents)
                            {
                                newComponents.Add(new EmployeeSalaryStructureComponentMap
                                {
                                    SalaryComponentID = component.SalaryComponentID,
                                    Amount = component.Amount,
                                    Formula = component.Formula
                                });
                            }

                            if (matchedStructure == null)
                            {
                                dbContext.EmployeeSalaryStructures.Add(new EmployeeSalaryStructure
                                {
                                    EmployeeID = promotion.EmployeeID,
                                    SalaryStructureID = promotion.SalaryStructureID,
                                    FromDate = promotion.FromDate,
                                    Amount = promotion.Amount,
                                    PayrollFrequencyID = promotion.PayrollFrequencyID,
                                    IsSalaryBasedOnTimeSheet = promotion.IsSalaryBasedOnTimeSheet,
                                    TimeSheetSalaryComponentID = promotion.TimeSheetSalaryComponentID,
                                    TimeSheetHourRate = promotion.TimeSheetHourRate,
                                    TimeSheetLeaveEncashmentPerDay = promotion.TimeSheetLeaveEncashmentPerDay,
                                    TimeSheetMaximumBenefits = promotion.TimeSheetMaximumBenefits,
                                    PaymentModeID = promotion.PaymentModeID,
                                    AccountID = promotion.AccountID,
                                    CreatedBy = promotion.CreatedBy,
                                    CreatedDate = promotion.CreatedDate,
                                    IsActive = true,
                                    EmployeeSalaryStructureComponentMaps = newComponents,
                                });
                            }
                            else
                            {
                                matchedStructure.SalaryStructureID = promotion.SalaryStructureID;
                                matchedStructure.Amount = promotion.Amount;
                                matchedStructure.PayrollFrequencyID = promotion.PayrollFrequencyID;
                                matchedStructure.IsSalaryBasedOnTimeSheet = promotion.IsSalaryBasedOnTimeSheet;
                                matchedStructure.TimeSheetSalaryComponentID = promotion.TimeSheetSalaryComponentID;
                                matchedStructure.TimeSheetHourRate = promotion.TimeSheetHourRate;
                                matchedStructure.TimeSheetLeaveEncashmentPerDay = promotion.TimeSheetLeaveEncashmentPerDay;
                                matchedStructure.TimeSheetMaximumBenefits = promotion.TimeSheetMaximumBenefits;
                                matchedStructure.PaymentModeID = promotion.PaymentModeID;
                                matchedStructure.AccountID = promotion.AccountID;
                                matchedStructure.UpdatedBy = promotion.UpdatedBy;
                                matchedStructure.UpdatedDate = promotion.UpdatedDate;
                                matchedStructure.IsActive = true;
                                matchedStructure.EmployeeSalaryStructureComponentMaps = newComponents;
                            }


                            // Step 4: Update EmployeeLeaveAllocations
                            var leaveAllocations = dbContext.EmployeeLeaveAllocations
                                .Where(a => a.EmployeeID == promotion.EmployeeID)
                                .ToList();

                            if (leaveAllocations.Any())
                                dbContext.EmployeeLeaveAllocations.RemoveRange(leaveAllocations);

                            var newLeaveAllocations = dbContext.EmployeePromotionLeaveAllocations
                                .Where(epc => epc.EmployeePromotionID == promotion.EmployeePromotionIID)
                                .Select(epc => new EmployeeLeaveAllocation
                                {
                                    EmployeeID = promotion.EmployeeID,
                                    LeaveTypeID = epc.LeaveTypeID,
                                    Description = epc.Description,
                                    AllocatedLeaves = epc.AllocatedLeaves,
                                    CreatedBy = epc.CreatedBy,
                                    CreatedDate = epc.CreatedDate,
                                    UpdatedBy = epc.UpdatedBy,
                                    UpdatedDate = epc.UpdatedDate
                                }).ToList();

                            dbContext.EmployeeLeaveAllocations.AddRange(newLeaveAllocations);

                        }

                        dbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                         ? ex.InnerException?.Message : ex.Message;
                        Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                    }
                }

                return GetEntity(toDto.EmployeePromotionIID);
            }
        }

    }
}