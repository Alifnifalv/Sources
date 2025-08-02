using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Inventory
{
    public class UnitMapper : DTOEntityDynamicMapper
    {
        public static UnitMapper Mapper(CallContext context)
        {
            var mapper = new UnitMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<UnitDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public UnitDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Units.Where(x => x.UnitID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new UnitDTO()
                {
                    UnitID = entity.UnitID,
                    UnitCode = entity.UnitCode,
                    UnitName = entity.UnitName,
                    Fraction = entity.Fraction,
                    UnitGroupID = entity.UnitGroupID.HasValue ? entity.UnitGroupID : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };

            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as UnitDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Unit()
            {
                UnitID = toDto.UnitID,
                UnitCode = toDto.UnitCode,
                UnitName = toDto.UnitName,
                Fraction = toDto.Fraction,
                UnitGroupID = toDto.UnitGroupID,
                CreatedBy = toDto.UnitID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.UnitID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.UnitID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.UnitID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.UnitID == 0)
                {
                    var maxGroupID = dbContext.Units.Max(a => (long?)a.UnitID);
                    entity.UnitID = Convert.ToByte(maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1);
                    dbContext.Units.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Units.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new UnitDTO()
            {
                UnitID = entity.UnitID,
                UnitCode = entity.UnitCode,
                UnitName = entity.UnitName,
                Fraction = entity.Fraction,
                UnitGroupID = entity.UnitGroupID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
            });
        }

        public List<KeyValueDTO> GetUnitByUnitGroup(int groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var UnitList = new List<KeyValueDTO>();
                var units = dbContext.Units.Where(x => x.UnitGroupID == groupID).AsNoTracking().ToList();

                foreach (var dat in units)
                {
                    UnitList.Add(new KeyValueDTO
                    {
                        Key = dat.UnitID.ToString(),
                        Value = dat.UnitName
                    });
                }

                return UnitList;
            }
        }

        public List<UnitDTO> GetUnitDataByUnitGroup(int groupID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var units = dbContext.Units
                    .Where(unts => unts.UnitGroupID == groupID)
                    .AsNoTracking()
                    .Select(unts => new UnitDTO
                    {
                        UnitID = unts.UnitID,
                        Fraction = unts.Fraction,
                        UnitCode = unts.UnitCode,
                        UnitGroupID = unts.UnitGroupID
                    }).ToList();

                return units;
            }
        }

    }
}