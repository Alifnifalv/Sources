using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class CampusTrasnsferMapper : DTOEntityDynamicMapper
    {
        public static CampusTrasnsferMapper Mapper(CallContext context)
        {
            var mapper = new CampusTrasnsferMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<CampusTransferDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private CampusTransferDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.CampusTransfers.Where(X => X.CampusTransferIID == IID)
                    .Include(i => i.FromClass)
                    .Include(i => i.FromSection)
                    .Include(i => i.ToClass)
                    .Include(i => i.ToSection)
                    .Include(i => i.FromAcademicYear)
                    .Include(i => i.ToAcademicYear)
                    .Include(i => i.Student)
                    .AsNoTracking()
                    .FirstOrDefault();


                var CampusTransferDTO = new CampusTransferDTO()
                {
                    CampusTransferIID = entity.CampusTransferIID,
                    StudentID = entity.StudentID,
                    TransferDate = entity.TransferDate,
                    FromClassID = entity.FromClassID,
                    FromSectionID = entity.FromSectionID,
                    FromClass = new KeyValueDTO() { Key = entity.FromClassID.ToString(), Value = entity.FromClass != null ? entity.FromClass.ClassDescription : null },
                    FromSection = new KeyValueDTO() { Key = entity.FromSectionID.ToString(), Value = entity.FromSection != null ? entity.FromSection.SectionName : null },
                    ToClassID = entity.ToClassID,
                    ToSectionID = entity.ToSectionID,
                    ToClass = new KeyValueDTO() { Key = entity.ToClassID.ToString(), Value = entity.ToClass.ClassDescription },
                    ToSection = new KeyValueDTO() { Key = entity.ToSectionID.ToString(), Value = entity.ToSection.SectionName },
                    Student = new KeyValueDTO() { Key = entity.StudentID.ToString(), Value = entity.Student.AdmissionNumber+" - "+entity.Student.FirstName+" "+entity.Student.MiddleName+" "+entity.Student.LastName },
                    ToAcademicYearID = entity.ToAcademicYearID,
                    ToAcademicYear = new KeyValueDTO() { Key = entity.ToAcademicYearID.ToString(), Value = entity.ToAcademicYear.Description },
                    ToSchoolID = entity.ToSchoolID,
                    AdmissionNumber = entity.Student.AdmissionNumber,
                    FromAcademicyear = entity.FromAcademicYear.Description,
                    Remarks = entity.Remarks,
                };

                CampusTransferDTO.CampusTransferMap = new List<CampusTransferMapDTO>();

                return CampusTransferDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as CampusTransferDTO;

            if (toDto.CampusTransferIID != 0)
            {
                throw new Exception("Unable to edit campus transfer screen");
            }

            var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate();
            if (monthlyClosingDate != null && monthlyClosingDate.Value.Year > 1900 && toDto.TransferDate.Value >= monthlyClosingDate)
            {
                throw new Exception("This details could not be saved due to monthly closing");
            }
            using (var dbContext = new dbEduegateSchoolContext())
            {
                #region Save Data to Campus Transfer related Tables -- Start

                var entity = new CampusTransfers()
                {
                    CampusTransferIID = toDto.CampusTransferIID,
                    Remarks = toDto.Remarks,
                    ToAcademicYearID = (int)toDto.ToAcademicYearID,
                    FromAcademicYearID = (int)toDto.FromAcademicYearID,
                    TransferDate = toDto.TransferDate,
                    StudentID = (long)toDto.StudentID,
                    ToClassID = (int)toDto.ToClassID,
                    ToSectionID = (int)toDto.ToSectionID,
                    FromClassID = (int)toDto.FromClassID,
                    FromSectionID = (int)toDto.FromSectionID,
                    FromSchoolID = toDto.FromSchoolID,
                    ToSchoolID = toDto.ToSchoolID,
                    CreatedBy = (int?)(toDto.CampusTransferIID == 0 ? _context.LoginID : toDto.CreatedBy),
                    CreatedDate = toDto.CampusTransferIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = (int?)_context.LoginID,
                    UpdatedDate = DateTime.Now,
                };
                dbContext.CampusTransfers.Add(entity);

                foreach (var feeTypes in toDto.FeeTypeMap)
                {
                    if (feeTypes.FeeDueFeeTypeMapsID.HasValue)
                    {
                        var monthlySplits = feeTypes.MontlySplitMaps
                            .Select(monthlySplit => new CampusTransferMonthlySplit()
                            {
                                CampusTransferMonthlySplitIID = monthlySplit.CampusTransferMonthlySplitIID,
                                MonthID = monthlySplit.MonthID,
                                FeeDueMonthlySplitID = monthlySplit.FeeDueMonthlySplitID,
                                Recievable = monthlySplit.ReceivableAmount,
                                Payable = monthlySplit.PayableAmount,
                            })
                            .ToList();

                        entity.CampusTransferFeeTypeMaps.Add(new CampusTransferFeeTypeMap()
                        {
                            CampusTransferFeeTypeMapsIID = feeTypes.CampusTransferFeeTypeMapsIID,
                            CampusTransferID = entity.CampusTransferIID,
                            FeeMasterID = feeTypes.FeeMasterID,
                            FeePeriodID = feeTypes.FeePeriodID == 0 ? (int?)null : feeTypes.FeePeriodID,
                            FeeDueFeeTypeMapsID = feeTypes.FeeDueFeeTypeMapsID,
                            Recievable = feeTypes.ReceivableAmount,
                            Payable = feeTypes.PayableAmount,
                            CreatedBy = (int?)(toDto.CampusTransferIID == 0 ? _context.LoginID : toDto.CreatedBy),
                            CreatedDate = toDto.CampusTransferIID == 0 ? DateTime.Now : toDto.CreatedDate,
                            UpdatedBy = (int?)_context.LoginID,
                            UpdatedDate = DateTime.Now,
                            CampusTransferMonthlySplits = monthlySplits
                        });
                    }
                }

                if (entity.CampusTransferIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                dbContext.SaveChanges();

                #endregion  -- End

                #region Campus Transfer Student Migration
                if (entity.CampusTransferIID != 0)
                {
                    //UPDATE/INSERT Tables :
                    //  1.Student table update
                    //  2.RemarkEntries
                    //  3.HealthEntries
                    //  4.Attendance
                    //  5.MarkEntries
                    //  6.Transport set as inactive
                    //  7.Fee Due Merge + Account posting


                    #region Student Update -- Start
                    var studentData = dbContext.Students
                                .AsNoTracking()
                                .FirstOrDefault(s => s.StudentIID == entity.StudentID);

                    var cmpsTransferStsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_STATUS_CAMPUS_TRANSFERID");

                    studentData.Status = byte.Parse(cmpsTransferStsID);
                    studentData.ClassID = entity.ToClassID;
                    studentData.SectionID = entity.ToSectionID;
                    studentData.SchoolID = entity.ToSchoolID;
                    studentData.AcademicYearID = entity.ToAcademicYearID;

                    dbContext.Entry(studentData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    #endregion Student -- End

                    #region Remarks Entry Migration -- Start
                    // Update or insert remarks entry
                    var oldRemarks = dbContext.RemarksEntryStudentMaps
                                    .Where(rm => rm.StudentID == entity.StudentID && rm.RemarksEntry.AcademicYearID == entity.FromAcademicYearID /*&& (rm.Remarks1 != null || rm.Remarks2 != null)*/)
                                    .Include(rm => rm.RemarksEntry)
                                    .ToList();

                    if (oldRemarks.Count > 0)
                    {
                        // Check if the new school has the same exam group, class, section, and academic year ID then update, else insert
                        var toSchoolRemarks = dbContext.RemarksEntries
                            .Where(nr => nr.AcademicYearID == entity.ToAcademicYearID && nr.ClassID == entity.ToClassID && nr.SectionID == entity.ToSectionID)
                            .ToList();

                        foreach (var old in oldRemarks)
                        {
                            // Update if exists
                            if (toSchoolRemarks.Any(newRmk => old.RemarksEntry.ExamGroupID == newRmk.ExamGroupID))
                            {
                                var newRmk = toSchoolRemarks.First(newRmk => old.RemarksEntry.ExamGroupID == newRmk.ExamGroupID);

                                old.RemarksEntryID = newRmk.RemarksEntryIID;
                                dbContext.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            // Insert 
                            else
                            {
                                var remarkEntity = new RemarksEntry()
                                {
                                    SchoolID = entity.ToSchoolID,
                                    AcademicYearID = entity.ToAcademicYearID,
                                    ClassID = entity.ToClassID,
                                    SectionID = entity.ToSectionID,
                                    ExamGroupID = old.RemarksEntry.ExamGroupID
                                };

                                remarkEntity.RemarksEntryStudentMaps.Add(new RemarksEntryStudentMap()
                                {
                                    RemarksEntryID = remarkEntity.RemarksEntryIID,
                                    StudentID = entity.StudentID,
                                    Remarks1 = old.Remarks1,
                                    Remarks2 = old.Remarks2,
                                });

                                // Add new entity to the database
                                dbContext.RemarksEntries.Add(remarkEntity);
                                dbContext.RemarksEntryStudentMaps.Remove(old);
                            }
                            dbContext.SaveChanges();
                        }
                    }
                    #endregion Remarks end ----

                    #region Health Entry Migration -- Start

                    var oldHealthEntry = dbContext.HealthEntryStudentMaps
                                    .Where(rm => rm.StudentID == entity.StudentID && rm.HealthEntry.AcademicYearID == entity.FromAcademicYearID /*&& (rm.BMS != null)*/)
                                    .Include(rm => rm.HealthEntry)
                                    .ToList();

                    if (oldHealthEntry.Count > 0)
                    {
                        // Check if the new school has the same exam group, class, section, and academic year ID then update, else insert
                        var toSchoolHealth = dbContext.HealthEntries
                            .Where(h => h.AcademicYearID == entity.ToAcademicYearID && h.ClassID == entity.ToClassID && h.SectionID == entity.ToSectionID)
                            .ToList();

                        foreach (var oldH in oldHealthEntry)
                        {
                            // Update if exists
                            if (toSchoolHealth.Any(newHlth => oldH.HealthEntry.ExamGroupID == newHlth.ExamGroupID))
                            {
                                var newHlth = toSchoolHealth.First(newHlth => oldH.HealthEntry.ExamGroupID == newHlth.ExamGroupID);

                                oldH.HealthEntryID = newHlth.HealthEntryIID;
                                dbContext.Entry(oldH).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            // Insert 
                            else
                            {
                                var healthEntity = new HealthEntry()
                                {
                                    SchoolID = entity.ToSchoolID,
                                    AcademicYearID = entity.ToAcademicYearID,
                                    ClassID = entity.ToClassID,
                                    SectionID = entity.ToSectionID,
                                    ExamGroupID = oldH.HealthEntry.ExamGroupID
                                };

                                healthEntity.HealthEntryStudentMaps.Add(new HealthEntryStudentMap()
                                {
                                    HealthEntryID = healthEntity.HealthEntryIID,
                                    StudentID = entity.StudentID,
                                    Height = oldH.Height,
                                    Weight = oldH.Weight,
                                    BMI = oldH.BMI,
                                    BMS = oldH.BMS,
                                    Vision = oldH.Vision,
                                    Remarks = oldH.Remarks,
                                });

                                // Add new entity to the database
                                dbContext.HealthEntries.Add(healthEntity);
                                dbContext.HealthEntryStudentMaps.Remove(oldH);
                            }
                            dbContext.SaveChanges();
                        }
                    }

                    #endregion Health Entry -- End

                    #region Attendance Migration -- Start

                    var oldAttendance = dbContext.StudentAttendences
                                    .Where(att => att.StudentID == entity.StudentID && att.AcademicYearID == entity.FromAcademicYearID)
                                    .ToList();

                    if (oldAttendance.Count > 0)
                    {
                        oldAttendance.ForEach(att =>
                        {
                            att.SchoolID = entity.ToSchoolID;
                            att.AcademicYearID = entity.ToAcademicYearID;
                            att.ClassID = entity.ToClassID;
                            att.SectionID = entity.ToSectionID;
                        });

                        dbContext.StudentAttendences.UpdateRange(oldAttendance);
                        dbContext.SaveChanges();
                    }

                    #endregion Attendance -- End

                    #region Mark Entry Migration -- Start

                    var oldMarkEnrty = dbContext.MarkRegisters
                        .Where(mr => mr.StudentId == entity.StudentID && mr.AcademicYearID == entity.FromAcademicYearID)
                        .Include(mr => mr.Exam)
                        .ToList();                 

                    if (oldMarkEnrty.Count > 0)
                    {
                        oldMarkEnrty.ForEach(mark =>
                        {
                            mark.SchoolID = entity.ToSchoolID;
                            mark.AcademicYearID = entity.ToAcademicYearID;
                            mark.ClassID = entity.ToClassID;
                            mark.SectionID = entity.ToSectionID;
                            mark.ExamID = dbContext.Exams
                                            .AsNoTracking()
                                            .FirstOrDefault(x => x.ExamDescription == mark.Exam.ExamDescription 
                                            && x.SchoolID == entity.ToSchoolID && x.ExamGroupID == mark.ExamGroupID)?.ExamIID;
                        });

                        dbContext.MarkRegisters.UpdateRange(oldMarkEnrty);
                        dbContext.SaveChanges();
                    }

                    #endregion MarkEntry -- End

                    #region Transport update As InActive -- Start

                    var transport = dbContext.StudentRouteStopMaps.Where(tr => tr.StudentID == entity.StudentID && tr.IsActive == true).ToList();

                    if (transport.Count > 0)
                    {
                        transport.ForEach(trns =>
                        {
                            trns.IsActive = false;
                            trns.CancelDate = DateTime.Now;
                            trns.Remarks = "Campus Transfered to " + entity.ToSchool?.SchoolName;
                        });

                        dbContext.StudentRouteStopMaps.UpdateRange(transport);
                        dbContext.SaveChanges();
                    }

                    #endregion Transport -- End

                    #region Fee Due Merge & Accounts Posting

                    if (toDto.FeeTypeMap.Count() > 0)
                    {

                        var feeMaster_Tuition_Adj = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CAMPUS_TRANSFER_TUTION_FEE_ADJ");
                        var feeMaster_Transport_Adj = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CAMPUS_TRANSFER_TRANSPORT_FEE_ADJ");
                        var feeMaster_OtherFee_Adj = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CAMPUS_TRANSFER_OTHER_FEE_ADJ");

                        var groupedData = toDto.FeeTypeMap.GroupBy(fee => new { fee.IsTransportFee, fee.IsTutionFee })
                        .Select(group => new FeeCollectionFeeTypeDTO
                        {
                            FeeDueDifference = group.Sum(fee => fee.ToCampusDue ?? 0) - group.Sum(fee => fee.FromCampusDue ?? 0),
                            IsTransportFee = group.First().IsTransportFee,
                            IsTutionFee = group.First().IsTutionFee,
                        })
                        .ToList();


                        if (groupedData.Count > 0)
                        {
                            int type = 0;

                            foreach (var item in groupedData)
                            {
                                if (item.IsTransportFee == true) //Transport fee
                                {
                                    item.FeeMasterID = int.Parse(feeMaster_Transport_Adj);

                                }
                                else if (item.IsTutionFee == true) //Tution fee
                                {
                                    item.FeeMasterID = int.Parse(feeMaster_Tuition_Adj);
                                }
                                else // Other fee
                                {
                                    item.FeeMasterID = int.Parse(feeMaster_OtherFee_Adj);
                                }

                                var lastInvoiceNo = dbContext.StudentFeeDues.OrderByDescending(s => s.StudentFeeDueIID).First().InvoiceNo;
                                string numericPart = lastInvoiceNo.Substring(4); // Extract the numeric part
                                int lastInvoiceNumber = int.Parse(numericPart);
                                int nextInvoiceNumber = lastInvoiceNumber + 1;

                                string nextInvoiceNo = $"INV-{nextInvoiceNumber:D7}";

                                var feeDueEntity = new StudentFeeDue()
                                {
                                    StudentId = entity.StudentID,
                                    ClassId = entity.FromClassID,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = (int?)_context.LoginID,
                                    InvoiceNo = nextInvoiceNo,
                                    InvoiceDate = DateTime.Now,
                                    AcadamicYearID = entity.FromAcademicYearID,
                                    SectionID = entity.FromSectionID,
                                    SchoolID = entity.FromSchoolID,
                                };
                                dbContext.StudentFeeDues.Add(feeDueEntity);

                                feeDueEntity.FeeDueFeeTypeMaps.Add(new FeeDueFeeTypeMap()
                                {
                                    StudentFeeDueID = feeDueEntity.StudentFeeDueIID,
                                    Amount = item.FeeDueDifference ?? 0,
                                    CreatedBy = (int?)_context.LoginID,
                                    CreatedDate = DateTime.Now,
                                    FeeMasterID = item.FeeMasterID,
                                });

                                dbContext.Entry(feeDueEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                dbContext.SaveChanges();

                                new AccountEntryMapper().AccountTransMergewithMultipleIDs((feeDueEntity.StudentFeeDueIID).ToString(), entity.TransferDate.Value, int.Parse(_context.LoginID.Value.ToString()), type);
                            }
                        }

                    }

                    #endregion

                    #endregion Migration -- End

                }

                return GetEntity(entity.CampusTransferIID);
            }

            #region -- Old Code -- Commented
            //convert the dto to entity and pass to the repository.
            //using (var dbContext = new dbEduegateSchoolContext())
            //{
            //    var academicYearDatas = dbContext.AcademicYears.AsNoTracking().ToList();
            //    var examgroups = dbContext.ExamGroups.AsNoTracking().ToList();

            //    var currentAcademicStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");

            //    if (toDto.CampusTransferMap.Count > 0)
            //    {
            //        var IIDs = toDto.CampusTransferMap
            //                .Select(a => a.StudentID).ToList();
            //        //delete maps
            //        var entities = dbContext.CampusTransfers.Where(x =>
            //            (x.FromClassID == toDto.FromClassID) && (x.FromSectionID == toDto.FromSectionID) && (x.TransferDate.Value.Year == toDto.TransferDate.Value.Year) && (x.TransferDate.Value.Month == toDto.TransferDate.Value.Month) &&
            //            !IIDs.Contains(x.StudentID)).AsNoTracking().ToList();

            //        if (entities.IsNotNull())
            //            dbContext.CampusTransfers.RemoveRange(entities);

            //        var studentIDs = toDto.CampusTransferMap.Select(x => x.StudentID);
            //        var studentData = dbContext.Students.Where(x => studentIDs.Contains(x.StudentIID)).AsNoTracking().ToList();

            //        foreach (var map in toDto.CampusTransferMap)
            //        {
            //            if (map.StudentID != 0)
            //            {
            //                var fromAcademicYearID = studentData.Where(x => x.StudentIID == map.StudentID).Select(y => y.AcademicYearID.Value).FirstOrDefault();
            //                var entity = new CampusTransfers()
            //                {
            //                    CampusTransferIID = map.CampusTransferIID,
            //                    StudentID = map.StudentID,
            //                    TransferDate = toDto.TransferDate,
            //                    FromClassID = studentData.Where(x => x.StudentIID == map.StudentID).Select(y => y.ClassID.Value).FirstOrDefault(),
            //                    ToClassID = toDto.ToClassID,
            //                    FromSectionID = studentData.Where(x => x.StudentIID == map.StudentID).Select(y => y.SectionID.Value).FirstOrDefault(),
            //                    ToSectionID = toDto.ToSectionID,
            //                    FromSchoolID = map.FromSchoolID,
            //                    ToSchoolID = toDto.ToSchoolID,
            //                    FromAcademicYearID = fromAcademicYearID,
            //                    ToAcademicYearID = toDto.ToAcademicYearID,
            //                    Remarks = map.Remarks,
            //                    CreatedBy = map.CampusTransferIID == 0 ? Convert.ToInt32(_context.LoginID) : toDto.CreatedBy,
            //                    CreatedDate = map.CampusTransferIID == 0 ? DateTime.Now : toDto.CreatedDate,
            //                    UpdatedBy = map.CampusTransferIID != 0 ? Convert.ToInt32(_context.LoginID) : toDto.UpdatedBy,
            //                    UpdatedDate = map.CampusTransferIID != 0 ? DateTime.Now : toDto.UpdatedDate,
            //                };

            //                dbContext.CampusTransfers.Add(entity);

            //                if (entity.CampusTransferIID == 0)
            //                {
            //                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //                }
            //                else
            //                {
            //                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                }

            //                #region Update / move student health entry full data when transfer student

            //                var healthEntryStudentMaps = dbContext.HealthEntryStudentMaps
            //                    .Where(m1 => m1.StudentID == map.StudentID && m1.HealthEntry.AcademicYearID == fromAcademicYearID)
            //                    .Include(i => i.HealthEntry).ThenInclude(i => i.ExamGroup)
            //                    .AsNoTracking().ToList();

            //                if (healthEntryStudentMaps.Count > 0)
            //                {
            //                    foreach (var healthEntyStudentmap in healthEntryStudentMaps)
            //                    {
            //                        //var oldAcademicCode1 = healthEntyStudentmap.HealthEntry.AcademicYear?.AcademicYearCode;
            //                        var oldExamGroup1 = healthEntyStudentmap.HealthEntry?.ExamGroup.ExamGroupName;

            //                        //var toAcdmicID = academicDatas.Find(a => a.AcademicYearCode == oldAcademicCode1 && toDto.ToSchoolID == a.SchoolID)?.AcademicYearID;
            //                        var toExmGrpID = examgroups.Find(x => x.ExamGroupName == oldExamGroup1 && toDto.ToSchoolID == x.SchoolID)?.ExamGroupID;

            //                        var checkHealthEntry = dbContext.HealthEntries.Where(a => a.ClassID == toDto.ToClassID && a.SectionID == toDto.ToSectionID
            //                        && a.AcademicYearID == map.ToAcademicYearID && a.ExamGroupID == toExmGrpID)
            //                            .AsNoTracking().FirstOrDefault();

            //                        if (checkHealthEntry != null)
            //                        {
            //                            //var toEntry = dbContext.HealthEntryStudentMaps.FirstOrDefault(x => x.HealthEntryStudentMapIID == insrtTo.HealthEntryStudentMapIID);
            //                            healthEntyStudentmap.HealthEntryID = checkHealthEntry.HealthEntryIID;
            //                            dbContext.Entry(healthEntyStudentmap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                            //UpdatetHealthEntry(insrtTo.HealthEntryStudentMapIID, checkHealthEntry.HealthEntryIID);
            //                        }

            //                    }
            //                }
            //                #endregion

            //                #region Update student, routestopmap, atendance data when transfer student

            //                //var entityStudent = repStudent.GetById(map.StudentID);
            //                var entityStudent = dbContext.Students.Where(s => s.StudentIID == map.StudentID && s.AcademicYearID == fromAcademicYearID)
            //                    .AsNoTracking().FirstOrDefault();

            //                if (entityStudent != null)
            //                {
            //                    entityStudent.ClassID = toDto.ToClassID;
            //                    entityStudent.SchoolID = toDto.ToSchoolID;
            //                    entityStudent.AcademicYearID = toDto.ToAcademicYearID;
            //                    entityStudent.SectionID = toDto.ToSectionID;

            //                    dbContext.Entry(entityStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                    //repStudent.Update(entityStudent);
            //                }


            //                //var entityTransport = repTransfer.Get(xw => xw.StudentID == map.StudentID).OrderByDescending(y => y.StudentRouteStopMapIID).FirstOrDefault();
            //                var entityTransport = dbContext.StudentRouteStopMaps.Where(srm => srm.StudentID == map.StudentID && srm.AcademicYearID == fromAcademicYearID)
            //                    .OrderByDescending(y => y.StudentRouteStopMapIID)
            //                    .AsNoTracking().FirstOrDefault();

            //                if (entityTransport != null)
            //                {
            //                    entityTransport.IsActive = false;

            //                    dbContext.Entry(entityTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                    //repTransfer.Update(entityTransport);
            //                }

            //                var studentAttendances = dbContext.StudentAttendences.Where(sa => sa.StudentID == map.StudentID && sa.AcademicYearID == fromAcademicYearID)
            //                    .AsNoTracking().ToList();

            //                if (studentAttendances != null)
            //                {
            //                    foreach (var datas in studentAttendances)
            //                    {
            //                        datas.ClassID = toDto.ToClassID;
            //                        datas.SectionID = toDto.ToSectionID;
            //                        datas.SchoolID = toDto.ToSchoolID;
            //                        datas.AcademicYearID = toDto.ToAcademicYearID;
            //                        dbContext.Entry(datas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                        //dbContext.SaveChanges();
            //                    }
            //                }
            //                #endregion

            //                #region school,academicyear,class,section data modification when toacademicyear is current academicyear check with current table data

            //                //var get_academicYear = dbContext.AcademicYears.FirstOrDefault(o => o.AcademicYearID == toDto.ToAcademicYearID);

            //                var get_academicYear = academicYearDatas.FirstOrDefault(ay => ay.AcademicYearID == toDto.ToAcademicYearID);

            //                var currentAcademicStsID = byte.Parse(currentAcademicStatusID);
            //                var toAcademicStatusID = get_academicYear.AcademicYearStatusID;

            //                //if (toAcademicStatusID == currentAcademicStsID)
            //                //{
            //                //Update remark entry data
            //                //var studRemarks = dbContext.RemarksEntries.Where(a => a.RemarksEntryStudentMaps.Any(r => r.StudentID == map.StudentID) && a.AcademicYear.AcademicYearStatusID == currentAcademicStsID).ToList();
            //                var studRemarks = dbContext.RemarksEntries
            //                .Where(a => a.RemarksEntryStudentMaps.Any(r => r.StudentID == map.StudentID) && a.AcademicYearID == fromAcademicYearID)
            //                .AsNoTracking().ToList();

            //                if (studRemarks != null)
            //                {
            //                    foreach (var remarkDat in studRemarks)
            //                    {
            //                        remarkDat.ClassID = toDto.ToClassID;
            //                        remarkDat.SectionID = toDto.ToSectionID;
            //                        remarkDat.SchoolID = toDto.ToSchoolID;
            //                        remarkDat.AcademicYearID = toDto.ToAcademicYearID;
            //                        dbContext.Entry(remarkDat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                        //dbContext.SaveChanges();
            //                    }
            //                }

            //                //Update mark register data
            //                //var studMarkdatas = dbContext.MarkRegisters.Where(m => m.StudentId == map.StudentID && m.AcademicYear.AcademicYearStatusID == currentAcademicStsID).ToList();
            //                var studMarkdatas = dbContext.MarkRegisters
            //                .Where(m => m.StudentId == map.StudentID && m.AcademicYearID == fromAcademicYearID)
            //                .AsNoTracking().ToList();

            //                if (studMarkdatas != null)
            //                {
            //                    foreach (var markDat in studMarkdatas)
            //                    {
            //                        markDat.ClassID = toDto.ToClassID;
            //                        markDat.SectionID = toDto.ToSectionID;
            //                        markDat.SchoolID = toDto.ToSchoolID;
            //                        markDat.AcademicYearID = toDto.ToAcademicYearID;
            //                        dbContext.Entry(markDat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //                        //dbContext.SaveChanges();
            //                    }
            //                }
            //                //}
            //                #endregion

            //                dbContext.SaveChanges();
            //            }
            //        }
            //        var feeDueCampusTransferID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEE_DUE_FROM_CAMPUSTRANSFER");
            //        if ((feeDueCampusTransferID == "1" ? true : false) == true)
            //        {
            //            #region Filteration

            //            //bool _sIsSucces = false;
            //            string _sStudent_IDs = string.Empty;
            //            if (toDto.CampusTransferMap != null && toDto.CampusTransferMap.Any())
            //                _sStudent_IDs = string.Join(",", toDto.CampusTransferMap.Select(w => w.StudentID));

            //            #endregion

            //            string message;
            //            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            //            _sBuilder.ConnectTimeout = 30; // Set Timedout
            //            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            //            {
            //                try { conn.Open(); }
            //                catch (Exception ex)
            //                {
            //                    message = ex.Message; return "0#" + message;
            //                }
            //                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_CAMPUSTRANSFER_FEEDUE]", conn))
            //                {
            //                    sqlCommand.CommandType = CommandType.StoredProcedure;
            //                    /*
            //                        @ACADEMICYEARID INT=1,
            //                        @COMPANYID INT=1,
            //                        @SCHOOLID INT=1,
            //                        @INVOICEDATE DATETIME='20210529',
            //                        @ACCOUNTDATE DATETIME='20210530',
            //                        @DUEDATE DATETIME='20210530',
            //                        @LOGINID INT=2,
            //                        @FEEPERIODIDs  VARCHAR(MAX)='',		-- Filter By Fee Period IDs
            //                        @CLASSIDs VARCHAR(MAX)='',			-- Filter By Class IDs
            //                        @SECTIONIDs VARCHAR(MAX)='',		-- Filter By Section IDs
            //                        @STUDENTIDs VARCHAR(MAX)='',		-- Filter By Student IDs
            //                        @FEEMASTERIDs  VARCHAR(MAX)='',		-- Filter By Fee Master IDs
            //                        @FINEMASTERIDs  VARCHAR(MAX)='',	-- Filter By Fine Master IDs
            //                        @PARENTIDs  VARCHAR(MAX)='',		-- Filter By Parent IDs
            //                        @FEEDUEIDs  VARCHAR(MAX)=''	,		-- Fee Due IDs for Editing
            //                        @FEEDUETYPEMAPIDs  VARCHAR(MAX)=''	-- Fee Due Type Map IDs for Editing
            //                        @AMOUNT  MONEY=0					-- Fee Amoun for Custom

            //                    */
            //                    sqlCommand.Parameters.Add(new SqlParameter("@OLDACADEMICYEARID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@OLDACADEMICYEARID"].Value = toDto.FromAcademicYearID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@OLDSCHOOLID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@OLDSCHOOLID"].Value = toDto.FromSchoolID ?? 0;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@OLDCLASSIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@OLDCLASSIDs"].Value = toDto.FromClassID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@OLDSECTIONIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@OLDSECTIONIDs"].Value = toDto.FromSectionID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@ACADEMICYEARID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@ACADEMICYEARID"].Value = toDto.ToAcademicYearID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@SCHOOLID"].Value = toDto.ToSchoolID ?? 0;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@COMPANYID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@COMPANYID"].Value = _context.CompanyID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@INVOICEDATE", SqlDbType.DateTime));
            //                    sqlCommand.Parameters["@INVOICEDATE"].Value = toDto.TransferDate;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@DUEDATE", SqlDbType.DateTime));
            //                    sqlCommand.Parameters["@DUEDATE"].Value = toDto.TransferDate;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@ACCOUNTDATE", SqlDbType.DateTime));
            //                    sqlCommand.Parameters["@ACCOUNTDATE"].Value = toDto.TransferDate;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@CLASSIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@CLASSIDs"].Value = toDto.ToClassID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@STUDENTIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@STUDENTIDs"].Value = _sStudent_IDs;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@SECTIONIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@SECTIONIDs"].Value = toDto.ToSectionID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@LOGINID", SqlDbType.Int));
            //                    sqlCommand.Parameters["@LOGINID"].Value = _context.LoginID;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@FEEPERIODIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@FEEPERIODIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@FEEMASTERIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@FEEMASTERIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@FINEMASTERIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@FINEMASTERIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@PARENTIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@PARENTIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUEIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@FEEDUEIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@FEEDUETYPEMAPIDs", SqlDbType.VarChar));
            //                    sqlCommand.Parameters["@FEEDUETYPEMAPIDs"].Value = string.Empty;

            //                    sqlCommand.Parameters.Add(new SqlParameter("@AMOUNT", SqlDbType.Decimal));
            //                    sqlCommand.Parameters["@AMOUNT"].Value = 0;

            //                    try
            //                    {
            //                        // Run the stored procedure.
            //                        message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        var data = ex.Message;
            //                        // throw new Exception("Something Wrong! Please check after sometime");
            //                        message = "0#Error on Saving";
            //                    }
            //                    finally
            //                    {
            //                        conn.Close();
            //                    }
            //                }
            //            }
            //        }
            //        ////update / move student healthentry full datas when student Transfers
            //        //if (toDto.CampusTransferMap.Count > 0)
            //        //{


            //        //    foreach (var mapDto1 in toDto.CampusTransferMap)
            //        //    {

            //        //    }
            //        //}

            //        //if (toDto.CampusTransferMap.Any())
            //        //{

            //        //    toDto.CampusTransferMap.All(w =>
            //        //    {


            //        //        #region school,academicyear,class,section data modification when toacademicyear is current academicyear check with current table data




            //        //        #endregion

            //        //        return true;
            //        //    });
            //        //}

            //    }
            //    return ToDTOString(ToDTO(toDto.FromClassID));
            //}
            #endregion -- Old Code end
        }

        public List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var classStudentList = new List<CampusTransferMapDTO>();

                var entities = dbContext.Students
                    .Where(a => a.ClassID == classId && a.AcademicYear1.AcademicYearStatusID != 3 && a.SectionID == sectionId && (a.IsActive == true))
                    .Include(i => i.AcademicYear1)
                    .AsNoTracking()
                    .OrderBy(a => a.AdmissionNumber).ToList();

                foreach (var classStud in entities)
                {
                    classStudentList.Add(new CampusTransferMapDTO
                    {
                        StudentID = classStud.StudentIID,
                        Student = new KeyValueDTO()
                        {
                            Key = classStud.StudentIID.ToString(),
                            Value = classStud.AdmissionNumber + " - " + classStud.FirstName + " " + classStud.MiddleName + " " + classStud.LastName
                        },
                        StudentSchoolID = classStud.SchoolID,
                    });
                }

                return classStudentList;
            }
        }


        #region GetFeeDuesForCampusTransfer --Start 
        public List<FeeCollectionFeeTypeDTO> GetFeeDuesForCampusTransfer(long studentId, int toSchoolID, int toAcademicYearID, int toClassID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var resultData = new List<FeeCollectionFeeTypeDTO>();

            using (SqlConnection connection = new SqlConnection(Infrastructure.ConfigHelper.GetSchoolConnectionString()))
            using (SqlCommand command = new SqlCommand("schools.SPS_FeeDuesForCampusTransfer", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentID", studentId);
                command.Parameters.AddWithValue("@ToSchoolID", toSchoolID);
                command.Parameters.AddWithValue("@ToAcademicYearID", toAcademicYearID);
                command.Parameters.AddWithValue("@ToClassID", toClassID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultData.Add(MapToFeeCollectionFeeTypeDTO(reader));
                        }
                    }
                }
            }

            var groupedData = resultData.GroupBy(fee => new { fee.FeeDueFeeTypeMapsID, fee.FeeMasterID, fee.FeePeriodID })
                .Select(group => new FeeCollectionFeeTypeDTO
                {
                    FeeDueFeeTypeMapsID = group.Key.FeeDueFeeTypeMapsID,
                    InvoiceNo = group.First().InvoiceNo,
                    InvoiceDateString = group.First().InvoiceDate.HasValue ? Convert.ToDateTime(group.First().InvoiceDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    CreditNoteAmount = group.Sum(fee => fee.CreditNoteAmount ?? 0),
                    FeeMasterID = group.Key.FeeMasterID.HasValue ? group.Key.FeeMasterID : 0,
                    FeePeriodID = group.Key.FeePeriodID.HasValue ? group.Key.FeePeriodID : 0,
                    FeeTypeID = group.First().FeeTypeID.HasValue ? group.First().FeeTypeID : 0,
                    FeePeriod = group.First().FeePeriod,
                    FeeMaster = group.First().FeeMaster,
                    FromCampusDue = group.Sum(fee => fee.FromCampusDue ?? 0),
                    ToCampusDue = group.Sum(fee => fee.ToCampusDue ?? 0),
                    PrvCollect = group.Sum(fee => fee.PrvCollect ?? 0),
                    ReceivableAmount = group.Sum(fee => fee.ReceivableAmount ?? 0),
                    PayableAmount = group.Sum(fee => fee.CreditNoteAmount ?? 0) > 0
    ? group.Sum(fee => fee.FromCampusDue ?? 0) - group.Sum(fee => fee.CreditNoteAmount ?? 0)
    : group.Sum(fee => fee.PayableAmount ?? 0),
                    IsTransportFee = group.First().IsTransportFee,
                    IsTutionFee = group.First().IsTutionFee,
                    MontlySplitMaps = group.Where(fee => fee.MonthID != null).Select(MapToFeeCollectionMonthlySplitDTO).ToList()
                })
                .ToList();

            return groupedData;
        }

        private FeeCollectionFeeTypeDTO MapToFeeCollectionFeeTypeDTO(SqlDataReader reader)
        {
            return new FeeCollectionFeeTypeDTO
            {
                InvoiceNo = reader["InvoiceNo"] != DBNull.Value ? reader["InvoiceNo"].ToString() : null,
                FeeDueFeeTypeMapsID = reader["FeeDueFeeTypeMapsIID"] != DBNull.Value ? (long?)reader["FeeDueFeeTypeMapsIID"] : null,
                FeeDueMonthlySplitIID = reader["FeeDueMonthlySplitIID"] != DBNull.Value ? (long?)reader["FeeDueMonthlySplitIID"] : null,
                MonthID = reader["MonthID"] != DBNull.Value ? (int?)reader["MonthID"] : null,
                MonthName = reader["MonthNme"] != DBNull.Value ? reader["MonthNme"].ToString() : null,
                InvoiceDate = reader["InvoiceDate"] != DBNull.Value ? (DateTime?)reader["InvoiceDate"] : null,
                FeeMaster = reader["FeeMaster"] != DBNull.Value ? reader["FeeMaster"].ToString() : null,
                FeePeriod = reader["FeePeriod"] != DBNull.Value ? reader["FeePeriod"].ToString() : null,
                FeeMasterID = reader["FeeMasterID"] != DBNull.Value ? (int?)reader["FeeMasterID"] : null,
                FeePeriodID = reader["FeePeriodID"] != DBNull.Value ? (int?)reader["FeePeriodID"] : null,
                FeeTypeID = reader["FeeTypeID"] != DBNull.Value ? (int?)reader["FeeTypeID"] : null,
                FromCampusDue = reader["FromCampusDue"] != DBNull.Value ? (decimal?)reader["FromCampusDue"] : null,
                ToCampusDue = reader["ToCampusDue"] != DBNull.Value ? (decimal?)reader["ToCampusDue"] : null,
                CreditNoteAmount = reader["CreditNote"] != DBNull.Value ? (decimal?)reader["CreditNote"] : null,
                PrvCollect = reader["PrvCollected"] != DBNull.Value ? (decimal?)reader["PrvCollected"] : null,
                ReceivableAmount = reader["Recievable"] != DBNull.Value ? (decimal?)reader["Recievable"] : null,
                PayableAmount = reader["Payable"] != DBNull.Value ? (decimal?)reader["Payable"] : null,
                IsTutionFee = reader["IsTutionFee"] != DBNull.Value ? (bool?)reader["IsTutionFee"] : null,
                IsTransportFee = reader["IsTransportFee"] != DBNull.Value ? (bool?)reader["IsTransportFee"] : null,
            };
        }

        private FeeCollectionMonthlySplitDTO MapToFeeCollectionMonthlySplitDTO(FeeCollectionFeeTypeDTO fee)
        {
            return new FeeCollectionMonthlySplitDTO
            {
                MonthID = (int)fee.MonthID,
                FeeDueMonthlySplitID = fee.FeeDueMonthlySplitIID,
                MonthName = fee.MonthName,
                CreditNoteAmount = fee.CreditNoteAmount ?? 0,
                PrvCollect = fee.PrvCollect ?? 0,
                PayableAmount = fee.PayableAmount ?? 0,
                ReceivableAmount = fee.ReceivableAmount ?? 0,
                FromCampusDue = fee.FromCampusDue ?? 0,
                ToCampusDue = fee.ToCampusDue ?? 0,
            };
        }

        #endregion --End (Done by Mohd.Shabeeb)


    }
}