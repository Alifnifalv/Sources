using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Eduegate.Domain.Entity.Contents;

namespace Eduegate.Domain.Mappers.School.Exams
{
    public class ProgressReportMapper : DTOEntityDynamicMapper
    {
        public static ProgressReportMapper Mapper(CallContext context)
        {
            var mapper = new ProgressReportMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ProgressReportNewDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ProgressReportDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.ProgressReports.Where(p => p.ProgressReportIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.Exam)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private ProgressReportDTO ToDTO(ProgressReport entity)
        {
            var progressReportDTO = new ProgressReportDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                string contentFileName = null;

                if (entity.ReportContentID.HasValue)
                {
                    var contentFileData = dbContext.ContentFiles.Where(c => c.ContentFileIID == entity.ReportContentID).AsNoTracking().FirstOrDefault();
                    contentFileName = contentFileData?.ContentFileName;
                }

                progressReportDTO = new ProgressReportDTO()
                {
                    ProgressReportIID = entity.ProgressReportIID,
                    StudentID = entity.StudentId,
                    Student = entity.StudentId.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.StudentId.ToString(),
                        Value = entity.Student?.AdmissionNumber + " - " + entity.Student?.FirstName + (entity.Student?.MiddleName != null ? " " + entity.Student?.MiddleName + " " : " " ) + entity.Student?.LastName,
                    } : new KeyValueDTO(),
                    ClassID = entity.ClassID,
                    Class = entity.ClassID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ClassID.ToString(),
                        Value = entity.Class?.ClassDescription,
                    } : new KeyValueDTO(),
                    SectionID = entity.SectionID,
                    Section = entity.SectionID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.SectionID.ToString(),
                        Value = entity.Section?.SectionName,
                    } : new KeyValueDTO(),
                    ExamGroupID = entity.ExamGroupID,
                    ExamGroup = entity.ExamGroupID.HasValue ? entity.ExamGroup?.ExamGroupName : null,
                    ExamID = entity.ExamID,
                    ExamName = entity.ExamID.HasValue ? entity.Exam?.ExamDescription : null,
                    SchoolID = entity.SchoolID,
                    SchoolName = entity.SchoolID.HasValue ? entity.School?.SchoolName : null,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYear = entity.AcademicYearID.HasValue ? entity.AcademicYear != null ? entity.AcademicYear?.Description + "(" + entity.AcademicYear?.AcademicYearCode + ")" : null : null,
                    ReportContentID = entity.ReportContentID,
                    ReportContentFileName = contentFileName,
                };
            }

            return progressReportDTO;
        }


        #region Progress report
        public List<ProgressReportNewDTO> GetStudentProgressReportData(long? StudentID, int classID, int? sectionID, int academicYearID)
        {
            List<ProgressReportNewDTO> sRetData = new List<ProgressReportNewDTO>();

            var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("PROGRESS_REPORT_PUBLISH_STATUS_PB", 2);

            using (var sContext = new dbEduegateSchoolContext())
            {
                var reportData = (from n in sContext.ProgressReports
                                      where (n.AcademicYearID == academicYearID
                                      && n.StudentId == StudentID && n.PublishStatusID == publishStatusID
                                      )
                                      select new ProgressReportNewDTO
                                      {
                                          AcademicYearID = n.AcademicYearID,
                                          StudentId = n.StudentId,
                                          ClassID = n.ClassID,
                                          ReportContentID = n.ReportContentID,
                                          ContentFileName = n.ExamGroupID.HasValue ? "Interim Report Card "+ n.ExamGroup.ExamGroupName : "Report Card Final",
                                      }).AsNoTracking().ToList();
                var dFndReportData = GetReportContentData(reportData);
                return dFndReportData;
            }

        }

        public List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            List<ProgressReportNewDTO> sRetData = new List<ProgressReportNewDTO>();
            //return sRetData;
            //using (var sContext = new dbEduegateSchoolContext())
            //{

            //    var dFndStudents = (
            //                      from n in sContext.Students
            //                      join a in sContext.AcademicYears on n.SchoolAcademicyearID equals a.AcademicYearID
            //                      where n.AcademicYearID == academicYearID && n.ClassID == classID && n.SectionID == sectionID
            //                      && n.IsActive == true && a.AcademicYearStatusID != 3
            //                      select new StudentDTO
            //                      {
            //                          StudentIID = n.StudentIID,
            //                          StudentFullName = n.AdmissionNumber + "-" + n.FirstName + " " + n.MiddleName + " " + n.LastName,
            //                          SectionID = n.SectionID

            //                      }).ToList();

            //    List<long> studentIDS = dFndStudents.Select(x => (x.StudentIID)).ToList();

            //    if (dFndStudents.Any())
            //    {

            //        var dFndReportData = (from n in sContext.ProgressReports
            //                                  //join cf in sContext.ContentFiles on n.ReportContentID equals cf.ContentFileIID
            //                              where (n.AcademicYearID == academicYearID
            //                              && n.ClassID == classID &&
            //                              studentIDS.Contains(n.StudentId.Value)
            //                                   )
            //                              select n)
            //      .Include(x => x.ProgressReportPublishStatus);
            //        var listContentIds = dFndReportData.Select(x => x.ReportContentID).ToList();
            //        var dFndContentData = sContext.ContentFiles.Where(x => listContentIds.Contains(x.ContentFileIID));

            //        dFndStudents.All(w =>
            //        {

            //            var reportData = dFndReportData.Where(x => x.StudentId == w.StudentIID).FirstOrDefault();
            //            var contentData = dFndContentData.Where(x => x.ContentFileIID == reportData.ReportContentID).FirstOrDefault();
            //            sRetData.Add(GetReportData(w, reportData, contentData));
            //            return true;
            //        });

            //    }

            //}

            return sRetData;
        }

        private ProgressReportNewDTO GetReportData(StudentDTO student, ProgressReport reportData, Entity.School.Models.ContentFile contentData)
        {
            var sRetData = new ProgressReportNewDTO()
            {
                ClassID = student.ClassID.Value,
                StudentId = student.StudentIID,
                SectionID = student.SectionID.HasValue ? reportData.SectionID : null,
                SchoolID = student.SchoolID == null ? (byte)_context.SchoolID : student.SchoolID,
                AcademicYearID = student.AcademicYearID == null ? (int)_context.AcademicYearID : student.AcademicYearID,
                PublishStatusID = reportData.PublishStatusID.HasValue ? reportData.PublishStatusID.Value : (byte?)null,
                CreatedBy = reportData.ProgressReportIID == 0 ? (int)_context.LoginID : reportData.CreatedBy,
                UpdatedBy = reportData.ProgressReportIID > 0 ? (int)_context.LoginID : reportData.UpdatedBy,
                CreatedDate = reportData.ProgressReportIID == 0 ? DateTime.Now : reportData.CreatedDate,
                UpdatedDate = reportData.ProgressReportIID > 0 ? DateTime.Now : reportData.UpdatedDate,
                ReportContentID = reportData.ReportContentID,
                ProgressReportIID = reportData.ProgressReportIID,
                PublishStatus = new KeyValueDTO
                {
                    Key = reportData.PublishStatusID.ToString(),
                    Value = reportData.ProgressReportPublishStatus.StatusName
                },
                StudentFullName = student.StudentFullName,
                ContentData = contentData.ContentData,
                ContentFileName = contentData.ContentFileName
            };

            return sRetData;
        }

        public List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            #region Map Entities
            var reportList = new List<ProgressReportNewDTO>();
            var entities = new List<ProgressReport>();
            if (toDtoList.Any())
            {
                toDtoList.All(w =>
                {

                    entities = (from s in toDtoList
                                select new ProgressReport()
                                {
                                    ClassID = s.ClassID.Value,
                                    StudentId = s.StudentId,
                                    SectionID = s.SectionID != null ? s.SectionID : null,
                                    SchoolID = s.SchoolID == null ? (byte)_context.SchoolID : s.SchoolID,
                                    AcademicYearID = s.AcademicYearID == null ? (int)_context.AcademicYearID : s.AcademicYearID,
                                    PublishStatusID = s.PublishStatusID.HasValue ? s.PublishStatusID.Value : (byte?)null,
                                    CreatedBy = (int)_context.LoginID,
                                    UpdatedBy = (int)_context.LoginID,
                                    CreatedDate = DateTime.Now,
                                    UpdatedDate = DateTime.Now,
                                    ReportContentID = s.ReportContentID,
                                    ProgressReportIID = s.ProgressReportIID
                                }).ToList();


                    return true;
                });
            }
            #endregion Map Entities

            #region DB Updates

            using (var dbContext = new dbEduegateSchoolContext())
            {
                // Add or Modify entities
                #region Save Entities
                if (entities.Any())
                {
                    entities.All(entity =>
                    {
                        dbContext.ProgressReports.Add(entity);

                        if (entity.ProgressReportIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }


                        return true;
                    });

                }
                dbContext.SaveChanges();
                #endregion Save Entities
            }

            #endregion DB Updates
            #region DTO Updates

            if (entities.Any())
            {
                entities.All(w =>
                {

                    entities = (from s in toDtoList
                                select new ProgressReport()
                                {
                                    ClassID = s.ClassID.Value,
                                    StudentId = s.StudentId,
                                    SectionID = s.SectionID != null ? s.SectionID : null,
                                    SchoolID = s.SchoolID == null ? (byte)_context.SchoolID : s.SchoolID,
                                    AcademicYearID = s.AcademicYearID == null ? (int)_context.AcademicYearID : s.AcademicYearID,
                                    PublishStatusID = s.PublishStatusID.HasValue ? s.PublishStatusID.Value : (byte?)null,
                                    CreatedBy = s.ProgressReportIID == 0 ? (int)_context.LoginID : s.CreatedBy,
                                    UpdatedBy = s.ProgressReportIID > 0 ? (int)_context.LoginID : s.UpdatedBy,
                                    CreatedDate = s.ProgressReportIID == 0 ? DateTime.Now : s.CreatedDate,
                                    UpdatedDate = s.ProgressReportIID > 0 ? DateTime.Now : s.UpdatedDate,
                                    ReportContentID = s.ReportContentID,
                                    ProgressReportIID = s.ProgressReportIID
                                }).ToList();


                    return true;
                });
            }
            #endregion
            return reportList;
        }

        public string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDtoList.Any())
                {
                    toDtoList.All(x =>
                    {
                        var progressReport = dbContext.ProgressReports.Where(p => p.ProgressReportIID == x.ProgressReportIID).AsNoTracking().FirstOrDefault();
                        if (progressReport != null)
                        {
                            progressReport.PublishStatusID = x.PublishStatusID;

                            dbContext.Entry(progressReport).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.SaveChanges();
                        }
                        return true;

                    });
                }
            }
            return null;
        }
        #endregion old data

        public bool InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs, List<SettingDTO> settings)
        {
            var returnMsg = false;

            try
            {
                var progressReportPublishedStatusSetting = settings != null && settings.Count > 0 ? settings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_PUBLISH_STATUS_PB") : null;
                byte publishStatusID = progressReportPublishedStatusSetting != null && !string.IsNullOrEmpty(progressReportPublishedStatusSetting.SettingValue) ? byte.Parse(progressReportPublishedStatusSetting.SettingValue) : (byte)2;

                var generatedStatusID = new Domain.Setting.SettingBL().GetSettingValue<byte>("PROGRESS_REPORT_PUBLISH_STATUS_GN");

                var markEntryPublishedStatusID = new Domain.Setting.SettingBL().GetSettingValue<byte>("MARK_ENTRY_STATUS_PUBLISHED");
                var markEntryGeneratedStatusID = new Domain.Setting.SettingBL().GetSettingValue<byte>("MARK_ENTRY_STATUS_GENERATED");

                foreach (var toDto in progressReportListDTOs)
                {
                    long? oldReportContentID = null;

                    var entity = new ProgressReport()
                    {
                        ProgressReportIID = 0,
                        StudentId = toDto.StudentID,
                        ClassID = toDto.ClassID,
                        SectionID = toDto.SectionID,
                        SchoolID = toDto.SchoolID,
                        AcademicYearID = toDto.AcademicYearID,
                        ExamID = toDto.ExamID,
                        ExamGroupID = toDto.ExamGroupID == 0 ? null : toDto.ExamGroupID,
                        ReportContentID = toDto.ReportContentID,
                        PublishStatusID = toDto.PublishStatusID,
                    };

                    using (var dbContext1 = new dbEduegateSchoolContext())
                    {
                        var oldReportEntry = dbContext1.ProgressReports.Where(p => p.StudentId == toDto.StudentID && p.ClassID == toDto.ClassID && p.SectionID == toDto.SectionID
                        && p.SchoolID == toDto.SchoolID && p.AcademicYearID == toDto.AcademicYearID && p.ExamID == toDto.ExamID && (toDto.ExamGroupID != 0 ? p.ExamGroupID == toDto.ExamGroupID : p.ExamGroupID == null)).AsNoTracking().FirstOrDefault();

                        if (oldReportEntry != null)
                        {
                            oldReportContentID = oldReportEntry.ReportContentID;

                            entity.ProgressReportIID = oldReportEntry.ProgressReportIID;
                            entity.CreatedBy = oldReportEntry.CreatedBy;
                            entity.CreatedDate = oldReportEntry.CreatedDate;
                            entity.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                            entity.UpdatedDate = DateTime.Now;
                        }
                    }

                    if (entity.ProgressReportIID == 0)
                    {
                        entity.CreatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                        entity.CreatedDate = DateTime.Now;
                    }

                    using (var dbContext = new dbEduegateSchoolContext())
                    {
                        if (entity.ProgressReportIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        dbContext.SaveChanges();

                        returnMsg = true;
                    }

                    if (oldReportContentID.HasValue)
                    {
                        using (var dbContext2 = new dbEduegateSchoolContext())
                        {
                            var contentFileData = dbContext2.ContentFiles.Where(c => c.ContentFileIID == oldReportContentID).AsNoTracking().FirstOrDefault();

                            if (contentFileData != null)
                            {
                                dbContext2.ContentFiles.Remove(contentFileData);
                                dbContext2.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                returnMsg = false;
            }

            return returnMsg;
        }

        public List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO toDTO)
        {
            var reportsList = new List<ProgressReportDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("PROGRESS_REPORT_PUBLISH_STATUS_PB", 2);

                var progressReportDatas = dbContext.ProgressReports.Where(p => p.StudentId == toDTO.StudentID && p.ExamID == toDTO.ExamID &&
                p.ExamGroupID == toDTO.ExamGroupID && p.ClassID == toDTO.ClassID && p.SectionID == toDTO.SectionID && 
                p.PublishStatusID == publishStatusID)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.Exam)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .ToList();

                if (progressReportDatas != null)
                {
                    foreach (var progress in progressReportDatas)
                    {
                        reportsList.Add(ToDTO(progress));
                    }
                }

            }

            return reportsList;
        }

        public List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID)
        {
            var reportsList = new List<ProgressReportDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var publishStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("PROGRESS_REPORT_PUBLISH_STATUS_PB", 2);

                var progressReportDatas = dbContext.ProgressReports.Where(p => p.StudentId == studentID && p.ExamID == examID &&
                p.PublishStatusID == publishStatusID)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.ExamGroup)
                    .Include(i => i.Exam)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking().ToList();

                if (progressReportDatas != null)
                {
                    foreach (var progress in progressReportDatas)
                    {
                        reportsList.Add(ToDTO(progress));
                    }
                }

            }

            return reportsList;
        }

        private List<ProgressReportNewDTO> GetReportContentData(List<ProgressReportNewDTO> progressReportDataList)
        {
            List<ProgressReportNewDTO> dFndReportData = new List<ProgressReportNewDTO>();
            using (var sContext = new dbContentContext())
            {
                foreach (var progressReportData in progressReportDataList)
                {
                    var reportData = (from n in sContext.ContentFiles
                                      where n.ContentFileIID == progressReportData.ReportContentID
                                      select new ProgressReportNewDTO
                                      {
                                          AcademicYearID = progressReportData.AcademicYearID,
                                          StudentId = progressReportData.StudentId,
                                          ClassID = progressReportData.ClassID,
                                          ReportContentID = n.ContentFileIID,
                                          ContentData = n.ContentData,
                                          ContentFileName = progressReportData.ContentFileName != null ? progressReportData.ContentFileName : n.ContentFileName
                                      }).AsNoTracking().FirstOrDefault();

                    if (reportData != null)
                    {
                        dFndReportData.Add(reportData);
                    }
                }
            }
            return dFndReportData;
        }


        public bool UpdateStudentProgressReportStatusID(MarkRegisterDTO dto)
        {
            bool updatesMade = false;

            if (dto == null || dto.MarkRegistersDetails == null || !dto.MarkRegistersDetails.Any())
            {
                return false; 
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var item in dto.MarkRegistersDetails)
                {
                    var progressReportData = dbContext.ProgressReports
                        .FirstOrDefault(x => x.ProgressReportIID == item.ProgressReportIID);

                    if (progressReportData != null)
                    {
                        progressReportData.PublishStatusID = item.PublishStatusID;
                        progressReportData.UpdatedBy = (int?)_context.LoginID; 
                        progressReportData.UpdatedDate = DateTime.UtcNow; 

                        dbContext.Entry(progressReportData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        updatesMade = true; 
                    }
                }

                if (updatesMade)
                {
                    dbContext.SaveChanges();
                }
            }

            return updatesMade;   
        }
    }
}