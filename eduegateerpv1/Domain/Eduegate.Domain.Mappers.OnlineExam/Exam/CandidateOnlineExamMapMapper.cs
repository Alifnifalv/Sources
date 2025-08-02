using Eduegate.Domain.Entity.OnlineExam.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Newtonsoft.Json;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.OnlineExam;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class CandidateOnlineExamMapMapper : DTOEntityDynamicMapper
    {
        public static CandidateOnlineExamMapMapper Mapper(CallContext context)
        {
            var mapper = new CandidateOnlineExamMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CandidateOnlineExamMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CandidateOnlineExamMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.CandidateOnlineExamMaps.Where(x => x.CandidateOnlinExamMapIID == IID)
                    .Include(i => i.OnlineExamOperationStatus)
                    .Include(i => i.OnlineExamStatus)
                    .Include(i => i.Candidate)
                    .Include(i => i.OnlineExam)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private CandidateOnlineExamMapDTO ToDTO(CandidateOnlineExamMap entity)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var question = new CandidateOnlineExamMapDTO()
                {
                    CandidateOnlinExamMapIID = entity.CandidateOnlinExamMapIID,
                    CandidateID = entity.CandidateID,
                    OnlineExamID = entity.OnlineExamID,
                    Duration = entity.Duration,
                    AdditionalTime = entity.AdditionalTime,
                    OnlineExamStatusID = entity.OnlineExamStatusID,
                    OnlineExamOperationStatusID = entity.OnlineExamOperationStatusID,
                    ExamStartTime = entity.ExamStartTime,
                    ExamEndTime = entity.ExamEndTime,
                    CandidateExamQuestionsMarks = entity.OnlineExam.MaximumMarks,
                };

                return question;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CandidateOnlineExamMapDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = new CandidateOnlineExamMap()
                {
                    CandidateOnlinExamMapIID = (long)toDto.CandidateOnlinExamMapIID,
                    CandidateID = toDto.CandidateID,
                    OnlineExamID = toDto.OnlineExamID,
                    Duration = toDto.Duration,
                    AdditionalTime = toDto.AdditionalTime,
                    OnlineExamStatusID = toDto.OnlineExamStatusID,
                    OnlineExamOperationStatusID = toDto.OnlineExamOperationStatusID,
                    ExamStartTime = toDto.ExamStartTime,
                    ExamEndTime = toDto.ExamEndTime
                };

                dbContext.CandidateOnlineExamMaps.Add(entity);

                if (entity.CandidateOnlinExamMapIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.CandidateOnlinExamMapIID));
            }
        }

        public CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID)
        {
            var candidateExamMapDTO = new CandidateOnlineExamMapDTO();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var mapData = dbContext.CandidateOnlineExamMaps
                    .Where(x => x.CandidateOnlinExamMapIID == candidateExamMapID)
                    .Include(i => i.OnlineExamOperationStatus)
                    .Include(i => i.OnlineExamStatus)
                    .Include(i => i.Candidate)
                    .Include(i => i.OnlineExam)
                    .AsNoTracking()
                    .FirstOrDefault();

                candidateExamMapDTO = ToDTO(mapData);
            }

            return candidateExamMapDTO;
        }

        public string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID)
        {
            try
            {
                var candidateExamMapDTO = new CandidateOnlineExamMapDTO();

                using (var dbContext = new dbEduegateOnlineExamContext())
                {
                    var mapData = dbContext.CandidateOnlineExamMaps.AsNoTracking().FirstOrDefault(x => x.CandidateOnlinExamMapIID == candidateOnlineExamMapID);

                    if (mapData != null)
                    {
                        mapData.ExamStartTime = startDate;

                        //dbContext.Entry(mapData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        //dbContext.SaveChanges();

                        return "Successfully updated!";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }            
        }

        public string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData)
        {
            try
            {
                var candidateExamMapDTO = new CandidateOnlineExamMapDTO();

                using (var dbContext = new dbEduegateOnlineExamContext())
                {
                    var completeStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ONLINE_EXAM_STATUSID_COMPLETE", (byte)7);

                    var mapData = dbContext.CandidateOnlineExamMaps.AsNoTracking().FirstOrDefault(x => x.CandidateOnlinExamMapIID == examMapData.CandidateOnlinExamMapIID);

                    if (mapData != null)
                    {
                        mapData.OnlineExamStatusID = completeStatusID;

                        dbContext.Entry(mapData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        dbContext.SaveChanges();

                        return "Successfully updated!";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return null;
            }
        }

    }
}