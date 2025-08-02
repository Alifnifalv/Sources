using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ClassRoomTypeMapper : DTOEntityDynamicMapper
    {
        public static ClassRoomTypeMapper Mapper(CallContext context)
        {
            var mapper = new ClassRoomTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ClassRoomTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ClassRoomTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ClassRoomTypes.Where(X => X.ClassRoomTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ClassRoomTypeDTO()
                {
                    ClassRoomTypeID = entity.ClassRoomTypeID,
                    TypeDescription = entity.TypeDescription,
                    IsShared = entity.IsShared,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ClassRoomTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ClassRoomType()
            {
                ClassRoomTypeID = toDto.ClassRoomTypeID,
                TypeDescription = toDto.TypeDescription,
                IsShared = toDto.IsShared,
                CreatedBy = toDto.ClassRoomTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ClassRoomTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ClassRoomTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ClassRoomTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.ClassRoomTypeID == 0)
                {
                    var maxGroupID = dbContext.ClassRoomTypes.Max(a => (byte?)a.ClassRoomTypeID);
                    entity.ClassRoomTypeID = Convert.ToByte((maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1).ToString());

                    dbContext.ClassRoomTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.ClassRoomTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ClassRoomTypeID));
        }

    }
}