using Eduegate.Domain.Entity.OnlineExam.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.OnlineExam;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class OnlineQuestionsMapper : DTOEntityDynamicMapper
    {
        public static OnlineQuestionsMapper Mapper(CallContext context)
        {
            var mapper = new OnlineQuestionsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private OnlineQuestionsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.Questions.Where(x => x.QuestionIID == IID)
                    .Include(i => i.Subject)
                    .Include(i => i.QuestionGroup)
                    .Include(i => i.QuestionOptionMaps)
                    .Include(i => i.PassageQuestion)
                    //.ThenInclude(i => i.QuestionAnswerMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private OnlineQuestionsDTO ToDTO(Question entity)
        {
            var textAnswerTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ANSWER_TYPEID_TEXTANSWER", 1);
            var multipleChoiceTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ANSWER_TYPEID_MULTIPLECHOICE", 2);
            var multiSelectTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ANSWER_TYPEID_MULTISELECT", 3);

            var question = new OnlineQuestionsDTO()
            {
                QuestionIID = entity.QuestionIID,
                AnswerTypeID = entity.AnswerTypeID,
                Description = entity.Description,
                Points = entity.Points,
                QuestionGroupID = entity.QuestionGroupID,
                SubjectID = entity.SubjectID,
                SubjectName = entity.SubjectID.HasValue ? entity.Subject?.SubjectName : null,
                QuestionGroupName = entity.QuestionGroupID.HasValue ? entity.QuestionGroup?.GroupName : null,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                TextAnswerTypeID = textAnswerTypeID,
                MultipleChoiceTypeID = multipleChoiceTypeID,
                MultiSelectTypeID = multiSelectTypeID,
                Docfile = entity.DocFile,
                PassageQuestionID = entity.PassageQuestionID,
                PassageQuestionName = entity.PassageQuestionID.HasValue ? entity.PassageQuestion?.PassageQuestion1 : null,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            question.QuestionOptionMaps = new List<QuestionOptionMapDTO>();
            if (entity.QuestionOptionMaps.Count > 0)
            {
                var orderedQnOptionMaps = entity.QuestionOptionMaps.OrderBy(o => o.OrderNo).ToList();
                foreach (var option in orderedQnOptionMaps)
                {
                    if (option.OptionText != null)
                    {
                        question.QuestionOptionMaps.Add(new QuestionOptionMapDTO()
                        {
                            QuestionOptionMapIID = option.QuestionOptionMapIID,
                            QuestionID = option.QuestionID,
                            OptionText = option.OptionText,
                            ContentID = option.ContentID,
                            ImageName = option.ImageName,
                            OrderNo = option.OrderNo,
                            IsCorrectAnswer = option.IsCorrectAnswer,
                        });
                    }
                }
            }

            return question;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as OnlineQuestionsDTO;

            if (toDto.Points <= 0)
            {
                throw new Exception("Enter score greater than zero!");
            }

            if (toDto.QuestionOptionMaps.Count == 0)
            {
                if ((toDto.AnswerTypeID == toDto.MultipleChoiceTypeID) || (toDto.AnswerTypeID == toDto.MultiSelectTypeID))
                {
                    throw new Exception("Options is required in selected answer type!");
                }
            }

            if (toDto.AnswerTypeID == toDto.TextAnswerTypeID && toDto.QuestionOptionMaps.Count > 0)
            {
                throw new Exception("Options are not allowed in the selected answer type!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                //Edit case
                if (toDto.QuestionIID != 0)
                {
                    //Validation
                    var candidateAnswers = dbContext.CandidateAnswers.Where(a => a.QuestionID == toDto.QuestionIID).AsNoTracking().ToList();
                    if (candidateAnswers.Count > 0)
                    {
                        throw new Exception("Students already answered this question, so cannot proceed to edit or modification!");
                    }

                    #region delete QuestionOptionMaps
                    var IIDs = toDto.QuestionOptionMaps
                               .Select(a => a.QuestionOptionMapIID).ToList();

                    //delete maps
                    var entities = dbContext.QuestionOptionMaps.Where(x =>
                        x.QuestionID == toDto.QuestionIID &&
                        !IIDs.Contains(x.QuestionOptionMapIID)).AsNoTracking().ToList();

                    if (entities.IsNotNull())
                        dbContext.QuestionOptionMaps.RemoveRange(entities);
                    #endregion

                    dbContext.SaveChanges();
                }

                var entity = new Question()
                {
                    QuestionIID = toDto.QuestionIID,
                    AnswerTypeID = toDto.AnswerTypeID,
                    Description = toDto.Description,
                    Points = toDto.Points,
                    QuestionGroupID = toDto.QuestionGroupID,
                    SubjectID = toDto.SubjectID,
                    DocFile = toDto.Docfile,
                    PassageQuestionID = toDto.PassageQuestionID,
                    SchoolID = toDto.SchoolID != null ? toDto.SchoolID : (byte)_context.SchoolID,
                    AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : _context.AcademicYearID,
                    CreatedBy = toDto.QuestionIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.QuestionIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.QuestionIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.QuestionIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                entity.QuestionOptionMaps = new List<QuestionOptionMap>();

                foreach (var qnOptionMap in toDto.QuestionOptionMaps)
                {

                    if (qnOptionMap.OptionText != null)
                    {
                        entity.QuestionOptionMaps.Add(new QuestionOptionMap()
                        {
                            QuestionOptionMapIID = qnOptionMap.QuestionOptionMapIID,
                            QuestionID = toDto.QuestionIID,
                            OptionText = qnOptionMap.OptionText,
                            ImageName = qnOptionMap.ImageName,
                            ContentID = qnOptionMap.ContentID,
                            OrderNo = qnOptionMap.OrderNo,
                            IsCorrectAnswer = qnOptionMap.IsCorrectAnswer,
                        });
                    }
                }

                dbContext.Questions.Add(entity);

                if (entity.QuestionIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var optionMap in entity.QuestionOptionMaps)
                    {
                        if (optionMap.QuestionOptionMapIID == 0)
                        {
                            dbContext.Entry(optionMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(optionMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.QuestionIID));
            }
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID)
        {
            var questionListDTO = new List<OnlineExamQuestionDTO>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateData = dbContext.Candidates.Where(x => x.CandidateIID == candidateID)
                    .Include(i => i.CandidateOnlineExamMaps).ThenInclude(i => i.OnlineExam)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (candidateData != null)
                {
                    foreach (var data in candidateData.CandidateOnlineExamMaps)
                    {
                        questionListDTO.Add(new OnlineExamQuestionDTO()
                        {
                            OnlineExamQuestionIID = 0,
                            CandidateID = candidateData.CandidateIID,
                            OnlineExamID = data.OnlineExamID,
                            ExamName = data.OnlineExamID.HasValue ? data.OnlineExam?.Name : null,
                            ExamDescription = data.OnlineExamID.HasValue ? data.OnlineExam?.Description : null,
                            GroupName = null,
                            QuestionID = 0,
                            Question = null,
                            AnswerType = null,
                            QuestionOptionCount = 0,
                        });
                    }
                }

            }
            return questionListDTO;
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID)
        {
            var questionListDTO = new List<OnlineExamQuestionDTO>();

            var questionOptionMapDTO = new List<QuestionOptionMapDTO>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var questions = dbContext.OnlineExamQuestions.Where(q => q.OnlineExamID == onlineExamID && q.CandidateID == candidateID).AsNoTracking().ToList();

                var examData = dbContext.OnlineExams.AsNoTracking().FirstOrDefault(e => e.OnlineExamIID == onlineExamID);

                var textAnswerTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ANSWER_TYPEID_TEXTANSWER", 1);

                var qnDetail = new Question();

                foreach (var qn in questions)
                {
                    questionOptionMapDTO = new List<QuestionOptionMapDTO>();

                    var qnAnswer = string.Empty;

                    if (qn.QuestionID.HasValue)
                    {
                        qnDetail = dbContext.Questions.Where(x => x.QuestionIID == qn.QuestionID)
                            .Include(i => i.QuestionOptionMaps)
                            .Include(i => i.PassageQuestion)
                            .AsNoTracking()
                            .FirstOrDefault();

                        if (qnDetail.AnswerTypeID == textAnswerTypeID)
                        {
                            qnAnswer = dbContext.CandidateAnswers.Where(a => a.CandidateOnlineExamMapID == candidateExamMapID &&
                                    a.QuestionID == qn.QuestionID && a.QuestionOptionMapID == null).AsNoTracking().FirstOrDefault()?.OtherAnswers;
                        }

                        //var qnOptionMapData = qn.QuestionID.HasValue ? dbContext.QuestionOptionMaps.Where(x => x.QuestionID == qn.QuestionID).ToList() : null;

                        if (qnDetail.QuestionOptionMaps != null || qnDetail.QuestionOptionMaps.Count > 0)
                        {
                            var orderedQnOptions = qnDetail.QuestionOptionMaps.OrderBy(o => o.OrderNo).ToList();

                            foreach (var option in orderedQnOptions)
                            {
                                var candidateAnswer = dbContext.CandidateAnswers.Where(a => a.CandidateOnlineExamMapID == candidateExamMapID &&
                                a.QuestionID == qn.QuestionID && a.QuestionOptionMapID == option.QuestionOptionMapIID).AsNoTracking().FirstOrDefault();

                                questionOptionMapDTO.Add(new QuestionOptionMapDTO()
                                {
                                    QuestionOptionMapIID = option.QuestionOptionMapIID,
                                    OptionText = option.OptionText,
                                    QuestionID = option.QuestionID,
                                    ImageName = option.ImageName,
                                    ContentID = option.ContentID,
                                    IsSelected = candidateAnswer != null ? true : false,
                                });
                            }
                        }
                    }

                    questionListDTO.Add(new OnlineExamQuestionDTO()
                    {
                        OnlineExamQuestionIID = qn.OnlineExamQuestionIID,
                        OnlineExamID = qn.OnlineExamID,
                        ExamName = qn.ExamName,
                        ExamDescription = qn.ExamDescription,
                        GroupName = qn.GroupName,
                        QuestionID = qn.QuestionID,
                        Question = qn.Question,
                        AnswerType = qn.AnswerType,
                        QuestionOptionCount = qn.QuestionOptionCount,
                        SchoolID = qn.SchoolID,
                        AcademicYearID = qn.AcademicYearID,
                        ExamMaximumDuration = examData != null ? examData.MaximumDuration : 0,
                        QuestionOptionMaps = questionOptionMapDTO,
                        QuestionAnswer = qnAnswer,
                        PassageQuestion = qnDetail.PassageQuestion?.PassageQuestion1,
                        IsPassageQn = qnDetail.PassageQuestion != null ? true : false,
                        DocFile = qnDetail.DocFile,
                    });
                }
            }
            return questionListDTO;
        }

        public OnlineExamQuestionDTO GetQuestionDetailsByQuestionID(long questionID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var dtos = new OnlineExamQuestionDTO();

                //Get Question details by QuestionID
                var getData = dbContext.Questions.Where(x => x.QuestionIID == questionID)
                    .Include(i => i.AnswerType)
                    .Include(i => i.QuestionGroup)
                    .Include(i => i.QuestionOptionMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var resultDTO = new OnlineExamQuestionDTO()
                {
                    QuestionID = getData.QuestionIID,
                    Question = getData.Description,
                    AnswerType = getData.AnswerTypeID.HasValue ? getData.AnswerType.TypeName : null,
                    Points = getData.Points != null ? getData.Points : null,
                    QuestionGroup = getData.QuestionGroupID.HasValue ? getData.QuestionGroup.GroupName : null,
                    QuestionOptionCount = getData.QuestionOptionMaps.Count(),
                };

                return resultDTO;
            }
        }

        public string GetPassageQuestionDetails(long passageQuestionID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var data = dbContext.PassageQuestions.Where(x => x.PassageQuestionIID == passageQuestionID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return data.PassageQuestion1;
            }
        }

        public CandidateDTO GetCandidateDetailsByCandidateID(long candidateID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var data = dbContext.Candidates.Where(x => x.CandidateIID == candidateID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new CandidateDTO()
                {
                    CandidateIID = data.CandidateIID,
                    CandidateName = data.CandidateName,
                    UserName = data.UserName,
                };
            }
        }
    }
}