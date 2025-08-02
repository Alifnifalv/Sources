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

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalaryComponentMapper : DTOEntityDynamicMapper
    {
        public static SalaryComponentMapper Mapper(CallContext context)
        {
            var mapper = new SalaryComponentMapper();
            mapper._context = context;
            return mapper;
        }

        public List<SalaryComponentDTO> ToDTO(List<SalaryComponent> entities)
        {
            var dtos = new List<SalaryComponentDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }
            return dtos;
        }

        public SalaryComponentDTO ToDTO(SalaryComponent entity)
        {
            return new SalaryComponentDTO()
            {
                SalaryComponentID = entity.SalaryComponentID,
                SalaryComponentGroupID = entity.SalaryComponentGroupID,
                ComponentTypeID = entity.ComponentTypeID,
                Description = entity.Description,
                Abbreviation = entity.Abbreviation,
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalaryComponentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as SalaryComponentDTO);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SalaryComponentDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.SalaryComponents.Where(a => a.SalaryComponentID == IID)
                    .Include(i => i.SalaryComponentRelationMaps1)
                    .Include(i => i.ExpenseLedgerAccount)
                    .Include(i => i.ProvisionLedgerAccount)
                     .Include(i => i.StaffLedgerAccount)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new SalaryComponentDTO()
                {
                    SalaryComponentID = entity.SalaryComponentID,
                    SalaryComponentGroupID = entity.SalaryComponentGroupID,
                    ComponentTypeID = entity.ComponentTypeID,
                    ReportHeadGroupID = entity.ReportHeadGroupID,
                    Description = entity.Description,
                    Abbreviation = entity.Abbreviation,
                    NoOfDaysApplicable = entity.NoOfDaysApplicable,
                    StaffLedgerAccountID = entity.StaffLedgerAccountID,
                    ExpenseLedgerAccountID = entity.ExpenseLedgerAccountID,
                    ProvisionLedgerAccountID = entity.ProvisionLedgerAccountID,
                    StaffLedgerAccount = entity.StaffLedgerAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.StaffLedgerAccountID.ToString(),
                        Value = entity.StaffLedgerAccount.AccountName
                    } : new KeyValueDTO(),
                    ExpenseLedgerAccount = entity.ExpenseLedgerAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ExpenseLedgerAccountID.ToString(),
                        Value = entity.ExpenseLedgerAccount.AccountName
                    } : new KeyValueDTO(),
                    ProvisionLedgerAccount = entity.ProvisionLedgerAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ProvisionLedgerAccountID.ToString(),
                        Value = entity.ProvisionLedgerAccount.AccountName
                    } : new KeyValueDTO(),
                };

                dto.SalaryComponentRelationMap = new List<SalaryComponentRelationMapDTO>();
                foreach (var relationMap in entity.SalaryComponentRelationMaps1)
                {
                    dto.SalaryComponentRelationMap.Add(new SalaryComponentRelationMapDTO()
                    {
                        SalaryComponentRelationMapIID = relationMap.SalaryComponentRelationMapIID,
                        RelatedComponentID = relationMap.RelatedComponentID,
                        RelationTypeID = relationMap.RelationTypeID,
                        SalaryComponentID = relationMap.SalaryComponentID,
                        NoOfDaysApplicable = relationMap.NoOfDaysApplicable
                    });
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SalaryComponentDTO;
            //convert the dto to entity and pass to the repository.

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = new SalaryComponent()
                {
                    SalaryComponentID = toDto.SalaryComponentID,
                    ComponentTypeID = toDto.ComponentTypeID,
                    SalaryComponentGroupID = toDto.SalaryComponentGroupID,
                    ProvisionLedgerAccountID = toDto.ProvisionLedgerAccountID,
                    ExpenseLedgerAccountID = toDto.ExpenseLedgerAccountID,
                    StaffLedgerAccountID = toDto.StaffLedgerAccountID,
                    ReportHeadGroupID = toDto.ReportHeadGroupID,
                    Description = toDto.Description,
                    Abbreviation = toDto.Abbreviation,
                    NoOfDaysApplicable = toDto.NoOfDaysApplicable,
                    CreatedBy = toDto.SalaryComponentID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SalaryComponentID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SalaryComponentID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SalaryComponentID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                entity.SalaryComponentRelationMaps1 = new List<SalaryComponentRelationMap>();
                foreach (var dat in toDto.SalaryComponentRelationMap)
                {
                    entity.SalaryComponentRelationMaps1.Add(new SalaryComponentRelationMap()
                    {
                        SalaryComponentRelationMapIID = dat.SalaryComponentRelationMapIID,
                        RelatedComponentID = dat.RelatedComponentID,
                        RelationTypeID = dat.RelationTypeID,
                        SalaryComponentID = toDto.SalaryComponentID,
                        NoOfDaysApplicable = dat.NoOfDaysApplicable,
                    });
                }

                var IIDs = toDto.SalaryComponentRelationMap
                        .Select(a => a.SalaryComponentRelationMapIID).ToList();

                //delete maps
                var entities = dbContext.SalaryComponentRelationMaps.Where(x =>
                    x.SalaryComponentID == entity.SalaryComponentID &&
                    !IIDs.Contains(x.SalaryComponentRelationMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SalaryComponentRelationMaps.RemoveRange(entities);


                if (entity.SalaryComponentID == 0)
                {
                    var maxGroupID = dbContext.SalaryComponents.Max(a => (int?)a.SalaryComponentID);
                    entity.SalaryComponentID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.SalaryComponents.Add(entity);

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.SalaryComponentRelationMaps1)
                    {
                        if (map.SalaryComponentRelationMapIID == 0)
                        {
                            dbContext.Entry(map).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(map).State = EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.SalaryComponentID));
            }
        }

    }
}