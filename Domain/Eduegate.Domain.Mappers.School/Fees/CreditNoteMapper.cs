using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Eduegate.Framework.Extensions;
using System.Data;
using Eduegate.Domain.Mappers.School.Accounts;
using System.Data.SqlClient;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class CreditNoteMapper : DTOEntityDynamicMapper
    {
        public static CreditNoteMapper Mapper(CallContext context)
        {
            var mapper = new CreditNoteMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SchoolCreditNoteDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SchoolCreditNoteDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SchoolCreditNotes.Where(x => x.SchoolCreditNoteIID == IID)
                    .Include(i => i.CreditNoteFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .Include(i => i.CreditNoteFeeTypeMaps).ThenInclude(i => i.FeePeriod)
                    .Include(i => i.Student)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                List<KeyValueDTO> studentList = new List<KeyValueDTO>();
                var schoolCreditNote = new SchoolCreditNoteDTO()
                {
                    SchoolCreditNoteIID = entity.SchoolCreditNoteIID,
                    CreditNoteDate = entity.CreditNoteDate,
                    CreditNoteNumber = entity.CreditNoteNumber,
                    Status = entity.Status,
                    SectionID = entity.SectionID,
                    //Class = new KeyValueDTO() { Key = entity.ClassID.ToString(), Value = entity.Class.ClassDescription },
                    //Section = entity.SectionID == null ? new KeyValueDTO()
                    //{
                    //    Key = null,
                    //    Value = null
                    //} : new KeyValueDTO() { Key = entity.SectionID.ToString(), Value = entity.Section.SectionName },
                    Student = new KeyValueDTO() { Key = entity.StudentID.ToString(), Value = (entity.Student.AdmissionNumber + '-' + entity.Student.FirstName + ' ' + entity.Student.MiddleName + ' ' + entity.Student.LastName) },
                    Description = entity.Description,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };

                foreach (var feeMasterMap in entity.CreditNoteFeeTypeMaps)
                {
                    if (feeMasterMap.Amount != 0)
                    {
                        schoolCreditNote.CreditNoteFeeTypeMapDTO.Add(new CreditNoteFeeTypeDTO()
                        {
                            CreditNoteFeeTypeMapIID = feeMasterMap.CreditNoteFeeTypeMapIID,
                            SchoolCreditNoteID = feeMasterMap.SchoolCreditNoteID,
                            Amount = feeMasterMap.Amount,
                            FeeMaster = new KeyValueDTO()
                            {
                                Key = feeMasterMap.FeeMasterID.ToString(),
                                Value = feeMasterMap.FeeMaster.Description
                            },
                            Status = feeMasterMap.Status,
                            FeePeriodID = feeMasterMap.PeriodID,
                            FeePeriod = feeMasterMap.PeriodID == null ? new KeyValueDTO()
                            {
                                Key = null,
                                Value = null
                            } : new KeyValueDTO()
                            {
                                Key = feeMasterMap.PeriodID.ToString(),
                                Value = feeMasterMap.FeePeriod.Description
                            },
                            MonthID = feeMasterMap.MonthID,
                            YearID = feeMasterMap.Year,
                            Years = feeMasterMap.Year == null ? new KeyValueDTO()
                            {
                                Key = null,
                                Value = null
                            } : new KeyValueDTO()
                            {
                                Key = feeMasterMap.Year.ToString(),
                                Value = feeMasterMap.Year.ToString()
                            },
                            Months = feeMasterMap.MonthID == null ? new KeyValueDTO()
                            {
                                Key = null,
                                Value = null
                            } : new KeyValueDTO()
                            {
                                Key = feeMasterMap.MonthID.ToString(),
                                Value = new DateTime(feeMasterMap.Year.Value, feeMasterMap.MonthID.Value, 1).ToString("MMM")
                            },
                            InvoiceNo = feeMasterMap.FeeDueFeeTypeMapsID == null ? new KeyValueDTO()
                            {
                                Key = null,
                                Value = null
                            } : GetInvoiceNoFromID(feeMasterMap.FeeDueFeeTypeMapsID),
                            FeeDueFeeTypeMapsID = feeMasterMap.FeeDueFeeTypeMapsID,
                            FeeDueMonthlySplitID = feeMasterMap.FeeDueMonthlySplitID
                        });
                    }
                }

                return schoolCreditNote;
            }
        }

        private KeyValueDTO GetInvoiceNoFromID(long? feedueFeeTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeDues = dbContext.FeeDueFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsIID == feedueFeeTypeID).AsNoTracking().Select(x => x.StudentFeeDue).FirstOrDefault();
                return new KeyValueDTO() { Key = feeDues.StudentFeeDueIID.ToString(), Value = feeDues.InvoiceNo };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SchoolCreditNoteDTO;
            string message = string.Empty;
            var studentAccountsId = "0";
            int? documentTypeID;
            int? studentCostCenter;
            var _sLstFee_IDs = new List<int>();
            var feeData = new List<FeeMaster>();


            if (toDto.SchoolCreditNoteIID != 0)
            {
                throw new Exception("Sorry, edit is not possible for this screen !");
            }

            if (toDto.CreditNoteFeeTypeMapDTO == null || toDto.CreditNoteFeeTypeMapDTO.Count == 0)
            {
                throw new Exception("Fee details needs to be filled!");
            }
            else if (toDto.CreditNoteFeeTypeMapDTO.Where(i => i.FeeMasterID != null && (i.Amount == null || i.Amount <= 0)).Count() > 0)
            {
                throw new Exception("Amount cannot be empty ,zero or negative value!");
            }

            _sLstFee_IDs = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.FeeMasterID.Value).ToList();

            var monthlyClosingDate = new MonthlyClosingMapper().GetMonthlyClosingDate((long?)_context.SchoolID);

            if (monthlyClosingDate.HasValue && monthlyClosingDate.Value.Year > 1900 && toDto.CreditNoteDate.Value.Date <= monthlyClosingDate.Value.Date)

            {
                throw new Exception("This Transaction could not be saved due to monthly closing");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var lstFeeDueFeeTypeMapsIDs = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.FeeDueFeeTypeMapsID).ToList();
                var feeDueFeeTypeMaps = dbContext.FeeDueFeeTypeMaps.Where(x => lstFeeDueFeeTypeMapsIDs.Contains(x.FeeDueFeeTypeMapsIID)).Include(x => x.StudentFeeDue).AsNoTracking().ToList();

                var invoiceDates = feeDueFeeTypeMaps.Select(x => x.StudentFeeDue.InvoiceDate).ToList();
                if (invoiceDates.Where(x => x.Value.Date > toDto.CreditNoteDate.Value.Date).Count() > 0)
                {
                    throw new Exception("Credit Note date should be higher or equal to the invoice dates. Please check selected invoice dates!");
                }

                studentAccountsId = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_ACCOUNT_ID", 1, null);

                feeData = (from fee in dbContext.FeeMasters where _sLstFee_IDs.Contains(fee.FeeMasterID) select fee).AsNoTracking().ToList();
                if (feeData.Any(x => !x.LedgerAccountID.HasValue))
                {
                    message = "Ledger Account ID not set for the fees";
                    throw new Exception(message);
                }

                documentTypeID = (from st in dbContext.DocumentTypes.Where(s => s.TransactionTypeName.ToUpper() == "FEE CREDIT NOTE").AsNoTracking() select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                    throw new Exception("Document Type 'FEE CREDIT NOTE' is not available.Please check");

                studentCostCenter = (from cls in dbContext.Classes.AsNoTracking() where cls.ClassID == toDto.ClassID select cls.CostCenterID).FirstOrDefault();


            }

            if (toDto.SchoolCreditNoteIID != 0)
            {
                if (toDto.Status == true)
                    throw new Exception("Transaction already done. Cannot be edited!");

                SaveCreditNote(toDto, long.Parse(studentAccountsId), documentTypeID.Value, studentCostCenter, feeData);

                return GetEntity(toDto.SchoolCreditNoteIID);
            }
            else
            {
                if (toDto.Student != null && toDto.Student.Key != null)//(toDto.Student.Count > 0)
                {
                    toDto.SchoolCreditNoteIID = StudentwiseMapping(toDto, long.Parse(studentAccountsId), documentTypeID.Value, studentCostCenter, feeData);
                }
            }

            return GetEntity(toDto.SchoolCreditNoteIID);
        }

        public string AllClassStudentsMapping(BaseMasterDTO dto, long studentAccountsId, int documentTypeID, int? studentCostCenter, List<FeeMaster> feeData)
        {
            string message = string.Empty;
            var toDto = dto as SchoolCreditNoteDTO;
            long studentId = 0;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var feeMasterIds = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.FeeMasterID);
                var feePeriods = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.FeePeriodID);
                var months = toDto.CreditNoteFeeTypeMapDTO.Select(y => y.MonthID);
                var years = toDto.CreditNoteFeeTypeMapDTO.Select(z => z.YearID);
                var queryStudentCreditNote = (from stFee in dbContext.SchoolCreditNotes
                                              join cft in dbContext.CreditNoteFeeTypeMaps
                                              on stFee.SchoolCreditNoteIID equals cft.SchoolCreditNoteID
                                              where stFee.ClassID == toDto.ClassID &&
                                              (months.Contains(cft.MonthID)
                                              && years.Contains(cft.Year))
                                              select stFee.StudentID);

                var queryStudent = (dbContext.Students.Where(s => s.ClassID == (toDto.ClassID.Value) &&
                                   s.IsActive == true && !queryStudentCreditNote.Contains(s.StudentIID)
                                   ).AsNoTracking().Select(st => st.StudentIID)).ToList();

                if (queryStudent.Count > 0)
                {
                    foreach (var stuDdet in queryStudent)
                    {
                        studentId = stuDdet;
                        var entity = new SchoolCreditNote()
                        {
                            //ClassID = toDto.ClassID,
                            //SectionID = toDto.SectionID,
                            StudentID = studentId,
                            Status = false,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            CreditNoteDate = toDto.CreditNoteDate,
                            Description = toDto.Description,
                            SchoolCreditNoteIID = toDto.SchoolCreditNoteIID,
                            IsDebitNote = toDto.IsDebitNote

                        };
                        if (toDto.SchoolCreditNoteIID == 0)
                        {
                            entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                            entity.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            entity.CreatedBy = toDto.CreatedBy;
                            entity.CreatedDate = toDto.CreatedDate;
                            entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                            entity.UpdatedDate = DateTime.Now;
                        }
                        entity.Amount = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.Amount).Sum();
                        entity.CreditNoteFeeTypeMaps = new List<CreditNoteFeeTypeMap>();

                        foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                        {
                            if (feeTypeDto.Amount.HasValue)
                            {
                                entity.CreditNoteFeeTypeMaps.Add(new CreditNoteFeeTypeMap()
                                {
                                    CreditNoteFeeTypeMapIID = feeTypeDto.CreditNoteFeeTypeMapIID,
                                    SchoolCreditNoteID = toDto.SchoolCreditNoteIID,
                                    FeeMasterID = feeTypeDto.FeeMasterID,
                                    Amount = feeTypeDto.Amount,
                                    Status = false,
                                    CreatedBy = (int)_context.LoginID,
                                    CreatedDate = DateTime.Now,
                                    PeriodID = feeTypeDto.FeePeriodID,
                                    MonthID = feeTypeDto.MonthID,
                                    Year = feeTypeDto.YearID

                                });
                            }
                        }

                        if (entity.SchoolCreditNoteIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            dbContext.SaveChanges();
                        }

                        toDto.SchoolCreditNoteIID = entity.SchoolCreditNoteIID;

                        AccountPosting(toDto.SchoolCreditNoteIID, toDto.CreditNoteDate, (int)_context.LoginID);
                    }
                }
                else
                {
                    return "0#There is no student found in this class to set creditnote for the selected months.";
                }
            }

            return "1#Saved successfully.";
        }

        public long StudentwiseMapping(BaseMasterDTO dto, long studentAccountsId, int documentTypeID, int? studentCostCenter, List<FeeMaster> feeData)
        {
            string message = string.Empty;
            var toDto = dto as SchoolCreditNoteDTO;
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            var creditNoteIID = toDto.SchoolCreditNoteIID;

            long studentId = 0;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sequenceType = toDto.IsDebitNote == true ? "DebitNoteNo" : "CrediNoteNo";
                if (string.IsNullOrEmpty(toDto.CreditNoteNumber))
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence(sequenceType, null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence for " + sequenceType);
                    }
                }

                studentId = long.Parse(toDto.Student.Key);
                toDto.ClassID = (dbContext.Students.Where(s => s.StudentIID == studentId).AsNoTracking().Select(st => st.ClassID)).FirstOrDefault();

                var feedueFeeTypeIds = new List<long?>();
                var FeeTypeIdMonthlySplits = new List<long?>();
                foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                {
                    if (feeTypeDto.Amount.HasValue)
                    {

                        feedueFeeTypeIds.Add(feeTypeDto.FeeDueFeeTypeMapsID);
                        FeeTypeIdMonthlySplits.Add(feeTypeDto.FeeDueMonthlySplitID);
                    }
                }
                var entity = new SchoolCreditNote()
                {
                    ClassID = toDto.ClassID,
                    //SectionID = toDto.SectionID,
                    StudentID = studentId,
                    Status = false,
                    CreditNoteDate = toDto.CreditNoteDate,
                    Description = toDto.Description,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                    SchoolCreditNoteIID = toDto.SchoolCreditNoteIID,
                    IsDebitNote = toDto.IsDebitNote,
                    CreditNoteNumber = string.IsNullOrEmpty(toDto.CreditNoteNumber) ? sequence.Prefix + sequence.LastSequence : toDto.CreditNoteNumber
                };

                if (toDto.SchoolCreditNoteIID == 0)
                {
                    entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = toDto.CreatedDate;
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;
                }
                entity.Amount = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.Amount).Sum();
                entity.CreditNoteFeeTypeMaps = new List<CreditNoteFeeTypeMap>();

                foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                {
                    if (feeTypeDto.Amount.HasValue)
                    {
                        entity.CreditNoteFeeTypeMaps.Add(new CreditNoteFeeTypeMap()
                        {
                            CreditNoteFeeTypeMapIID = feeTypeDto.CreditNoteFeeTypeMapIID,
                            SchoolCreditNoteID = toDto.SchoolCreditNoteIID,
                            FeeMasterID = feeTypeDto.FeeMasterID,
                            Amount = feeTypeDto.Amount,
                            Status = false,
                            CreatedBy = toDto.SchoolCreditNoteIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            CreatedDate = toDto.SchoolCreditNoteIID == 0 ? DateTime.Now : dto.CreatedDate,
                            PeriodID = feeTypeDto.FeePeriodID == 0 ? null : feeTypeDto.FeePeriodID,
                            MonthID = feeTypeDto.MonthID == 0 ? null : feeTypeDto.MonthID,
                            Year = feeTypeDto.YearID == 0 ? null : feeTypeDto.YearID,
                            FeeDueFeeTypeMapsID = feeTypeDto.FeeDueFeeTypeMapsID == 0 ? null : feeTypeDto.FeeDueFeeTypeMapsID,
                            FeeDueMonthlySplitID = feeTypeDto.FeeDueMonthlySplitID == 0 ? null : feeTypeDto.FeeDueMonthlySplitID
                        });
                    }
                }

                dbContext.SchoolCreditNotes.Add(entity);
                if (entity.SchoolCreditNoteIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();
                }

                toDto.SchoolCreditNoteIID = entity.SchoolCreditNoteIID;
                creditNoteIID = entity.SchoolCreditNoteIID;

                AccountPosting(toDto.SchoolCreditNoteIID, toDto.CreditNoteDate, (int)_context.LoginID);

            }
            return creditNoteIID;
        }

        public string SaveCreditNote(BaseMasterDTO dto, long studentAccountsId, int documentTypeID, int? studentCostCenter, List<FeeMaster> feeData)
        {
            string message = string.Empty;
            var toDto = dto as SchoolCreditNoteDTO;
            long studentId = 0;
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //var queryStudent = (from st in dbContext.Students.Where(s => s.ClassID == (toDto.ClassID.Value) && s.IsActive == true) select st.StudentIID).ToList();
                studentId = long.Parse(toDto.Student.Key);
                toDto.ClassID = (dbContext.Students.Where(s => s.StudentIID == studentId).AsNoTracking().Select(st => st.ClassID)).FirstOrDefault();

                var feedueFeeTypeIds = new List<long?>();
                var FeeTypeIdMonthlySplits = new List<long?>();
                foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                {
                    if (feeTypeDto.Amount.HasValue)
                    {

                        feedueFeeTypeIds.Add(feeTypeDto.FeeDueFeeTypeMapsID);
                        FeeTypeIdMonthlySplits.Add(feeTypeDto.FeeDueMonthlySplitID);
                    }
                }

                var SchoolCreditNotelst = new List<SchoolCreditNote>();
                var CreditNoteMonthlylst = new List<SchoolCreditNote>();

                SchoolCreditNotelst = (from stFee in dbContext.SchoolCreditNotes
                                       join cft in dbContext.CreditNoteFeeTypeMaps
                                       on stFee.SchoolCreditNoteIID equals cft.SchoolCreditNoteID
                                       where stFee.SchoolCreditNoteIID != toDto.SchoolCreditNoteIID && stFee.StudentID == studentId
                                       && feedueFeeTypeIds.Contains(cft.FeeDueFeeTypeMapsID)
                                       select stFee).AsNoTracking().ToList();

                if (SchoolCreditNotelst.Count() != 0)
                {
                    CreditNoteMonthlylst = (from stFee in dbContext.SchoolCreditNotes
                                            join cft in dbContext.CreditNoteFeeTypeMaps
                                            on stFee.SchoolCreditNoteIID equals cft.SchoolCreditNoteID
                                            where stFee.SchoolCreditNoteIID != toDto.SchoolCreditNoteIID && stFee.StudentID == studentId
                                            && feedueFeeTypeIds.Contains(cft.FeeDueFeeTypeMapsID)
                                            select stFee).AsNoTracking().ToList();
                }

                if (SchoolCreditNotelst.Count() == 0 && CreditNoteMonthlylst.Count() == 0)
                {
                    var entity = new SchoolCreditNote()
                    {
                        ClassID = toDto.ClassID,
                        //SectionID = toDto.SectionID,
                        StudentID = studentId,
                        Status = false,
                        CreditNoteDate = toDto.CreditNoteDate,
                        Description = toDto.Description,
                        SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                        AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                        SchoolCreditNoteIID = toDto.SchoolCreditNoteIID,
                        IsDebitNote = toDto.IsDebitNote,
                        CreditNoteNumber = toDto.CreditNoteNumber
                    };

                    if (toDto.SchoolCreditNoteIID == 0)
                    {
                        entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.CreatedBy = toDto.CreatedBy;
                        entity.CreatedDate = toDto.CreatedDate;
                        entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                        entity.UpdatedDate = DateTime.Now;
                    }

                    entity.Amount = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.Amount).Sum();
                    //get existing parent iids
                    var parentRowIIDs = toDto.CreditNoteFeeTypeMapDTO.Select(s => s.CreditNoteFeeTypeMapIID).ToList();
                    //get existing parent entities
                    var parentRowEntities = dbContext.CreditNoteFeeTypeMaps.Where(x =>
                        x.SchoolCreditNoteID == entity.SchoolCreditNoteIID &&
                        !parentRowIIDs.Contains(x.CreditNoteFeeTypeMapIID)).AsNoTracking().ToList();

                    //delete parent maps
                    if (parentRowEntities != null && parentRowEntities.Count > 0)
                    {

                        dbContext.CreditNoteFeeTypeMaps.RemoveRange(parentRowEntities);
                    }

                    entity.CreditNoteFeeTypeMaps = new List<CreditNoteFeeTypeMap>();
                    foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                    {
                        if (feeTypeDto.Amount.HasValue)
                        {
                            entity.CreditNoteFeeTypeMaps.Add(new CreditNoteFeeTypeMap()
                            {
                                CreditNoteFeeTypeMapIID = feeTypeDto.CreditNoteFeeTypeMapIID,
                                SchoolCreditNoteID = toDto.SchoolCreditNoteIID,
                                FeeMasterID = feeTypeDto.FeeMasterID,
                                Amount = feeTypeDto.Amount,
                                Status = false,
                                UpdatedBy = (int)_context.LoginID,
                                UpdatedDate = DateTime.Now,
                                PeriodID = feeTypeDto.FeePeriodID == 0 ? null : feeTypeDto.FeePeriodID,
                                MonthID = feeTypeDto.MonthID == 0 ? null : feeTypeDto.MonthID,
                                Year = feeTypeDto.YearID == 0 ? null : feeTypeDto.YearID,
                                FeeDueFeeTypeMapsID = feeTypeDto.FeeDueFeeTypeMapsID == 0 ? null : feeTypeDto.FeeDueFeeTypeMapsID,
                                FeeDueMonthlySplitID = feeTypeDto.FeeDueMonthlySplitID == 0 ? null : feeTypeDto.FeeDueMonthlySplitID,
                            });
                        }
                    }

                    dbContext.SchoolCreditNotes.Add(entity);
                    if (entity.SchoolCreditNoteIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var creditMap in entity.CreditNoteFeeTypeMaps)
                        {
                            if (creditMap.CreditNoteFeeTypeMapIID != 0)
                            {
                                dbContext.Entry(creditMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(creditMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    toDto.SchoolCreditNoteIID = entity.SchoolCreditNoteIID;

                    AccountPosting(toDto.SchoolCreditNoteIID, toDto.CreditNoteDate, (int)_context.LoginID);
                }
                else
                {
                    return "0#Credit Note already exist for this month.";
                }
            }

            return "1#Saved successfully.";
        }

        public long? SaveCreditNoteFromConcession(SchoolCreditNoteDTO toDto)
        {
            if (toDto.CreditNoteFeeTypeMapDTO == null || toDto.CreditNoteFeeTypeMapDTO.Count == 0)
            {
                throw new Exception("Fee details needs to be filled!");
            }
            else if (toDto.CreditNoteFeeTypeMapDTO.Where(i => i.FeeMasterID != null && (i.Amount == null || i.Amount <= 0)).Count() > 0)
            {
                throw new Exception("Amount cannot be empty ,zero or negative value!");
            }

            string message = string.Empty;
            int? documentTypeID;
            int? studentCostCenter;
            var _sLstFee_IDs = new List<int>();
            var feeData = new List<FeeMaster>();

            var studentAccountsId = new Domain.Setting.SettingBL(null).GetSettingValue<long>("STUDENT_ACCOUNT_ID", 1, 0);

            _sLstFee_IDs = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.FeeMasterID.Value).ToList();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                feeData = (from fee in dbContext.FeeMasters where _sLstFee_IDs.Contains(fee.FeeMasterID) select fee).AsNoTracking().ToList();
                if (feeData.Any(x => !x.LedgerAccountID.HasValue))
                {
                    message = "Ledger Account ID not set for the fees";
                    throw new Exception(message);
                }

                documentTypeID = (from st in dbContext.DocumentTypes.Where(s => s.TransactionTypeName.ToUpper() == "FEE CREDIT NOTE").AsNoTracking() select st.DocumentTypeID).FirstOrDefault();
                if (documentTypeID.IsNull())
                    throw new Exception("Document Type 'FEE CREDIT NOTE' is not available.Please check");

                studentCostCenter = (from cls in dbContext.Classes.AsNoTracking() where cls.ClassID == toDto.ClassID select cls.CostCenterID).FirstOrDefault();

                if (toDto.SchoolCreditNoteIID != 0)
                    toDto.Status = (from st in dbContext.SchoolCreditNotes.Where(s => s.SchoolCreditNoteIID == toDto.SchoolCreditNoteIID).AsNoTracking() select st.Status).FirstOrDefault();
            }

            if (toDto.SchoolCreditNoteIID != 0)
            {

                if (toDto.Status == true)
                    throw new Exception("Transaction already done. Cannot be edited!");

                return EditCreditNoteFromConcession(toDto, studentAccountsId, documentTypeID.Value, studentCostCenter, feeData);
            }
            else
            {
                if (toDto.Student != null && toDto.Student.Key != null)//(toDto.Student.Count > 0)
                {
                    return SaveCreditnoteFromConcession(toDto, studentAccountsId, documentTypeID.Value, studentCostCenter, feeData);
                }
            }

            return null;
        }

        private long? SaveCreditnoteFromConcession(SchoolCreditNoteDTO toDto, long studentAccountsId, int documentTypeID, int? studentCostCenter, List<FeeMaster> feeData)
        {
            string message = string.Empty;

            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            long studentId = 0;
            int? loginBy = 0;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var sequenceType = toDto.IsDebitNote == true ? "DebitNoteNo" : "CrediNoteNo";
                if (string.IsNullOrEmpty(toDto.CreditNoteNumber))
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence(sequenceType, null);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }

                //var queryStudent = (from st in dbContext.Students.Where(s => s.ClassID == (toDto.ClassID.Value) && s.IsActive == true) select st.StudentIID).ToList();
                studentId = long.Parse(toDto.Student.Key);
                toDto.ClassID = (dbContext.Students.Where(s => s.StudentIID == studentId).AsNoTracking().Select(st => st.ClassID)).FirstOrDefault();

                var feedueFeeTypeIds = new List<long?>();
                var FeeTypeIdMonthlySplits = new List<long?>();

                foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                {
                    if (feeTypeDto.Amount.HasValue)
                    {

                        feedueFeeTypeIds.Add(feeTypeDto.FeeDueFeeTypeMapsID);
                        FeeTypeIdMonthlySplits.Add(feeTypeDto.FeeDueMonthlySplitID);
                    }
                }

                var SchoolCreditNotelst = new List<SchoolCreditNote>();
                SchoolCreditNotelst = (from stFee in dbContext.SchoolCreditNotes
                                       join cft in dbContext.CreditNoteFeeTypeMaps
                                       on stFee.SchoolCreditNoteIID equals cft.SchoolCreditNoteID
                                       where stFee.SchoolCreditNoteIID != toDto.SchoolCreditNoteIID && stFee.StudentID == studentId
                                       && feedueFeeTypeIds.Contains(cft.FeeDueFeeTypeMapsID)
                                       select stFee).AsNoTracking().ToList();

                if ((SchoolCreditNotelst.IsNull() || SchoolCreditNotelst.Count() == 0))
                {
                    //foreach (var stuDdet in queryStudent)
                    //{
                    //studentId = stuDdet;

                    var entity = new SchoolCreditNote()
                    {
                        ClassID = toDto.ClassID,
                        //SectionID = toDto.SectionID,
                        StudentID = studentId,
                        Status = false,
                        CreditNoteDate = toDto.CreditNoteDate,
                        Description = toDto.Description,
                        SchoolID = toDto.SchoolID,
                        AcademicYearID = toDto.AcademicYearID,
                        SchoolCreditNoteIID = toDto.SchoolCreditNoteIID,
                        IsDebitNote = toDto.IsDebitNote,
                        CreditNoteNumber = string.IsNullOrEmpty(toDto.CreditNoteNumber) ? sequence.Prefix + sequence.LastSequence : toDto.CreditNoteNumber
                    };

                    if (toDto.SchoolCreditNoteIID == 0)
                    {
                        entity.CreatedBy = toDto.CreatedBy;
                        entity.CreatedDate = DateTime.Now;
                        loginBy = toDto.CreatedBy;
                    }
                    else
                    {
                        loginBy = toDto.UpdatedBy;
                        entity.UpdatedBy = toDto.UpdatedBy;
                        entity.UpdatedDate = DateTime.Now;
                    }

                    entity.Amount = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.Amount).Sum();

                    entity.CreditNoteFeeTypeMaps = new List<CreditNoteFeeTypeMap>();
                    foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                    {
                        if (feeTypeDto.Amount.HasValue)
                        {
                            entity.CreditNoteFeeTypeMaps.Add(new CreditNoteFeeTypeMap()
                            {
                                CreditNoteFeeTypeMapIID = feeTypeDto.CreditNoteFeeTypeMapIID,
                                SchoolCreditNoteID = toDto.SchoolCreditNoteIID,
                                FeeMasterID = feeTypeDto.FeeMasterID,
                                Amount = feeTypeDto.Amount,
                                Status = false,
                                CreatedBy = toDto.CreatedBy,
                                CreatedDate = toDto.SchoolCreditNoteIID == 0 ? DateTime.Now : toDto.CreatedDate,
                                PeriodID = feeTypeDto.FeePeriodID == 0 ? null : feeTypeDto.FeePeriodID,
                                MonthID = feeTypeDto.MonthID == 0 ? null : feeTypeDto.MonthID,
                                Year = feeTypeDto.YearID == 0 ? null : feeTypeDto.YearID,
                                FeeDueFeeTypeMapsID = feeTypeDto.FeeDueFeeTypeMapsID == 0 ? null : feeTypeDto.FeeDueFeeTypeMapsID,
                                FeeDueMonthlySplitID = feeTypeDto.FeeDueMonthlySplitID == 0 ? null : feeTypeDto.FeeDueMonthlySplitID
                            });
                        }
                    }
                    dbContext.SchoolCreditNotes.Add(entity);

                    if (entity.SchoolCreditNoteIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbContext.SaveChanges();
                    }

                    toDto.SchoolCreditNoteIID = entity.SchoolCreditNoteIID;

                    AccountPosting(toDto.SchoolCreditNoteIID, toDto.CreditNoteDate, loginBy);

                    return toDto.SchoolCreditNoteIID;
                }
                else
                {
                    return null;
                }
            }
        }

        private long? EditCreditNoteFromConcession(BaseMasterDTO dto, long studentAccountsId, int documentTypeID, int? studentCostCenter, List<FeeMaster> feeData)
        {
            string message = string.Empty;
            var toDto = dto as SchoolCreditNoteDTO;
            long studentId = 0;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                studentId = long.Parse(toDto.Student.Key);
                toDto.ClassID = (dbContext.Students.Where(s => s.StudentIID == studentId).AsNoTracking().Select(st => st.ClassID)).FirstOrDefault();
                int? loginBy = 0;

                var entity = new SchoolCreditNote()
                {
                    ClassID = toDto.ClassID,
                    //SectionID = toDto.SectionID,
                    StudentID = studentId,
                    Status = false,
                    //CreditNoteDate = toDto.CreditNoteDate,
                    Description = toDto.Description,
                    SchoolID = toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolCreditNoteIID = toDto.SchoolCreditNoteIID,
                    IsDebitNote = toDto.IsDebitNote,
                    CreditNoteNumber = toDto.CreditNoteNumber
                };
                if (toDto.SchoolCreditNoteIID == 0)
                {
                    entity.CreatedBy = toDto.CreatedBy;
                    entity.CreatedDate = DateTime.Now;
                    loginBy = toDto.CreatedBy;
                }
                else
                {
                    entity.UpdatedBy = toDto.UpdatedBy;
                    entity.UpdatedDate = DateTime.Now;
                    loginBy = toDto.UpdatedBy;
                }

                entity.Amount = toDto.CreditNoteFeeTypeMapDTO.Select(x => x.Amount).Sum();
                //get existing parent iids
                var parentRowIIDs = toDto.CreditNoteFeeTypeMapDTO.Select(s => s.CreditNoteFeeTypeMapIID).ToList();
                //get existing parent entities
                var parentRowEntities = dbContext.CreditNoteFeeTypeMaps.Where(x =>
                    x.SchoolCreditNoteID == entity.SchoolCreditNoteIID &&
                    !parentRowIIDs.Contains(x.CreditNoteFeeTypeMapIID)).AsNoTracking().ToList();

                //delete parent maps
                if (parentRowEntities != null && parentRowEntities.Count > 0)
                {
                    dbContext.CreditNoteFeeTypeMaps.RemoveRange(parentRowEntities);
                }
                entity.CreditNoteFeeTypeMaps = new List<CreditNoteFeeTypeMap>();

                foreach (var feeTypeDto in toDto.CreditNoteFeeTypeMapDTO)
                {
                    if (feeTypeDto.Amount.HasValue)
                    {
                        entity.CreditNoteFeeTypeMaps.Add(new CreditNoteFeeTypeMap()
                        {
                            CreditNoteFeeTypeMapIID = feeTypeDto.CreditNoteFeeTypeMapIID,
                            SchoolCreditNoteID = toDto.SchoolCreditNoteIID,
                            FeeMasterID = feeTypeDto.FeeMasterID,
                            Amount = feeTypeDto.Amount,
                            Status = false,
                            UpdatedBy = toDto.UpdatedBy,
                            UpdatedDate = DateTime.Now,
                            PeriodID = feeTypeDto.FeePeriodID == 0 ? null : feeTypeDto.FeePeriodID,
                            MonthID = feeTypeDto.MonthID == 0 ? null : feeTypeDto.MonthID,
                            Year = feeTypeDto.YearID == 0 ? null : feeTypeDto.YearID,
                            FeeDueFeeTypeMapsID = feeTypeDto.FeeDueFeeTypeMapsID == 0 ? null : feeTypeDto.FeeDueFeeTypeMapsID,
                            FeeDueMonthlySplitID = feeTypeDto.FeeDueMonthlySplitID == 0 ? null : feeTypeDto.FeeDueMonthlySplitID,
                        });
                    }
                }

                if (entity.SchoolCreditNoteIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    foreach (var creditMap in entity.CreditNoteFeeTypeMaps)
                    {

                        if (creditMap.CreditNoteFeeTypeMapIID != 0)
                        {
                            dbContext.Entry(creditMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(creditMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                toDto.SchoolCreditNoteIID = entity.SchoolCreditNoteIID;

                AccountPosting(toDto.SchoolCreditNoteIID, toDto.CreditNoteDate, loginBy);

                return toDto.SchoolCreditNoteIID;
            }
        }

        public void AccountPosting(long schoolCreditNoteIID, DateTime? creditNoteDate, int? loginBy)
        {
            new AccountEntryMapper().AccountTransMerge(schoolCreditNoteIID, creditNoteDate.Value, loginBy ?? 0, 4);
        }

        public SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type)
        {
            var creditNoteDTO = new SchoolCreditNoteDTO();

            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetSchoolConnectionString());
            _sBuilder.ConnectTimeout = 30; // Set Timedout
            using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {

                }
                using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_AUTO_CREDIT_NOTE]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@STUDENTID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@STUDENTID"].Value = studentID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SCHOOLID", SqlDbType.TinyInt));
                    adapter.SelectCommand.Parameters["@SCHOOLID"].Value = 30;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@REFERENCE_IDs", SqlDbType.NVarChar));
                    adapter.SelectCommand.Parameters["@REFERENCE_IDs"].Value = accountTransactionHeadIID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@TYPE"].Value = type;

                    try
                    {
                        // Run the stored procedure.
                        var message = Convert.ToString(sqlCommand.ExecuteScalar() ?? string.Empty);

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        conn.Close();
                    }

                    return creditNoteDTO;
                }
            }
        }

        public List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID)
        {
            List<SchoolCreditNoteDTO> dtos = new List<SchoolCreditNoteDTO>();
            using (dbEduegateSchoolContext db = new dbEduegateSchoolContext())
            {
                var lists = (from sc in db.SchoolCreditNotes
                             where sc.StudentID == studentID && sc.IsDebitNote != true && sc.Status != true
                             select sc).AsNoTracking().ToList();

                lists.ForEach(x =>
                {
                    dtos.Add(new SchoolCreditNoteDTO
                    {
                        SchoolCreditNoteIID = x.SchoolCreditNoteIID,
                        CreditNoteNumber = x.CreditNoteNumber,
                        Amount = x.Amount
                    });
                });
            }

            return dtos;
        }

    }
}