using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class MarkPublishMapper : DTOEntityDynamicMapper
    {
        public static MarkPublishMapper Mapper(CallContext context)
        {
            var mapper = new MarkPublishMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<MarkRegisterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public string UpdateMarkEntryStatus(MarkRegisterDTO dto)
        {
            #region Declaration

            var toDto = dto;
            List<long> markregSubjectMapIDs = new List<long>();
            List<long> markregSkillMapIDs = new List<long>();

            #endregion Declaration

            #region Validate DTOs

            if (toDto.Class == null || toDto.Section == null || toDto.Exam == null)
            {
                throw new Exception("Please fill required fields!");
            }
            else if (toDto.MarkRegistersDetails == null || toDto.MarkRegistersDetails.Count == 0 || !toDto.MarkRegistersDetails[0].StudentID.HasValue)
            {
                throw new Exception("Please fill student details!");
            }
            #endregion Validate DTOs

            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            List<MarkRegisterSubjectMap> _markRegisterSubMap = new List<MarkRegisterSubjectMap>();
            List<MarkRegisterSkill> _markRegisterSkillMap = new List<MarkRegisterSkill>();
            if (toDto.MarkRegistersDetails.Any())
                {
                    toDto.MarkRegistersDetails.All(x =>
                    {
                        if (x.MarkRegisterSplitDTO.Any())
                        {
                            x.MarkRegisterSplitDTO.All(y =>
                            {
                                //using (var dbContext = new dbEduegateSchoolContext())
                                //{
                                //    var markregSubjectMap = dbContext.MarkRegisterSubjectMaps.Where(m => m.MarkRegisterSubjectMapIID == y.MarkRegisterSubjectMapIID).AsNoTracking().FirstOrDefault();
                                //    if (markregSubjectMap != null)
                                //    {
                                //        markregSubjectMap.MarkEntryStatusID = y.MarkEntryStatusID;
                                //        markregSubjectMap.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                                //        markregSubjectMap.UpdatedDate = DateTime.Now;

                                //        //dbContext.Entry(markregSubjectMap).State = EntityState.Modified;
                                //        //dbContext.SaveChanges();
                                //        _markRegisterSubMap.Add(markregSubjectMap);
                                //    }
                                //}

                                _markRegisterSubMap.Add(new MarkRegisterSubjectMap() { MarkRegisterSubjectMapIID = y.MarkRegisterSubjectMapIID, 
                                    MarkEntryStatusID = y.MarkEntryStatusID , 
                                    UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null,
                                    UpdatedDate = DateTime.Now
                                });

                                return true;
                            });
                        }

                        if (x.MarkRegisterSkillGroupDTO.Any())
                        {
                            x.MarkRegisterSkillGroupDTO.All(z =>
                            {
                                if (z.MarkRegisterSkillsDTO.Any())
                                {
                                    z.MarkRegisterSkillsDTO.All(y =>
                                    {
                                        //using (var dbContext = new dbEduegateSchoolContext())
                                        //{
                                        //    //var markRegisterSkill = repositoryMarkRegisterSkill.GetById(y.MarkRegisterSkillIID);
                                        //    var markRegisterSkill = dbContext.MarkRegisterSkills.Where(m => m.MarkRegisterSkillIID == y.MarkRegisterSkillIID).AsNoTracking().FirstOrDefault();
                                        //    if (markRegisterSkill != null)
                                        //    {
                                        //        markRegisterSkill.MarkEntryStatusID = y.MarkEntryStatusID;
                                        //        markRegisterSkill.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                                        //        markRegisterSkill.UpdatedDate = DateTime.Now;

                                        //        //dbContext.Entry(markRegisterSkill).State = EntityState.Modified;
                                        //        //dbContext.SaveChanges();
                                        //        _markRegisterSkillMap.Add(markRegisterSkill);
                                        //    }
                                        //}

                                        _markRegisterSkillMap.Add(new MarkRegisterSkill()
                                        {
                                            MarkRegisterSkillIID = y.MarkRegisterSkillIID,
                                            MarkEntryStatusID = y.MarkEntryStatusID,
                                            UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null,
                                            UpdatedDate = DateTime.Now
                                        });

                                        return true;
                                    });
                                }

                                return true;
                            });

                        }

                        return true;
                    });
                }
            //  }

            if (_markRegisterSubMap.Any())
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var markregSubjectMap = dbContext.MarkRegisterSubjectMaps.Where(m => _markRegisterSubMap.Select(a => a.MarkRegisterSubjectMapIID).Contains(m.MarkRegisterSubjectMapIID)).AsNoTracking().ToList();
                    if (markregSubjectMap.Any())
                    {
                        foreach (var item in markregSubjectMap)
                        {
                            var dFound = _markRegisterSubMap.Find(w => w.MarkRegisterSubjectMapIID == item.MarkRegisterSubjectMapIID);
                            if (dFound != null)
                            {
                                item.MarkEntryStatusID = dFound.MarkEntryStatusID;
                                item.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                                item.UpdatedDate = DateTime.Now;

                                dbContext.Entry(item).State = EntityState.Modified;
                            }

                        }
                        dbContext.SaveChanges();

                        //markregSubjectMap.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                        //markregSubjectMap.UpdatedDate = DateTime.Now;

                        //dbContext.Entry(markregSubjectMap).State = EntityState.Modified;
                        //dbContext.SaveChanges();
                        //_markRegisterSubMap.Add(markregSubjectMap);
                    }
                }

                if (_markRegisterSkillMap.Any())
                {
                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        var markregSkillMap = dbContext.MarkRegisterSkills.Where(m => _markRegisterSkillMap.Select(a => a.MarkRegisterSkillIID).Contains(m.MarkRegisterSkillIID)).AsNoTracking().ToList();
                        if (markregSkillMap.Any())
                        {
                            foreach (var item in markregSkillMap)
                            {
                                var dFound = _markRegisterSkillMap.Find(w => w.MarkRegisterSkillIID == item.MarkRegisterSkillIID);
                                if (dFound != null)
                                {
                                    item.MarkEntryStatusID = dFound.MarkEntryStatusID;
                                    item.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                                    item.UpdatedDate = DateTime.Now;

                                    dbContext.Entry(item).State = EntityState.Modified;
                                }

                            }
                            dbContext.SaveChanges();

                            //markregSubjectMap.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                            //markregSubjectMap.UpdatedDate = DateTime.Now;

                            //dbContext.Entry(markregSubjectMap).State = EntityState.Modified;
                            //dbContext.SaveChanges();
                            //_markRegisterSubMap.Add(markregSubjectMap);
                        }
                    }
                }
            }

            return ToDTOString(toDto);
        }

        public List<KeyValueDTO> GetExamsByClassAndGroup(int classID, int? sectionID, int? examGroupID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var examList = new List<KeyValueDTO>();

                examList = (from s in dbContext.ExamClassMaps
                            where s.Exam.IsActive == true && s.Exam.ExamGroupID == examGroupID && s.Exam.AcademicYearID == academicYearID && s.ClassID == classID && (sectionID == null || s.SectionID == sectionID)
                            select new KeyValueDTO
                            {
                                Key = s.ExamID.ToString(),
                                Value = s.Exam.ExamDescription
                            }).AsNoTracking().ToList();

                return examList;
            }
        }

        public List<KeyValueDTO> GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var skillList = new List<KeyValueDTO>();

                skillList = (from map in dbContext.ExamSkillMaps
                             join skillGrpSkill in dbContext.ClassSubjectSkillGroupSkillMaps on map.ClassSubjectSkillGroupMapID equals skillGrpSkill.ClassSubjectSkillGroupMapID
                             join skill in dbContext.SkillMasters on skillGrpSkill.SkillMasterID equals skill.SkillMasterID
                             //join skillGrp in dbContext.SkillGroupMasters on map.SkillGroupMasterID equals skillGrp.SkillGroupMasterID
                             //join skill in dbContext.SkillMasters on map.SkillGroupMasterID equals skill.SkillGroupMasterID
                             join clsMap in dbContext.ExamClassMaps on map.ExamID equals clsMap.ExamID
                             where map.ExamID == examID && map.Exam.AcademicYearID == academicYearID
                             && map.Exam.ExamGroupID == termID && clsMap.ClassID == classID &&
                             (sectionID == null || clsMap.SectionID == sectionID)
                             select new KeyValueDTO
                             {
                                 Key = skill.SkillMasterID.ToString(),
                                 Value = skill.SkillName
                             }).AsNoTracking().ToList();

                return skillList;
            }
        }

        private List<long> GetStudentsFeePending(List<long> _sStudents, int _sAcademicYearID )
        {
            List<long>_sRetData= new List<long>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                List<int> _sAcademicYearIDs = new List<int>();
                DateTime _sEdate = DateTime.Now;
                var _sACD= dbContext.AcademicYears.Where(w => w.AcademicYearID == _sAcademicYearID);
                if (_sACD.Any())
                {
                    _sEdate = _sACD.Max(w => w.EndDate) ?? DateTime.Now;
                    _sAcademicYearIDs= dbContext.AcademicYears.Where(w => w.EndDate <= _sEdate).Select(w=> w.AcademicYearID).ToList();
                }

                string searchQuery = "select * from schools.VWS_Fee_Outstanding_Current where AcadamicYearID in(" + string.Join(",",_sAcademicYearIDs) +") and  FeeCycleID=1 AND Fee_Bal>0 and StudentIID in(" + string.Join(",", _sStudents) +")";
                var feeDueList = dbContext.VWS_Fee_Outstanding_Currents.FromSqlRaw($@"{searchQuery}").AsNoTracking().ToList();
                if (feeDueList.Any())
                {
                    _sRetData.AddRange(feeDueList.Select(w => w.StudentIID));
                }
            }
            return _sRetData;
        }


        public List<MarkRegisterDetailsDTO> GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            List<MarkRegisterDetailsDTO> sRetData = new List<MarkRegisterDetailsDTO>();

            using (var sContext = new dbEduegateSchoolContext())
            {
                var examDraftStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("MARK_ENTRY_STATUS_DRAFT", 1);

                var dFndexam = (from n in sContext.Exams
                                join ec in sContext.ExamClassMaps on n.ExamIID equals ec.ExamID
                                join eb in sContext.ExamSubjectMaps on n.ExamIID equals eb.ExamID
                                where (n.ExamIID == markEntrySearchArgsDTO.ExamID
                                  && n.ExamGroupID == markEntrySearchArgsDTO.TermID
                                  && n.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                  && ec.ClassID == markEntrySearchArgsDTO.ClassID &&
                                  ec.SectionID == markEntrySearchArgsDTO.SectionID)
                                select eb).AsNoTracking().Include(x => x.Subject);

                //var dFndSkills = (from n in sContext.ClassSubjectSkillGroupSkillMaps
                //                  where (n.ClassSubjectSkillGroupMapID == markEntrySearchArgsDTO.SkillSetID
                //                  && n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                //                  && markEntrySearchArgsDTO.SkillID == null || n.SkillGroupMasterID == markEntrySearchArgsDTO.SkillGroupID
                //                  && n.SkillMasterID == markEntrySearchArgsDTO.SkillID
                //                  )
                //                  select new SkillSData
                //                  {
                //                      SkillGroupMasterID = n.SkillGroupMasterID,
                //                      SkillGroup = n.SkillGroupMaster.SkillGroup,
                //                      SkillMasterID = n.SkillMasterID,
                //                      Skill = n.SkillMaster.SkillName,
                //                      MarkGradeID = n.MarkGradeID
                //                  });

                var dFndSkills = (from map in sContext.ExamSkillMaps
                                  join skillGrp in sContext.ClassSubjectSkillGroupSkillMaps on map.ClassSubjectSkillGroupMapID equals skillGrp.ClassSubjectSkillGroupMapID
                                  join skill in sContext.SkillMasters on skillGrp.SkillMasterID equals skill.SkillMasterID
                                  join clsMap in sContext.ExamClassMaps on map.ExamID equals clsMap.ExamID
                                  where map.ExamID == markEntrySearchArgsDTO.ExamID && map.Exam.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                  && map.Exam.ExamGroupID == markEntrySearchArgsDTO.TermID && clsMap.ClassID == markEntrySearchArgsDTO.ClassID &&
                                  (markEntrySearchArgsDTO.SectionID == null || clsMap.SectionID == markEntrySearchArgsDTO.SectionID)
                                  select new SkillSDataDTO
                                  {
                                      SkillGroupMasterID = skillGrp.SkillGroupMasterID,
                                      SkillGroup = skillGrp.SkillGroupMaster.SkillGroup,
                                      SkillMasterID = skillGrp.SkillMasterID,
                                      Skill = skillGrp.SkillMaster.SkillName,
                                      MarkGradeID = skillGrp.MarkGradeID
                                  }).AsNoTracking();

                List<long> _sStudentList = new List<long>();
                if (dFndexam.Any() || dFndSkills.Any())
                {

                    
                    var dPromoStudent= sContext.StudentPromotionLogs.Where(w => w.ShiftFromClassID == markEntrySearchArgsDTO.ClassID && w.ShiftFromSectionID == markEntrySearchArgsDTO.SectionID && w.ShiftFromAcademicYearID == markEntrySearchArgsDTO.AcademicYearID);
                    if (dPromoStudent.Any())
                        _sStudentList.AddRange(dPromoStudent.Select(w => w.StudentID));
                    var dCurrent = sContext.Students.Where(w => w.ClassID == markEntrySearchArgsDTO.ClassID && w.SectionID == markEntrySearchArgsDTO.SectionID && w.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID && w.IsActive == true && !_sStudentList.Contains(w.StudentIID));
                    if(dCurrent.Any())
                        _sStudentList.AddRange(dCurrent.Select(w => w.StudentIID));

                    var dFnd = (from n in sContext.Students
                                where _sStudentList.Contains( n.StudentIID )
                                select new
                                {
                                    StudentID = n.StudentIID,
                                    StudentName = n.FirstName + " " + n.MiddleName + " " + n.LastName,
                                    EnrolNo = n.AdmissionNumber
                                }).AsNoTracking().ToList();



                    if (dFnd.Any())
                    {
                        var _prgrssRprt = (from prgrsRprt in sContext.ProgressReports
                                           join prgsSts in sContext.ProgressReportPublishStatuses on
                                           prgrsRprt.PublishStatusID equals prgsSts.ProgressReportPublishStatusID
                                           where prgrsRprt.ClassID == markEntrySearchArgsDTO.ClassID
                                           && prgrsRprt.SectionID == markEntrySearchArgsDTO.SectionID
                                           && prgrsRprt.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                           && prgrsRprt.ExamGroupID == markEntrySearchArgsDTO.TermID && prgrsRprt.ExamID == markEntrySearchArgsDTO.ExamID
                                           select new ProgressReportDTO()
                                           {
                                               StudentID = prgrsRprt.StudentId,
                                               Student = new KeyValueDTO()
                                               {
                                                   Key = prgrsRprt.Student.StudentIID.ToString(),
                                                   Value = (prgrsRprt.Student.AdmissionNumber + '_' + prgrsRprt.Student.FirstName +' ' + prgrsRprt.Student.MiddleName + ' ' + prgrsRprt.Student.LastName)
                                               },
                                               PublishStatusID = prgsSts.ProgressReportPublishStatusID,
                                               PublishStatusName = prgsSts.StatusName
                                           }).AsNoTracking().ToList();

                        var dfndMarkReg = (from markregsub in sContext.MarkRegisterSubjectMaps
                                           join markreg in sContext.MarkRegisters on
                                           markregsub.MarkRegisterID equals markreg.MarkRegisterIID
                                           where markreg.ClassID == markEntrySearchArgsDTO.ClassID
                                           && markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                           && markreg.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                           && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                           select new StudentMarkEntryDTO()
                                           {
                                               StudentID = markreg.StudentId.Value,
                                               SubjectID = markregsub.SubjectID,
                                               MarksObtained = markregsub.Mark,
                                               GradeID = markregsub.MarksGradeMapID,
                                               PresentStatusID = markreg.PresentStatusID,
                                               MarkRegisterID = markreg.MarkRegisterIID,
                                               MarkRegisterSubjectMapID = markregsub.MarkRegisterSubjectMapIID,
                                               SubjectName = markregsub.Subject.SubjectName,
                                               MarkEntryStatusID = markregsub.MarkEntryStatusID.HasValue ? markregsub.MarkEntryStatusID : examDraftStatusID,
                                               MarkEntryStatusName = markregsub.MarkEntryStatus != null ? markregsub.MarkEntryStatus.MarkEntryStatusName : null,
                                               TotalMark = markregsub.TotalMark,
                                           }).AsNoTracking();

                        var dfndMarkRegSkl = (from markregsub in sContext.MarkRegisterSkills
                                              join markreg in sContext.MarkRegisters on markregsub.MarkRegisterSkillGroup.MarkRegisterID
                                              equals markreg.MarkRegisterIID
                                              where markreg.ClassID == markEntrySearchArgsDTO.ClassID && markreg.SectionID == markEntrySearchArgsDTO.SectionID
                                              && markreg.ExamGroupID == markEntrySearchArgsDTO.TermID && markreg.ExamID == markEntrySearchArgsDTO.ExamID
                                              select new StudentSkillsDTO()
                                              {
                                                  StudentID = markreg.StudentId.Value,
                                                  SkillGroupMasterID = markregsub.SkillGroupMasterID,
                                                  SkillMasterID = markregsub.SkillMasterID,
                                                  MarksObtained = markregsub.MarksObtained,
                                                  GradeID = markregsub.MarksGradeMapID,
                                                  GradeName = markregsub.MarksGradeMapID.HasValue ? markregsub.MarkGradeMap.GradeName + " - " + markregsub.MarkGradeMap.Description : null,
                                                  PresentStatusID = markreg.PresentStatusID,
                                                  MarkRegisterSkillGroupID = markregsub.MarkRegisterSkillGroupID,
                                                  MarkRegisterSkillID = markregsub.MarkRegisterSkillIID,
                                                  MarkRegisterID = markregsub.MarkRegisterSkillGroup.MarkRegisterID,
                                                  MarkEntryStatusID = markregsub.MarkEntryStatusID,
                                                  MarkEntryStatusName = markregsub.MarkEntryStatus != null ? markregsub.MarkEntryStatus.MarkEntryStatusName : null,
                                              }).AsNoTracking();

                        var _sStudentFee = GetStudentsFeePending(_sStudentList,markEntrySearchArgsDTO.AcademicYearID??0);

                        dFnd.All(w =>
                        {
                            var studentMarkEntryDTO = new List<StudentMarkEntryDTO>();
                            var studentSkillsDTO = new List<StudentSkillsDTO>();

                            if (dfndMarkReg.Any())
                            {
                                studentMarkEntryDTO = dfndMarkReg.Where(s => s.StudentID == w.StudentID).ToList();
                            }
                            if (dfndMarkRegSkl.Any())
                            {
                                studentSkillsDTO = dfndMarkRegSkl.Where(s => s.StudentID == w.StudentID).ToList();
                            }

                            //var feeDueList = sContext.StudentFeeDues.Where(sf => sf.StudentId == w.StudentID && sf.CollectionStatus == false).AsNoTracking().ToList();
                            //string feeDefaulterStatus = feeDueList != null ? feeDueList.Count > 0 ? "The fee is due for this student" : null : null;

                            //var markRegDetail = sContext.MarkRegisters.Where(mrk => mrk.ExamID == markEntrySearchArgsDTO.ExamID && mrk.Exam.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                            //      && mrk.Exam.ExamGroupID == markEntrySearchArgsDTO.TermID && mrk.ClassID == markEntrySearchArgsDTO.ClassID && mrk.SectionID == markEntrySearchArgsDTO.SectionID
                            //      && mrk.StudentId == w.StudentID).AsNoTracking().FirstOrDefault();

                            //sRetData.Add(GetStudentData(w.EnrolNo, w.StudentID, w.StudentName,
                            //                                              dFndSkills.ToList(), studentSkillsDTO, studentMarkEntryDTO, examDraftStatusID, feeDefaulterStatus));

                            //var feeDueList = sContext.StudentFeeDues.Where(sf => sf.StudentId == w.StudentID && sf.CollectionStatus == false).AsNoTracking().ToList();

                            string feeDefaulterStatus = String.Empty;
                            if (_sStudentFee.Contains(w.StudentID))
                            {
                                feeDefaulterStatus = "The fee is due for this student";
                            }

                            var reportContentID = GetPreviousPublishedReports(w.StudentID, markEntrySearchArgsDTO.AcademicYearID ?? 0, markEntrySearchArgsDTO.ClassID ?? 0, markEntrySearchArgsDTO.SectionID ?? 0, markEntrySearchArgsDTO.TermID, markEntrySearchArgsDTO.ExamID);

                            var markRegDetail = sContext.MarkRegisters.Where(mrk => mrk.ExamID == markEntrySearchArgsDTO.ExamID && mrk.Exam.AcademicYearID == markEntrySearchArgsDTO.AcademicYearID
                                      && mrk.Exam.ExamGroupID == markEntrySearchArgsDTO.TermID && mrk.ClassID == markEntrySearchArgsDTO.ClassID && mrk.SectionID == markEntrySearchArgsDTO.SectionID
                                      && mrk.StudentId == w.StudentID).AsNoTracking().FirstOrDefault();

                                sRetData.Add(GetStudentData(w.EnrolNo, w.StudentID, w.StudentName,
                                                                              dFndSkills.ToList(), studentSkillsDTO, studentMarkEntryDTO, examDraftStatusID, feeDefaulterStatus, reportContentID));
                            return true;
                        });
                    }
                }

            }

            return sRetData;
        }

        private MarkRegisterDetailsDTO GetStudentData(string EnrolNo, long StudentID, string StudentName, List<SkillSDataDTO> skill, List<StudentSkillsDTO> markRegData, List<StudentMarkEntryDTO> studentMarkEntryDTO, byte examDraftStatusID, string feeDefaultStatus, long reportContentID)
        {
            var markRegisterSkillGroupDTO = GetSkillGroupData(skill, markRegData);
            var markRegisterSplitDTO = new List<MarkRegisterDetailsSplitDTO>();

            if (studentMarkEntryDTO.Any())
            {
                studentMarkEntryDTO.All(w =>
                {
                    markRegisterSplitDTO.Add(new MarkRegisterDetailsSplitDTO()
                    {
                        Mark = w.MarksObtained,
                        MarkRegisterID = w.MarkRegisterID,
                        MarksGradeMapID = w.GradeID,
                        MarkRegisterSubjectMapIID = w.MarkRegisterSubjectMapID.Value,
                        SubjectID = w.SubjectID,
                        Subject = w.SubjectName,
                        MarkEntryStatusID = w.MarkEntryStatusID,
                        MarkEntryStatusName = w.MarkEntryStatusName,
                        TotalMark = w.TotalMark,
                    });

                    return true;
                });
            }

            byte? entryStatusID = examDraftStatusID;
            string entryStatusName = null;

            if (studentMarkEntryDTO != null && studentMarkEntryDTO.Count > 0)
            {
                entryStatusID = studentMarkEntryDTO[0].MarkEntryStatusID;
                entryStatusName = studentMarkEntryDTO[0].MarkEntryStatusName;
            }
            else if(markRegData != null && markRegData.Count > 0)
            {
                entryStatusID = markRegData[0].MarkEntryStatusID;
                entryStatusName = markRegData[0].MarkEntryStatusName;
            }

            var sRetData = new MarkRegisterDetailsDTO()
            {
                StudentID = StudentID,
                Student = new KeyValueDTO() { Key = StudentID.ToString(), Value = StudentName },
                AdmissionNumber = EnrolNo,
                MarkRegisterSkillGroupDTO = markRegisterSkillGroupDTO,
                MarkRegisterSplitDTO = markRegisterSplitDTO,
                MarkStatusID = markRegData.Select(x => x.PresentStatusID).FirstOrDefault(),
                MarkEntryStatusID = entryStatusID,
                MarkEntryStatusName = entryStatusName,
                FeeDefaulterStatus = feeDefaultStatus,
                ReportContentID = reportContentID
            };

            return sRetData;
        }

        private List<MarkRegisterSkillGroupDTO> GetSkillGroupData(List<SkillSDataDTO> skill, List<StudentSkillsDTO> markRegData)
        {
            var sRetData = (from s in skill
                            select new MarkRegisterSkillGroupDTO
                            {
                                SkillGroupMasterID = s.SkillGroupMasterID,
                                SkillGroup = s.SkillGroup,
                                MarkEntryStatusID = s.MarkEntryStatusID,
                                MarkRegisterSkillsDTO = (from sk in skill
                                                         select new MarkRegisterSkillsDTO
                                                         {
                                                             SkillGroupMasterID = s.SkillGroupMasterID,
                                                             SkillGroup = s.SkillGroup,
                                                             SkillMasterID = sk.SkillMasterID,
                                                             Skill = sk.Skill,
                                                             MarkEntryStatusID = sk.MarkEntryStatusID,
                                                             MarksObtained = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                 && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarksObtained).FirstOrDefault() : (decimal?)null,
                                                             MarksGradeMapID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                  && x.SkillMasterID == sk.SkillMasterID).Select(y => y.GradeID).FirstOrDefault() : (long?)null,
                                                             MarkGradeMap = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                  && x.SkillMasterID == sk.SkillMasterID).Select(y => y.GradeName).FirstOrDefault() : null,

                                                             MarkRegisterSkillGroupID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                      && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillGroupID).FirstOrDefault() : (long?)null,
                                                             MarkRegisterSkillIID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterSkillID ?? 0).FirstOrDefault() : 0,

                                                             MarkRegisterID = markRegData.Count > 0 ? markRegData.Where(x => x.SkillGroupMasterID == sk.SkillGroupMasterID
                                                                     && x.SkillMasterID == sk.SkillMasterID).Select(y => y.MarkRegisterID).FirstOrDefault() : (long?)null,
                                                         }).ToList()
                            }).ToList();

            return sRetData;
        }

        private long GetPreviousPublishedReports(long studentID, int _sAcademicYearID, int classID, int sectionID, long termID, long examID)
        {
            long _rprtData = new long();
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var datas = dbContext.ProgressReports.Where(a => a.StudentId == studentID && a.AcademicYearID == _sAcademicYearID && a.ClassID == classID && a.SectionID == sectionID && a.ExamGroupID == termID && a.ExamID == examID).AsNoTracking().FirstOrDefault();

                if (datas != null)
                {
                    _rprtData = datas.ReportContentID ?? 0;
                }
            }
            return _rprtData;
        }
    }
}