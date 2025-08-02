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
using Eduegate.Domain.Entity.OnlineExam;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class OnlineQuestionGroupsMapper : DTOEntityDynamicMapper
    {
        public static OnlineQuestionGroupsMapper Mapper(CallContext context)
        {
            var mapper = new OnlineQuestionGroupsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<OnlineQuestionGroupsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private OnlineQuestionGroupsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.QuestionGroups.Where(x => x.QuestionGroupID == IID)
                  .Include(i => i.Subject)
                  .AsNoTracking()
                  .FirstOrDefault();

                var groupData = new OnlineQuestionGroupsDTO()
                {
                    GroupName = entity.GroupName,
                    QuestionGroupID = entity.QuestionGroupID,
                    SubjectID = entity.SubjectID,
                    SubjectName = entity.SubjectID.HasValue ? entity.Subject.SubjectName : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    AcademicYearID = entity.AcademicYearID.HasValue ? entity.AcademicYearID : null,
                    SchoolID = entity.SchoolID.HasValue ? entity.SchoolID : null,
                };

                return groupData;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as OnlineQuestionGroupsDTO;

            if (!toDto.SubjectID.HasValue)
            {
                throw new Exception("Please select subject");
            }

            using (var dbContext1 = new dbEduegateOnlineExamContext())
            {
                var groupData = dbContext1.QuestionGroups.AsNoTracking().FirstOrDefault(g => g.GroupName == toDto.GroupName);
                if (groupData != null)
                {
                    if (groupData.QuestionGroupID != toDto.QuestionGroupID)
                    {
                        throw new Exception("The group name you entered already exists. Please choose a different name as duplicate names are not allowed.");
                    }
                }
            }

            //convert the dto to entity and pass to the repository.
            var entity = new QuestionGroup()
            {
                QuestionGroupID = toDto.QuestionGroupID,
                GroupName = toDto.GroupName,
                SubjectID = toDto.SubjectID,
                SchoolID = toDto.SchoolID != null ? toDto.SchoolID : (byte)_context.SchoolID,
                AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : _context.AcademicYearID,
                CreatedBy = toDto.QuestionGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.QuestionGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.QuestionGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.QuestionGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (entity.QuestionGroupID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionGroupID));
        }

        public List<KeyValueDTO> GetSubjectbyQuestionGroup(long questionGroupID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var subjectList = dbContext.QuestionGroups.Where(x => x.QuestionGroupID == questionGroupID)
                    .Include(i => i.Subject)
                    .AsNoTracking().ToList();

                var list = new List<KeyValueDTO>();

                foreach (var subject in subjectList)
                {
                    list.Add(new KeyValueDTO()
                    {
                        Value = subject.Subject.SubjectName,
                        Key = subject.Subject.SubjectID.ToString(),
                    });
                }

                return list;
            }
        }

        public OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var QnGroupDTO = new OnlineQuestionGroupsDTO();

                var groupData = dbContext.QuestionGroups.Where(g => g.QuestionGroupID == qnGroupID)
                    .Include(i => i.Questions).AsNoTracking().FirstOrDefault();

                if (groupData != null)
                {
                    QnGroupDTO = new OnlineQuestionGroupsDTO()
                    {
                        QuestionGroupID = groupData.QuestionGroupID,
                        TotalQuestions = groupData.Questions != null ? groupData.Questions.Count() : 0,
                    };
                }

                return QnGroupDTO;
            }
        }

    }
}