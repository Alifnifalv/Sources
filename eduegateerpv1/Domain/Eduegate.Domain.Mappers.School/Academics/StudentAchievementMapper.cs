using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.VisualBasic;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class StudentAchievementMapper : DTOEntityDynamicMapper
    {
        public static StudentAchievementMapper Mapper(CallContext context)
        {
            var mapper = new StudentAchievementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentAchievementDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentAchievementDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentAchievements.Where(a => a.StudentAchievementIID == IID)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AchievementCategory)
                    .Include(i => i.AchievementType)
                    .Include(i => i.AchievementRanking)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private StudentAchievementDTO ToDTO(StudentAchievement entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var currentDate = DateTime.Now.Date;
            var studAchievementDTO = new StudentAchievementDTO()
            
            {
                StudentAchievementIID = entity.StudentAchievementIID,
                StudentID = entity.StudentID,
                StudentName = entity.StudentID.HasValue && entity.Student != null ? $"{entity.Student.AdmissionNumber} - {entity.Student.FirstName} {entity.Student.MiddleName} {entity.Student.LastName}" : null,
                Class = entity.StudentID.HasValue ? entity.Student?.Class?.ClassDescription : null,
                Section = entity.StudentID.HasValue ? entity.Student?.Section?.SectionName : null,
                CategoryID = entity.CategoryID,
                TypeID = entity.TypeID,
                RankingID = entity.RankingID,
                SchoolID = entity.SchoolID,
                AcademicYearID = entity.AcademicYearID,
                AchievementDescription = entity.AchievementDescription,
                Title = entity.Title,
                HeldOnDate = entity.HeldOnDate,
                HeldOnDateString = entity.HeldOnDate.HasValue ? entity.HeldOnDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return studAchievementDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentAchievementDTO;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new StudentAchievement()
                {
                    StudentAchievementIID = toDto.StudentAchievementIID,
                    StudentID = toDto.StudentID,
                    CategoryID = toDto.CategoryID,
                    TypeID = toDto.TypeID,
                    RankingID = toDto.RankingID,
                    AchievementDescription = toDto.AchievementDescription,
                    Title = toDto.Title,
                    HeldOnDate = toDto.HeldOnDate,
                    SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte)_context.SchoolID,
                    AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : null,
                    CreatedBy = toDto.StudentAchievementIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentAchievementIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentAchievementIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentAchievementIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.StudentAchievements.Add(entity);

                if (entity.StudentAchievementIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.StudentAchievementIID));
            }
        }

    }
}