using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class MediumMapper : DTOEntityDynamicMapper
    {
        public static MediumMapper Mapper(CallContext context)
        {
            var mapper = new MediumMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<MediumDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private MediumDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Mediums.Where(X => X.MediumID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new MediumDTO()
                {
                    MediumID = entity.MediumID,
                    MediumDescription = entity.MediumDescription,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as MediumDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Medium()
            {
                MediumID = toDto.MediumID,
                MediumDescription = toDto.MediumDescription,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.MediumID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.MediumID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.MediumID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.MediumID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.MediumID == 0)
                {
                    var maxGroupID = dbContext.Mediums.Max(a => (byte?)a.MediumID);
                    entity.MediumID = Convert.ToByte((maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1).ToString());
                    dbContext.Mediums.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Mediums.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.MediumID));
        }

    }
}