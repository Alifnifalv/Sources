using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using System;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class SubjectTypeMapper : DTOEntityDynamicMapper
    {   
        public static  SubjectTypeMapper Mapper(CallContext context)
        {
            var mapper = new  SubjectTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SubjectTypes.Where(X => X.SubjectTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SubjectTypeDTO()
                {
                    SubjectTypeID = entity.SubjectTypeID,
                    TypeName = entity.TypeName,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubjectTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SubjectType()
            {
                SubjectTypeID = toDto. SubjectTypeID,
                TypeName = toDto.TypeName,
                CreatedBy = toDto.SubjectTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SubjectTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SubjectTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SubjectTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.SubjectTypeID == 0)
                {
                    var maxGroupID = dbContext.SubjectTypes.Max(a => (byte?)a.SubjectTypeID);
                    entity.SubjectTypeID = maxGroupID == null ? (byte)1 : Convert.ToByte(int.Parse(maxGroupID.ToString()) + 1);
                    dbContext.SubjectTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.SubjectTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new SubjectTypeDTO()
            {
                SubjectTypeID = entity. SubjectTypeID,
                TypeName = entity. TypeName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy
            });
        }

    }
}