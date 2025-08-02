using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using Eduegate.Services.Contracts.School.Students;
using System.Linq;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class StudentConcessionMapper : DTOEntityDynamicMapper
    {
        public static StudentConcessionMapper Mapper(CallContext context)
        {
            var mapper = new StudentConcessionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentFeeConcessionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StudentFeeConcessionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentFeeConcessions.Where(X => X.StudentFeeConcessionIID == IID)
                    .Include(x => x.Employee)
                    .Include(x => x.Parent)
                    .Include(x => x.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                var student = dbContext.Students.AsNoTracking().FirstOrDefault(a => a.StudentIID == entity.StudentID);
                var studentName = student.AdmissionNumber + "-" + student.FirstName + " " + student.MiddleName + " " + student.LastName;
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var StudentFeeConcessionDTO = new StudentFeeConcessionDTO()
                {
                    AcademicYearID = entity.AcademicYearID,
                    StudentFeeConcessionIID = entity.StudentFeeConcessionIID,
                    ConcessionDate = entity.ConcessionDate,
                    //SchoolID = entity.SchoolID,
                    StudentID = entity.StudentID,
                    StudentName = studentName,
                    StaffID = entity.StaffID,
                    Staff = entity.StaffID.HasValue ? new KeyValueDTO() { Key = entity.StaffID.Value.ToString(), Value = entity.Employee.EmployeeCode + " " + "-" + " " + entity.Employee.EmployeeName } : new KeyValueDTO(),
                    ParentID = entity.ParentID,
                    Parent = entity.ParentID.HasValue ? new KeyValueDTO() { Key = entity.ParentID.Value.ToString(), Value = entity.Parent.ParentCode + " " + entity.Parent.FatherFirstName } : new KeyValueDTO(),

                    ConcessionApprovalTypeID = entity.ConcessionApprovalTypeID,

                    //AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.Description + " " + "( " + entity.AcademicYear.AcademicYearCode + ")" } : new KeyValueDTO(),
                };

                StudentFeeConcessionDTO.StudentFeeConcessionDetail = new List<StudentFeeConcessionDetailDTO>();

                var feeDetails = dbContext.StudentFeeConcessions.Where(ssm => ssm.StudentID == entity.StudentID && ssm.AcademicYearID == entity.AcademicYearID
                && ssm.StaffID == entity.StaffID && ssm.ParentID == entity.ParentID)
                    .Include(i => i.FeeMaster)
                    .Include(i => i.FeePeriod)
                    .AsNoTracking()
                    .ToList();

                foreach (var data in feeDetails)
                {
                    if (data.ConcessionAmount.HasValue)
                    {
                        StudentFeeConcessionDTO.StudentFeeConcessionDetail.Add(new StudentFeeConcessionDetailDTO()
                        {
                            StudentFeeConcessionID = data.StudentFeeConcessionIID,
                            FeeDueFeeTypeMapsID = data.FeeDueFeeTypeMapsID,
                            StudentFeeDueID = data.StudentFeeDueID,
                            PercentageAmount = data.PercentageAmount,
                            ConcessionAmount = data.ConcessionAmount,
                            DueAmount = data.DueAmount,
                            NetAmount = data.NetAmount,

                            FeeInvoice = GetInvoiceNoFromID(data.StudentFeeDueID),
                            FeeMasterID = data.FeeMasterID,
                            FeeMaster = new KeyValueDTO()
                            {
                                Key = data.FeeMasterID.ToString(),
                                Value = data.FeeMaster.Description
                            },
                            FeePeriodID = data.FeePeriodID,
                            FeePeriod = data.FeePeriodID.HasValue ? new KeyValueDTO()
                            {
                                Key = data.FeePeriodID.HasValue ? Convert.ToString(data.FeePeriodID) : null,
                                Value = !data.FeePeriodID.HasValue ? null : data.FeePeriod.Description + " ( " + data.FeePeriod.PeriodFrom.ToString(dateFormat, CultureInfo.InvariantCulture) + '-'
                                        + data.FeePeriod.PeriodTo.ToString(dateFormat, CultureInfo.InvariantCulture) + " ) "
                            } : new KeyValueDTO(),
                            CreditNoteID = data.CreditNoteID
                        });
                    }
                }

                return StudentFeeConcessionDTO;
            }
        }
        private KeyValueDTO GetInvoiceNoFromID(long? studentFeeDueID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDues = dbContext.StudentFeeDues.Where(x => x.StudentFeeDueIID == studentFeeDueID).AsNoTracking().FirstOrDefault();
                return new KeyValueDTO() { Key = feeDues.StudentFeeDueIID.ToString(), Value = feeDues.InvoiceNo };
            }
        }

        private List<FeeDueMonthlySplitDTO> GetFeeDueMonthlySplit(long studentFeeDueID, int? feeMasterID, int? feePeriodID, long? creditNoteID)
        {
            var lstMonthlySplit = new List<FeeDueMonthlySplitDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {

                lstMonthlySplit = (from n in dbContext.FeeDueMonthlySplits
                                   join FeeTypeMap in dbContext.FeeDueFeeTypeMaps on n.FeeDueFeeTypeMapsID
                                   equals FeeTypeMap.FeeDueFeeTypeMapsIID
                                   where FeeTypeMap.StudentFeeDueID == studentFeeDueID && FeeTypeMap.Status != true && n.Status != true
                                   && FeeTypeMap.FeeMasterID == feeMasterID && (feePeriodID == null || FeeTypeMap.FeePeriodID == feePeriodID)
                                   orderby n.Year, n.MonthID
                                   select new FeeDueMonthlySplitDTO
                                   {
                                       FeeStructureMontlySplitMapID = n.FeeStructureMontlySplitMapID ?? 0,
                                       FeeDueMonthlySplitIID = n.FeeDueMonthlySplitIID,
                                       FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                                       MonthID = n.MonthID,
                                       Amount = n.Amount ?? 0,
                                       Year = n.Year ?? 0,
                                       CreditNoteAmount = 0,
                                       Balance = n.Amount ?? 0,
                                       CreditNoteFeeTypeMapID = 0,
                                       TaxAmount = n.TaxAmount ?? 0,
                                       TaxPercentage = n.TaxPercentage ?? 0
                                   }).AsNoTracking().ToList();

                if (lstMonthlySplit.Count() == 0)
                {
                    lstMonthlySplit = (from n in dbContext.FeeDueFeeTypeMaps
                                       where n.StudentFeeDueID == studentFeeDueID
                                       select new FeeDueMonthlySplitDTO
                                       {
                                           FeeStructureMontlySplitMapID = 0,
                                           FeeDueMonthlySplitIID = 0,
                                           FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsIID,
                                           MonthID = 0,
                                           Amount = n.Amount ?? 0,
                                           Year = 0,
                                           CreditNoteAmount = 0,
                                           Balance = n.Amount ?? 0,
                                           CreditNoteFeeTypeMapID = 0
                                       }).AsNoTracking().ToList();
                }

            }
            CheckCreditNoteExist(lstMonthlySplit, creditNoteID);

            return lstMonthlySplit;
        }


        private void CheckCreditNoteExist(List<FeeDueMonthlySplitDTO> lstMonthlySplit, long? creditNoteID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var SchoolCreditNotelst = new List<SchoolCreditNote>();
                var FeeTypeIdMonthlySplits = new List<long>();
                var feedueFeeTypeIds = new List<long>();
                feedueFeeTypeIds.AddRange(lstMonthlySplit.Select(x => x.FeeDueFeeTypeMapsID).ToList());

                SchoolCreditNotelst = (from stFee in dbContext.SchoolCreditNotes
                                       join cft in dbContext.CreditNoteFeeTypeMaps
                                       on stFee.SchoolCreditNoteIID equals cft.SchoolCreditNoteID
                                       where stFee.SchoolCreditNoteIID != creditNoteID
                                       && feedueFeeTypeIds.Contains(cft.FeeDueFeeTypeMapsID.Value)
                                       select stFee).AsNoTracking().ToList();

                if (SchoolCreditNotelst.Count() != 0)
                    throw new Exception("Creditnote/Concession already exist for this invoice!");

            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentFeeConcessionDTO;

            var entity = new StudentFeeConcession();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.StudentFeeConcessionDetail == null || toDto.StudentFeeConcessionDetail.Count() <= 0)
                {
                    throw new Exception("Please enter concession amount!");
                }
                else
                {
                    var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate(toDto.SchoolID == null ? (long?)_context.SchoolID : toDto.SchoolID);


                    if (monthlyClosingDate.HasValue && monthlyClosingDate.Value.Year > 1900 && toDto.ConcessionDate.Value.Date <= monthlyClosingDate.Value.Date)
                    {
                        throw new Exception("This Transaction could not be saved due to monthly closing");
                    }

                    var defaultDecimalPlaces = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_DECIMALPOINTS");
                    if (defaultDecimalPlaces.IsNull())
                    {
                        throw new Exception("There is no defaultDecimalPlaces in settings!");
                    }
                    var defaultDecimalNos = int.Parse(defaultDecimalPlaces);


                    //foreach (var stud in toDto.StudentList)
                    //{
                    var classID = dbContext.Students.Where(x => x.StudentIID == toDto.StudentID).AsNoTracking().Select(y => y.ClassID).FirstOrDefault();
                    CreateCreditNotewithFeeDueMonthlyDetails(toDto.StudentFeeConcessionDetail, classID.Value, toDto.StudentID.Value, null, DateTime.Now, defaultDecimalNos);


                    foreach (var detail in toDto.StudentFeeConcessionDetail)
                    {
                        entity = new StudentFeeConcession()
                        {
                            StudentFeeConcessionIID = detail.StudentFeeConcessionID,
                            ConcessionDate = toDto.ConcessionDate,
                            SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                            AcademicYearID = toDto.AcademicYearID,
                            StudentID = toDto.StudentID,
                            StaffID = toDto.StaffID,
                            ParentID = toDto.ParentID,
                            ConcessionApprovalTypeID = toDto.ConcessionApprovalTypeID,
                            FeePeriodID = detail.FeePeriodID,
                            FeeMasterID = detail.FeeMasterID,
                            CreditNoteID = detail.CreditNoteID,
                            PercentageAmount = detail.PercentageAmount,
                            FeeDueFeeTypeMapsID = detail.FeeDueFeeTypeMapsID,
                            StudentFeeDueID = detail.StudentFeeDueID,
                            ConcessionAmount = detail.ConcessionAmount,
                            DueAmount = detail.DueAmount,
                            NetAmount = detail.NetAmount,
                            StudentGroupFeeMasterID = toDto.StudentGroupFeeMasterID,
                            CreatedBy = detail.StudentFeeConcessionID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = detail.StudentFeeConcessionID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = detail.StudentFeeConcessionID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = detail.StudentFeeConcessionID > 0 ? DateTime.Now : dto.UpdatedDate,
                        };


                        dbContext.StudentFeeConcessions.Add(entity);

                        if (entity.StudentFeeConcessionIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.StudentFeeConcessionIID));
                //}
            }
        }

        private string CreateCreditNotewithFeeDueMonthlyDetails(List<StudentFeeConcessionDetailDTO> toDto,
            int classID, long studentID, long? CreditNoteID, DateTime? creditNoteDate, int defaultDecimalNos)
        {
            toDto.All(w =>
            {
                List<FeeDueMonthlySplitDTO> feeDueMonthlySplitDTO
                = GetFeeDueMonthlySplit(w.StudentFeeDueID.Value, w.FeeMasterID, w.FeePeriodID, w.CreditNoteID);
                w.CreditNoteID = CreateCreditNote(feeDueMonthlySplitDTO, w, classID, studentID, w.CreditNoteID, creditNoteDate, w.FeeMasterID, w.FeePeriodID, defaultDecimalNos);
                return true;
            });
            return null;
        }
        private long? CreateCreditNote(List<FeeDueMonthlySplitDTO> feeDueMonthlySplitDTO, StudentFeeConcessionDetailDTO w,
            int classID, long studentID, long? CreditNoteID, DateTime? creditNoteDate
            , int? feeMasterID, int? feePeriodID, int defaultDecimalNos)
        {
            decimal? dedAmount = 0;
            var dto = new SchoolCreditNoteDTO();
            dto.Description = "From Concession";
            dto.SchoolCreditNoteIID = CreditNoteID ?? 0;

            dto.CreditNoteFeeTypeMapDTO = new List<CreditNoteFeeTypeDTO>();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            List<KeyValueDTO> studentList = new List<KeyValueDTO>();

            dto.Student = new KeyValueDTO { Key = studentID.ToString(), Value = null };
            dto.ClassID = classID;
            dto.CreditNoteDate = creditNoteDate;
            dto.IsDebitNote = false;
            dto.SchoolID = (byte)_context.SchoolID;
            dto.AcademicYearID = (int)_context.AcademicYearID;
            if (dto.SchoolCreditNoteIID == 0)
            {
                dto.CreatedBy = (int)_context.LoginID;
                dto.CreatedDate = DateTime.Now;
            }
            else
            {
                dto.UpdatedBy = (int)_context.LoginID;
                dto.UpdatedDate = DateTime.Now;
            }

            var feeDueMonthlySplit = GetFormatAmount(feeDueMonthlySplitDTO, w.ConcessionAmount.Value, defaultDecimalNos);

            foreach (var data in feeDueMonthlySplit)
            {
                if (data.Amount.HasValue)
                {
                    dto.CreditNoteFeeTypeMapDTO.Add(new CreditNoteFeeTypeDTO()
                    {
                        CreditNoteFeeTypeMapIID = w.CreditNoteFeeTypeMapID ?? 0,
                        SchoolCreditNoteID = w.CreditNoteID,
                        FeeMasterID = feeMasterID,
                        Amount = data.Amount ?? 0,
                        MonthID = data.MonthID,
                        YearID = data.Year,
                        FeePeriodID = feePeriodID,
                        FeeDueFeeTypeMapsID = data.FeeDueFeeTypeMapsID,
                        FeeDueMonthlySplitID = data.FeeDueMonthlySplitIID
                    });
                }
            }

            w.CreditNoteID = new CreditNoteMapper().SaveCreditNoteFromConcession(dto);
            return w.CreditNoteID;

        }
        private List<FeeDueMonthlySplitDTO> GetFormatAmount(List<FeeDueMonthlySplitDTO> feeDueMonthlySplitDTO, decimal concessionAmount, int defaultDecimalNos)
        {
            decimal sum = 0;
            for (var i = 0; i < feeDueMonthlySplitDTO.Count(); i++)
            {
                if (i == (feeDueMonthlySplitDTO.Count() - 1))
                {
                    feeDueMonthlySplitDTO[i].Amount = decimal.Round(concessionAmount - sum, 0, MidpointRounding.AwayFromZero);
                    break;
                }
                else
                {
                    feeDueMonthlySplitDTO[i].Amount = decimal.Round((concessionAmount / feeDueMonthlySplitDTO.Count()), 0, MidpointRounding.AwayFromZero);
                    sum = sum + feeDueMonthlySplitDTO[i].Amount.Value;
                }
            }
            return feeDueMonthlySplitDTO;
        }


        public List<KeyValueDTO> GetStaffDetailsByStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {

                var staffDetails = (from ssm in dbContext.StudentStaffMaps
                                    join em in dbContext.Employees on ssm.StaffID equals em.EmployeeIID
                                    where ssm.StudentID == studentID
                                    select new KeyValueDTO
                                    {
                                        Key = em.EmployeeIID.ToString(),
                                        Value = em.EmployeeName
                                    }).AsNoTracking().ToList();

                return staffDetails;
            }
        }

        public List<KeyValueDTO> GetStudentDetailsByStaff(long staffID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentDetails = (from ssm in dbContext.StudentStaffMaps
                                      join ss in dbContext.Students on ssm.StudentID equals ss.StudentIID
                                      where ssm.StaffID == staffID
                                      select new KeyValueDTO
                                      {
                                          Key = ss.StudentIID.ToString(),
                                          Value = ss.AdmissionNumber + "-" + ss.FirstName + " " + ss.MiddleName + " " + ss.LastName
                                      }).AsNoTracking().ToList();

                return studentDetails;
            }
        }

        public List<KeyValueDTO> GetParentDetailsByStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var parentDetails = (from ss in dbContext.Students
                                     join pp in dbContext.Parents on ss.ParentID equals pp.ParentIID
                                     where ss.StudentIID == studentID
                                     select new KeyValueDTO
                                     {
                                         Key = pp.ParentIID.ToString(),
                                         Value = pp.ParentCode + "-" + pp.FatherFirstName + " " + pp.FatherMiddleName + " " + pp.FatherLastName
                                     }).AsNoTracking().ToList();

                return parentDetails;
            }
        }

        public decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int? feePeriodID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeAmount = (from sfd in dbContext.StudentFeeDues
                                 join fdft in dbContext.FeeDueFeeTypeMaps on sfd.StudentFeeDueIID equals fdft.StudentFeeDueID /*&& fdft.FeeMasterID equals fc.FeeMasterID*/
                                 where fdft.FeePeriodID == feePeriodID
                                 && sfd.AcadamicYearID == academicYearID
                                 && sfd.StudentId == studentID
                                 select fdft.Amount).ToList().Sum();

                return feeAmount;
            }
        }
        public List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
                var sRetData = new List<StudentFeeConcessionDetailDTO>();

                var concession = dbContext.StudentFeeConcessions.Where(sfc => sfc.StudentID == studentID).AsNoTracking().Select(sfc => sfc.StudentFeeDueID).ToList();

                var feeData = (from sfd in dbContext.StudentFeeDues
                               join fdft in dbContext.FeeDueFeeTypeMaps on sfd.StudentFeeDueIID equals fdft.StudentFeeDueID
                               where sfd.CollectionStatus != true && fdft.Status != true && sfd.IsCancelled != true
                               && sfd.AcadamicYearID == academicYearID
                               && sfd.StudentId == studentID && !concession.Contains(sfd.StudentFeeDueIID)
                               select fdft)
                               .Include(i => i.StudentFeeDue)
                               .Include(i => i.FeeMaster)
                               .Include(i => i.FeePeriod)
                               .AsNoTracking().ToList();

                if (feeData.Any())
                {
                    feeData.All(w => { sRetData.Add(GetFeeMasterData(dbContext, w, dateFormat)); return true; });
                }

                return sRetData;


            }
        }

        private StudentFeeConcessionDetailDTO GetFeeMasterData(dbEduegateSchoolContext sContext, FeeDueFeeTypeMap fData, string dateFormat)
        {
            var data = new StudentFeeConcessionDetailDTO()
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

    }
}