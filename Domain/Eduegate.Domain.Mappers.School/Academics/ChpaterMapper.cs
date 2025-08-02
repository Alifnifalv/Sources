using Eduegate.Domain.Entity.Contents;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class ChapterMapper : DTOEntityDynamicMapper
    {
        public static ChapterMapper Mapper(CallContext context)
        {
            var mapper = new ChapterMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ChapterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ChapterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Chapters.Where(x => x.ChapterIID == IID)
                    .Include(i => i.Class)
                    .Include(i => i.Subject)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private ChapterDTO ToDTO(Chapter entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var ChapterDTO = new ChapterDTO()
            {
                ChapterIID = entity.ChapterIID,
                ChapterTitle = entity.ChapterTitle,
                Description = entity.Description,
                ClassID = entity.ClassID,
                SubjectID = entity.SubjectID,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                Subject = entity.SubjectID.HasValue ? new KeyValueDTO() { Key = entity.Subject?.SubjectID.ToString(), Value = entity.Subject?.SubjectName } : new KeyValueDTO(),
                Class = entity.ClassID.HasValue ? new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class?.ClassDescription } : new KeyValueDTO(),
                CreatedDate = entity.CreatedDate,
                TotalHours = entity.TotalHours,
                TotalPeriods = (int?)entity.TotalPeriods,
            };





            return ChapterDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ChapterDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.SubjectID == null)
                {
                    throw new Exception("Please select a subject!");
                }

                if (string.IsNullOrEmpty(toDto.ChapterTitle))
                {
                    throw new Exception("Please enter the chapter title!");
                }

                var entity = new Chapter()
                {
                    ChapterIID = toDto.ChapterIID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolID = toDto.SchoolID,
                    ClassID = toDto.ClassID,
                    SubjectID = toDto.SubjectID,
                    ChapterTitle = toDto.ChapterTitle,
                    Description = toDto.Description,
                    TotalPeriods = toDto.TotalPeriods,
                    TotalHours = toDto.TotalHours,
                    CreatedDate = toDto.CreatedDate ?? DateTime.Now,
                };

                // If it's a new Chapter
                if (toDto.ChapterIID == 0)
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
                return ToDTOString(ToDTO(entity.ChapterIID));
            }
        }
       

        public List<ChapterDTO> SaveChapterEntries(List<LessonPlanChapterDTO> chapterList)
        {
            try
            {
                var savedChaptersResult = new List<ChapterDTO>();

                using (var dbContext = new dbEduegateSchoolContext())
                {
                    foreach (var chapterDTO in chapterList)
                    {
                        // 1. Find or Create the Chapter
                        var existingChapter = dbContext.Chapters
                            .Include(c => c.SubjectUnits) // Eager load related entities if you plan to modify them
                            .ThenInclude(su => su.LessonPlans)
                            .FirstOrDefault(x => x.ChapterIID == chapterDTO.ChapterIID);

                        Chapter chapterEntity;
                        if (existingChapter == null)
                        {
                            // This is a new chapter
                            chapterEntity = new Chapter()
                            {
                                ChapterTitle = chapterDTO.ChapterTitle,
                                AcademicYearID = chapterDTO.AcademicYearID,
                                ClassID = chapterDTO.ClassID,
                                SectionID = chapterDTO.SectionID,
                                SubjectID = chapterDTO.SubjectID,
                                SchoolID = (byte?)_context.SchoolID,
                                CreatedDate = DateTime.Now,
                                SubjectUnits = new List<SubjectUnit>()
                            };
                            dbContext.Chapters.Add(chapterEntity);
                        }
                        else
                        {
                            chapterEntity = existingChapter;
                        }

                        // 2. Process Child Entities (SubjectUnits and LessonPlans)
                        var subUnitDto = chapterDTO.SubjectUnits?.FirstOrDefault();
                        if (subUnitDto != null)
                        {
                            var subjectUnitEntity = new SubjectUnit()
                            {
                                AcademicYearID = chapterDTO.AcademicYearID,
                                SchoolID = (byte?)_context.SchoolID,
                                ClassID = chapterDTO.ClassID,
                                SectionID = chapterDTO.SectionID,
                                SubjectID = chapterDTO.SubjectID,
                                UnitTitle = subUnitDto.LessonName, // Assuming the DTO's "LessonName" is the Unit's Title
                                CreatedDate = DateTime.Now,
                                LessonPlans = new List<LessonPlan>()
                            };

                            foreach (var lessonDto in chapterDTO.SubjectUnits)
                            {
                                var lessonObjectives = new List<LessonPlanLearningObjectiveMap>();
                                foreach (var objName in lessonDto.LessonLearningObjectiveName)
                                {
                                    lessonObjectives.Add(new LessonPlanLearningObjectiveMap()
                                    {
                                        ObjectiveName = objName
                                    });
                                }

                                var lessonOutcomes = new List<LessonPlanLearningOutcomeMap>();
                                foreach (var outcomeName in lessonDto.LessonLearningOutcomeName)
                                {
                                    lessonOutcomes.Add(new LessonPlanLearningOutcomeMap()
                                    {
                                        OutcomeName = outcomeName
                                    });
                                }
                                
                                var skilldevelopment = new List<LessonPlanSkillDevelopmentMap>();
                                foreach (var skill in lessonDto.SkillDevolepment)
                                {
                                    skilldevelopment.Add(new LessonPlanSkillDevelopmentMap()
                                    {
                                        Description = skill
                                    });
                                }
                                
                                var assessments = new List<LessonPlanAssessmentMap>();
                                foreach (var assmnt in lessonDto.Assessment)
                                {
                                    assessments.Add(new LessonPlanAssessmentMap()
                                    {
                                        AssessmentName = assmnt.AssessmentName,
                                        Description = assmnt.AssessmentDescription,
                                    });
                                }

                                var teachingMethodology = new List<LessonPlanTeachingMethodologyMap>();
                                foreach (var tch in lessonDto.TeachingMethodology)
                                {
                                    teachingMethodology.Add(new LessonPlanTeachingMethodologyMap()
                                    {
                                        ActivityName = tch.ActivityName,
                                        ActivityDescription = tch.ActivityDescription,
                                        Duration = byte.Parse(tch.EstimatedDuration.Split(' ')[0]),
                                    });
                                }

                                var lessonPlanEntity = new LessonPlan()
                                {
                                    AcademicYearID = chapterDTO.AcademicYearID,
                                    ClassID = chapterDTO.ClassID,
                                    SectionID = chapterDTO.SectionID,
                                    SubjectID = chapterDTO.SubjectID,
                                    SchoolID = (byte?)_context.SchoolID,
                                    Title = lessonDto.LessonName,
                                    AllignmentToVisionAndMission = lessonDto.AllignmentToVisionAndMission,
                                    CrossDisciplinaryConnection = lessonDto.CrossDisciplinaryConnection,
                                    HomeWorks = lessonDto.HomeWorks,
                                    Content = lessonDto.Content,
                                    LocalAndGlobalConnection = lessonDto.LocalAndGlobalConnection,
                                    Resourses = lessonDto.Resources,
                                    LessonPlanStatusID = 1,
                                    CreatedDate = DateTime.Now,
                                    ReferenceBook = lessonDto.ReferenceBook,
                                    SEN = lessonDto.DifferentiatedLearning?.SEN,
                                    HighAchievers = lessonDto.DifferentiatedLearning?.HighAchievers,
                                    StudentsWhoNeedImprovement = lessonDto.DifferentiatedLearning?.StudentsWhoNeedImprovement,
                                    LessonPlanLearningObjectiveMaps = lessonObjectives,
                                    LessonPlanLearningOutcomeMaps = lessonOutcomes,
                                    LessonPlanSkillDevelopmentMaps = skilldevelopment,
                                    LessonPlanAssessmentMaps = assessments,
                                    LessonPlanTeachingMethodologyMaps = teachingMethodology,
                                };

                                subjectUnitEntity.LessonPlans.Add(lessonPlanEntity);
                            }

                            chapterEntity.SubjectUnits.Add(subjectUnitEntity);
                        }

                        dbContext.SaveChanges();

                        savedChaptersResult.Add(new ChapterDTO
                        {
                            ChapterIID = chapterEntity.ChapterIID,
                            ChapterTitle = chapterEntity.ChapterTitle,
                            Description = chapterEntity.Description
                        });
                    }

                    return savedChaptersResult;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Exception in SaveChapterEntries. Error: {ex.Message}";
                Eduegate.Logger.LogHelper<string>.Fatal(errorMessage, ex);
                throw;
            }
        }


        public void SaveExtractedJson(string extractedJson, long IID)
        {
            try
            {
                using (var dbContext = new dbContentContext())
                {
                    var extractedDataEntity = dbContext.ContentFiles
                        .Include(x => x.ContentType)
                        .AsNoTracking()
                        .FirstOrDefault(x => x.ContentFileIID == IID);

                    if (extractedDataEntity != null)
                    {
                        extractedDataEntity.ExtractedData = System.Text.Encoding.UTF8.GetBytes(extractedJson);
                        extractedDataEntity.UpdatedDate = DateTime.UtcNow;
                        extractedDataEntity.ContentTypeID = 101;

                        dbContext.ContentFiles.Update(extractedDataEntity);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Exception in SaveExtractedJson. Error: {ex.Message}";
                Eduegate.Logger.LogHelper<string>.Fatal(errorMessage, ex);
                throw;
            }
        }




    }
}