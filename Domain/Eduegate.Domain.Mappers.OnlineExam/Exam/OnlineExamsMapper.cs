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
using Eduegate.Domain.Entity.School.Models.School;

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
                    .Include(i => i.ExamQuestionGroupMaps).ThenInclude(i => i.OnlineExamType)
                    .Include(i => i.MarkGrade)
                    .AsNoTracking()
                    .FirstOrDefault();

                decimal? totalSubjectiveMarks = 0;
                decimal? totalObjectiveMarks = 0;

                var subject = entity.OnlineExamSubjectMaps.FirstOrDefault();

                var OnlineExamDetails = new OnlineExamsDTO()
                {
                    OnlineExamIID = entity.OnlineExamIID,
                    Description = entity.Description,
                    MaximumDuration = entity.MaximumDuration,
                    MinimumDuration = entity.MinimumDuration,
                    Name = entity.Name,
                    PassNos = entity.PassNos,
                    PassPercentage = entity.PassPercentage,
                    Classes = entity.ClassID.HasValue ? new KeyValueDTO { Key = entity.ClassID.ToString(), Value = entity.Class?.ClassDescription } : new KeyValueDTO(),
                    MinimumMarks = entity.MinimumMarks,
                    MaximumMarks = entity.MaximumMarks,
                    MarkGrade = entity.MarkGradeID.HasValue ? new KeyValueDTO { Key = entity.MarkGradeID.ToString(), Value = entity.MarkGrade?.Description } : new KeyValueDTO(),
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID.HasValue ? entity.AcademicYearID : _context.AcademicYearID,
                    SchoolID = entity.SchoolID.HasValue ? entity.SchoolID : (byte?)_context.SchoolID,
                };

                OnlineExamDetails.OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();
                foreach (var qnGroup in entity.ExamQuestionGroupMaps)
                {
                    var totalQuestions = dbContext.Questions.Where(x => x.SubjectID == entity.OnlineExamSubjectMaps.FirstOrDefault().SubjectID && x.AcademicYearID == entity.AcademicYearID
                    && x.QuestionGroup.ClassID == entity.ClassID && x.PassageQuestionID == null && x.Points == qnGroup.MaximumMarks)
                        .AsNoTracking()
                        .Include(i => i.QuestionGroup)
                        .ToList();

                    var questionsWithPassage = dbContext.Questions.Where(x => x.SubjectID == entity.OnlineExamSubjectMaps.FirstOrDefault().SubjectID && x.AcademicYearID == entity.AcademicYearID
                    && x.QuestionGroup.ClassID == entity.ClassID && x.PassageQuestionID != null)
                        .AsNoTracking()
                        .Include(i => i.QuestionGroup)
                        .GroupBy(a => a.PassageQuestionID)
                        .Select(a => new { PassageQuestionID = a.Select(a => a.PassageQuestionID), Points = a.Select(a => a.Points).Sum() })
                        .ToList();

                    var totalPassageQuestions = questionsWithPassage.Where(a => a.Points == qnGroup.MaximumMarks)
                        
                        .ToList();

                    OnlineExamDetails.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                    {
                        ExamQuestionGroupMapIID = qnGroup.ExamQuestionGroupMapIID,
                        OnlineExamID = qnGroup.OnlineExamID,
                        NumberOfQuestions = qnGroup.NumberOfQuestions,
                        MaximumMarks = qnGroup.MaximumMarks,
                        TotalQuestions = qnGroup.OnlineExamTypeID == 1 ? totalQuestions.Count() : totalPassageQuestions.Count(),
                        OnlineExamTypeID = qnGroup.OnlineExamTypeID,
                        OnlineExamTypeName = qnGroup.OnlineExamTypeID.HasValue ? qnGroup.OnlineExamType?.ExamTypeName : null,
                        TotalMarks = qnGroup.MaximumMarks * qnGroup.NumberOfQuestions,
                    });

                    if (qnGroup.OnlineExamTypeID == 1)
                    {
                        totalObjectiveMarks += qnGroup.MaximumMarks * qnGroup.NumberOfQuestions;
                    }
                    else if (qnGroup.OnlineExamTypeID == 2)
                    {
                        totalSubjectiveMarks += qnGroup.MaximumMarks * qnGroup.NumberOfQuestions;
                    }
                }

                OnlineExamDetails.SubjectiveMarks = totalSubjectiveMarks;
                OnlineExamDetails.ObjectiveMarks = totalObjectiveMarks;

                OnlineExamDetails.Subject = new KeyValueDTO()
                {
                    Key = subject.SubjectID.ToString(),
                    Value = subject.Subject?.SubjectName
                };

                OnlineExamDetails.ObjTotalQuestions = GetQuestionDetails(subject.SubjectID ?? 0, entity.ClassID ?? 0)[0];
                OnlineExamDetails.SubTotalQuestions = GetQuestionDetails(subject.SubjectID ?? 0, entity.ClassID ?? 0)[1];

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

            //if (!toDto.OnlineExamTypeID.HasValue)
            //{
            //    throw new Exception("Please select an exam type.");
            //}

            //if (toDto.OnlineExamQuestionGroupMaps.Count == 0)
            //{
            //    throw new Exception("Error: Please select at least one question group.");
            //}

            if (toDto.OnlineExamQuestionGroupMaps.Count > 0)
            {
                if (toDto.ObjectiveMarks != toDto.OnlineExamQuestionGroupMaps.Where(a => a.OnlineExamTypeID == 1).Sum(s => s.TotalMarks))
                {
                    throw new Exception("The total objective maximum marks must be equal to the sum of the distributed objective marks.");
                }

                if (toDto.SubjectiveMarks != toDto.OnlineExamQuestionGroupMaps.Where(a => a.OnlineExamTypeID == 2).Sum(s => s.TotalMarks))
                {
                    throw new Exception("The total subjective maximum marks must be equal to the sum of the distributed subjective marks.");
                }

                if ((toDto.ObjectiveMarks + toDto.SubjectiveMarks) != toDto.MaximumMarks)
                {
                    throw new Exception("The total subjective maximum marks and objective marks must be equal to the total marks.");
                }
            }
            else
            {
                throw new Exception("Please enter atleast one mark distribution.");
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

                if (toDto.OnlineExamIID != 0)
                {
                    bool isExamMapped = dbContext1.CandidateOnlineExamMaps.Where(a => a.OnlineExamID == toDto.OnlineExamIID).Any();

                    if (isExamMapped)
                    {
                        throw new Exception("This exam does not support editing, as it has already been assigned to the candidates.");
                    }
                }
            }

            using (var dbContext1 = new dbEduegateSchoolContext())
            {
                var markGrade = dbContext1.MarkGrades.Where(a => a.MarkGradeIID == int.Parse(toDto.MarkGrade.Key))
                        .Include(a => a.MarkGradeMaps).AsNoTracking().FirstOrDefault();

                if (markGrade != null)
                {
                    if (markGrade.MarkGradeMaps.OrderByDescending(a => a.GradeTo).FirstOrDefault().GradeTo != toDto.MaximumMarks)
                    {
                        throw new Exception("The provided mark grade doesn't match to the total marks.");
                    }
                }
            }

            //    foreach (var question in toDto.OnlineExamQuestionGroupMaps)
            //    {
            //        var questionGroup = dbContext1.Questions.Where(a => a.QuestionGroupID == question.QuestionGroupID).Include(i => i.QuestionGroup).AsNoTracking().ToList();

            //        var totalmarks = questionGroup.Select(a => a.Points).Sum();

            //        if (totalmarks < question.MaximumMarks)
            //        {
            //            throw new Exception("The maximum marks assigned to the respective question group (" + questionGroup.FirstOrDefault().QuestionGroup.GroupName + ") is greater than the maximum marks to be allotted.");
            //        }
            //    }
            //}

            if(toDto.MinimumDuration == 0 || toDto.MaximumDuration == 0)
            {
                throw new Exception("Durations cannot be 0");
            }

            if (toDto.MinimumDuration > toDto.MaximumDuration)
            {
                throw new Exception("Minimum Duration cannot be greater than Maximum Duration");
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
                //QuestionSelectionID = toDto.QuestionSelectionID,
                OnlineExamTypeID = toDto.OnlineExamTypeID,
                MarkGradeID = toDto.MarkGrade == null ? (int?)null : int.Parse(toDto.MarkGrade.Key),
                CreatedBy = toDto.OnlineExamIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.OnlineExamIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.OnlineExamIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                UpdatedDate = toDto.OnlineExamIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : (byte?)_context.SchoolID,
            };

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (toDto.Subjects.IsNotNull())
                {
                    var oldSubjectMaps = dbContext.OnlineExamSubjectMaps.Where(s => s.OnlineExamID == entity.OnlineExamIID).AsNoTracking().ToList();

                    if (oldSubjectMaps != null && oldSubjectMaps.Count > 0)
                    {
                        dbContext.OnlineExamSubjectMaps.RemoveRange(oldSubjectMaps);
                    }
                }

                entity.OnlineExamSubjectMaps.Add(new OnlineExamSubjectMap()
                {
                    OnlineExamSubjectMapIID = 0,
                    OnlineExamID = entity.OnlineExamIID,
                    SubjectID = int.Parse(toDto.Subject.Key),
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                });

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
                        //QuestionGroupID = examGroupMap.QuestionGroupID,
                        NumberOfQuestions = examGroupMap.NumberOfQuestions,
                        MaximumMarks = examGroupMap.MaximumMarks,
                        //NoOfPassageQuestions = examGroupMap.NoOfPassageQuestions,
                        OnlineExamTypeID = examGroupMap.OnlineExamTypeID,
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
                    int? candidateExamQnsTotalQuestions = 0;

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
                        candidateExamQnsTotalQuestions = GetCandidateQuestionCount(examID, candidateID);
                        candidateExamQnsTotalMark = GetCandidateTotalMarks(examID, candidateID);
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
                        TotalQuestions = candidateExamQnsTotalQuestions,
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

        //public decimal? GetCandidateQuestionCount(long examID, long candidateID)
        //{
        //    using (var dbContext = new dbEduegateOnlineExamContext())
        //    {
        //        decimal? totalMarks = 0;
        //        var candidateExamQuestions = dbContext.OnlineExamQuestions.Where(m => m.OnlineExamID == examID && m.CandidateID == candidateID).AsNoTracking().ToList();

        //        foreach (var qn in candidateExamQuestions)
        //        {
        //            var question = dbContext.Questions.AsNoTracking().FirstOrDefault(q => q.QuestionIID == qn.QuestionID);

        //            if (question != null)
        //            {
        //                totalMarks += question.Points.HasValue ? question.Points : 0;
        //            }
        //        }

        //        return totalMarks;
        //    }
        //}

        public int? GetCandidateQuestionCount(long examID, long candidateID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateAllExamQuestions = dbContext.OnlineExamQuestions
                    .Where(m => m.OnlineExamID == examID && m.CandidateID == candidateID)
                    .AsNoTracking()
                    .ToList();

                var candidateExamQuestions = dbContext.Questions
                    .Where(a => candidateAllExamQuestions.Select(a => a.QuestionID).Contains(a.QuestionIID))
                    .AsNoTracking()
                    .ToList();

                var passageQuestions = candidateExamQuestions
                    .Where(a => a.PassageQuestionID == null)
                    .ToList();

                var questionsWithPassage = candidateExamQuestions
                    .Where(a => a.PassageQuestionID != null)
                    .ToList();

                var groupedExamQuestions = questionsWithPassage
                    .GroupBy(a => a.PassageQuestionID)
                    .ToList();

                var totalQuestions = groupedExamQuestions.Count() + passageQuestions.Count();
                return totalQuestions;
            }
        }

        public decimal? GetCandidateTotalMarks(long examID, long candidateID)
        {
            decimal? totalMarks = 0;

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateAllExamQuestions = dbContext.OnlineExamQuestions
                    .Where(m => m.OnlineExamID == examID && m.CandidateID == candidateID)
                    .AsNoTracking()
                    .ToList();

                var candidateExamQuestions = dbContext.Questions
                    .Where(a => candidateAllExamQuestions.Select(a => a.QuestionID).Contains(a.QuestionIID))
                    .AsNoTracking()
                    .ToList();

                var totalQuestions = candidateExamQuestions
                    .Where(a => a.PassageQuestionID == null)
                    .ToList();

                var questionsWithPassage = candidateExamQuestions
                    .Where(a => a.PassageQuestionID != null)
                    .ToList();

                var groupedExamQuestions = questionsWithPassage
                    .GroupBy(a => a.QuestionIID).Select(g => g.Select(q => q.Points ?? 0))
                    .ToList();

                foreach (var qun in totalQuestions)
                {
                    totalMarks += qun.Points;
                }

                foreach (var points in groupedExamQuestions)
                {
                    totalMarks += points.FirstOrDefault();
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

        public List<KeyValueDTO> GetQnGroupDetailsByID(List<int> subjectIDs)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var keyValues = new List<KeyValueDTO>();

                var examMaps = dbContext.QuestionGroups.Where(x => subjectIDs.Contains(x.SubjectID ?? 0))
                    .AsNoTracking()
                    .ToList();

                foreach (var map in examMaps)
                {
                    keyValues.Add(new KeyValueDTO()
                    {
                        Key = map.QuestionGroupID.ToString(),
                        Value = map.GroupName,
                    });
                }

                return keyValues;
            }
        }

        public string GetQuestionsByMarks(int marks, int subjectID, int classID, bool isPassage, int? academicYearID = null)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                string count = string.Empty;

                if (!isPassage)
                {
                    var examMaps = dbContext.Questions.Where(x => x.SubjectID == subjectID && x.AcademicYearID == (academicYearID == null ? _context.AcademicYearID : academicYearID) 
                    && x.Points == marks && x.QuestionGroup.ClassID == classID && x.PassageQuestionID == null)
                        .AsNoTracking()
                        .Include(i => i.QuestionGroup)
                        .ToList();

                    count = examMaps.Count().ToString();
                }
                else
                {
                    var allExamMaps = dbContext.Questions.Where(x => x.SubjectID == subjectID && x.AcademicYearID == (academicYearID == null ? _context.AcademicYearID : academicYearID) 
                    && x.QuestionGroup.ClassID == classID && x.PassageQuestionID != null)
                        .AsNoTracking()
                        .Include(i => i.QuestionGroup)
                        .GroupBy(a => a.PassageQuestionID)
                        .Select(a => new { PassageQuestionID = a.Select(a => a.PassageQuestionID), Points = a.Select(a => a.Points).Sum() })
                        .ToList();

                    var examMaps = allExamMaps.Where(a => a.Points == marks).ToList();

                    count = examMaps.Count().ToString();
                }

                return count;
            }
        }

        public List<string> GetQuestionDetails(int subjectID, int classID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var count = new List<string>();
                var objCount = new List<string>();
                var subCount = new List<string>();

                var objQuestions = dbContext.Questions
                    .Where(x => x.SubjectID == subjectID &&
                                x.QuestionGroup.AcademicYearID == _context.AcademicYearID &&
                                x.QuestionGroup.ClassID == classID &&
                                x.PassageQuestionID == null)
                    .AsNoTracking()
                    .Include(i => i.QuestionGroup)
                    .ToList();

                var objQuestionCount = objQuestions
                    .GroupBy(a => a.Points)
                    .Select(a => new { Points = a.Key, Count = a.Count() })
                    .ToList();

                var subQuestions = dbContext.Questions
                    .Where(x => x.SubjectID == subjectID &&
                                x.QuestionGroup.AcademicYearID == _context.AcademicYearID &&
                                x.QuestionGroup.ClassID == classID &&
                                x.PassageQuestionID != null)
                    .AsNoTracking()
                    .Include(i => i.QuestionGroup)
                    .GroupBy(a => a.PassageQuestionID)
                    .Select(a => new { Points = a.Select(b => b.Points).Sum() })
                    .ToList();

                var subQuestionCount = subQuestions
                    .GroupBy(a => new { a.Points })
                    .Select(a => new { Points = a.Key, Count = a.Count() })
                    .ToList();

                foreach (var objqn in objQuestionCount)
                {
                    objCount.Add("Marks : " + Convert.ToInt32(objqn.Points).ToString() + ", No Of Questions : " + objqn.Count);
                }

                foreach (var subqn in subQuestionCount)
                {
                    subCount.Add("Marks : " + Convert.ToInt32(subqn.Points.Points).ToString() + ", No Of Questions : " + subqn.Count);
                }

                count.Add(string.Join("<br>", objCount));

                count.Add(string.Join("<br>", subCount));

                return count;
            }
        }

    }
}