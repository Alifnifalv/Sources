using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class UnitsMapper : DTOEntityDynamicMapper
    {
        public static UnitsMapper Mapper(CallContext context)
        {
            var mapper = new UnitsMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectUnitDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SubjectUnitDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SubjectUnits.Where(x => x.UnitIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Subject)
                    .Include(i => i.Chapter)
                    .Include(i => i.ParentSubjectUnit)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private SubjectUnitDTO ToDTO(SubjectUnit entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var SubjectUnitDTO = new SubjectUnitDTO()
            {
                ClassID = entity.ClassID,
                SubjectID = entity.SubjectID,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                UnitTitle = entity.UnitTitle,
                Description = entity.Description,
                UnitIID = entity.UnitIID,
                ChapterID = entity.ChapterID,
                ParentSubjectUnitID = entity.ParentSubjectUnitID,
                Subject = entity.SubjectID.HasValue ? new KeyValueDTO() { Key = entity.Subject?.SubjectID.ToString(), Value = entity.Subject?.SubjectName } : new KeyValueDTO(),
                ParentSubjectUnit = entity.ParentSubjectUnitID.HasValue ? new KeyValueDTO() { Key = entity.ParentSubjectUnit?.UnitIID.ToString(), Value = entity.ParentSubjectUnit?.UnitTitle } : new KeyValueDTO(),
                Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class?.ClassDescription } : new KeyValueDTO(),
                Chapter = entity.ChapterID.HasValue ? new KeyValueDTO() { Key = entity.ChapterID.ToString(), Value = entity.Chapter.ChapterTitle } : new KeyValueDTO(),
                CreatedDate = entity.CreatedDate,
                TotalHours = (long?)entity.TotalHours,
                TotalPeriods = entity.TotalPeriods,
            };


            return SubjectUnitDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubjectUnitDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.SubjectID == null)
                {
                    throw new Exception("Please select a subject!");
                }

                if (string.IsNullOrEmpty(toDto.UnitTitle))
                {
                    throw new Exception("Please enter the chapter title!");
                }

                var entity = new SubjectUnit()
                {
                    UnitIID = toDto.UnitIID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolID = toDto.SchoolID,
                    ClassID = toDto.ClassID,
                    ParentSubjectUnitID = toDto.ParentSubjectUnitID,
                    ChapterID = toDto.ChapterID,
                    SubjectID = toDto.SubjectID,
                    UnitTitle = toDto.UnitTitle,
                    Description = toDto.Description,
                    TotalPeriods = toDto.TotalPeriods,
                    TotalHours = toDto.TotalHours,
                    CreatedDate = toDto.CreatedDate ?? DateTime.Now,
                };

                // If it's a new Chapter
                if (toDto.UnitIID == 0)
                {
                    //entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else // Updating an existing Chapter
                {
                    //entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    //entity.UpdatedDate = DateTime.Now;
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return ToDTOString(ToDTO(entity.UnitIID));

            }
        }

      

        
   


       

  


    

    }
}