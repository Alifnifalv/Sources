using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Domain.Entity.OnlineExam.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System;
using Eduegate.Domain.Entity.OnlineExam;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class CandidateAnswerMapper : DTOEntityDynamicMapper
    {
        public static CandidateAnswerMapper Mapper(CallContext context)
        {
            var mapper = new CandidateAnswerMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CandidateDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CandidateAnswerDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateAnswer = new CandidateAnswerDTO();

                var entity = dbContext.CandidateAnswers.Where(x => x.CandidateAnswerIID == IID)
                    .Include(i => i.Question)
                    .Include(i => i.QuestionOptionMap)
                    .Include(i => i.CandidateOnlineExamMap).ThenInclude(i => i.OnlineExam)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    var answerLists = dbContext.CandidateAnswers.Where(a => a.CandidateOnlineExamMapID == entity.CandidateOnlineExamMapID
                    && a.CandidateID == entity.CandidateID)
                        .Include(i => i.Question)
                        .Include(i => i.QuestionOptionMap).ThenInclude(i => i.Question)
                        .Include(i => i.CandidateOnlineExamMap).ThenInclude(i => i.OnlineExam)
                        .AsNoTracking()
                        .ToList();

                    candidateAnswer = new CandidateAnswerDTO()
                    {
                        CandidateAnswerIID = entity.CandidateAnswerIID,
                        CandidateID = entity.CandidateID,
                        CandidateName = entity.Candidate?.CandidateName,
                        CandidateOnlineExamMapID = entity.CandidateOnlineExamMapID,
                        OnlineExamID = entity.CandidateOnlineExamMap?.OnlineExamID,
                        OnlineExamName = entity.CandidateOnlineExamMap?.OnlineExam?.Description,
                        DateOfAnswer = entity.DateOfAnswer,
                        Comments = entity.Comments,
                        QuestionOptionMapID = entity.QuestionOptionMapID,
                        OnlineExamQuestionID = entity.QuestionID,
                        OnlineExamQuestion = entity.QuestionID.HasValue ? entity.Question?.Description : null,
                        OtherDetails = entity.OtherDetails,
                        OtherAnswers = entity.OtherAnswers,
                    };

                    foreach (var answer in answerLists)
                    {
                        var examID = answer.CandidateOnlineExamMap?.OnlineExamID;

                        var examQuestions = dbContext.OnlineExamQuestions.Where(q => q.OnlineExamID == examID)
                            .AsNoTracking()
                            .ToList();

                        var onlineExamQuestions = new List<OnlineExamQuestionDTO>();

                        foreach (var examQn in examQuestions)
                        {
                            var questionOptionMaps = new List<QuestionOptionMapDTO>();

                            var onlineQuestion = dbContext.Questions
                                .Where(q => q.QuestionIID == examQn.QuestionID)
                                .Include(i => i.QuestionOptionMaps)
                                .AsNoTracking()
                                .FirstOrDefault();

                            foreach (var map in onlineQuestion.QuestionOptionMaps)
                            {
                                questionOptionMaps.Add(new QuestionOptionMapDTO()
                                {
                                    QuestionOptionMapIID = map.QuestionOptionMapIID,
                                    OptionText = map.OptionText,
                                    QuestionID = map.QuestionID,
                                    ImageName = map.ImageName,
                                    ContentID = map.ContentID,
                                });
                            }

                            onlineExamQuestions.Add(new OnlineExamQuestionDTO()
                            {
                                QuestionID = onlineQuestion.QuestionIID,
                                Question = onlineQuestion.Description,
                                QuestionOptionMaps = questionOptionMaps
                            });
                        }

                        candidateAnswer.AnswerList.Add(new CandidateAnswerDTO()
                        {
                            CandidateAnswerIID = answer.CandidateAnswerIID,
                            CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                            Comments = answer.Comments,
                            QuestionOptionMapID = answer.QuestionOptionMapID,
                            QuestionOptionMap = answer.QuestionOptionMap?.OptionText,
                            OtherDetails = answer.OtherDetails,
                            OtherAnswers = answer.OtherAnswers,
                            //OnlineExamQuestionID = answer.QuestionOptionMap?.QuestionID,
                            //ExamQuestion = answer.QuestionOptionMap?.Question?.Description,
                            OnlineExamQuestionList = onlineExamQuestions
                        });
                    }
                }

                return candidateAnswer;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CandidateAnswerDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = new CandidateAnswer()
                {
                    CandidateAnswerIID = toDto.CandidateAnswerIID,
                    CandidateID = toDto.CandidateID,
                    CandidateOnlineExamMapID = toDto.CandidateOnlineExamMapID,
                    DateOfAnswer = toDto.DateOfAnswer,
                    Comments = toDto.Comments,
                    QuestionOptionMapID = toDto.QuestionOptionMapID,
                    OtherDetails = toDto.OtherDetails,
                    OtherAnswers = toDto.OtherAnswers,
                    QuestionID = toDto.OnlineExamQuestionID,
                };

                if (entity.CandidateAnswerIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                return ToDTOString(ToDTO(entity.CandidateAnswerIID));
            }
        }

        public string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                try
                {
                    foreach (var answer in answerList)
                    {
                        var entity = new CandidateAnswer()
                        {
                            CandidateAnswerIID = answer.CandidateAnswerIID,
                            CandidateID = answer.CandidateID,
                            CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                            DateOfAnswer = answer.DateOfAnswer.HasValue ? answer.DateOfAnswer : DateTime.Now.Date,
                            Comments = answer.Comments,
                            QuestionOptionMapID = answer.QuestionOptionMapID,
                            OtherDetails = answer.OtherDetails,
                            OtherAnswers = answer.OtherAnswers,
                            QuestionID = answer.OnlineExamQuestionID
                        };

                        if (entity.CandidateAnswerIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        dbContext.SaveChanges();
                    }

                    return "Successfully Saved";
                }
                catch
                {
                    return "";
                }
            }
        }

        public void CheckCandidateAnswer(CandidateAnswerDTO answer, long? selectedOption)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var answerData = dbContext.CandidateAnswers.Where(a => a.CandidateID == answer.CandidateID
                        && a.CandidateOnlineExamMapID == answer.CandidateOnlineExamMapID && a.QuestionID == answer.OnlineExamQuestionID &&
                        a.QuestionOptionMapID == selectedOption).AsNoTracking().FirstOrDefault();

                if (answerData != null)
                {
                    answer.CandidateAnswerIID = answerData.CandidateAnswerIID;
                }
                else
                {
                    answer.CandidateAnswerIID = 0;
                }
            }
        }

        public string SaveCandidateAnswer(CandidateAnswerDTO answer)
        {
            var multipleChoiceAnswerType = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ANSWER_TYPEID_MULTIPLECHOICE", 2);

            try
            {
                using (var dbContext = new dbEduegateOnlineExamContext())
                {
                    var questionAnswers = dbContext.CandidateAnswers.Where(a => a.QuestionID == answer.OnlineExamQuestionID)
                        .AsNoTracking().ToList();

                    if (questionAnswers.Count > 0)
                    {
                        dbContext.CandidateAnswers.RemoveRange(questionAnswers);
                    }

                    if (answer.QuestionOptionMapIDs.Count > 0)
                    {
                        foreach (var selectedOption in answer.QuestionOptionMapIDs)
                        {
                            var entity = new CandidateAnswer()
                            {
                                CandidateAnswerIID = answer.CandidateAnswerIID,
                                CandidateID = answer.CandidateID,
                                CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                                DateOfAnswer = answer.DateOfAnswer.HasValue ? answer.DateOfAnswer : DateTime.Now.Date,
                                QuestionOptionMapID = selectedOption,
                                QuestionID = answer.OnlineExamQuestionID
                            };

                            if (entity.CandidateAnswerIID == 0)
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            dbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        var multipleChoiceQuestion = dbContext.Questions.Where(a => a.QuestionIID == answer.OnlineExamQuestionID && a.AnswerTypeID == multipleChoiceAnswerType).ToList();

                        if (multipleChoiceQuestion != null && multipleChoiceQuestion.Count() > 0)
                        {
                            dbContext.SaveChanges();
                        }
                        else 
                        {
                            if (string.IsNullOrEmpty(answer.OtherAnswers))
                            {
                                throw new Exception("Type answer to save!");
                            }
                            else
                            {
                                var entity = new CandidateAnswer()
                                {
                                    CandidateAnswerIID = answer.CandidateAnswerIID,
                                    CandidateID = answer.CandidateID,
                                    CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                                    DateOfAnswer = answer.DateOfAnswer.HasValue ? answer.DateOfAnswer : DateTime.Now.Date,
                                    Comments = answer.Comments,
                                    QuestionOptionMapID = answer.QuestionOptionMapID,
                                    OtherDetails = answer.OtherDetails,
                                    OtherAnswers = answer.OtherAnswers,
                                    QuestionID = answer.OnlineExamQuestionID
                                };

                                if (entity.CandidateAnswerIID == 0)
                                {
                                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                                else
                                {
                                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }

                                dbContext.SaveChanges();
                            }
                        }
                    }

                    return "Successfully Saved";
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

        public List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var answerDTOs = new List<CandidateAnswerDTO>();

                var candidateAnswers = dbContext.CandidateAnswers.Where(a => a.CandidateID == examMapData.CandidateID
                        && a.CandidateOnlineExamMapID == examMapData.CandidateOnlinExamMapIID).Include(i => i.CandidateOnlineExamMap)
                        .AsNoTracking().ToList();

                var groupedAnswers = candidateAnswers.GroupBy(g => g.QuestionID).ToList();

                foreach (var answerList in groupedAnswers)
                {
                    var answer = answerList.FirstOrDefault();

                    var optionMapIDs = new List<long?>();

                    foreach (var ans in answerList)
                    {
                        optionMapIDs.Add(ans.QuestionOptionMapID);
                    }
                    answerDTOs.Add(new CandidateAnswerDTO()
                    {
                        CandidateAnswerIID = answer.CandidateAnswerIID,
                        CandidateID = answer.CandidateID,
                        CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                        DateOfAnswer = answer.DateOfAnswer,
                        Comments = answer.Comments,
                        QuestionOptionMapID = answer.QuestionOptionMapID,
                        OtherDetails = answer.OtherDetails,
                        OtherAnswers = answer.OtherAnswers,
                        OnlineExamID = answer.CandidateOnlineExamMap?.OnlineExamID,
                        OnlineExamQuestionID = answer.QuestionID,
                        QuestionOptionMapIDs = optionMapIDs
                    });
                }

                return answerDTOs;
            }
        }

    }
}