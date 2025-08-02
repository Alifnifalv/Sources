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
    public class SubjectMapper : DTOEntityDynamicMapper
    {
        public static SubjectMapper Mapper(CallContext context)
        {
            var mapper = new SubjectMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(GetEntityDTO(IID));
        }

        public BaseMasterDTO GetEntityDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Subjects.Where(X => X.SubjectID == IID)
                    .Include(i => i.SubjectType)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new SubjectDTO()
                {
                    SubjectID = entity.SubjectID,
                    SubjectTypeID = entity.SubjectTypeID,
                    SubjectName = entity.SubjectName,
                    SubjectText = entity.SubjectText,
                    SubjectTypeName = entity.SubjectType.TypeName,
                    HexCodeLower = entity.HexCodeLower,
                    HexCodeUpper = entity.HexCodeUpper,
                    IsActive = entity.IsActive,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    ProgressReportHeader = entity.ProgressReportHeader,
                    HexColorCode = entity.HexCodeLower,
                    IconFileName = entity.IconFileName,
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
            var toDto = dto as SubjectDTO;
            //convert the dto to entity and pass to the repository.
            if (toDto.SubjectTypeID == null || toDto.SubjectTypeID == 0)
            {
                throw new Exception("Please Select Subject Type");
            }
            var entity = new Subject()
            {
                SubjectID = toDto.SubjectID,
                SubjectTypeID = toDto.SubjectTypeID,
                SubjectName = toDto.SubjectName,
                SubjectText = toDto.SubjectText,
                IsActive = toDto.IsActive,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                ProgressReportHeader = toDto.ProgressReportHeader,
                HexCodeLower = toDto.HexColorCode?.ToLower(),
                HexCodeUpper = toDto.HexColorCode?.ToUpper(),
                IconFileName = toDto.IconFileName,
                CreatedBy = toDto.SubjectID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SubjectID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SubjectID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SubjectID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.SubjectID == 0)
                {
                    var maxGroupID = dbContext.Subjects.Max(a => (int?)a.SubjectID);                   
                    entity.SubjectID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Subjects.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Subjects.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(GetEntityDTO(entity.SubjectID));
        }

        public List<KeyValueDTO> GetSubjectByType(byte subjectTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var subjectList = new List<KeyValueDTO>();
                var entities = dbContext.Subjects.Where(X => X.SubjectTypeID == subjectTypeID).AsNoTracking().ToList();

                foreach (var subject in entities)
                {
                    subjectList.Add(new KeyValueDTO
                    {
                        Key = subject.SubjectID.ToString(),
                        Value = subject.SubjectName
                    });
                }

                return subjectList;
            }
        }

        public List<KeyValueDTO> GetSubjectBySubjectID(byte subjectID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var result = dbContext.Subjects
                       .Where(s => s.SubjectID == subjectID)
                       .Select(s => new KeyValueDTO
                       {
                           Key = s.SubjectID.ToString(),  
                           Value = s.SubjectName // Replace with the actual column name for SubjectName
                       })
                       .ToList();

                return result;
            }
        }

        public List<KeyValueDTO> GetSubjectBySubjectTypeID(byte subjectTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var result = dbContext.Subjects
                       .Where(s => s.SubjectTypeID == subjectTypeID)
                       .Select(s => new KeyValueDTO
                       {
                           Key = s.SubjectID.ToString(),
                           Value = s.SubjectName // Replace with the actual column name for SubjectName
                       })
                       .ToList();

                return result;
            }
        }

        public List<KeyValueDTO> Getteachersubjectlist(long employeeID)
        {
            var result = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null)
                    .GetSettingValue<int?>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                if (currentAcademicYearStatusID == null)
                    throw new Exception("CURRENT_ACADEMIC_YEAR_STATUSID is null");

                result = dbContext.SubjectTeacherMaps
                    .Where(stm => stm.EmployeeID == employeeID && stm.AcademicYear.AcademicYearStatusID == currentAcademicYearStatusID)
                    .Select(stm => new KeyValueDTO
                    {
                        Key = stm.Subject.SubjectID.ToString(),
                        Value = stm.Subject.SubjectName
                    })
                    .Distinct()
                    .OrderBy(s => s.Value)
                    .ToList();
            }

            return result;
        }



    }
}