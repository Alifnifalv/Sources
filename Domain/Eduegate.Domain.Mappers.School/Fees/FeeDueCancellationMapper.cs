using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework;
using System;
using Eduegate.Services.Contracts.School.Students;
using System.Linq;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Domain.Entity;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeeDueCancellationMapper : DTOEntityDynamicMapper
    {
        public static FeeDueCancellationMapper Mapper(CallContext context)
        {
            var mapper = new FeeDueCancellationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeDueCancellationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FeeDueCancellationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FeeDueCancellations.Where(X => X.FeeDueCancellationIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var student = dbContext.Students.Where(a => a.StudentIID == entity.StudentID).AsNoTracking().FirstOrDefault();
                var studentName = student.AdmissionNumber + "-" + student.FirstName + " " + student.MiddleName + " " + student.LastName;

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var FeeDueCancellationDTO = new FeeDueCancellationDTO()
                {
                    AcademicYearID = entity.AcademicYearID,
                    FeeDueCancellationIID = entity.FeeDueCancellationIID,
                    CancelationDate = entity.CancelationDate,
                    //SchoolID = entity.SchoolID,
                    StudentID = entity.StudentID,
                    StudentName = studentName,
                    //Acade = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.Description + " " + "( " + entity.AcademicYear.AcademicYearCode + ")" } : new KeyValueDTO(),
                };

                FeeDueCancellationDTO.FeeDueCancellation = new List<FeeDueCancellationDetailDTO>();
                var feeDetails = dbContext.FeeDueCancellations.Where(ssm => ssm.StudentID == entity.StudentID && ssm.AcademicYearID == entity.AcademicYearID)
                    .Include(s => s.StudentFeeDue)
                    .AsNoTracking()
                    .ToList();

                foreach (var data in feeDetails)
                {
                    //var studentFeeDue = dbContext.StudentFeeDues.FirstOrDefault(s => s.StudentFeeDueIID == data.StudentFeeDueID);
                    var studentFeeDue = data.StudentFeeDue;
                    var FeeDueFeeTypeMap = dbContext.FeeDueFeeTypeMaps.Where(s => s.FeeDueFeeTypeMapsIID == data.FeeDueFeeTypeMapsID)
                        .Include(s => s.FeeMaster)
                        .Include(s => s.FeePeriod)
                        .AsNoTracking()
                        .FirstOrDefault();


                    if (data.StudentID.HasValue)
                    {
                        FeeDueCancellationDTO.FeeDueCancellation.Add(new FeeDueCancellationDetailDTO()
                        {
                            FeeDueCancellationID = data.FeeDueCancellationIID,
                            FeeDueFeeTypeMapsID = data.FeeDueFeeTypeMapsID,
                            StudentFeeDueID = data.StudentFeeDueID,
                            //DueAmount = data.DueAmount,

                            FeeInvoice = studentFeeDue != null ? new KeyValueDTO()
                            {
                                Key = studentFeeDue.StudentFeeDueIID.ToString(),
                                Value = studentFeeDue.InvoiceNo
                            } : GetInvoiceNoFromID(data.StudentFeeDueID),
                            FeeMasterID = FeeDueFeeTypeMap.FeeMasterID,
                            FeeMaster = new KeyValueDTO()
                            {
                                Key = FeeDueFeeTypeMap.FeeMasterID.ToString(),
                                Value = FeeDueFeeTypeMap.FeeMaster.Description
                            },
                            FeePeriodID = FeeDueFeeTypeMap.FeePeriodID,
                            FeePeriod = FeeDueFeeTypeMap.FeePeriodID.HasValue ? new KeyValueDTO()
                            {
                                Key = FeeDueFeeTypeMap.FeePeriodID.HasValue ? Convert.ToString(FeeDueFeeTypeMap.FeePeriodID) : null,
                                Value = !FeeDueFeeTypeMap.FeePeriodID.HasValue ? null : FeeDueFeeTypeMap.FeePeriod.Description + " ( " + FeeDueFeeTypeMap.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + '-'
                                        + FeeDueFeeTypeMap.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + " ) "
                            } : new KeyValueDTO(),
                            //CreditNoteID = data.CreditNoteID
                            Remarks = studentFeeDue.CancelReason,
                            IsCancel = data.IsCancelled,
                            DueAmount = (FeeDueFeeTypeMap.Amount - FeeDueFeeTypeMap.CollectedAmount),
                        });
                    }
                }

                return FeeDueCancellationDTO;
            }
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeeDueCancellationDTO;

            if (toDto.FeeDueCancellationIID != 0)
            {
                throw new Exception("Sorry, edit is not possible for this screen !");
            }

            var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate((long?)_context.SchoolID);

            if (monthlyClosingDate.HasValue && monthlyClosingDate.Value.Year > 1900 && toDto.CancelationDate.Value.Date <= monthlyClosingDate.Value.Date)

            {
                throw new Exception("This Transaction could not be saved due to monthly closing");
            }

            var entity = new FeeDueCancellation();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var due in toDto.FeeDueCancellation)
                {
                    if (due.InvoiceNo != null && due.IsCancel == true)
                    {
                        var feeDueData = dbContext.StudentFeeDues.Where(d => d.InvoiceNo == due.InvoiceNo).AsNoTracking().FirstOrDefault();

                        feeDueData.IsCancelled = due.IsCancel;
                        feeDueData.CancelReason = due.Remarks;
                        feeDueData.CancelledDate = DateTime.Now.Date;
                        feeDueData.UpdatedBy = (int)_context.LoginID;
                        feeDueData.UpdatedDate = DateTime.Now;

                        dbContext.Entry(feeDueData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        entity = new FeeDueCancellation()
                        {
                            FeeDueCancellationIID = due.FeeDueCancellationID,
                            CancelationDate = toDto.CancelationDate,
                            StudentID = due.StudentID,
                            AcademicYearID = due.AcademicYearID,
                            IsCancelled = due.IsCancel,
                            StudentFeeDueID = feeDueData.StudentFeeDueIID,
                            FeeDueFeeTypeMapsID = due.FeeDueFeeTypeMapsID,
                            CreatedBy = due.FeeDueCancellationID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = due.FeeDueCancellationID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = due.FeeDueCancellationID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = due.FeeDueCancellationID > 0 ? DateTime.Now : dto.UpdatedDate,
                        };

                        dbContext.FeeDueCancellations.Add(entity);

                        if (entity.FeeDueCancellationIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        var relationmaps = dbContext.StudentFeeRelationMaps.Where(a => a.SFRM_DValue == feeDueData.StudentFeeDueIID && a.SFRM_STable == "inventory.TransactionHead" && a.SFRM_DTable == "schools.StudentFeeDues").AsNoTracking().FirstOrDefault();

                        if (relationmaps != null)
                        {
                            using (var dbContext1 = new dbEduegateERPContext())
                            {
                                var transactions = dbContext1.TransactionHeads.Where(a => a.HeadIID == relationmaps.SFRM_SValue).AsNoTracking().FirstOrDefault();

                                if (transactions != null) 
                                {
                                    transactions.DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Cancelled;
                                    transactions.TransactionStatusID = (byte)Eduegate.Services.Contracts.Enums.TransactionStatus.Cancelled;

                                    dbContext1.Entry(transactions).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext1.SaveChanges();
                                }
                            }
                        }

                    }
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.FeeDueCancellationIID));

            }
        }

        public List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
                var currentAcademicStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CURRENT_ACADEMIC_YEAR_STATUSID");
                var advanceAcademicStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ADVANCE_ACADEMIC_YEAR_STATUSID");
                var onAccountFeeCycleID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ON_ACCOUNT_FEE_CYCLE");
                var inventoryFeeCycleID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("INVENTORY_FEE_CYCLE");
                var finalsettlementDueMasterID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FINALSETTLEMENTFEEMASTERID");

                var sRetData = new List<FeeDueCancellationDetailDTO>();

                var feeData = (from sfd in dbContext.StudentFeeDues
                               join fdft in dbContext.FeeDueFeeTypeMaps on sfd.StudentFeeDueIID equals fdft.StudentFeeDueID
                               join ac in dbContext.AcademicYears on sfd.AcadamicYearID equals ac.AcademicYearID
                               join fm in dbContext.FeeMasters on fdft.FeeMasterID equals fm.FeeMasterID
                               where sfd.CollectionStatus != true
                               && fdft.Status != true
                               //&& (ac.AcademicYearStatusID == byte.Parse(advanceAcademicStatusID) || ac.AcademicYearStatusID == byte.Parse(currentAcademicStatusID))
                               && fdft.FeeMasterID != int.Parse(finalsettlementDueMasterID)
                               && (fm.FeeCycleID == byte.Parse(onAccountFeeCycleID) || fm.FeeCycleID == byte.Parse(inventoryFeeCycleID))
                               && sfd.IsCancelled != true
                               //&& sfd.AcadamicYearID == academicYearID
                               && sfd.StudentId == studentID
                               select fdft)
                               .Include(i => i.StudentFeeDue)
                               .Include(i => i.FeeMaster)
                               .Include(i => i.FeePeriod)
                               .AsNoTracking().ToList();

                var feeDueFeetypeID = feeData.Select(x => x.FeeDueFeeTypeMapsIID);
                var partialllyCollected = dbContext.FeeDueMonthlySplits.Where(x => feeDueFeetypeID.Contains(x.FeeDueFeeTypeMapsID) && x.Status == true).Select(x => x.FeeDueFeeTypeMapsID).ToList();
                if (partialllyCollected.Count() > 0)
                {
                    feeData.RemoveAll(x => partialllyCollected.Contains(x.FeeDueFeeTypeMapsIID));
                }


                var creditnoteFeetypeIDs = dbContext.CreditNoteFeeTypeMaps.Where(x => feeDueFeetypeID.Contains(x.FeeDueFeeTypeMapsID.Value)).Select(x => x.FeeDueFeeTypeMapsID).ToList();
                if (creditnoteFeetypeIDs.Count() > 0)
                {
                    feeData.RemoveAll(x => creditnoteFeetypeIDs.Contains(x.FeeDueFeeTypeMapsIID));
                }


                if (feeData.Any())
                {
                    feeData.All(w => { sRetData.Add(GetFeeMasterData(dbContext, w, dateFormat)); return true; });
                }
                return sRetData;
            }
        }

        private FeeDueCancellationDetailDTO GetFeeMasterData(dbEduegateSchoolContext sContext, FeeDueFeeTypeMap fData, string dateFormat)
        {
            var data = new FeeDueCancellationDetailDTO()
            {
                FeeInvoice = new KeyValueDTO()
                {
                    Key = fData.StudentFeeDueID.ToString(),
                    Value = fData.StudentFeeDue.InvoiceNo
                },
                FeeMasterID = fData.FeeMasterID,
                FeeMaster = new KeyValueDTO()
                {
                    Key = fData.FeeMasterID.ToString(),
                    Value = fData.FeeMaster.Description
                },
                FeePeriodID = fData.FeePeriodID,
                FeePeriod = fData.FeePeriodID.HasValue ? new KeyValueDTO()
                {
                    Key = fData.FeePeriodID.HasValue ? Convert.ToString(fData.FeePeriodID) : null,
                    Value = !fData.FeePeriodID.HasValue ? null : fData.FeePeriod.Description + " ( " + fData.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + '-'
                                          + fData.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + " ) "
                } : new KeyValueDTO(),
                FeeDueFeeTypeMapsID = fData.FeeDueFeeTypeMapsIID,
                StudentFeeDueID = fData.StudentFeeDueID,
                DueAmount = fData.Amount - fData.CollectedAmount
            };
            return data;
        }

        private KeyValueDTO GetInvoiceNoFromID(long? studentFeeDueID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDues = dbContext.StudentFeeDues.Where(x => x.StudentFeeDueIID == studentFeeDueID).AsNoTracking().FirstOrDefault();
                return new KeyValueDTO() { Key = feeDues.StudentFeeDueIID.ToString(), Value = feeDues.InvoiceNo };
            }
        }

    }
}