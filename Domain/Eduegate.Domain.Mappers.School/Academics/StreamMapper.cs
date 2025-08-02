using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class StreamMapper : DTOEntityDynamicMapper
    {
        public static StreamMapper Mapper(CallContext context)
        {
            var mapper = new StreamMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StreamDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StreamDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Streams.Where(x => x.StreamID == IID)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new StreamDTO()
                {
                    StreamID = entity.StreamID,
                    StreamGroupID = entity.StreamGroupID,
                    Code = entity.Code,
                    Description = entity.Description,
                    IsActive = entity.IsActive,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    //AcademicYearName = entity.AcademicYearID.HasValue ? entity.AcademicYear.Description + " (" + entity.AcademicYear.AcademicYearCode + ")" : null,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };

                return dto;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StreamDTO;
            //convert the dto to entity and pass to the repository.
            if (toDto.StreamGroupID == null || toDto.StreamGroupID == 0)
            {
                throw new Exception("Please Select Stream Group");
            }
            var entity = new Stream()
            {
                StreamID = toDto.StreamID,
                Code = toDto.Code,
                Description = toDto.Description,
                StreamGroupID = toDto.StreamGroupID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StreamID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StreamID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StreamID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.StreamID > 0 ? DateTime.Now : dto.UpdatedDate,
                IsActive=toDto.IsActive,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Streams.Add(entity);

                if (entity.StreamID == 0)
                {
                    var maxGroupID = dbContext.Streams.Max(a => (byte?)a.StreamID);
                    entity.StreamID = (byte)(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.StreamID));
        }

    }
}