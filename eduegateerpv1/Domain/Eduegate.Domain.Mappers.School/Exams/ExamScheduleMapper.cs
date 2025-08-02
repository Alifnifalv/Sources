using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System;


namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ExamScheduleMapper : DTOEntityDynamicMapper
    {   
        public static  ExamScheduleMapper Mapper(CallContext context)
        {
            var mapper = new  ExamScheduleMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ExamScheduleDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private ExamScheduleDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<ExamSchedule, dbEduegateSchoolContext>(dbContext);
                var entity = repository.GetById(IID);

                return new ExamScheduleDTO()
                {
                    ExamScheduleIID = entity.ExamScheduleIID,
                    ExamID = entity.ExamID,
                    Exam = new KeyValueDTO()
                    {
                        Key = entity.ExamID.ToString(),
                        Value = entity.Exam.ExamDescription
                    },
                    //SubjectID = entity.SubjectID,
                    //Date = entity.Date,
                    ExamStartDate = entity.ExamStartDate,
                    ExamEndDate = entity.ExamEndDate,
                    StartTime = entity.StartTime,
                    EndTime = entity.EndTime,
                    Room = entity.Room,
                    FullMarks = entity.FullMarks,
                    PassingMarks = entity.PassingMarks,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),

                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ExamScheduleDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new ExamSchedule()
            {
                ExamScheduleIID = toDto.ExamScheduleIID,
                ExamID = toDto.ExamID,
                //SubjectID = toDto.SubjectID,
                //Date = toDto.Date,
                ExamStartDate = toDto.ExamStartDate,
                ExamEndDate = toDto.ExamEndDate,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                Room = toDto.Room,
                FullMarks = toDto.FullMarks,
                PassingMarks = toDto.PassingMarks,
                CreatedBy = toDto.CreatedBy,
                UpdatedBy = toDto.UpdatedBy,
                CreatedDate = toDto.CreatedDate,
                UpdatedDate = toDto.UpdatedDate,
                TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<ExamSchedule, dbEduegateSchoolContext>(dbContext);

                if (entity.ExamScheduleIID == 0)
                {
                    var maxGroupID = repository.GetMaxID(a => a.ExamScheduleIID);
                    entity.ExamScheduleIID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    entity = repository.Insert(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }
            }

            return ToDTOString(ToDTO(entity.ExamScheduleIID));
        }       
    }
}




