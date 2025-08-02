using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Fees
{
    public class FeePaymentMapper : DTOEntityDynamicMapper
    {
        public static FeePaymentMapper Mapper(CallContext context)
        {
            var mapper = new FeePaymentMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FeeCollectionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FeePaymentDTO;
            decimal paidAmount = 0;            
            decimal collectedAmount = 0;
            //if ((toDto.FeeTypes == null || toDto.FeeTypes.Count == 0) && (toDto.FeeFines == null || toDto.FeeFines.Count == 0))
            //{
            //    throw new Exception("The invoice needs to be selected for collecting fees!");
            //}
            //if (toDto.FeeCollectionPaymentModeMapDTO == null || toDto.FeeCollectionPaymentModeMapDTO.Count == 0)
            //{
            //    throw new Exception("The Payment method needs to be selected for collecting fees!");
            //}
            //if (toDto.FeeCollectionPaymentModeMapDTO == null || toDto.FeeCollectionPaymentModeMapDTO.Count == 0)
            //{
            //    throw new Exception("The Payment Amount needs to be selected for collecting fees!");
            //}

            //if (toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount).Sum() <= 0)
            //{
            //    throw new Exception("The Payment amount cannot be zero!");
            //}

            //paidAmount = toDto.FeeCollectionPaymentModeMapDTO.Select(x => x.Amount.Value).Sum();
            //collectedAmount = toDto.FeeTypes.Select(x => x.Amount.Value).Sum();
            //fineAmount = toDto.FeeFines.Select(x => x.Amount.Value).Sum();
            //if (paidAmount != (collectedAmount + fineAmount))
            //{
            //    throw new Exception("Amount need to be collected and Paid amount must be equal");
            //}
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                MutualRepository mutualRepository = new MutualRepository();

                foreach (FeeCollectionDTO toDtoColl in toDto.FeeCollection)
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence("FeeReceiptNo", null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence with 'FeeReceiptNo'");
                    }

                    var studentDetail = (from stud in dbContext.Students

                                         where (toDtoColl.StudentID == stud.StudentIID)
                                         orderby stud.StudentIID descending
                                         group stud by new
                                         {
                                             stud.StudentIID,
                                             stud.ClassID,
                                             stud.SectionID,
                                             stud.Section,
                                             stud.Class,
                                         } into studentDetailGroup
                                         select new StudentDTO()
                                         {
                                             StudentIID = studentDetailGroup.Key.StudentIID,
                                             ClassID = studentDetailGroup.Key.ClassID,
                                             SectionID = studentDetailGroup.Key.SectionID,
                                             SectionName = studentDetailGroup.Key.Section.SectionName,
                                             ClassName = studentDetailGroup.Key.Class.ClassDescription,
                                         }).AsNoTracking().FirstOrDefault();
                    collectedAmount = toDtoColl.FeeTypes.Select(x => x.Amount.Value).Sum();

                    var entity = new FeeCollection()
                    {
                        ClassID = studentDetail.ClassID,
                        SectionID = studentDetail.SectionID,
                        StudentID = toDtoColl.StudentID,
                        Amount = collectedAmount,
                        //DiscountAmount = toDto.DiscountAmount,
                        //FineAmount = toDto.FineAmount,
                        PaidAmount = paidAmount,
                        IsPaid = true,
                        FeeReceiptNo = sequence.Prefix + sequence.LastSequence,
                        CollectionDate = DateTime.Now,
                        CreatedBy = (int)_context.LoginID,
                        // UpdatedBy = toDto.FeeCollectionIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = DateTime.Now,
                        // UpdatedDate = toDto.FeeCollectionIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    };


                    /*//newly added   */
                    //entity.FeeCollectionPaymentModeMaps = new List<FeeCollectionPaymentModeMap>();
                    //foreach (var paymentType in toDtoColl.FeeCollectionPaymentModeMapDTO)
                    //{
                    //    entity.FeeCollectionPaymentModeMaps.Add(new FeeCollectionPaymentModeMap()
                    //    {
                    //        //CreatedBy = int.Parse(_context.UserId),
                    //        Amount = paymentType.Amount,
                    //        CreatedDate = System.DateTime.Now,
                    //        FeeCollectionID = toDto.FeeCollectionIID,
                    //        PaymentModeID = paymentType.PaymentModeID,
                    //        ReferenceNo = paymentType.ReferenceNo

                    //    });
                    //}

                    entity.FeeCollectionFeeTypeMaps = new List<FeeCollectionFeeTypeMap>();
                    foreach (var feeType in toDtoColl.FeeTypes)
                    {
                        var monthlySplit = new List<FeeCollectionMonthlySplit>();
                        foreach (var feeMasterMonthlyDto in feeType.MontlySplitMaps)
                        {
                            var entityChild = new FeeCollectionMonthlySplit()
                            {
                                FeeDueMonthlySplitID = feeMasterMonthlyDto.FeeDueMonthlySplitID,
                                FeeCollectionMonthlySplitIID = feeMasterMonthlyDto.FeeCollectionMonthlySplitIID,
                                MonthID = byte.Parse(feeMasterMonthlyDto.MonthID.ToString()),
                                Year = feeMasterMonthlyDto.Year,
                                FeePeriodID = feeType.FeePeriodID,
                                Amount = feeMasterMonthlyDto.Amount.HasValue ? feeMasterMonthlyDto.Amount : (decimal?)null,
                                FeeCollectionFeeTypeMapId = feeType.FeeCollectionFeeTypeMapsIID
                            };

                            monthlySplit.Add(entityChild);

                            var entityFeeDueMonthlySplit = dbContext.FeeDueMonthlySplits.Where(x => x.FeeDueMonthlySplitIID == feeMasterMonthlyDto.FeeDueMonthlySplitID).AsNoTracking().FirstOrDefault();
                            
                            entityFeeDueMonthlySplit.Status = true;

                            dbContext.Entry(entityFeeDueMonthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            dbContext.SaveChanges();

                        }

                        if (feeType.Amount.HasValue)
                        {
                            entity.FeeCollectionFeeTypeMaps.Add(new FeeCollectionFeeTypeMap()
                            {
                                FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                                FeeCollectionID = toDtoColl.FeeCollectionIID,
                                FeeMasterID = feeType.FeeMasterID,
                                FeePeriodID = feeType.FeePeriodID,
                                TaxAmount = feeType.TaxAmount.HasValue ? feeType.TaxAmount : (decimal?)null,
                                TaxPercentage = feeType.TaxPercentage.HasValue ? feeType.TaxPercentage : (decimal?)null,
                                Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                                FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                                FeeCollectionMonthlySplits = monthlySplit
                            });

                            if (dbContext.FeeDueMonthlySplits.Where(i => i.FeeDueFeeTypeMapsID == feeType.FeeDueFeeTypeMapsID && i.Status != true).AsNoTracking().Count() == 0)
                            {
                                var entityFeeDueTypeMap = dbContext.FeeDueFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsIID == feeType.FeeDueFeeTypeMapsID).AsNoTracking().FirstOrDefault();

                                entityFeeDueTypeMap.Status = true;
                                entityFeeDueTypeMap.CollectedAmount = entityFeeDueTypeMap.CollectedAmount + feeType.Amount;
                                
                                dbContext.Entry(entityFeeDueTypeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbContext.SaveChanges();

                            }
                            else
                            {
                                var entityFeeDueTypeMap = dbContext.FeeDueFeeTypeMaps.Where(x => x.FeeDueFeeTypeMapsIID == feeType.FeeDueFeeTypeMapsID).AsNoTracking().FirstOrDefault();
                                
                                entityFeeDueTypeMap.CollectedAmount = feeType.Amount;

                                dbContext.Entry(entityFeeDueTypeMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbContext.SaveChanges();
                            }


                            if (dbContext.FeeDueFeeTypeMaps.Where(i => i.StudentFeeDueID == feeType.StudentFeeDueID && i.Status != true).AsNoTracking().Count() == 0)
                            {
                                //var entityFeeDue = repositoryStudentFeeDue.GetById(feeType.StudentFeeDueID);
                                var entityFeeDue = dbContext.StudentFeeDues.Where(x => x.StudentFeeDueIID == feeType.StudentFeeDueID).AsNoTracking().FirstOrDefault();
                                entityFeeDue.CollectionStatus = true;

                                dbContext.Entry(entityFeeDue).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                dbContext.SaveChanges();
                            }
                        }
                    }

                    //foreach (var feeFines in toDto.FeeFines)
                    //{
                    //    if (feeFines.Amount.HasValue)
                    //    {

                    //        entity.FeeCollectionFeeTypeMaps.Add(new FeeCollectionFeeTypeMap()
                    //        {

                    //            FeeCollectionID = toDto.FeeCollectionIID,
                    //            FineMasterID = feeFines.FineMasterID,
                    //            FineMasterStudentMapID = feeFines.FineMasterStudentMapID,
                    //            Amount = feeFines.Amount.HasValue ? feeFines.Amount : (decimal?)null,
                    //            FeeDueFeeTypeMapsID = feeFines.FeeDueFeeTypeMapsID,
                    //            FeeCollectionFeeTypeMapsIID = feeFines.FeeCollectionFeeTypeMapsIID,
                    //            //StudentFeeDu = feeFines.StudentFeeDueID,
                    //        });

                    //        var entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.GetById(feeFines.FeeDueFeeTypeMapsID);
                    //        entityFeeDueTypeMap.Status = true;
                    //        entityFeeDueTypeMap = repositoryFeeDueFeeTypeMap.Update(entityFeeDueTypeMap);

                    //        if (dbContext.FeeDueFeeTypeMaps.AsEnumerable().Where(i => i.StudentFeeDueID == feeFines.StudentFeeDueID && i.Status != true).Count() == 0)
                    //        {
                    //            var entityFeeDue = repositoryStudentFeeDue.GetById(feeFines.StudentFeeDueID);
                    //            entityFeeDue.CollectionStatus = true;
                    //            entityFeeDue = repositoryStudentFeeDue.Update(entityFeeDue);
                    //        }

                    //        var repositoryFineStudentMap = new EntityRepository<FineMasterStudentMap, dbEduegateSchoolContext>(dbContext);
                    //        var entityFineStudentMap = repositoryFineStudentMap.GetById(feeFines.FineMasterStudentMapID);
                    //        entityFineStudentMap.IsCollected = true;
                    //        entityFineStudentMap = repositoryFineStudentMap.Update(entityFineStudentMap);
                    //    }
                    //}

                    dbContext.FeeCollections.Add(entity);
                    if (entity.FeeCollectionIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var paymentMode in entity.FeeCollectionPaymentModeMaps)
                        {
                            if (paymentMode.FeeCollectionPaymentModeMapIID != 0)
                            {
                                dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(paymentMode).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        foreach (var classMap in entity.FeeCollectionFeeTypeMaps)
                        {
                            foreach (var monthlySplit in classMap.FeeCollectionMonthlySplits)
                            {
                                if (monthlySplit.FeeCollectionMonthlySplitIID != 0)
                                {
                                    dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                }
                                else
                                {
                                    dbContext.Entry(monthlySplit).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }
                            }

                            if (classMap.FeeCollectionFeeTypeMapsIID != 0)
                            {
                                dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(classMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                }
                //return GetEntity(entity.FeeCollectionIID);
                return ToDTOString(new FeeCollectionDTO());
            }
        }

    }
}