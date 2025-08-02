using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeSalaryStructureComponentMapper : DTOEntityDynamicMapper
    {
        public static EmployeeSalaryStructureComponentMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeSalaryStructureComponentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeSalaryStructureComponentMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeSalaryStructureComponentMaps.Where(X => X.EmployeeSalaryStructureComponentMapIID == IID)
                  .AsNoTracking()
                  .FirstOrDefault();

                return ToDTOString(new EmployeeSalaryStructureComponentMapDTO()
                {
                    EmployeeSalaryStructureComponentMapIID = entity.EmployeeSalaryStructureComponentMapIID,
                    EmployeeSalaryStructureID = entity.EmployeeSalaryStructureID,
                    SalaryComponentID = entity.SalaryComponentID,
                    Amount = entity.Amount,
                    Formula = entity.Formula
                });
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeSalaryStructureComponentMapDTO;
            //convert the dto to entity and pass to the repository.
            if (toDto.SalaryComponentID == null || toDto.SalaryComponentID == 0)
            {
                throw new Exception("Please Select a Salary Component!");
            }
            var entity = new EmployeeSalaryStructureComponentMap()
            {
                EmployeeSalaryStructureComponentMapIID = toDto.EmployeeSalaryStructureComponentMapIID,
                EmployeeSalaryStructureID = toDto.EmployeeSalaryStructureID,
                SalaryComponentID = toDto.SalaryComponentID,
                Amount = toDto.Amount,
                Formula = toDto.Formula
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (string.IsNullOrEmpty(Convert.ToString(entity.EmployeeSalaryStructureComponentMapIID)))
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(new EmployeeSalaryStructureComponentMapDTO()
            {
                EmployeeSalaryStructureComponentMapIID = entity.EmployeeSalaryStructureComponentMapIID,
                EmployeeSalaryStructureID = entity.EmployeeSalaryStructureID,
                SalaryComponentID = entity.SalaryComponentID,
                Amount = entity.Amount,
                Formula = entity.Formula
            });
        }
    }
}