using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeDepartmentAccountMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static EmployeeDepartmentAccountMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeDepartmentAccountMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeDepartmentAccountMapDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeDepartmentAccountMapDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new EmployeeDepartmentAccountMap();
            var entityList = new List<EmployeeDepartmentAccountMap>();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                foreach (var dtl in toDto.EmployeeDeptAccountMapDetailDTO)
                {
                    entity = new EmployeeDepartmentAccountMap()
                    {
                        EmployeeDepartmentAccountMaplIID = dtl.EmployeeDepartmentAccountMaplIID,
                        SalaryComponentID = toDto.SalaryComponentID,
                        DepartmentID = dtl.DepartmentID,
                        ProvisionLedgerAccountID = dtl.ProvisionLedgerAccountID,
                        ExpenseLedgerAccountID = dtl.ExpenseLedgerAccountID,
                        StaffLedgerAccountID = dtl.StaffLedgerAccountID,
                        TaxLedgerAccountID = dtl.TaxLedgerAccountID,
                        CreatedBy = toDto.SalaryComponentID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.SalaryComponentID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.SalaryComponentID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = toDto.SalaryComponentID > 0 ? DateTime.Now : dto.UpdatedDate,

                    };
                    entityList.Add(entity);
                }


                foreach (var map in entityList)
                {
                    if (map.EmployeeDepartmentAccountMaplIID == 0)
                    {
                        dbContext.Entry(map).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(map).State = EntityState.Modified;
                    }
                }                

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.EmployeeDepartmentAccountMaplIID));
            }
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private EmployeeDepartmentAccountMapDTO ToDTO(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeDepartmentAccountMaps.Where(X => X.EmployeeDepartmentAccountMaplIID == IID)
                    .FirstOrDefault();

                var entityDetail = dbContext.EmployeeDepartmentAccountMaps.Where(X => X.SalaryComponentID == entity.SalaryComponentID)
                  .Include(x => x.ExpenseLedgerAccount)
                  .Include(x => x.ProvisionLedgerAccount)
                  .Include(x => x.StaffLedgerAccount)
                  .Include(x => x.TaxLedgerAccount)
                  .Include(x => x.Departments1)
                  .Include(x => x.SalaryComponent)
                  .AsNoTracking()
                  .ToList();
                var detail = new EmployeeDepartmentAccountMapDTO();
                if (entityDetail != null && entityDetail.Count() > 0)
                {
                    detail = new EmployeeDepartmentAccountMapDTO()
                    {
                        SalaryComponent = entityDetail[0].SalaryComponentID.HasValue ?
                        new KeyValueDTO() { Key = entityDetail[0].SalaryComponentID.ToString(), Value = entityDetail[0].SalaryComponent.Description } : new KeyValueDTO(),
                        SalaryComponentID = entityDetail[0]?.SalaryComponentID,

                    };
                    detail.EmployeeDeptAccountMapDetailDTO = new List<EmployeeDeptAccountMapDetailDTO>();
                    foreach (var edtl in entityDetail)
                    {
                        var employeeDeptAccountMapDetailDTO = new EmployeeDeptAccountMapDetailDTO()
                        {
                            EmployeeDepartmentAccountMaplIID = edtl.EmployeeDepartmentAccountMaplIID,
                            DepartmentID = edtl.DepartmentID,
                            ExpenseLedgerAccountID = edtl.ExpenseLedgerAccountID,
                            ProvisionLedgerAccountID = edtl.ProvisionLedgerAccountID,
                            TaxLedgerAccountID = edtl.TaxLedgerAccountID,
                            StaffLedgerAccountID = edtl.StaffLedgerAccountID,
                            Departments = edtl.DepartmentID.HasValue ?
                        new KeyValueDTO() { Key = edtl.DepartmentID.ToString(), Value = edtl.Departments1.DepartmentName } : new KeyValueDTO(),

                            ExpenseLedgerAccount = edtl.ExpenseLedgerAccountID.HasValue ?
                        new KeyValueDTO() { Key = edtl.ExpenseLedgerAccountID.ToString(), Value = edtl.ExpenseLedgerAccount.AccountName } : new KeyValueDTO(),
                            ProvisionLedgerAccount = edtl.ProvisionLedgerAccountID.HasValue ?
                        new KeyValueDTO() { Key = edtl.ProvisionLedgerAccountID.ToString(), Value = edtl.ProvisionLedgerAccount.AccountName } : new KeyValueDTO(),
                            StaffLedgerAccount = edtl.StaffLedgerAccountID.HasValue ?
                        new KeyValueDTO() { Key = edtl.StaffLedgerAccountID.ToString(), Value = edtl.StaffLedgerAccount.AccountName } : new KeyValueDTO(),
                            TaxLedgerAccount = edtl.TaxLedgerAccountID.HasValue ?
                        new KeyValueDTO() { Key = edtl.TaxLedgerAccountID.ToString(), Value = edtl.TaxLedgerAccount.AccountName } : new KeyValueDTO(),

                        };
                        detail.EmployeeDeptAccountMapDetailDTO.Add(employeeDeptAccountMapDetailDTO);
                    }
                }
                return detail;
            }
        }
    }
}
