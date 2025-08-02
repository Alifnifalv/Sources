using Eduegate.Domain.Entity.OnlineExam;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Domain.Entity.OnlineExam.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class OnlineExamQuestionMapMapper : DTOEntityDynamicMapper
    {
        public static OnlineExamQuestionMapMapper Mapper(CallContext context)
        {
            var mapper = new OnlineExamQuestionMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<OnlineExamQuestionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private OnlineExamQuestionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.OnlineExamQuestions.Where(x => x.OnlineExamQuestionIID == IID).AsNoTracking().FirstOrDefault();

                var candidateID = entity.CandidateID;
                var onlineExamID = entity.OnlineExamID;

                var fullData = dbContext.OnlineExamQuestions.Where(y => y.OnlineExamID == onlineExamID && y.CandidateID == candidateID).AsNoTracking().ToList();

                var candDet = entity.CandidateID.HasValue ? dbContext.Candidates.AsNoTracking().FirstOrDefault(c => c.CandidateIID == candidateID) : null;

                var returnDTO = new OnlineExamQuestionDTO()
                {
                    OnlineExamQuestionIID = entity.OnlineExamQuestionIID,
                    CandidateID = entity.CandidateID,
                    CandidateName = candDet?.CandidateName,
                    OnlineExamID = entity.OnlineExamID,
                    ExamName = entity.ExamName ?? null,
                    ExamDescription = entity.ExamDescription ?? null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID.HasValue ? entity.AcademicYearID : null,
                    SchoolID = entity.SchoolID.HasValue ? entity.SchoolID : null,
                };

                foreach (var list in fullData)
                {
                    var points = dbContext.Questions.Where(c => c.QuestionIID == list.QuestionID).Select(a => a.Points).FirstOrDefault();

                    returnDTO.QuestionListMap.Add(new OnlineExamQuestionDTO()
                    {
                        QuestionID = list.QuestionID.HasValue ? list.QuestionID : null,
                        Question = list.Question ?? null,
                        AnswerType = list.AnswerType ?? null,
                        QuestionOptionCount = list.QuestionOptionCount != null ? list.QuestionOptionCount : 0,
                        Points = points ?? 0,

                    });
                }

                return returnDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as OnlineExamQuestionDTO;
            var entity = new List<OnlineExamQuestion>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                //checkold data
                var checkOld = dbContext.OnlineExamQuestions.Where(x => x.OnlineExamID == toDto.OnlineExamID && x.AcademicYearID == _context.AcademicYearID
                && (toDto.CandidateID != null && x.CandidateID == toDto.CandidateID) ).AsNoTracking().ToList();

                if (toDto.OnlineExamQuestionIID == 0 && checkOld.Count > 0)
                {
                    throw new Exception(toDto.ExamName + " is already exist for this academic year, please use edit !");
                }

                //delete exist data and re-create
                if (toDto.OnlineExamQuestionIID != 0 && checkOld != null)
                {
                    dbContext.OnlineExamQuestions.RemoveRange(checkOld);
                    dbContext.SaveChanges();
                    toDto.OnlineExamQuestionIID = 0;
                }

                foreach (var map in toDto.QuestionListMap)
                {
                    entity.Add(new OnlineExamQuestion()
                    {
                        OnlineExamQuestionIID = toDto.OnlineExamQuestionIID,
                        OnlineExamID = toDto.OnlineExamID,
                        CandidateID = toDto.CandidateID,
                        ExamName = toDto.ExamName,
                        ExamDescription = toDto.ExamDescription,
                        QuestionID = map.QuestionID,
                        Question = map.Question,
                        AnswerType = map.AnswerType,
                        QuestionOptionCount = map.QuestionOptionCount,
                        SchoolID = toDto.SchoolID != null ? toDto.SchoolID : (byte)_context.SchoolID,
                        AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : _context.AcademicYearID,
                        CreatedBy = toDto.OnlineExamQuestionIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.OnlineExamQuestionIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.OnlineExamQuestionIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                        UpdatedDate = toDto.OnlineExamQuestionIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                    });
                }

                foreach (var saveData in entity)
                {
                    if (saveData.OnlineExamQuestionIID == 0)
                    {
                        dbContext.Entry(saveData).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    dbContext.SaveChanges();
                    toDto.OnlineExamQuestionIID = saveData.OnlineExamQuestionIID;
                }
            }

            return ToDTOString(ToDTO(toDto.OnlineExamQuestionIID));
        }
    }
}