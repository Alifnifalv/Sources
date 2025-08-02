using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Fees;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class CostCenterMapper : DTOEntityDynamicMapper
    {
        public static CostCenterMapper Mapper(CallContext context)
        {
            var mapper = new CostCenterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CostCenterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CostCenterDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.CostCenters.Where(a => a.CostCenterID == IID)
                    .Include(x => x.CostCenterAccountMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new CostCenterDTO()
                {
                    CostCenterID = entity.CostCenterID,
                    CostCenterCode = entity.CostCenterCode,
                    CostCenterName = entity.CostCenterName,
                    IsActive = entity.IsActive,
                    IsAffect_A = entity.IsAffect_A,
                    IsAffect_L = entity.IsAffect_L,
                    IsAffect_C = entity.IsAffect_C,
                    IsAffect_E = entity.IsAffect_E,
                    IsAffect_I = entity.IsAffect_I,
                    IsFixed = entity.IsFixed,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                dto.CostCenterAccountMap = new List<CostCenterAccountMapDTO>();

                var lstAccounts = entity.CostCenterAccountMaps.Select(x => x.AccountID).ToList();
                var accountData = (from x in dbContext.Accounts
                                   where lstAccounts.Contains(x.AccountID)
                                   select new CostCenterAccountMapDTO()
                                   {
                                       AccountID = x.AccountID,
                                       AccountName = x.AccountName
                                   }).AsNoTracking().ToList();

                if (entity.CostCenterAccountMaps.Count > 0)
                {
                    foreach (var accnts in entity.CostCenterAccountMaps)
                    {
                        dto.CostCenterAccountMap.Add(new CostCenterAccountMapDTO()
                        {
                            CostCenterAccountMapIID = accnts.CostCenterAccountMapIID,
                            AccountID = accnts.AccountID,
                            IsAffect_A = accnts.IsAffect_A,
                            IsAffect_L = accnts.IsAffect_L,
                            IsAffect_C = accnts.IsAffect_C,
                            IsAffect_E = accnts.IsAffect_E,
                            IsAffect_I = accnts.IsAffect_I,
                            AccountName = accountData.Where(x => x.AccountID == accnts.AccountID).Select(y => y.AccountName).FirstOrDefault()
                        });
                    }
                }

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CostCenterDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new CostCenter()
            {
                CostCenterID = toDto.CostCenterID,
                CostCenterCode = toDto.CostCenterCode,
                CostCenterName = toDto.CostCenterName,
                IsActive = toDto.IsActive,
                IsAffect_A = toDto.IsAffect_A,
                IsAffect_L = toDto.IsAffect_L,
                IsAffect_C = toDto.IsAffect_C,
                IsAffect_E = toDto.IsAffect_E,
                IsAffect_I = toDto.IsAffect_I,
                IsFixed = toDto.IsFixed,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.CostCenterID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.CostCenterID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.CostCenterID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.CostCenterID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.CostCenterID == 0)
                {
                    var maxGroupID = dbContext.CostCenters.Max(a => (int?)a.CostCenterID);                   
                    entity.CostCenterID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.CostCenters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.CostCenters.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    var removableEntities = dbContext.CostCenterAccountMaps
                                        .Where(x => x.CostCenterID == entity.CostCenterID).AsNoTracking();

                    if (removableEntities.Any())
                    {
                        dbContext.CostCenterAccountMaps
                                 .RemoveRange(removableEntities);
                        dbContext.SaveChanges();
                    }
                }

                var entities = new List<CostCenterAccountMap>();
                foreach (var accnts in toDto.CostCenterAccountMap)
                {
                    entities.Add(new CostCenterAccountMap()
                    {
                        CostCenterAccountMapIID = accnts.CostCenterAccountMapIID,
                        CostCenterID = entity.CostCenterID,
                        AccountID = accnts.AccountID,
                        IsAffect_A = accnts.IsAffect_A,
                        IsAffect_C = accnts.IsAffect_C,
                        IsAffect_E = accnts.IsAffect_E,
                        IsAffect_I = accnts.IsAffect_I,
                        IsAffect_L = accnts.IsAffect_L,
                    });
                }

                foreach (var data in entities)
                {
                    dbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }
            }

            return GetEntity(entity.CostCenterID);
        }

        public List<KeyValueDTO> GetCostCenterByAccount(long accountID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var costCenters = (from ca in dbContext.CostCenterAccountMaps
                                   join c in dbContext.CostCenters on ca.CostCenterID equals c.CostCenterID
                                   where ca.AccountID == accountID && c.IsActive == true
                                   select new KeyValueDTO
                                   {
                                       Key = c.CostCenterID.ToString(),
                                       Value = c.CostCenterName
                                   }).AsNoTracking().ToList();
                return costCenters;
            }
        }

    }
}