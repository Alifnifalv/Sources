using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class SectionMapper : DTOEntityDynamicMapper
    {   
        public static  SectionMapper Mapper(CallContext context)
        {
            var mapper = new  SectionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SectionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Sections.Where(X => X.SectionID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new SectionDTO()
                {
                    SectionID = entity.SectionID,
                    SectionName = entity.SectionName,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SectionDTO;

            var isValid = ValidateField(toDto, "");

            if (isValid.Key.Equals("true"))
            {
                throw new Exception(isValid.Value);
            }
            //convert the dto to entity and pass to the repository.
            var entity = new Section()
            {
                SectionID = toDto. SectionID,
                SectionName = toDto. SectionName,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.SectionID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SectionID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SectionID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SectionID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {                
                if (entity.SectionID == 0)
                {                   
                    var maxGroupID = dbContext.Sections.Max(a => (int?)a.SectionID);
                    entity.SectionID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Sections.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Sections.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new SectionDTO()
            {
                SectionID = entity. SectionID,
                SectionName = entity. SectionName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.UpdatedBy
            });
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as SectionDTO;
            var valueDTO = new KeyValueDTO();


            if (!string.IsNullOrEmpty(toDto.SectionName.ToString()))
            {
                var hasDuplicated = IsSectionDuplicated(toDto.SectionName, toDto.SectionID);
                if (hasDuplicated)
                {
                    valueDTO.Key = "true";
                    valueDTO.Value = "Section already exists, Please try with different Section.";
                }
                else
                {
                    valueDTO.Key = "false";
                }
            }

            return valueDTO;
        }

        public bool IsSectionDuplicated(string sectionName, long sectionID)
        {
            using (var db = new dbEduegateSchoolContext())
            {
                List<Section> sectionList;

                if (sectionID == 0)
                {
                    sectionList = db.Sections.Where(x => (x.SectionName).ToUpper().Replace(" ", string.Empty) == sectionName.ToUpper().Replace(" ", string.Empty) && x.SchoolID == _context.SchoolID).AsNoTracking().ToList();
                }
                else
                {
                    sectionList = db.Sections.Where(x => x.SectionID != sectionID && x.SectionName.ToUpper().Replace(" ", string.Empty) == sectionName.ToUpper().Replace(" ", string.Empty) && x.SchoolID == _context.SchoolID).AsNoTracking().ToList();
                }

                if (sectionList.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}