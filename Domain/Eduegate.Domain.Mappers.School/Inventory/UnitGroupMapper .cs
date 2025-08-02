using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Inventory
{
    public class UnitGroupMapper : DTOEntityDynamicMapper
    {
        public static UnitGroupMapper Mapper(CallContext context)
        {
            var mapper = new UnitGroupMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<UnitGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }
        public UnitGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.UnitGroups.Where(x => x.UnitGroupID == IID).AsNoTracking().FirstOrDefault();

                var UnitGroupDTO = new UnitGroupDTO()
                {
                    UnitGroupID = entity.UnitGroupID,
                    UnitGroupCode = entity.UnitGroupCode,
                    UnitGroupName = entity.UnitGroupName,
                    Fraction = entity.Fraction,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };

                return UnitGroupDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as UnitGroupDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new UnitGroup()
                {
                    UnitGroupID = toDto.UnitGroupID,
                    UnitGroupCode = toDto.UnitGroupCode,
                    UnitGroupName = toDto.UnitGroupName,
                    Fraction = toDto.Fraction,
                    //CreatedBy = toDto.UnitID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    //UpdatedBy = toDto.UnitID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    //CreatedDate = toDto.UnitID == 0 ? DateTime.Now : dto.CreatedDate,
                    //UpdatedDate = toDto.UnitID > 0 ? DateTime.Now : dto.UpdatedDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                if (entity.UnitGroupID == 0)
                {
                    var maxGroupID = dbContext.UnitGroups.OrderByDescending(a => a.UnitGroupID).FirstOrDefault()?.UnitGroupID;
                    entity.UnitGroupID = (byte)(maxGroupID == null || maxGroupID == 0 ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.UnitGroupID));
            }
        }

    }
}