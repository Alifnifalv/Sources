using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Payment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Payment
{
    public class FeePaymentBL
    {
        private CallContext Context { get; set; }

        public FeePaymentBL(CallContext context)
        {
            Context = context;
        }

        public List<FeePaymentHistoryDTO> GetFeeCollectionHistoriesByFilter(byte? schoolID, int? academicYearID)
        {
            var studentDatas = StudentMapper.Mapper(Context).GetStudentsSiblings(Context.LoginID.HasValue ? Context.LoginID.Value : 0);

            var feeCollectionDatas = FeeCollectionMapper.Mapper(Context).GetFeeCollectionHistories(studentDatas, schoolID, academicYearID);

            var feeCollectionHistories = FillFeePaymentHistoryDTOs(feeCollectionDatas);

            if (feeCollectionHistories.Count > 0)
            {
                var currentDate = DateTime.Now;
                var selectedCollection = feeCollectionHistories.FirstOrDefault();

                //var durationDays = new Domain.Setting.SettingBL(null).GetSettingValue<int>("PAYMENT_RETRY_DURATION_IN_DAYS", 0);
                var durationMinutes = new Domain.Setting.SettingBL(null).GetSettingValue<int>("PAYMENT_RETRY_DURATION_IN_MINUTES", 30);

                double differenceInDays = 0;
                double differenceInMinutes = 0;

                if (selectedCollection.CreatedDate.HasValue)
                {
                    TimeSpan timeDifference = currentDate - selectedCollection.CreatedDate.Value;

                    // Display difference in days
                    differenceInDays = timeDifference.TotalDays;

                    // Convert time difference to minutes
                    differenceInMinutes = timeDifference.TotalMinutes;
                }

                if (differenceInMinutes <= durationMinutes)
                {
                    if (selectedCollection.FeeCollectionStatusID != selectedCollection.FeeCollectionCollectedStatusID)
                    {
                        selectedCollection.IsEnableRetry = true;
                    }
                }
            }

            return feeCollectionHistories;
        }

        public List<FeePaymentHistoryDTO> GetLastTenFeeCollectionHistories()
        {
            var studentDatas = StudentMapper.Mapper(Context).GetStudentsSiblings(Context.LoginID.HasValue ? Context.LoginID.Value : 0);

            var feeCollectionDatas = FeeCollectionMapper.Mapper(Context).GetLastTenFeeCollectionHistories(studentDatas);

            var feeCollectionHistories = FillFeePaymentHistoryDTOs(feeCollectionDatas);

            feeCollectionHistories = feeCollectionHistories.Count > 10 ? feeCollectionHistories.Take(10).ToList() : feeCollectionHistories;

            if (feeCollectionHistories.Count > 0)
            {
                var currentDate = DateTime.Now;
                var selectedCollection = feeCollectionHistories.FirstOrDefault();

                //var durationDays = new Domain.Setting.SettingBL(null).GetSettingValue<int>("PAYMENT_RETRY_DURATION_IN_DAYS", 0);

                var durationMinutes = new Domain.Setting.SettingBL(null).GetSettingValue<int>("PAYMENT_RETRY_DURATION_IN_MINUTES", 30);

                double differenceInDays = 0;
                double differenceInMinutes = 0;

                if (selectedCollection.CreatedDate.HasValue)
                {
                    TimeSpan timeDifference = currentDate - selectedCollection.CreatedDate.Value;

                    // Display difference in days
                    differenceInDays = timeDifference.TotalDays;

                    // Convert time difference to minutes
                    differenceInMinutes = timeDifference.TotalMinutes;
                }

                if (differenceInMinutes <= durationMinutes)
                {
                    if (selectedCollection.FeeCollectionStatusID != selectedCollection.FeeCollectionCollectedStatusID)
                    {
                        selectedCollection.IsEnableRetry = true;
                    }
                }
            }

            return feeCollectionHistories;
        }
        public List<FeePaymentHistoryDTO> GetFeeCollectionHistory(long studentID)
        {
            var feeCollectionDatas = FeeCollectionMapper.Mapper(Context).GetFeeCollectionHistory(studentID);

            var feeCollectionHistories = FillFeePaymentHistoryDTOs(feeCollectionDatas);

            feeCollectionHistories = feeCollectionHistories.Count > 10 ? feeCollectionHistories.Take(10).ToList() : feeCollectionHistories;

            if (feeCollectionHistories.Count > 0)
            {
                var currentDate = DateTime.Now;
                var selectedCollection = feeCollectionHistories.FirstOrDefault();

                var durationMinutes = new Domain.Setting.SettingBL(null).GetSettingValue<int>("PAYMENT_RETRY_DURATION_IN_MINUTES", 30);

                double differenceInDays = 0;
                double differenceInMinutes = 0;

                if (selectedCollection.CreatedDate.HasValue)
                {
                    TimeSpan timeDifference = currentDate - selectedCollection.CreatedDate.Value;

                    // Display difference in days
                    differenceInDays = timeDifference.TotalDays;

                    // Convert time difference to minutes
                    differenceInMinutes = timeDifference.TotalMinutes;
                }

                if (differenceInMinutes <= durationMinutes)
                {
                    if (selectedCollection.FeeCollectionStatusID != selectedCollection.FeeCollectionCollectedStatusID)
                    {
                        selectedCollection.IsEnableRetry = true;
                    }
                }
            }

            return feeCollectionHistories;

        }

        public List<FeePaymentHistoryDTO> FillFeePaymentHistoryDTOs(List<FeeCollectionDTO> feeCollectionDatas)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            var feeCollectionHistories = new List<FeePaymentHistoryDTO>();

            var studentFeeCollections = new List<FeePaymentHistoryStudentDTO>();

            var feeCollectionTypeHistoryList = new List<FeePaymentHistoryFeeTypeDTO>();

            var feeCollectionGrouping = feeCollectionDatas.GroupBy(g => g.GroupTransactionNumber).ToList();

            var keyNullCollections = feeCollectionGrouping.Where(x => string.IsNullOrEmpty(x.Key)).ToList();

            if (keyNullCollections.Count > 0)
            {
                // Remove the original keyNullCollections from feeCollectionGrouping
                feeCollectionGrouping.RemoveAll(g => string.IsNullOrEmpty(g.Key));

                foreach (var coln in keyNullCollections)
                {
                    foreach (var item in coln.ToList())
                    {
                        // Create a new group with coln.Key and add this item as part of the group
                        var newGroup = new[] { item }
                            .GroupBy(g => coln.Key)
                            .FirstOrDefault();

                        // Add the new group to the original collection
                        feeCollectionGrouping.Add(newGroup);
                    }
                }
            }

            string transactionNo = string.Empty;
            foreach (var collection in feeCollectionGrouping)
            {
                studentFeeCollections = new List<FeePaymentHistoryStudentDTO>();

                transactionNo = string.IsNullOrEmpty(collection?.Key) ? "Counter payment" : collection?.Key;

                foreach (var feeCollection in collection)
                {
                    feeCollectionTypeHistoryList = new List<FeePaymentHistoryFeeTypeDTO>();

                    foreach (var feeType in feeCollection.FeeTypes)
                    {
                        feeCollectionTypeHistoryList.Add(new FeePaymentHistoryFeeTypeDTO()
                        {
                            IsExpand = false,
                            Amount = feeType.Amount,
                            FeeMaster = new KeyValueDTO { Key = feeType.FeeMasterID.ToString(), Value = feeType.FeeMaster },
                            FeePeriod = feeType.FeePeriodID.HasValue ? new KeyValueDTO()
                            {
                                Key = feeType.FeePeriodID.HasValue ? Convert.ToString(feeType.FeePeriodID) : null,
                                Value = !feeType.FeePeriodID.HasValue ? null : feeType.FeePeriod
                            } : new KeyValueDTO(),
                            FeeMonthly = (from split in feeType.MontlySplitMaps
                                          select new FeePaymentHistoryMonthlySplitDTO()
                                          {
                                              IsExpand = false,
                                              Amount = split.DueAmount,
                                              MonthID = split.MonthID,
                                              CreditNote = split.CreditNoteAmount,
                                              Balance = split.Balance,
                                              MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                          }).ToList(),
                        });
                    }

                    studentFeeCollections.Add(new FeePaymentHistoryStudentDTO()
                    {
                        IsExpand = false,
                        StudentID = feeCollection.StudentID,
                        StudentName = feeCollection.StudentName,
                        ClassID = feeCollection.ClassID,
                        ClassName = feeCollection.ClassName,
                        SectionID = feeCollection.SectionID,
                        SectionName = feeCollection.SectionName,
                        SchoolID = feeCollection.SchoolID,
                        SchoolName = feeCollection.SchoolName,
                        AcademicYearID = feeCollection.AcadamicYearID,
                        AcademicYear = feeCollection.AcademicYear?.Value,
                        FeeCollectionStatusID = feeCollection.FeeCollectionStatusID,
                        FeeCollectionStatus = feeCollection.FeeCollectionStatusName,
                        FeeCollectionDraftStatusID = feeCollection.FeeCollectionDraftStatusID,
                        FeeCollectionCollectedStatusID = feeCollection.FeeCollectionCollectedStatusID,
                        FeeReceiptNo = feeCollection.FeeReceiptNo,
                        CollectionDate = feeCollection.CollectionDate,
                        CollectionDateString = feeCollection.CollectionDate.HasValue ? Convert.ToDateTime(feeCollection.CollectionDate).ToString(dateFormat) : null,
                        Amount = feeCollectionTypeHistoryList.Sum(t => t.Amount),
                        TransactionNumber = feeCollection.GroupTransactionNumber,
                        FeePaymentModeID = feeCollection.FeePaymentModeID,
                        FeePaymentMode = feeCollection.FeePaymentMode,
                        IsEnableRetry = feeCollection.IsEnableRetry,
                        CreatedDate = feeCollection.CreatedDate,
                        FeeCollectionTypeHistories = feeCollectionTypeHistoryList,
                    });
                }

                if (studentFeeCollections.Count > 0)
                {
                    feeCollectionHistories.Add(new FeePaymentHistoryDTO()
                    {
                        IsExpand = false,
                        FeeCollectionStatusID = studentFeeCollections.Max(s => s.FeeCollectionStatusID),
                        FeeCollectionStatus = studentFeeCollections.Max(s => s.FeeCollectionStatus),
                        FeeCollectionDraftStatusID = studentFeeCollections.Max(s => s.FeeCollectionDraftStatusID),
                        FeeCollectionCollectedStatusID = studentFeeCollections.Max(s => s.FeeCollectionCollectedStatusID),
                        TransactionNumber = transactionNo,
                        CollectionDate = studentFeeCollections.Max(s => s.CollectionDate),
                        CollectionDateString = studentFeeCollections.Max(s => s.CollectionDateString),
                        Amount = studentFeeCollections.Sum(s => s.Amount),
                        StudentHistories = studentFeeCollections,
                        ParentEmailID = feeCollectionDatas.FirstOrDefault()?.EmailID,
                        FeePaymentModeID = studentFeeCollections.FirstOrDefault()?.FeePaymentModeID,
                        FeePaymentMode = studentFeeCollections.FirstOrDefault()?.FeePaymentMode,
                        IsEnableRetry = studentFeeCollections.FirstOrDefault()?.IsEnableRetry,
                        CreatedDate = studentFeeCollections.FirstOrDefault()?.CreatedDate,
                    });
                }
            }

            feeCollectionHistories = feeCollectionHistories.OrderByDescending(o => o.CollectionDate).ToList();

            return feeCollectionHistories;
        }

    }
}