using Eduegate.Domain.Entity.OnlineExam.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.OnlineExam;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class OnlineExamsMapper : DTOEntityDynamicMapper
    {
        public static OnlineExamsMapper Mapper(CallContext context)
        {
            var mapper = new OnlineExamsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<OnlineExamsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private OnlineExamsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.OnlineExams.Where(x => x.OnlineExamIID == IID)
                    .Include(i => i.QuestionSelection)
                    .Include(i => i.Class)
                    .Include(i => i.OnlineExamSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ExamQuestionGroupMaps).ThenInclude(i => i.QuestionGroup).ThenInclude(i => i.Questions)
                    .AsNoTracking()
                    .FirstOrDefault();

                var OnlineExamDetails = new OnlineExamsDTO()
                {
                    OnlineExamIID = entity.OnlineExamIID,
                    Description = entity.Description,
                    MaximumDuration = entity.MaximumDuration,
                    MinimumDuration = entity.MinimumDuration,
                    Name = entity.Name,
                    PassNos = entity.PassNos,
                    PassPercentage = entity.PassPercentage,
                    QuestionSelectionID = entity.QuestionSelectionID,
                    QuestionSelectionName = entity.QuestionSelection.SelectName,
                    Classes = entity.ClassID.HasValue ? new KeyValueDTO { Key = entity.ClassID.ToString(), Value = entity.Class?.ClassDescription } : new KeyValueDTO(),
                    MinimumMarks = entity.MinimumMarks,
                    MaximumMarks = entity.MaximumMarks,
                    OnlineExamTypeID = entity.OnlineExamTypeID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    AcademicYearID = entity.AcademicYearID.HasValue ? entity.AcademicYearID : _context.AcademicYearID,
                    SchoolID = entity.SchoolID.HasValue ? entity.SchoolID : (byte?)_context.SchoolID,
                };

                OnlineExamDetails.OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();
                foreach (var qnGroup in entity.ExamQuestionGroupMaps)
                {
                    OnlineExamDetails.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                    {
                        ExamQuestionGroupMapIID = qnGroup.ExamQuestionGroupMapIID,
                        OnlineExamID = qnGroup.OnlineExamID,
                        QuestionGroupID = qnGroup.QuestionGroupID,
                        NumberOfQuestions = qnGroup.NumberOfQuestions,
                        GroupName = qnGroup.QuestionGroupID.HasValue ? qnGroup.QuestionGroup?.GroupName : null,
                        MaximumMarks = qnGroup.MaximumMarks,
                        GroupTotalQnCount = qnGroup.QuestionGroupID.HasValue ? qnGroup.QuestionGroup?.Questions != null ? qnGroup.QuestionGroup?.Questions.Count() : 0 : 0,
                    });
                }

                OnlineExamDetails.Subjects = new List<KeyValueDTO>();
                foreach (var sub in entity.OnlineExamSubjectMaps)
                {
                    OnlineExamDetails.Subjects.Add(new KeyValueDTO()
                    {
                        Key = sub.SubjectID.ToString(),
                        Value = sub.Subject?.SubjectName
                    });
                }

                return OnlineExamDetails;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as OnlineExamsDTO;

            if (toDto.MinimumMarks >= toDto.MaximumMarks)
            {
                throw new Exception("The minimum mark must be less than the maximum mark.");
            }

            if (toDto.MinimumDuration > toDto.MaximumDuration)
            {
                throw new Exception("The minimum duration must be less than or equal to the maximum duration.");
            }

            if (!toDto.OnlineExamTypeID.HasValue)
            {
                throw new Exception("Please select an exam type.");
            }

            if (toDto.OnlineExamQuestionGroupMaps.Count == 0)
            {
                throw new Exception("Error: Please select at least one question group.");
            }

            if (toDto.OnlineExamQuestionGroupMaps.Count > 0)
            {
                if (toDto.MaximumMarks != toDto.OnlineExamQuestionGroupMaps.Sum(s => s.MaximumMarks))
                {
                    throw new Exception("Exam maximum marks must match the sum of group maximum marks.");
                }
            }

            using (var dbContext1 = new dbEduegateOnlineExamContext())
            {
                var examData = dbContext1.OnlineExams.AsNoTracking().FirstOrDefault(g => g.Name == toDto.Name);
                if (examData != null)
                {
                    if (examData.OnlineExamIID != toDto.OnlineExamIID)
                    {
                        throw new Exception("The exam name you entered already exists. Please choose a different name as duplicate names are not allowed.");
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            var entity = new Eduegate.Domain.Entity.OnlineExam.Models.OnlineExam()
            {
                OnlineExamIID = toDto.OnlineExamIID,
                Description = toDto.Description,
                MaximumDuration = toDto.MaximumDuration,
                MinimumDuration = toDto.MinimumDuration,
                ClassID = toDto.Classes == null ? (int?)null : int.Parse(toDto.Classes.Key),
                MinimumMarks = toDto.MinimumMarks,
                MaximumMarks = toDto.MaximumMarks,
                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                Name = toDto.Name,
                PassNos = toDto.PassNos,
                PassPercentage = toDto.PassPercentage,
                QuestionSelectionID = toDto.QuestionSelectionID,
                OnlineExamTypeID = toDto.OnlineExamTypeID,
                CreatedBy = toDto.OnlineExamIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.OnlineExamIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.OnlineExamIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                UpdatedDate = toDto.OnlineExamIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte?)_context.SchoolID,
            };

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (toDto.Subjects.Count > 0)
                {
                    var oldSubjectMaps = dbContext.OnlineExamSubjectMaps.Where(s => s.OnlineExamID == entity.OnlineExamIID).AsNoTracking().ToList();

                    if (oldSubjectMaps != null && oldSubjectMaps.Count > 0)
                    {
                        dbContext.OnlineExamSubjectMaps.RemoveRange(oldSubjectMaps);
                    }
                }

                entity.OnlineExamSubjectMaps = new List<OnlineExamSubjectMap>();
                foreach (var sub in toDto.Subjects)
                {
                    entity.OnlineExamSubjectMaps.Add(new OnlineExamSubjectMap()
                    {
                        OnlineExamSubjectMapIID = 0,
                        OnlineExamID = entity.OnlineExamIID,
                        SubjectID = int.Parse(sub.Key),
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    });
                }

                var IIDs = toDto.OnlineExamQuestionGroupMaps
                    .Select(a => a.ExamQuestionGroupMapIID).ToList();
                //delete maps

                var entities = dbContext.ExamQuestionGroupMaps.Where(x =>
                    x.OnlineExamID == entity.OnlineExamIID &&
                    !IIDs.Contains(x.ExamQuestionGroupMapIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.ExamQuestionGroupMaps.RemoveRange(entities);

                entity.ExamQuestionGroupMaps = new List<ExamQuestionGroupMap>();
                foreach (var examGroupMap in toDto.OnlineExamQuestionGroupMaps)
                {
                    entity.ExamQuestionGroupMaps.Add(new ExamQuestionGroupMap()
                    {
                        ExamQuestionGroupMapIID = examGroupMap.ExamQuestionGroupMapIID,
                        OnlineExamID = examGroupMap.OnlineExamID,
                        QuestionGroupID = examGroupMap.QuestionGroupID,
                        NumberOfQuestions = examGroupMap.NumberOfQuestions,
                        MaximumMarks = examGroupMap.MaximumMarks,
                        CreatedBy = entity.CreatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedBy = entity.UpdatedBy,
                        UpdatedDate = entity.UpdatedDate,
                    });
                }

                dbContext.OnlineExams.Add(entity);

                if (entity.OnlineExamIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.ExamQuestionGroupMaps.Count > 0)
                    {
                        foreach (var examGroup in entity.ExamQuestionGroupMaps)
                        {
                            if (examGroup.ExamQuestionGroupMapIID == 0)
                            {
                                dbContext.Entry(examGroup).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(examGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    if (entity.OnlineExamSubjectMaps.Count > 0)
                    {
                        foreach (var sub in entity.OnlineExamSubjectMaps)
                        {
                            if (sub.OnlineExamSubjectMapIID == 0)
                            {
                                dbContext.Entry(sub).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(sub).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.OnlineExamIID));
            }
        }

        public List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID)
        {
            var examMapsDTO = new List<CandidateOnlineExamMapDTO>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateExamMaps = dbContext.CandidateOnlineExamMaps
                    .Include(i => i.OnlineExamOperationStatus)
                    .Include(i => i.OnlineExamStatus)
                    .Include(i => i.Candidate)
                    .Include(i => i.OnlineExam)
                    .Where(x => x.CandidateID == candidateID)
                    .AsNoTracking().ToList();

                var completeStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_STATUSID_COMPLETE", 7);
                byte examCompleteStatusID = byte.Parse(completeStatus);

                foreach (var data in candidateExamMaps)
                {
                    var onlineExamDto = new OnlineExamsDTO();
                    decimal? candidateExamQnsTotalMark = 0;

                    bool? isExamConducted = false;
                    if (data.OnlineExamStatusID == examCompleteStatusID)
                    {
                        var examAnswerData = dbContext.CandidateAnswers.Where(x => x.CandidateID == data.CandidateID && x.CandidateOnlineExamMapID == data.CandidateOnlinExamMapIID).AsNoTracking().FirstOrDefault();
                        if (examAnswerData != null)
                        {
                            isExamConducted = true;
                        }
                        else
                        {
                            isExamConducted = false;
                        }
                    }

                    if (data.OnlineExamID.HasValue)
                    {
                        long examID = Convert.ToInt64(data.OnlineExamID);

                        onlineExamDto = GetOnlineExamDetailsByExamID(examID);

                        candidateExamQnsTotalMark = GetCandidateQuestionCount(examID, candidateID);
                    }

                    examMapsDTO.Add(new CandidateOnlineExamMapDTO()
                    {
                        CandidateOnlinExamMapIID = data.CandidateOnlinExamMapIID,
                        CandidateID = data.CandidateID,
                        OnlineExamID = data.OnlineExamID,
                        OnlineExamName = data.OnlineExamID.HasValue ? data.OnlineExam?.Name : null,
                        OnlineExamDescription = data.OnlineExamID.HasValue ? data.OnlineExam?.Description : null,
                        Duration = data.Duration,
                        AdditionalTime = data.AdditionalTime,
                        OnlineExamStatusID = data.OnlineExamStatusID,
                        OnlineExamStatusName = data.OnlineExamStatusID.HasValue ? data.OnlineExamStatus?.ExamStatusName : null,
                        OnlineExamOperationStatusID = data.OnlineExamOperationStatusID,
                        OnlineExamOperationStatusName = data.OnlineExamOperationStatusID.HasValue ? data.OnlineExamOperationStatus?.OperationStatus : null,
                        OnlineExamDTO = onlineExamDto,
                        IsCandidateConductedExam = isExamConducted,
                        ExamStartTime = data.ExamStartTime,
                        ExamEndTime = data.ExamEndTime,
                        CandidateExamQuestionsMarks = candidateExamQnsTotalMark,
                    });
                }
            }

            return examMapsDTO;
        }

        public OnlineExamsDTO GetOnlineExamDetailsByExamID(long examID)
        {
            var onlineExamDto = new OnlineExamsDTO();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var examData = dbContext.OnlineExams.Where(e => e.OnlineExamIID == examID)
                    .Include(i => i.QuestionSelection)
                    .Include(i => i.OnlineExamSubjectMaps).ThenInclude(i => i.Subject)
                    .Include(i => i.ExamQuestionGroupMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (examData != null)
                {
                    var subjectListDTO = new List<KeyValueDTO>();

                    if (examData.OnlineExamSubjectMaps.Count > 0)
                    {
                        foreach (var map in examData.OnlineExamSubjectMaps)
                        {
                            subjectListDTO.Add(new KeyValueDTO()
                            {
                                Key = map.Subject?.SubjectID.ToString(),
                                Value = map.Subject?.SubjectName
                            });
                        }
                    }

                    onlineExamDto = new OnlineExamsDTO()
                    {
                        OnlineExamIID = examData.OnlineExamIID,
                        Name = examData.Name,
                        Description = examData.Description,
                        QuestionSelectionID = examData.QuestionSelectionID,
                        QuestionSelectionName = examData.QuestionSelection?.SelectName,
                        MinimumDuration = examData.MinimumDuration,
                        MaximumDuration = examData.MaximumDuration,
                        PassPercentage = examData.PassPercentage,
                        PassNos = examData.PassNos,
                        MaximumMarks = examData.MaximumMarks,
                        MinimumMarks = examData.MinimumMarks,
                        ClassID = examData.ClassID,
                        Subjects = subjectListDTO,
                        TotalQnGroupsInExam = examData.ExamQuestionGroupMaps != null ? examData.ExamQuestionGroupMaps.Count() : 0,
                    };
                }
            }
            return onlineExamDto;
        }

        public decimal? GetCandidateQuestionCount(long examID, long candidateID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                decimal? totalMarks = 0;
                var candidateExamQuestions = dbContext.OnlineExamQuestions.Where(m => m.OnlineExamID == examID && m.CandidateID == candidateID).AsNoTracking().ToList();

                foreach (var qn in candidateExamQuestions)
                {
                    var question = dbContext.Questions.AsNoTracking().FirstOrDefault(q => q.QuestionIID == qn.QuestionID);

                    if (question != null)
                    {
                        totalMarks += question.Points.HasValue ? question.Points : 0;
                    }
                }

                return totalMarks;
            }
        }

        public List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var keyValues = new List<KeyValueDTO>();

                var examMaps = dbContext.CandidateOnlineExamMaps.Where(x => x.CandidateID == candidateID)
                    .Include(i => i.OnlineExam)
                    .AsNoTracking()
                    .ToList();

                foreach (var map in examMaps)
                {
                    if (map.OnlineExamID.HasValue)
                    {
                        if (map.OnlineExam?.AcademicYearID == academicYearID)
                        {
                            keyValues.Add(new KeyValueDTO()
                            {
                                Key = map.OnlineExamID.ToString(),
                                Value = map.OnlineExam?.Name,
                            });
                        }
                    }
                }

                return keyValues;
            }
        }

    }
}