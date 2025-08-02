using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.School.Students;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ExamGroupMapper : DTOEntityDynamicMapper
    {
        public static ExamGroupMapper Mapper(CallContext context)
        {
            var mapper = new ExamGroupMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ExamGroupDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ExamGroups.Where(a => a.ExamGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var examdetail = new ExamGroupDTO()
                {
                    ExamGroupID = entity.ExamGroupID,
                    ExamGroupName = entity.ExamGroupName,
                    IsActive = entity.IsActive,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                };

                return ToDTOString(examdetail);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ExamGroupDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new ExamGroup()
                {
                    ExamGroupID = toDto.ExamGroupID,
                    ExamGroupName = toDto.ExamGroupName,
                    IsActive = toDto.IsActive,
                    FromDate = toDto.FromDate,
                    ToDate = toDto.ToDate,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                };

                if (toDto.FromDate >= toDto.ToDate)
                {
                    throw new Exception("Select Date Properlly!!");
                }

                dbContext.ExamGroups.Add(entity);
                if (entity.ExamGroupID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                }
                dbContext.SaveChanges();

                return GetEntity(entity.ExamGroupID);
            }
        }


        public List<KeyValueDTO> GetExamGroupsByAcademicYearID(int? academicYearID)
        {
            var listValue = new List<KeyValueDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examGroups = dbContext.ExamGroups
                                          .Where(x => x.IsActive == true &&  x.AcademicYearID == (academicYearID.HasValue ? academicYearID : _context.AcademicYearID))
                                          .AsNoTracking()
                                          .ToList();
 
                listValue = examGroups.Select(t => new KeyValueDTO
                {
                    Key = t.ExamGroupID.ToString(), 
                    Value = t.ExamGroupName.ToString() 
                }).ToList();
            }

            return listValue;
        }

    }
}