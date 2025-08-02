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
    public class OnlinePassageQuestionMapper : DTOEntityDynamicMapper
    {
        public static OnlinePassageQuestionMapper Mapper(CallContext context)
        {
            var mapper = new OnlinePassageQuestionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PassageQuestionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private PassageQuestionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.PassageQuestions.Where(x => x.PassageQuestionIID == IID)
                  .AsNoTracking()
                  .FirstOrDefault();

                var groupData = new PassageQuestionDTO()
                {
                    PassageQuestion = entity.PassageQuestion1,
                    PassageQuestionIID = entity.PassageQuestionIID,
                    ShortDescription = entity.ShortDescription,
                    MinimumMark = entity.MinimumMark,
                    MaximumMark = entity.MaximumMark,
                };

                return groupData;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PassageQuestionDTO;

            //convert the dto to entity and pass to the repository.
            var entity = new PassageQuestion()
            {
                PassageQuestionIID = toDto.PassageQuestionIID,
                PassageQuestion1 = toDto.PassageQuestion,
                ShortDescription = toDto.ShortDescription,
                MinimumMark = toDto.MinimumMark,
                MaximumMark = toDto.MaximumMark,
            };

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (entity.PassageQuestionIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.PassageQuestionIID));
        }

    }
}