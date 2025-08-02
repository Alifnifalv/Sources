using Eduegate.Domain.Entity.OnlineExam;
using Eduegate.Domain.Entity.OnlineExam.Models;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.OnlineExam.Exam
{
    public class OnlineExamResultsMapper : DTOEntityDynamicMapper
    {
        public static OnlineExamResultsMapper Mapper(CallContext context)
        {
            var mapper = new OnlineExamResultsMapper();
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
                var entity = dbContext.OnlineExams.Where(X => X.OnlineExamIID == IID)
                    .Include(x => x.Class)
                    .Include(x => x.QuestionSelection)
                    .Include(x => x.ExamQuestionGroupMaps).ThenInclude(x => x.QuestionGroup)
                    .Include(x => x.OnlineExamSubjectMaps).ThenInclude(x => x.Subject)
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
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };

                //OnlineExamDetails.OnlineExamQuestionGroupMaps = new List<OnlineExamQuestionGroupMapDTO>();

                //foreach (var qnGroup in entity.ExamQuestionGroupMaps)
                //{
                //    OnlineExamDetails.OnlineExamQuestionGroupMaps.Add(new OnlineExamQuestionGroupMapDTO()
                //    {
                //        ExamQuestionGroupMapIID = qnGroup.ExamQuestionGroupMapIID,
                //        QuestionGroupID = qnGroup.QuestionGroupID,
                //        NumberOfQuestions = qnGroup.NumberOfQuestions,
                //        GroupName = qnGroup.QuestionGroupID.HasValue ? qnGroup.QuestionGroup.GroupName : null,
                //    });
                //}

                OnlineExamDetails.Subjects = new List<KeyValueDTO>();
                foreach (var sub in entity.OnlineExamSubjectMaps)
                {
                    OnlineExamDetails.Subjects.Add(new KeyValueDTO()
                    {
                        Key = sub.SubjectID.ToString(),
                        Value = sub.Subject.SubjectName
                    });
                }

                return OnlineExamDetails;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as OnlineExamsDTO;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var entity = new Eduegate.Domain.Entity.OnlineExam.Models.OnlineExam()
                {
                    OnlineExamIID = toDto.OnlineExamIID,
                    Description = toDto.Description,
                    MaximumDuration = toDto.MaximumDuration,
                    MinimumDuration = toDto.MinimumDuration,
                    ClassID = toDto.Classes == null ? (int?)null : int.Parse(toDto.Classes.Key),
                    MinimumMarks = toDto.MinimumMarks,
                    MaximumMarks = toDto.MaximumMarks,
                    Name = toDto.Name,
                    PassNos = toDto.PassNos,
                    PassPercentage = toDto.PassPercentage,
                    QuestionSelectionID = toDto.QuestionSelectionID,
                    CreatedBy = toDto.OnlineExamIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.OnlineExamIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.OnlineExamIID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.OnlineExamIID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                    //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                };


                //var IIDs = toDto.OnlineExamQuestionGroupMaps
                //.Select(a => a.ExamQuestionGroupMapIID).ToList();
                ////delete maps

                //var entities = dbContext.ExamQuestionGroupMaps.Where(x =>
                //    x.OnlineExamID == entity.OnlineExamIID &&
                //    !IIDs.Contains(x.ExamQuestionGroupMapIID)).ToList();

                //if (entities.IsNotNull())
                //    dbContext.ExamQuestionGroupMaps.RemoveRange(entities);

                if (toDto.Subjects.Count > 0)
                {
                    List<int> IIDs = toDto.Subjects.Select(a => int.Parse(a.Key)).ToList();
                    var Subjentities = dbContext.OnlineExamSubjectMaps.Where(x =>
                        x.OnlineExamID == entity.OnlineExamIID &&
                        !IIDs.Contains(x.SubjectID.Value)).AsNoTracking().ToList();

                    if (Subjentities.IsNotNull())
                        dbContext.OnlineExamSubjectMaps.RemoveRange(Subjentities);

                    entity.OnlineExamSubjectMaps = new List<OnlineExamSubjectMap>();
                    foreach (var examsubjectMap in toDto.Subjects)
                    {
                        int? subjectID = int.Parse(examsubjectMap.Key);
                        var submapID = dbContext.OnlineExamSubjectMaps.Where(h => h.OnlineExamID == toDto.OnlineExamIID && h.SubjectID == subjectID).AsNoTracking().Select(x => x.OnlineExamSubjectMapIID).FirstOrDefault();

                        entity.OnlineExamSubjectMaps.Add(new OnlineExamSubjectMap()
                        {
                            SubjectID = int.Parse(examsubjectMap.Key),
                            OnlineExamID = toDto.OnlineExamIID,
                            OnlineExamSubjectMapIID = submapID
                        });
                    }
                }
                //entity.ExamQuestionGroupMaps = new List<ExamQuestionGroupMap>();
                //foreach (var examGroupMap in toDto.OnlineExamQuestionGroupMaps)
                //{
                //    entity.ExamQuestionGroupMaps.Add(new ExamQuestionGroupMap()
                //    {
                //        ExamQuestionGroupMapIID = examGroupMap.ExamQuestionGroupMapIID,
                //        QuestionGroupID = examGroupMap.QuestionGroupID,
                //        NumberOfQuestions = examGroupMap.NumberOfQuestions,
                //    });
                //}

                if (entity.OnlineExamIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    //foreach (var examGroup in entity.ExamQuestionGroupMaps)
                    //{
                    //    if (examGroup.ExamQuestionGroupMapIID != 0)
                    //    {
                    //        dbContext.Entry(examGroup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //    }
                    //    else
                    //    {
                    //        dbContext.Entry(examGroup).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    //    }
                    //}
                    foreach (var sub in entity.OnlineExamSubjectMaps)
                    {
                        if (sub.OnlineExamSubjectMapIID != 0)
                        {
                            dbContext.Entry(sub).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(sub).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.OnlineExamIID));
            }
        }

        public List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var subjectList = new List<KeyValueDTO>();
                subjectList = (from s in dbContext.OnlineExamSubjectMaps
                               join c in dbContext.OnlineExams on s.OnlineExamID equals c.OnlineExamIID
                               where
                               s.OnlineExamID == examID
                               && (languageTypeID == null || (languageTypeID != null && s.Subject.SubjectTypeID == languageTypeID))
                               select new KeyValueDTO
                               {
                                   Key = s.SubjectID.ToString(),
                                   Value = s.Subject.SubjectName
                               }).AsNoTracking().ToList();

                return subjectList;
            }
        }

        public List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID)
        {
            List<OnlineExamResultDTO> sRetData = new List<OnlineExamResultDTO>();
            using (var sContext = new dbEduegateOnlineExamContext())
            {

                var dFndexam = (from n in sContext.OnlineExams
                                join ec in sContext.OnlineExamSubjectMaps on n.OnlineExamIID equals ec.OnlineExamID

                                where (n.OnlineExamIID == examID
                                  && n.AcademicYearID == academicYearID
                                  && (subjectID == null || ec.SubjectID == subjectID))
                                select new OnlineExamSubjectMap
                                {
                                    OnlineExamSubjectMapIID = ec.OnlineExamSubjectMapIID,
                                    OnlineExamID = ec.OnlineExamID,
                                    SubjectID = ec.SubjectID,
                                    Subject = ec.Subject,
                                    OnlineExam = ec.OnlineExam,
                                }).AsNoTracking().ToList();


                if (dFndexam.Any())
                {
                    var maxMark = dFndexam.Select(x => x.OnlineExam.MaximumMarks).FirstOrDefault();

                    List<CandidateDTO> studentList = (from n in sContext.Candidates
                                                      join a in sContext.CandidateOnlineExamMaps on n.CandidateIID equals a.CandidateID
                                                      join stud in sContext.Students on n.StudentID equals stud.StudentIID
                                                      where a.OnlineExamID == examID
                                                      orderby n.CandidateIID descending
                                                      select new CandidateDTO
                                                      {
                                                          CandidateIID = n.CandidateIID,
                                                          StudentID = n.StudentID,
                                                          CandidateName = stud.AdmissionNumber != null ? stud.AdmissionNumber + " - " + n.CandidateName : n.CandidateName
                                                      }).AsNoTracking().ToList();


                    if (studentList.Any())
                    {
                        var dfndMarkReg = (from markregsub in sContext.OnlineExamResultSubjectMaps
                                           join markreg in sContext.OnlineExamResults on
                                           markregsub.OnlineExamResultsID equals markreg.OnlineExamResultIID
                                           where
                                          (subjectID == null || markregsub.SubjectID == subjectID)
                                           && markreg.OnlineExamID == examID
                                           && markreg.AcademicYearID == academicYearID
                                           && markreg.SchoolID == _context.SchoolID
                                           select new OnlineExamResultCandidateMapDTO()
                                           {
                                               CandidateID = markreg.CandidateID,
                                               SubjectID = markregsub.SubjectID,
                                               SubjectName = markregsub.Subject.SubjectName,
                                               Marks = markregsub.Mark,
                                               Remarks = markreg.Remarks,
                                               //OnlineExamID = markreg.OnlineExamID,
                                               //OnlineExamResultSubjectMapID = markregsub.OnlineExamResultSubjectMapIID,
                                               CandidateName = markreg.Candidate.CandidateName
                                           }).AsNoTracking().ToList();

                        studentList.All(w =>
                        {
                            var onlineExamResultCandidateMapDTO = new List<OnlineExamResultCandidateMapDTO>();

                            if (dfndMarkReg.Any())
                            {
                                onlineExamResultCandidateMapDTO = dfndMarkReg.Where(s => s.CandidateID == w.CandidateIID).ToList();
                            }
                            sRetData.Add(GetStudentForMarkEntry(w.CandidateIID, w.CandidateName, maxMark, dFndexam.ToList(), onlineExamResultCandidateMapDTO)); return true;

                        });
                    }
                }
            }

            return sRetData;
        }

        private OnlineExamResultDTO GetStudentForMarkEntry(long CandidateID, string CandidateName, decimal? maxMark, List<OnlineExamSubjectMap> examSubjectMaps, List<OnlineExamResultCandidateMapDTO> OnlineExamResultCandidateMapDTO)
        {
            var onlineExamResultSubjectMapDTO = GetResultSubjeMapData(CandidateID, maxMark, examSubjectMaps, OnlineExamResultCandidateMapDTO);
            OnlineExamResultDTO sRetData = new OnlineExamResultDTO()
            {
                CandidateID = CandidateID,
                CandidateName = CandidateName,
                MaxMark = maxMark,
                Remarks = OnlineExamResultCandidateMapDTO.Count() > 0 ? OnlineExamResultCandidateMapDTO.FirstOrDefault().Remarks : "",
                OnlineExamResultSubjectMapDTOs = onlineExamResultSubjectMapDTO
            };

            return sRetData;
        }

        private List<OnlineExamResultSubjectMapDTO> GetResultSubjeMapData(long CandidateID, decimal? maxMark, List<OnlineExamSubjectMap> examSubjectMaps, List<OnlineExamResultCandidateMapDTO> onlineExamResultCandidateMapDTO)
        {
            var sRetData = (from s in examSubjectMaps
                            select new OnlineExamResultSubjectMapDTO
                            {
                                SubjectID = s.SubjectID,
                                SubjectName = s.Subject.SubjectName,
                                Marks = onlineExamResultCandidateMapDTO.Count > 0 ? onlineExamResultCandidateMapDTO.Where(x => x.SubjectID == s.SubjectID).Select(y => y.Marks).FirstOrDefault() : (decimal?)null,
                            }

            ).ToList();

            return sRetData;

        }

        public string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> toDto)
        {
            #region Validate DTOs

            if (toDto == null || toDto.Count == 0 || !toDto[0].CandidateID.HasValue)
            {
                throw new Exception("Please fill candidate details!");
            }
            #endregion Validate DTOs

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                //Map DTO to Entities
                #region Map Entities
                var entities = new List<OnlineExamResult>();
                var _sLstonlineExamRes_IDs = new List<long>();
                var _sLstnlineExamResultSub_IDs = new List<long>();

                entities = (from s in toDto
                            select new OnlineExamResult()
                            {
                                OnlineExamID = s.OnlineExamID,
                                CandidateID = s.CandidateID,
                                OnlineExamResultSubjectMaps = GetResultEntrySubjectMaps(s.OnlineExamResultSubjectMapDTOs),
                                Remarks = s.Remarks,
                                SchoolID = (byte)_context.SchoolID,
                                AcademicYearID = s.AcademicYearID,

                                CreatedBy = (int)_context.LoginID,
                                UpdatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now

                            }
                             ).ToList();

                #endregion Map Entities

                //#region DB Updates

                //Deletes entitie
                #region Delete Entities
                var academicYearId = toDto[0].AcademicYearID;
                var examID = toDto[0].OnlineExamID;
                List<long?> candidateIDs = entities.Select(x => (x.CandidateID)).ToList();
                List<int?> subjectIds = entities.SelectMany(x => x.OnlineExamResultSubjectMaps.Select(z => z.SubjectID)).ToList();
                try
                {
                    var existMarkReg =
                           (
                               from onlineExamsub in dbContext.OnlineExamResultSubjectMaps
                               join onlineExam in dbContext.OnlineExamResults on onlineExamsub.OnlineExamResultsID equals onlineExam.OnlineExamResultIID
                               where
                                onlineExam.AcademicYearID == academicYearId
                               && onlineExam.OnlineExamID == examID
                               && candidateIDs.Contains(onlineExam.CandidateID)
                               && subjectIds.Contains(onlineExamsub.SubjectID)


                               select new OnlineExamResultCandidateMapDTO()
                               {
                                   CandidateID = onlineExam.CandidateID,
                                   OnlineExamID = onlineExam.OnlineExamID,
                                   AcademicYearID = onlineExam.AcademicYearID,
                                   SubjectID = onlineExamsub.SubjectID,
                                   OnlineExamResultID = onlineExam.OnlineExamResultIID,
                                   OnlineExamResultSubjectMapID = onlineExamsub.OnlineExamResultSubjectMapIID
                               }
                           ).AsNoTracking().ToList();
                    _sLstonlineExamRes_IDs = existMarkReg.Select(x => x.OnlineExamResultID.Value).ToList();
                    _sLstnlineExamResultSub_IDs = existMarkReg.Select(x => x.OnlineExamResultSubjectMapID.Value).ToList();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
                //Delete:- subjects
                if (_sLstnlineExamResultSub_IDs.Any())
                {

                    var removableSubEntities = dbContext.OnlineExamResultSubjectMaps
                                         .Where(x => _sLstnlineExamResultSub_IDs.Contains(x.OnlineExamResultSubjectMapIID)).AsNoTracking();

                    if (removableSubEntities.Any())
                        dbContext.OnlineExamResultSubjectMaps
                                 .RemoveRange(removableSubEntities);
                    dbContext.SaveChanges();
                    //    return true;
                    //});
                }
                //Delete:- Online Exam Resut

                if (_sLstonlineExamRes_IDs.Any())
                {

                    var removableOnlineResEntities = dbContext.OnlineExamResults
                                         .Where(x => _sLstonlineExamRes_IDs.Contains(x.OnlineExamResultIID) && x.OnlineExamResultSubjectMaps.Count() == 0).AsNoTracking();

                    if (removableOnlineResEntities.Any())
                    {
                        dbContext.OnlineExamResults
                                 .RemoveRange(removableOnlineResEntities);
                        dbContext.SaveChanges();
                    }
                    //    return true;
                    //});

                }

                //Delete
                #endregion Delete Entities

                //Add or Modify entities
                #region Save Entities

                dbContext.OnlineExamResults.AddRange(entities);
                dbContext.SaveChanges();


                #endregion Save Entities
                //dbContext.SaveChanges();
            }

            //#endregion DB Updates

            return ToDTOString(toDto[0]);
        }

        private List<OnlineExamResultSubjectMap> GetResultEntrySubjectMaps(List<OnlineExamResultSubjectMapDTO> SubjectMapDTO)
        {


            var OnlineExamResultSubjectMap = (from sk in SubjectMapDTO
                                              select new OnlineExamResultSubjectMap
                                              {
                                                  SubjectID = sk.SubjectID,
                                                  Mark = sk.Marks,
                                                  CreatedBy = (int)_context.LoginID,
                                                  CreatedDate = DateTime.Now

                                              }).ToList();

            return OnlineExamResultSubjectMap;
        }

        public List<OnlineExamResultDTO> GetOnlineExamsResultByCandidateID(long candidateID)
        {
            var examMapsDTO = new List<OnlineExamResultDTO>();

            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var candidateResults = dbContext.OnlineExamResults.Where(x => x.CandidateID == candidateID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .Include(i => i.OnlineExam)
                    .AsNoTracking().ToList();

                foreach (var data in candidateResults)
                {
                    var isPassed = data.OnlineExam?.MinimumMarks > data.Marks ? true : false;

                    examMapsDTO.Add(new OnlineExamResultDTO()
                    {
                        OnlineExamResultIID = data.OnlineExamResultIID,
                        Marks = data.Marks,
                        MaxMark = data.OnlineExam?.MaximumMarks,
                        CandidateID = data.CandidateID,
                        Remarks = data.Remarks,
                        AcademicYearID = data.AcademicYearID,
                        AcademicYear = data.AcademicYearID.HasValue ? data.AcademicYear != null ? data.AcademicYear.Description + ' ' + '(' + data.AcademicYear.AcademicYearCode + ')' : null : null,
                        SchoolID = data.SchoolID,
                        SchoolName = data.SchoolID.HasValue ? data.AcademicYear != null ? data.School.SchoolName : null : null,
                        OnlineExamID = data.OnlineExamID,
                        OnlineExamName = data.OnlineExamID.HasValue ? data.OnlineExam != null ? data.OnlineExam.Description : "NA" : "NA",
                        IsPassed = isPassed,
                    });
                }
            }

            return examMapsDTO;
        }

        #region autosave mark old code
        public void AutoSaveMarksByCandidateAnswersOLD(List<CandidateAnswerDTO> answerList)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var reviewStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ONLINE_EXAM_RESULT_STATUSID_REVIEW", (byte)1);

                var onlineExamID = answerList[0]?.OnlineExamID;
                var candidateID = answerList[0]?.CandidateID;
                var examDetails = dbContext.OnlineExams.AsNoTracking().FirstOrDefault(e => e.OnlineExamIID == onlineExamID);

                var questions = dbContext.OnlineExamQuestions.Where(q => q.OnlineExamID == onlineExamID && q.CandidateID == candidateID).AsNoTracking().ToList();

                var resultQuestionMaps = new List<OnlineExamResultQuestionMap>();

                foreach (var qn in questions)
                {
                    var questionDetails = dbContext.Questions
                        .Where(q => q.QuestionIID == qn.QuestionID)
                        .Include(i => i.QuestionOptionMaps)
                        //.Include(i => i.QuestionAnswerMaps)
                        .AsNoTracking()
                        .FirstOrDefault();

                    var candidateSelectOptionID = answerList.Find(a => a.OnlineExamQuestionID == qn.QuestionID)?.QuestionOptionMapID;
                    decimal? mark = 0;

                    if (questionDetails != null && questionDetails.QuestionOptionMaps.Select(x => x.IsCorrectAnswer).FirstOrDefault() == true)
                    {
                        mark = questionDetails.Points.HasValue ? questionDetails.Points : 0;
                    }

                    resultQuestionMaps.Add(new OnlineExamResultQuestionMap()
                    {
                        OnlineExamResultQuestionMapIID = 0,
                        QuestionID = qn.QuestionID,
                        Mark = mark,
                        EntryType = "Automatic",
                        CreatedDate = DateTime.UtcNow
                    });
                }

                var entity = new OnlineExamResult()
                {
                    OnlineExamResultIID = 0,
                    CandidateID = answerList[0].CandidateID,
                    OnlineExamID = examDetails?.OnlineExamIID,
                    Marks = resultQuestionMaps.Sum(s => s.Mark),
                    ResultStatusID = reviewStatusID,
                    CreatedDate = DateTime.UtcNow,
                    OnlineExamResultQuestionMaps = resultQuestionMaps
                };

                dbContext.OnlineExamResults.Add(entity);

                if (entity.OnlineExamResultIID == 0)
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
        #endregion

        public void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                if (answerList.Count > 0)
                {
                    var reviewStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_RESULT_STATUSID_REVIEW");
                    var passedStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_RESULT_STATUSID_PASSED");
                    var failedStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_RESULT_STATUSID_FAILED");

                    var objectiveType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_TYPEID_OBJ");
                    var subjectiveType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_EXAM_TYPEID_SUBJ");

                    var objectiveTypeID = string.IsNullOrEmpty(objectiveType) ? (byte)1 : byte.Parse(objectiveType);
                    var subjectiveTypeID = string.IsNullOrEmpty(subjectiveType) ? (byte)2 : byte.Parse(subjectiveType);

                    var resultStatusID = string.IsNullOrEmpty(reviewStatus) ? (byte)1 : byte.Parse(reviewStatus);

                    var onlineExamID = answerList[0]?.OnlineExamID;
                    var candidateID = answerList[0]?.CandidateID;

                    var examDetails = dbContext.OnlineExams.AsNoTracking().FirstOrDefault(e => e.OnlineExamIID == onlineExamID);

                    var questions = dbContext.OnlineExamQuestions.Where(q => q.OnlineExamID == onlineExamID && q.CandidateID == candidateID).AsNoTracking().ToList();

                    var resultQuestionMaps = new List<OnlineExamResultQuestionMap>();

                    decimal? totalMarksObtained = 0;

                    foreach (var qn in questions)
                    {
                        var questionDetails = dbContext.Questions
                            .Where(q => q.QuestionIID == qn.QuestionID)
                            .Include(i => i.QuestionOptionMaps)
                            //.Include(i => i.QuestionAnswerMaps)
                            .AsNoTracking()
                            .FirstOrDefault();

                        var candidateQNAnswer = answerList.FirstOrDefault(a => a.OnlineExamQuestionID == qn.QuestionID);

                        decimal? mark = 0;

                        if (questionDetails != null)
                        {
                            var answers = questionDetails.QuestionOptionMaps.Where(x => x.IsCorrectAnswer == true).ToList();

                            if (answers.Count > 0)
                            {
                                if (answers.Count == 1)
                                {
                                    if (candidateQNAnswer != null && candidateQNAnswer.QuestionOptionMapIDs.Count > 0)
                                    {
                                        var candidateSelectOptionID = candidateQNAnswer.QuestionOptionMapIDs.FirstOrDefault();
                                        var answermapID = answers.FirstOrDefault().QuestionOptionMapIID;

                                        if (answermapID == candidateSelectOptionID)
                                        {
                                            mark = questionDetails.Points.HasValue ? questionDetails.Points : 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if (candidateQNAnswer != null && candidateQNAnswer.QuestionOptionMapIDs.Count > 0)
                                    {
                                        foreach (var selectedOptionID in candidateQNAnswer.QuestionOptionMapIDs)
                                        {
                                            if (answers.Any(o => o.QuestionOptionMapIID == selectedOptionID))
                                            {
                                                decimal? answerCount = Convert.ToDecimal(answers.Count);
                                                mark += questionDetails.Points.HasValue ? (questionDetails.Points / answerCount) : 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        totalMarksObtained += mark;

                        resultQuestionMaps.Add(new OnlineExamResultQuestionMap()
                        {
                            OnlineExamResultQuestionMapIID = 0,
                            QuestionID = qn.QuestionID,
                            Mark = mark,
                            EntryType = "Automatic",
                            CreatedDate = DateTime.UtcNow
                        });
                    }

                    if (examDetails.OnlineExamTypeID == objectiveTypeID && totalMarksObtained < examDetails.MinimumMarks)
                    {
                        resultStatusID = string.IsNullOrEmpty(failedStatus) ? (byte)3 : byte.Parse(failedStatus);
                    }

                    if (examDetails.OnlineExamTypeID == objectiveTypeID && totalMarksObtained >= examDetails.MinimumMarks)
                    {
                        resultStatusID = string.IsNullOrEmpty(passedStatus) ? (byte)2 : byte.Parse(passedStatus);
                    }

                    var entity = new OnlineExamResult()
                    {
                        OnlineExamResultIID = 0,
                        CandidateID = answerList[0].CandidateID,
                        OnlineExamID = examDetails?.OnlineExamIID,
                        Marks = totalMarksObtained,
                        ResultStatusID = resultStatusID,
                        CreatedDate = DateTime.UtcNow,
                        OnlineExamResultQuestionMaps = resultQuestionMaps
                    };

                    dbContext.OnlineExamResults.Add(entity);

                    if (entity.OnlineExamResultIID == 0)
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

        public OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                try
                {
                    var subjectiveType = new Domain.Setting.SettingBL(null).GetSettingValue<int>("ANSWER_TYPEID_TEXTANSWER");

                    var examResultDTO = new OnlineExamResultDTO();

                    var resultQuestionMaps = new List<OnlineExamResultQuestionMapDTO>();

                    var result = dbContext.OnlineExamResults.Where(x => x.CandidateID == candidateID && x.OnlineExamID == examID)
                        .Include(i => i.OnlineExam)
                        .Include(i => i.Candidate)
                        .Include(i => i.ResultStatus)
                        .Include(i => i.AcademicYear)
                        .Include(i => i.School)
                        .Include(i => i.OnlineExamResultSubjectMaps)
                        .Include(i => i.OnlineExamResultQuestionMaps).ThenInclude(i => i.Question)
                        .OrderByDescending(o => o.OnlineExamResultIID)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (result != null)
                    {
                        foreach (var map in result.OnlineExamResultQuestionMaps)
                        {
                            var candidateAnswers = dbContext.CandidateAnswers.Where(a => a.CandidateID == candidateID && a.QuestionID == map.QuestionID)
                                .Include(i => i.Question)
                                .Include(i => i.QuestionOptionMap)
                                .OrderByDescending(o => o.CandidateAnswerIID)
                                .AsNoTracking().ToList();

                            var questionOptionMaps = dbContext.QuestionOptionMaps.Where(a => a.QuestionID == map.QuestionID)
                                .AsNoTracking().ToList();

                            var QnOptionMaps = new List<QuestionOptionMapDTO>();
                            foreach (var option in questionOptionMaps)
                            {
                                var answer = candidateAnswers.Find(a => a.QuestionOptionMapID == option.QuestionOptionMapIID);

                                QnOptionMaps.Add(new QuestionOptionMapDTO()
                                {
                                    QuestionOptionMapIID = option.QuestionOptionMapIID,
                                    OptionText = option.OptionText,
                                    QuestionID = option.QuestionID,
                                    ContentID = option.ContentID,
                                    ImageName = option.ImageName,
                                    IsSelected = answer == null ? false : true,
                                });
                            }

                            var nonOptionAnswer = candidateAnswers.Find(a => a.QuestionOptionMapID == null && a.OtherAnswers != null);

                            resultQuestionMaps.Add(new OnlineExamResultQuestionMapDTO()
                            {
                                OnlineExamResultQuestionMapIID = map.OnlineExamResultQuestionMapIID,
                                OnlineExamResultID = map.OnlineExamResultID,
                                QuestionID = map.QuestionID,
                                Question = map.QuestionID.HasValue ? map.Question?.Description : null,
                                TotalMarksOfQuestion = map.QuestionID.HasValue ? map.Question?.Points : null,
                                SelectedOptionID = candidateAnswers != null && candidateAnswers.Count > 0 ? candidateAnswers[0]?.QuestionOptionMapID : null,
                                SelectedOption = candidateAnswers != null && candidateAnswers.Count > 0 ? candidateAnswers[0]?.QuestionOptionMap?.OptionText : null,
                                Mark = map.Mark,
                                Remarks = map.Remarks,
                                EntryType = map.EntryType,
                                CreatedBy = map.CreatedBy,
                                CreatedDate = map.CreatedDate,
                                UpdatedBy = map.UpdatedBy,
                                UpdatedDate = map.UpdatedDate,
                                QuestionOptionMaps = QnOptionMaps,
                                CandidateTextAnswer = nonOptionAnswer?.OtherAnswers,
                                IsMarkEditable = map.Question.AnswerTypeID == subjectiveType ? true : false,
                            });
                        }

                        examResultDTO = new OnlineExamResultDTO()
                        {
                            OnlineExamResultIID = result.OnlineExamResultIID,
                            Marks = result.Marks,
                            CandidateID = result.CandidateID,
                            CandidateName = result.CandidateID.HasValue ? result.Candidate?.CandidateName : null,
                            Remarks = result.Remarks,
                            OnlineExamID = result.OnlineExamID,
                            OnlineExamName = result.OnlineExam?.Description,
                            ResultStatusID = result.ResultStatusID,
                            ResultStatus = result.ResultStatusID.HasValue ? result.ResultStatus?.StatusNameEn : null,
                            AcademicYearID = result.AcademicYearID,
                            AcademicYear = result.AcademicYearID.HasValue ? result.AcademicYear != null ? result.AcademicYear?.Description + ' ' + '(' + result.AcademicYear?.AcademicYearCode + ')' : null : null,
                            SchoolID = result.SchoolID,
                            SchoolName = result.SchoolID.HasValue ? result.School?.SchoolName : null,
                            CreatedBy = result.CreatedBy,
                            CreatedDate = result.CreatedDate,
                            UpdatedBy = result.UpdatedBy,
                            UpdatedDate = result.UpdatedDate,
                            OnlineExamResultQuestionMaps = resultQuestionMaps
                        };
                    }

                    return examResultDTO;
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                    return null;
                }
            }
        }

        public OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO)
        {
            using (var dbContext = new dbEduegateOnlineExamContext())
            {
                var message = new OperationResultDTO();

                try
                {
                    var entity = new OnlineExamResult()
                    {
                        OnlineExamResultIID = resultDTO.OnlineExamResultIID,
                        Marks = resultDTO.Marks,
                        CandidateID = resultDTO.CandidateID,
                        Remarks = resultDTO.Remarks,
                        OnlineExamID = resultDTO.OnlineExamID,
                        ResultStatusID = resultDTO.ResultStatusID,
                        AcademicYearID = resultDTO.AcademicYearID,
                        SchoolID = resultDTO.SchoolID,
                        CreatedBy = resultDTO.CreatedBy,
                        CreatedDate = resultDTO.CreatedDate,
                        UpdatedBy = (int)_context.LoginID,
                        UpdatedDate = DateTime.Now,
                    };

                    entity.OnlineExamResultQuestionMaps = new List<OnlineExamResultQuestionMap>();

                    foreach (var qnMap in resultDTO.OnlineExamResultQuestionMaps)
                    {
                        entity.OnlineExamResultQuestionMaps.Add(new OnlineExamResultQuestionMap()
                        {
                            OnlineExamResultQuestionMapIID = qnMap.OnlineExamResultQuestionMapIID,
                            OnlineExamResultID = qnMap.OnlineExamResultID,
                            QuestionID = qnMap.QuestionID,
                            Mark = qnMap.Mark,
                            Remarks = qnMap.Remarks,
                            EntryType = qnMap.EntryType,
                            CreatedBy = qnMap.CreatedBy,
                            CreatedDate = qnMap.CreatedDate,
                            UpdatedBy = (int)_context.LoginID,
                            UpdatedDate = DateTime.Now,
                        });
                    }

                    if (entity.OnlineExamResultIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var resultQnMap in entity.OnlineExamResultQuestionMaps)
                        {
                            if (resultQnMap.OnlineExamResultQuestionMapIID == 0)
                            {
                                dbContext.Entry(resultQnMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(resultQnMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }

                    dbContext.SaveChanges();

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

    }
}