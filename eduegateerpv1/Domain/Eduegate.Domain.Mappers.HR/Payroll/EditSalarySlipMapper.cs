using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Newtonsoft.Json;
using Eduegate.Framework.Extensions;
using System;
using Eduegate.Framework;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EditSalarySlipMapper : DTOEntityDynamicMapper
    {
        public static EditSalarySlipMapper Mapper(CallContext context)
        {
            var mapper = new EditSalarySlipMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {               
                var entity = dbContext.SalarySlips.Where(X => X.SalarySlipIID == IID)   
                    .Include(x => x.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                var editsalaryslip = new SalarySlipDTO()
                {
                    SalarySlipIID = entity.SalarySlipIID,
                    EmployeeID = entity.EmployeeID,
                    EmployeeName = entity.EmployeeID.HasValue ? entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : string.Empty,
                    SlipDate = entity.SlipDate,
                    BranchID = entity.Employee.BranchID,
                    EmployeeCode=entity.Employee?.EmployeeCode
                };

                var editsalary = dbContext.SalarySlips.Where(sa => sa.EmployeeID == entity.EmployeeID && sa.SlipDate == entity.SlipDate)
                    .Include(i => i.SalaryComponent)
                    .AsNoTracking()
                    .ToList();

                editsalaryslip.SalaryComponent = new List<SalarySlipComponentDTO>();

                foreach (var salarycomponent in editsalary)
                {
                    editsalaryslip.SalaryComponent.Add(new SalarySlipComponentDTO()
                    {
                        SalarySlipIID = salarycomponent.SalarySlipIID,
                        SalaryComponentID = salarycomponent.SalaryComponentID,
                        Amount = salarycomponent.Amount,
                        Description = salarycomponent.Description,
                        SalaryComponent = salarycomponent.SalaryComponentID.HasValue ? new KeyValueDTO()
                        {
                            Key = salarycomponent.SalaryComponentID.ToString(),
                            Value = salarycomponent.SalaryComponent.Description
                        } : new KeyValueDTO(),
                        NoOfDays = salarycomponent.NoOfDays,
                        NoOfHours = salarycomponent.NoOfHours,
                        ReportContentID = salarycomponent.ReportContentID,
                    });
                }

                return ToDTOString(editsalaryslip);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalarySlipDTO;

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                SalarySlip slip = null;
                var pendingStatus = new SettingRepository().GetSettingDetail("SALARYSLIPSTATUS_PENDING");
                var issuedStatus = new SettingRepository().GetSettingDetail("SALARYSLIPAPPROVED_ID");
                
                if (pendingStatus.IsNull())
                {
                    throw new Exception("There is no SALARYSLIPSTATUS_PENDING  settings!");

                }

                var vrfd_issd_salarySlip =  dbContext.SalarySlips.Where(x =>
                        (x.EmployeeID == toDto.EmployeeID) && (x.SlipDate.Value.Year == toDto.SlipDate.Value.Year) && (x.SlipDate.Value.Month == toDto.SlipDate.Value.Month) 
                        && (x.SalarySlipStatusID == byte.Parse(issuedStatus.SettingValue)) && (x.IsVerified == true)).AsNoTracking().ToList();

                if (vrfd_issd_salarySlip.Any(x => x.SalarySlipStatusID == byte.Parse(issuedStatus.SettingValue)))
                {
                    throw new Exception("This salary slip already verified and issued!");

                }

                var pendingStatusID = byte.Parse(pendingStatus.SettingValue);

                if (toDto.SalaryComponent.Count > 0)
                {
                    var IIDs = toDto.SalaryComponent
                        .Select(a => a.SalarySlipIID).ToList();
                    //delete maps
                    var entities = dbContext.SalarySlips.Where(x =>
                        (x.EmployeeID == toDto.EmployeeID) && (x.SlipDate.Value.Year == toDto.SlipDate.Value.Year) && (x.SlipDate.Value.Month == toDto.SlipDate.Value.Month) &&
                        !IIDs.Contains(x.SalarySlipIID)).AsNoTracking().ToList();

                    if (entities.IsNotNull())
                        dbContext.SalarySlips.RemoveRange(entities);

                    foreach (var e in toDto.SalaryComponent)
                    {
                        var entity = new SalarySlip()
                        {
                            SalarySlipIID = e.SalarySlipIID,
                            EmployeeID = toDto.EmployeeID,
                            SlipDate = toDto.SlipDate,
                            SalaryComponentID = e.SalaryComponentID,
                            Amount = e.Amount,
                            Description = e.Description,
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            //CreatedBy = toDto.SalarySlipIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            //UpdatedBy = toDto.SalarySlipIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.SalarySlipIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = toDto.SalarySlipIID > 0 ? DateTime.Now : dto.UpdatedDate,
                            //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                        };

                        slip = entity;

                        if (entity.SalarySlipIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        dbContext.SaveChanges();
                    }
                }

                dbContext.SaveChanges();

                return GetEntity(slip.SalarySlipIID);

            }
        }


        public OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto)
        {
            var toDto = dto as SalarySlipDTO;
            var operationResultWithIDsDTO = new OperationResultWithIDsDTO();
            long slipID = 0;
            try
            {
                SalarySlip slip = null;
                byte? pendingStatusID = 0;
                using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
                {
                    var pendingStatus = new SettingRepository().GetSettingDetail("SALARYSLIPSTATUS_PENDING");
                    if (pendingStatus.IsNull())
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "There is no SALARYSLIPSTATUS_PENDING in settings!";
                        return operationResultWithIDsDTO;
                    }
                    var salarySlipApprovedID = (from st in dbContext.Settings where st.SettingCode == "SALARYSLIPAPPROVED_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (salarySlipApprovedID.IsNull())
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "There is no SALARYSLIPAPPROVED_ID in settings!";
                        return operationResultWithIDsDTO;
                    }
                    var salarySlipEntities = (from ss in dbContext.SalarySlips
                                              where ss.EmployeeID.Value == toDto.EmployeeID && (ss.SlipDate.Value.Year == toDto.SlipDate.Value.Year
                                              && ss.SlipDate.Value.Month == toDto.SlipDate.Value.Month)
                                              select ss).AsNoTracking().ToList();
                    var salarySlipApproved = byte.Parse(salarySlipApprovedID);
                    if (salarySlipEntities.Any(x => x.SalarySlipStatusID == salarySlipApproved))
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "Cannot be regenerated.Already Issued!";
                        return operationResultWithIDsDTO;
                    }
                    pendingStatusID = byte.Parse(pendingStatus.SettingValue);
                    if (toDto.SalaryComponent.Count > 0)
                    {
                        var IIDs = toDto.SalaryComponent
                            .Select(a => a.SalarySlipIID).ToList();
                        //delete maps
                        var entities = dbContext.SalarySlips.Where(x => 
                            (x.EmployeeID == toDto.EmployeeID) && (x.SlipDate.Value.Year == toDto.SlipDate.Value.Year)
                            && (x.SlipDate.Value.Month == toDto.SlipDate.Value.Month) && !IIDs.Contains(x.SalarySlipIID)).AsNoTracking().ToList();

                        if (entities.IsNotNull())
                        {
                            dbContext.SalarySlips.RemoveRange(entities);
                            dbContext.SaveChanges();
                        }
                    }
                }

                using (dbEduegateHRContext dbContextSal = new dbEduegateHRContext())
                {
                    if (toDto.SalaryComponent.Count > 0)
                    {
                        foreach (var e in toDto.SalaryComponent)
                        {
                            var entity = new SalarySlip()
                            {
                                SalarySlipIID = e.SalarySlipIID,
                                EmployeeID = toDto.EmployeeID,
                                SlipDate = toDto.SlipDate,
                                SalaryComponentID = e.SalaryComponentID,
                                Amount = e.Amount,
                                Description = e.Description,
                                SalarySlipStatusID = pendingStatusID,
                                IsVerified = false,
                                NoOfDays = e.NoOfDays,
                                NoOfHours = e.NoOfHours,
                                //CreatedBy = toDto.SalarySlipIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                //UpdatedBy = toDto.SalarySlipIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.SalarySlipIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = toDto.SalarySlipIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                            };

                            slip = entity;

                            if (entity.SalarySlipIID == 0)
                            {
                                dbContextSal.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContextSal.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            dbContextSal.SaveChanges();

                            slipID = entity.SalarySlipIID;
                        }
                    }
                }


                operationResultWithIDsDTO.SalarySlipIDList = new List<SalarySlipDTO>();

                operationResultWithIDsDTO.SalarySlipIDList.Add(new SalarySlipDTO()
                {
                    SalarySlipIID = slipID,
                    BranchID = toDto.BranchID,
                    EmployeeCode = toDto.EmployeeCode,
                    SlipDate=toDto.SlipDate
                });

                operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
                operationResultWithIDsDTO.Message = "Salaryslip modified successfully!";

            }
            //TODO: Need to handle the exception properly
            //catch (DbEntityValidationException ex)
            //{

            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;

            //}
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
            }

            return operationResultWithIDsDTO;

        }

    }
}