using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class MarkGradeMapper : DTOEntityDynamicMapper
    {
        public static MarkGradeMapper Mapper(CallContext context)
        {
            var mapper = new MarkGradeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<MarkGradeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.MarkGrades.Where(a => a.MarkGradeIID == IID)
                    .Include(i => i.MarkGradeMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var gradetodto = new MarkGradeDTO()
                {
                    MarkGradeIID = entity.MarkGradeIID,
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };

                gradetodto.GradeTypes = new List<MarkGradeMapDTO>();
                foreach (var markdto in entity.MarkGradeMaps)
                {
                    gradetodto.GradeTypes.Add(new MarkGradeMapDTO()
                    {
                        MarksGradeMapIID = markdto.MarksGradeMapIID,
                        MarksGradeID = entity.MarkGradeIID,
                        GradeName = markdto.GradeName,
                        GradeFrom = markdto.GradeFrom,
                        GradeTo = markdto.GradeTo,
                        IsPercentage = markdto.IsPercentage,
                        Description = markdto.Description
                    });
                }

                return ToDTOString(gradetodto);
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as MarkGradeDTO;

            var grade = toDto.GradeTypes.Where(x => x.GradeFrom == 0 || x.GradeTo == 0).ToList();

            if (grade.Count == 0)
            {
                throw new Exception("Please set grade upto zero mark!");
            }

            if (grade.Count > 1)
            {
                throw new Exception("Found zero mark set in more than one row!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new MarkGrade()
                {
                    MarkGradeIID = toDto.MarkGradeIID,
                    Description = toDto.Description,
                    CreatedBy = toDto.MarkGradeIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.MarkGradeIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.MarkGradeIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.MarkGradeIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };

                var IIDs = toDto.GradeTypes
                    .Select(a => a.MarksGradeMapIID).ToList();
                //delete maps
                var entities = dbContext.MarkGradeMaps.Where(x =>
                    (x.MarkGradeID == entity.MarkGradeIID) &&
                    !IIDs.Contains(x.MarksGradeMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.MarkGradeMaps.RemoveRange(entities);

                entity.MarkGradeMaps = new List<MarkGradeMap>();
                foreach (var markgr in toDto.GradeTypes)
                {
                    entity.MarkGradeMaps.Add(new MarkGradeMap()
                    {
                        MarksGradeMapIID = markgr.MarksGradeMapIID,
                        MarkGradeID = toDto.MarkGradeIID,
                        GradeName = markgr.GradeName,
                        GradeFrom = markgr.GradeFrom,
                        GradeTo = markgr.GradeTo,
                        IsPercentage = markgr.IsPercentage,
                        Description = markgr.Description
                    });
                }

                dbContext.MarkGrades.Add(entity);
                if (entity.MarkGradeIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var grades in entity.MarkGradeMaps)
                    {
                        if (grades.MarksGradeMapIID != 0)
                        {
                            dbContext.Entry(grades).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(grades).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return GetEntity(entity.MarkGradeIID);
            }
        }
    }
}