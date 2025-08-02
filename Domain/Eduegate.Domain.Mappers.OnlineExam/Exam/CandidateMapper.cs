using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Domain.Entity.OnlineExam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Mappers.Common;
using Eduegate.Domain.Entity.OnlineExam;
using Eduegate.Services.Contracts.School.Exams;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class CandidateMapper : DTOEntityDynamicMapper
    {
        public static CandidateMapper Mapper(CallContext context)
        {
            var mapper = new CandidateMapper();
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

        private CandidateDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = dbContext.Candidates.Where(c => c.CandidateIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.CandidateOnlineExamMaps).ThenInclude(i => i.OnlineExam)
                    .AsNoTracking()
                    .FirstOrDefault();

                var candidateDetails = new CandidateDTO()
                {
                    CandidateIID = entity.CandidateIID,
                    CandidateName = entity.CandidateName,
                    NationalID = entity.NationalID,
                    Email = entity.Email,
                    MobileNumber = entity.MobileNumber,
                    Notes = entity.Notes,
                    Telephone = entity.Telephone,
                    UserName = entity.UserName,
                    Password = entity.Password,
                    StudentID = entity.StudentID,
                    StudentName = entity.StudentID.HasValue ? entity.Student?.AdmissionNumber + " " + entity.Student?.FirstName + " " + entity.Student?.MiddleName + " " + entity.Student?.LastName : null,
                    ClassID = entity.ClassID,
                    ClassName = entity.ClassID.HasValue ? entity.Class?.ClassDescription : null,
                    CandidateStatusID = entity.CandidateStatusID,
                    StudentApplicationID = entity.StudentApplicationID,
                };

                candidateDetails.CandidateOnlineExamMaps = new List<CandidateOnlineExamMapDTO>();

                foreach (var onlineExam in entity.CandidateOnlineExamMaps)
                {
                    candidateDetails.CandidateOnlineExamMaps.Add(new CandidateOnlineExamMapDTO()
                    {
                        CandidateOnlinExamMapIID = onlineExam.CandidateOnlinExamMapIID,
                        CandidateID = onlineExam.CandidateID,
                        OnlineExamID = onlineExam.OnlineExamID,
                        OnlineExamName = onlineExam.OnlineExamID.HasValue ? onlineExam.OnlineExam?.Name : null,
                        Duration = onlineExam.Duration,
                        AdditionalTime = onlineExam.AdditionalTime,
                        OnlineExamStatusID = onlineExam.OnlineExamStatusID,
                        OnlineExamOperationStatusID = onlineExam.OnlineExamOperationStatusID,
                        ExamStartTime = onlineExam.ExamStartTime,
                        ExamEndTime = onlineExam.ExamEndTime
                    });
                }

                return candidateDetails;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CandidateDTO;

            if (string.IsNullOrEmpty(toDto.CandidateName) && toDto.IsNewCandidate == true)
            {
                throw new Exception("Please Enter Candidate Name!");
            }

            if (toDto.Student.Count > 0 && toDto.IsAllStudents == true)
            {
                throw new Exception("Please Untick 'All Students' While Using Student Selection !");
            }

            if (toDto.IsAllStudents == true && toDto.ClassID == null || toDto.IsAllStudents == true && toDto.ClassID == 0)
            {
                throw new Exception("Please Select a Class while using 'All Students' !");
            }

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                //checking for duplicate 
                var exceptList = toDto.ExceptStudentList.Select(a => long.Parse(a.Key)).ToList();

                var allStudCheck = toDto.IsAllStudents == true ? dbContext.Students.Where(s => s.ClassID == toDto.ClassID && s.SchoolAcademicyearID == _context.AcademicYearID && (exceptList.Count == 0 || !exceptList.Contains(s.StudentIID))).AsNoTracking().ToList() : null;

                var getStudentLists = allStudCheck != null ? allStudCheck.Select(a => a.StudentIID).ToList() : toDto.Student.Select(a => long.Parse(a.Key)).ToList();

                var tabledatas = toDto.IsAllStudents == true ? dbContext.Candidates.Where(x => (x.ClassID == toDto.ClassID) && getStudentLists.Contains(x.StudentID.Value)).AsNoTracking().ToList()
                    : dbContext.Candidates.Where(x => (x.ClassID == toDto.ClassID) && getStudentLists.Contains(x.StudentID.Value)).AsNoTracking().ToList();

                if (tabledatas.Count > 0 || tabledatas != null)
                {
                    foreach (var dup in tabledatas)
                    {
                        throw new Exception(@"Student : " + dup.CandidateName + @" is Already Registered. Please Check !");
                    }
                }
            };

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (toDto.CandidateIID != 0)
                {
                    var candidate = SaveCandidate(toDto, dbContext, dto);

                    return ToDTOString(ToDTO(candidate.CandidateIID));
                }
                else
                {
                    if (toDto.IsNewCandidate == true && !string.IsNullOrEmpty(toDto.CandidateName))
                    {
                        var candidate = SaveCandidate(toDto, dbContext, dto);

                        return ToDTOString(ToDTO(candidate.CandidateIID));
                    }
                    else
                    {
                        var entityCandidateList = new List<Candidate>();
                        var entityCandidate = new Candidate();

                        var exceptStudentListIDs = toDto.ExceptStudentList
                                  .Select(a => long.Parse(a.Key)).ToList();


                        var studentListIDs = toDto.Student
                                  .Select(a => long.Parse(a.Key)).ToList();


                        var studentList = toDto.IsAllStudents == false || toDto.IsAllStudents == null ? (from n in dbContext.Students
                                                                                                         join a in dbContext.AcademicYears on n.SchoolAcademicyearID equals a.AcademicYearID
                                                                                                         where n.ClassID == toDto.ClassID && n.IsActive == true && a.AcademicYearStatusID != 3
                                                                                                         && (exceptStudentListIDs.Count == 0 || !exceptStudentListIDs.Contains(n.StudentIID))
                                                                                                         && (studentListIDs.Count == 0 || studentListIDs.Contains(n.StudentIID))
                                                                                                         orderby n.AdmissionNumber
                                                                                                         select new CandidateDTO
                                                                                                         {
                                                                                                             StudentID = n.StudentIID,
                                                                                                             CandidateName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                                                                                             Telephone = n.MobileNumber,
                                                                                                             Email = n.EmailID,
                                                                                                             NationalID = n.StudentPassportDetails.FirstOrDefault().NationalIDNo,
                                                                                                             MobileNumber = n.MobileNumber,
                                                                                                             UserName = n.AdmissionNumber,
                                                                                                             Password = toDto.Password,
                                                                                                             CandidateStatusID = toDto.CandidateStatusID,
                                                                                                             StudentApplicationID = toDto.StudentApplicationID,
                                                                                                         }).AsNoTracking().ToList() : null;

                        var allStudents = toDto.IsAllStudents == true ? (from s in dbContext.Students
                                                                         where s.ClassID == toDto.ClassID && s.SchoolAcademicyearID == _context.AcademicYearID && s.IsActive == true
                                                                         && (exceptStudentListIDs.Count == 0 || !exceptStudentListIDs.Contains(s.StudentIID))
                                                                         orderby s.AdmissionNumber
                                                                         select new CandidateDTO
                                                                         {
                                                                             StudentID = s.StudentIID,
                                                                             CandidateName = s.FirstName + " " + s.MiddleName + " " + s.LastName,
                                                                             Telephone = s.MobileNumber,
                                                                             Email = s.EmailID,
                                                                             NationalID = s.StudentPassportDetails.FirstOrDefault().NationalIDNo,
                                                                             MobileNumber = s.MobileNumber,
                                                                             UserName = s.AdmissionNumber,
                                                                             Password = toDto.Password,
                                                                             CandidateStatusID = toDto.CandidateStatusID,
                                                                             StudentApplicationID = toDto.StudentApplicationID,
                                                                         }).AsNoTracking().ToList() : null;

                        //var IIDs = allStudents != null ? allStudents.Select(x => x.StudentID).ToList() : studentList.Select(x => x.StudentID).ToList();
                        #region delete maping commented .bsc, candidateID connection with Online results
                        //delete maps
                        //var entities = dbContext.Candidates.Where(x =>
                        //    (x.ClassID == toDto.ClassID) &&
                        //    IIDs.Contains(x.StudentID.Value)).ToList();

                        //var candidateIDs = entities.Select(x => x.CandidateIID).ToList();

                        //var candidateOnlineExamMaps = dbContext.CandidateOnlineExamMaps.Where(x =>

                        //   candidateIDs.Contains(x.CandidateID.Value)).ToList();

                        //if (candidateOnlineExamMaps.IsNotNull())
                        //    dbContext.CandidateOnlineExamMaps.RemoveRange(candidateOnlineExamMaps);

                        //if (entities.IsNotNull())
                        //    dbContext.Candidates.RemoveRange(entities);
                        #endregion

                        if (toDto.IsAllStudents == true)
                        {
                            if (allStudents.Any())
                            {
                                allStudents.All(w =>
                                {
                                    entityCandidate = new Candidate()
                                    {
                                        CandidateName = w.CandidateName,
                                        Email = w.Email,
                                        NationalID = w.NationalID,
                                        ClassID = toDto.ClassID,
                                        MobileNumber = w.MobileNumber,
                                        Notes = w.Notes,
                                        Telephone = w.Telephone,
                                        StudentID = w.StudentID,
                                        UserName = w.UserName,
                                        Password = w.Password,
                                        CandidateStatusID = w.CandidateStatusID,
                                        StudentApplicationID = w.StudentApplicationID,
                                    };

                                    entityCandidate.CandidateOnlineExamMaps = new List<CandidateOnlineExamMap>();
                                    foreach (var examMap in toDto.CandidateOnlineExamMaps)
                                    {
                                        entityCandidate.CandidateOnlineExamMaps.Add(new CandidateOnlineExamMap()
                                        {
                                            CandidateOnlinExamMapIID = (long)examMap.CandidateOnlinExamMapIID,
                                            OnlineExamID = examMap.OnlineExamID,
                                            Duration = examMap.Duration,
                                            AdditionalTime = examMap.AdditionalTime,
                                            OnlineExamStatusID = examMap.OnlineExamStatusID,
                                            OnlineExamOperationStatusID = examMap.OnlineExamOperationStatusID,
                                        });
                                    }
                                    entityCandidateList.Add(entityCandidate);
                                    return true;
                                });
                            }
                        }
                        else
                        {
                            if (studentList.Any())
                            {
                                studentList.All(w =>
                                {
                                    entityCandidate = new Candidate()
                                    {
                                        CandidateName = w.CandidateName,
                                        Email = w.Email,
                                        NationalID = w.NationalID,
                                        ClassID = toDto.ClassID,
                                        MobileNumber = w.MobileNumber,
                                        Notes = w.Notes,
                                        Telephone = w.Telephone,
                                        StudentID = w.StudentID,
                                        UserName = w.UserName,
                                        Password = w.Password,
                                        CandidateStatusID = w.CandidateStatusID,
                                        StudentApplicationID = w.StudentApplicationID,
                                    };

                                    entityCandidate.CandidateOnlineExamMaps = new List<CandidateOnlineExamMap>();
                                    foreach (var examMap in toDto.CandidateOnlineExamMaps)
                                    {
                                        entityCandidate.CandidateOnlineExamMaps.Add(new CandidateOnlineExamMap()
                                        {
                                            CandidateOnlinExamMapIID = (long)examMap.CandidateOnlinExamMapIID,
                                            OnlineExamID = examMap.OnlineExamID,
                                            Duration = examMap.Duration,
                                            AdditionalTime = examMap.AdditionalTime,
                                            OnlineExamStatusID = examMap.OnlineExamStatusID,
                                            OnlineExamOperationStatusID = examMap.OnlineExamOperationStatusID,
                                        });
                                    }

                                    entityCandidateList.Add(entityCandidate);
                                    return true;
                                });
                            }
                        }

                        dbContext.Candidates.AddRange(entityCandidateList);
                        dbContext.SaveChanges();

                        var candidateDTO = new CandidateDTO();
                        return ToDTOString(candidateDTO);
                    }
                }
            }
        }

        private Candidate SaveCandidate(CandidateDTO toDto, dbEduegateOnlineExamContext dbContext, BaseMasterDTO dto)
        {
            using (var dbContext1 = new dbEduegateOnlineExamContext())
            {
                var candidateData = dbContext1.Candidates.Where(g => g.NationalID == toDto.NationalID).AsNoTracking().FirstOrDefault();
                if (candidateData != null)
                {
                    if (candidateData.CandidateIID != toDto.CandidateIID)
                    {
                        throw new Exception("Candidate already registered, please check!");
                    }
                }
            }

            var entity = new Candidate()
            {
                CandidateIID = toDto.CandidateIID,
                CandidateName = toDto.CandidateName,
                Email = toDto.Email,
                NationalID = toDto.NationalID,
                ClassID = toDto.ClassID,
                MobileNumber = toDto.MobileNumber,
                Notes = toDto.Notes,
                Telephone = toDto.Telephone,
                StudentID = toDto.StudentID,
                UserName = toDto.UserName,
                Password = toDto.Password,
                CandidateStatusID = toDto.CandidateStatusID,
                StudentApplicationID = toDto.StudentApplicationID,
                CreatedBy = toDto.CandidateIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.CandidateIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.CandidateIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.CandidateIID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            var IIDs = toDto.CandidateOnlineExamMaps
            .Select(a => a.CandidateOnlinExamMapIID).ToList();
            //delete maps
            var entities = dbContext.CandidateOnlineExamMaps.Where(x =>
                x.CandidateID == entity.CandidateIID &&
                !IIDs.Contains(x.CandidateOnlinExamMapIID)).ToList();

            if (entities.IsNotNull())
            {
                foreach (var map in entities)
                {
                    var candidateAnswers = dbContext.CandidateAnswers.Where(a => a.CandidateOnlineExamMapID == map.CandidateOnlinExamMapIID).ToList();
                    if (candidateAnswers.Count > 0)
                    {
                        throw new Exception("Candidate has already started the exam; it is not possible to delete map data.");
                    }
                }

                dbContext.CandidateOnlineExamMaps.RemoveRange(entities);
            }

            entity.CandidateOnlineExamMaps = new List<CandidateOnlineExamMap>();
            foreach (var examMap in toDto.CandidateOnlineExamMaps)
            {
                var xmEndTime = examMap.ExamEndTime;
                if (examMap.ExamStartTime.HasValue)
                {
                    if (examMap.AdditionalTime != examMap.OldAdditionalTime)
                    {
                        if (examMap.AdditionalTime < examMap.OldAdditionalTime)
                        {
                            throw new Exception("Enter new additional minutes grater then previous minutes!");
                        }
                        else
                        {
                            double totalMinutes = Convert.ToDouble(examMap.Duration + examMap.AdditionalTime);
                            xmEndTime = examMap.ExamStartTime.Value.AddMinutes(totalMinutes);
                        }
                    }
                }

                entity.CandidateOnlineExamMaps.Add(new CandidateOnlineExamMap()
                {
                    CandidateOnlinExamMapIID = (long)examMap.CandidateOnlinExamMapIID,
                    CandidateID = examMap.CandidateID,
                    OnlineExamID = examMap.OnlineExamID,
                    Duration = examMap.Duration,
                    AdditionalTime = examMap.AdditionalTime,
                    OnlineExamStatusID = examMap.OnlineExamStatusID,
                    OnlineExamOperationStatusID = examMap.OnlineExamOperationStatusID,
                    ExamStartTime = examMap.ExamStartTime,
                    ExamEndTime = xmEndTime
                });
            }

            dbContext.Candidates.Add(entity);

            if (entity.CandidateIID == 0)
            {
                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
            else
            {
                foreach (var examDet in entity.CandidateOnlineExamMaps)
                {
                    if (examDet.CandidateOnlinExamMapIID == 0)
                    {
                        dbContext.Entry(examDet).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(examDet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }

                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            dbContext.SaveChanges();

            if (entity.CandidateOnlineExamMaps.Count > 0)
            {
                ValidateAndShuffleExamQuestions(entity.CandidateOnlineExamMaps.ToList(), entity.CandidateIID);
            }

            return entity;
        }

        public CandidateDTO GetCandidateDetails(string username, string password)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateDetails = new CandidateDTO();

                var candidate = dbContext.Candidates.Where(X => X.UserName == username && X.Password == password).AsNoTracking().FirstOrDefault();

                if (candidate != null)
                {
                    candidateDetails.CandidateIID = candidate.CandidateIID;
                    candidateDetails.CandidateName = candidate.CandidateName;
                    candidateDetails.Email = candidate.Email;
                    candidateDetails.UserName = candidate.UserName;
                }

                return candidateDetails;
            }
        }

        public string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                try
                {
                    var currentTime = DateTime.Now;
                    var examMap = dbContext.CandidateOnlineExamMaps.Where(x => x.CandidateOnlinExamMapIID == candidateOnlinExamMapID).AsNoTracking().FirstOrDefault();

                    if (examMap != null)
                    {
                        if (!examMap.ExamStartTime.HasValue)
                        {
                            examMap.ExamStartTime = currentTime;
                        }
                        if (!examMap.ExamEndTime.HasValue)
                        {
                            examMap.ExamEndTime = currentTime.AddMinutes(durationInMinutes);
                        }

                        examMap.OnlineExamStatusID = examStartStatusID;

                        dbContext.Entry(examMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        dbContext.SaveChanges();
                    }

                    return "Updated successfully";
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    return null;
                }
            }
        }

        #region Exam question shuflling and save
        public void ValidateAndShuffleExamQuestions(List<CandidateOnlineExamMap> candidateOnlineExamMaps, long? candidateID)
        {
            var examQuestionList = new List<OnlineExamQuestion>();

            var filteredOnlineExamMaps = new List<CandidateOnlineExamMap>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                foreach (var examMap in candidateOnlineExamMaps)
                {
                    var candidateAnswers = dbContext.CandidateAnswers.Where(a => a.CandidateOnlineExamMapID == examMap.CandidateOnlinExamMapIID).AsNoTracking().ToList();

                    if (candidateAnswers.Count > 0)
                    {
                        continue;
                    }
                    else
                    {
                        var candidateOldQuestions = dbContext.OnlineExamQuestions.Where(q => q.CandidateID == candidateID && q.OnlineExamID == examMap.OnlineExamID).ToList();
                        if (candidateOldQuestions.Count > 0)
                        {
                            dbContext.OnlineExamQuestions.RemoveRange(candidateOldQuestions);
                            dbContext.SaveChanges();
                        }

                        filteredOnlineExamMaps.Add(examMap);
                    }
                }

                GetExamQuestionsByMapData(filteredOnlineExamMaps, candidateID);
            }
        }

        public void GetExamQuestionsByMapData(List<CandidateOnlineExamMap> candidateOnlineExamMaps, long? candidateID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                int shuffleMaxLoopCount = new Domain.Setting.SettingBL(null).GetSettingValue<int>("ONLINEEXAM_SHUFFLING_LOOP_MAX_COUNT", 1);

                byte examDraftStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ONLINE_EXAM_STATUSID_DRAFT", 1);

                byte examSubmittedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ONLINE_EXAM_STATUSID_SUBMITTED", (byte)2);

                foreach (var examMap in candidateOnlineExamMaps)
                {
                    var examQuestionList = new List<OnlineExamQuestion>();

                    var examGroupMaps = dbContext.ExamQuestionGroupMaps.Where(g => g.OnlineExamID == examMap.OnlineExamID)
                        .Include(i => i.QuestionGroup).ThenInclude(i => i.Questions).ThenInclude(i => i.AnswerType)
                        .Include(i => i.QuestionGroup).ThenInclude(i => i.Questions).ThenInclude(i => i.QuestionOptionMaps)
                        .Include(i => i.OnlineExam).ThenInclude(i => i.OnlineExamSubjectMaps)
                        .AsNoTracking()
                        .ToList();

                    string examName = examGroupMaps != null && examGroupMaps.Count > 0 ? examGroupMaps[0].OnlineExam?.Name : null;
                    string examDescription = examGroupMaps != null && examGroupMaps.Count > 0 ? examGroupMaps[0].OnlineExam?.Description : null;
                    int? subjectId = examGroupMaps[0].OnlineExam.OnlineExamSubjectMaps != null && examGroupMaps[0].OnlineExam.OnlineExamSubjectMaps.Count > 0
                    ? examGroupMaps[0].OnlineExam.OnlineExamSubjectMaps.FirstOrDefault()?.SubjectID : null;

                    int totalMarks = examGroupMaps != null && examGroupMaps.Count > 0 ? Convert.ToInt32(examGroupMaps[0].OnlineExam?.MaximumMarks) : 0;

                    var questionGroupDTOList = new List<OnlineQuestionGroupsDTO>();

                    foreach (var grpMap in examGroupMaps)
                    {
                        var groupDTO = new OnlineQuestionGroupsDTO();
                        groupDTO = GroupWiseQestions(grpMap, shuffleMaxLoopCount, examName, totalMarks, subjectId);
                        questionGroupDTOList.Add(groupDTO);
                    }

                    foreach (var groupData in questionGroupDTOList)
                    {
                        foreach (var qnID in groupData.QuestionIDs)
                        {
                            //var questionDetail = examGroupMaps.SelectMany(map => map.QuestionGroup.Questions.Where(q => q.QuestionIID == qnID)).FirstOrDefault();
                            var questionDetail = dbContext.Questions.Where(a => a.QuestionIID == qnID)
                                .Include(a => a.QuestionGroup)
                                .Include(a => a.AnswerType)
                                .Include(a => a.QuestionOptionMaps)
                                .FirstOrDefault();

                            examQuestionList.Add(new OnlineExamQuestion()
                            {
                                OnlineExamQuestionIID = 0,
                                CandidateID = candidateID,
                                OnlineExamID = examMap?.OnlineExamID,
                                ExamName = examName,
                                ExamDescription = examDescription,
                                GroupName = questionDetail?.QuestionGroup?.GroupName,
                                QuestionID = qnID,
                                Question = questionDetail?.Description,
                                AnswerType = questionDetail?.AnswerType?.TypeName,
                                QuestionOptionCount = questionDetail?.QuestionOptionMaps.Count,
                                AcademicYearID = _context != null && _context.AcademicYearID.HasValue ? _context.AcademicYearID : null,
                                SchoolID = _context != null && _context.SchoolID.HasValue ? (byte)_context.SchoolID : (byte?)null,
                                CreatedBy = _context != null && _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                                CreatedDate = DateTime.Now
                            });
                        }
                    }

                    if (examQuestionList.Count > 0)
                    {
                        SaveShuffledExamQuestions(examQuestionList);
                    }

                    var errorFetchGroup = questionGroupDTOList.FirstOrDefault(g => g.IsErrorOccuredWhileShuffling == true);
                    if (errorFetchGroup != null)
                    {
                        UpdateCandidateExamMapData(examMap.CandidateOnlinExamMapIID, examDraftStatusID);

                        throw new Exception(errorFetchGroup.QnShufflingErrorMessage);
                    }
                    else
                    {
                        if (examMap.OnlineExamStatusID != examSubmittedStatusID)
                        {
                            UpdateCandidateExamMapData(examMap.CandidateOnlinExamMapIID, examSubmittedStatusID);
                        }
                    }

                }
            }
        }

        public void UpdateCandidateExamMapData(long examMapID, byte examstatusID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var examMapData = dbContext.CandidateOnlineExamMaps.AsNoTracking().FirstOrDefault(map => map.CandidateOnlinExamMapIID == examMapID);

                if (examMapData != null)
                {
                    examMapData.OnlineExamStatusID = examstatusID;

                    dbContext.Entry(examMapData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }
        }

        public OnlineQuestionGroupsDTO GroupWiseQestions(ExamQuestionGroupMap grpMap, int shuffleMaxLoopCount, string examName, decimal? totalMarks, int? subjectId)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var groupData = new OnlineQuestionGroupsDTO();

                var examData = grpMap.OnlineExam;

                //var questionList = new List<Question>();

                if (grpMap.OnlineExamTypeID == 1)
                {
                    var questionList = dbContext.Questions
                    .Where(q => q.SubjectID == subjectId && q.AcademicYearID == examData.AcademicYearID && q.QuestionGroup.ClassID == examData.ClassID && q.Points == grpMap.MaximumMarks && q.PassageQuestionID == null)
                    .ToList();

                    var totalNoofQuestions = Convert.ToInt32(grpMap.NumberOfQuestions);

                    var maximumMarks = grpMap.NumberOfQuestions * grpMap.MaximumMarks;

                    int shuffleLoopCount = 0;

                    if (totalNoofQuestions > 0)
                    {
                        groupData = ShuffleAndGetQuestions(questionList, totalNoofQuestions, grpMap.MaximumMarks, shuffleMaxLoopCount, shuffleLoopCount, examName, maximumMarks);
                    }
                }
                else if (grpMap.OnlineExamTypeID == 2)
                {
                    var passageQuestions = dbContext.Questions
                    .Where(q => q.SubjectID == subjectId && q.AcademicYearID == examData.AcademicYearID && q.QuestionGroup.ClassID == examData.ClassID && q.PassageQuestionID != null)
                    .AsNoTracking()
                    .Include(i => i.QuestionGroup)
                    .GroupBy(a => a.PassageQuestionID)
                    .Select(a => new { PassageQuestionID = a.Key, Points = a.Sum(q => q.Points ?? 0) })
                    .ToList();

                    var totalNoofQuestions = Convert.ToInt32(grpMap.NumberOfQuestions);

                    var examMaps = passageQuestions.Where(a => a.Points == grpMap.MaximumMarks).ToList();

                    var questionIDsList = examMaps.Select(x => x.PassageQuestionID).ToList();

                    var shuffledPsgQnIDs = ListExtensionsMapper.Mapper(_context).ShuffleIDListWithCount(questionIDsList, totalNoofQuestions);

                    var questionIDs = dbContext.Questions.Where(a => shuffledPsgQnIDs.Contains(a.PassageQuestionID ?? 0)).Select(a => a.QuestionIID).ToList();

                    groupData.IsErrorOccuredWhileShuffling = false;
                    groupData.QuestionIDs = questionIDs;
                }

                return groupData;
            }
        }

        public OnlineQuestionGroupsDTO ShuffleAndGetQuestions(List<Question> questionList, int totalNoofQuestions, decimal? groupMaxMark, int shuffleMaxLoopCount, int shuffleLoopCount, string examName, decimal? totalMarks)
        {
            var groupData = new OnlineQuestionGroupsDTO();

            var shuffledQnIDs = new List<long>();

            var questionIDsList = questionList.Select(x => x.QuestionIID).ToList();

            shuffledQnIDs = ListExtensionsMapper.Mapper(_context).ShuffleIDListWithCount(questionIDsList, totalNoofQuestions);

            decimal? shuffledQnsTotalMark = 0;
            if (shuffledQnIDs.Count > 0)
            {
                foreach (var qnID in shuffledQnIDs)
                {
                    var questionData = questionList.Find(q => q.QuestionIID == qnID);

                    shuffledQnsTotalMark += questionData.Points;
                }
            }

            if (shuffledQnsTotalMark != totalMarks)
            {
                if (shuffleLoopCount <= shuffleMaxLoopCount)
                {
                    shuffleLoopCount += 1;
                    groupData = ShuffleAndGetQuestions(questionList, totalNoofQuestions, groupMaxMark, shuffleMaxLoopCount, shuffleLoopCount, examName, totalMarks);
                }
                else
                {
                    if (shuffledQnsTotalMark < groupMaxMark)
                    {
                        groupData.QuestionIDs = shuffledQnIDs;
                        groupData.IsErrorOccuredWhileShuffling = true;
                        groupData.QnShufflingErrorMessage = "Some questions were shuffled in " + examName + " but didn't meet the required marks. Please add additional questions manually or reshuffle.";
                    }
                    else
                    {
                        groupData.QuestionIDs = new List<long>();
                        groupData.IsErrorOccuredWhileShuffling = true;
                        groupData.QnShufflingErrorMessage = "Unable to shuffle questions for " + examName + " due to insufficient marks. Please add more questions to meet the mark requirements for this exam, or remove that exam from mapping";
                    }
                }
            }
            else
            {
                groupData.IsErrorOccuredWhileShuffling = false;
                groupData.QuestionIDs = shuffledQnIDs;
            }

            return groupData;
        }

        public void SaveShuffledExamQuestions(List<OnlineExamQuestion> examQuestions)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                dbContext.OnlineExamQuestions.AddRange(examQuestions);
                dbContext.SaveChanges();
            }
        }
        #endregion

    }
}