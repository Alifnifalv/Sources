using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class FeeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FeePayment()
        {
            return View();
        }

        [HttpGet]
        public JsonResult FillFeePaymentDetails()
        {
            var dateFormat = new Domain.SettingBL().GetSettingValue<string>("DateFormat", 0 , "dd/MM/yyyy");

            var feePays = new List<FeePaymentViewModel>();

            var studentDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            foreach (var studDet in studentDatas)
            {
                var feeTypeList = new List<FeePaymentFeeTypeViewModel>();
                List<StudentFeeDueDTO> feeDueData = ClientFactory.SchoolServiceClient(CallContext).FillFeeDue(0, studDet.StudentIID);

                decimal? nowPaying = 0;

                foreach (var data in feeDueData)
                {
                    foreach (var feeType in data.FeeDueFeeTypeMap)
                    {
                        var balanceTypeAmountToPay = feeType.Amount - feeType.CreditNoteAmount;

                        if (balanceTypeAmountToPay != 0)
                        {
                            bool isPayingNow = true;
                            decimal? typeNowPaying = 0;

                            if (feeType.IsExternal == true)
                            {
                                isPayingNow = false;
                            }
                            else
                            {
                                nowPaying += feeType.Amount;
                                typeNowPaying = balanceTypeAmountToPay;
                            }

                            feeTypeList.Add(new FeePaymentFeeTypeViewModel()
                            {
                                InvoiceNo = feeType.InvoiceNo,
                                Amount = balanceTypeAmountToPay,
                                NowPaying = typeNowPaying,
                                FeePeriodID = feeType.FeePeriodID,
                                StudentFeeDueID = feeType.StudentFeeDueID,
                                //FeeMasterClassMapID = feeType.FeeMasterClassMapID,
                                FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                                FeeMaster = feeType.FeeMaster.Value,
                                IsPayingNow = isPayingNow,
                                FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod.Value : null,
                                FeeMasterID = feeType.FeeMasterID,
                                IsExternal = feeType.IsExternal,
                                InvoiceDateString = feeType.InvoiceDate.HasValue ? Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat) : "NA",
                                InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                                FeeMonthly = (from split in feeType.FeeDueMonthlySplit
                                              select new FeeAssignMonthlySplitViewModel()
                                              {
                                                  Amount = split.Amount,
                                                  CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                                  Balance = 0,
                                                  NowPaying = split.Balance,
                                                  OldNowPaying = split.Balance,
                                                  MonthID = split.MonthID,
                                                  Year = split.Year,
                                                  FeeDueMonthlySplitID = split.FeeDueMonthlySplitIID,
                                                  IsRowSelected = true,
                                                  MonthName = split.MonthID == 0 ? null : new DateTime(split.Year, split.MonthID, 1).ToString("MMM")
                                              }).ToList(),
                            });
                        }
                    }
                }

                feePays.Add(new FeePaymentViewModel()
                {
                    IsExpand = true,
                    IsSelected = true,
                    StudentID = studDet.StudentIID,
                    Student = studDet.AdmissionNumber + " - " + studDet.FirstName + " " + studDet.MiddleName + " " + studDet.LastName,
                    ClassID = studDet.ClassID,
                    ClassName = studDet.ClassName,
                    SectionID = studDet.SectionID,
                    SectionName = studDet.SectionName,
                    SchoolID = studDet.SchoolID,
                    SchoolName = studDet.SchoolName,
                    AcademicYearID = studDet.AcademicYearID,
                    AcademicYear = studDet.AcademicYear,
                    TotalAmount = feeTypeList.Sum(s => s.Amount),
                    OldTotalAmount = feeTypeList.Sum(s => s.Amount),
                    NowPaying = nowPaying,
                    FeeTypes = feeTypeList,
                });

            }

            if (feePays == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feePays });
            }
        }

        public ActionResult InitiateFeeCollections(List<FeeCollectionDTO> feeCollections)
        {
            var feeCollectionStatus = ClientFactory.SchoolServiceClient(CallContext).FeeCollectionEntry(feeCollections);

            if (feeCollectionStatus != null)
            {
                return Json(new { IsError = false, Response = feeCollectionStatus });
            }
            else
            {
                return Json(new { IsError = true, Response = "There are some issues!" });
            }
        }

        public ActionResult UpdatePaymentStatus()
        {
            var masterVisaData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

            var transactionNo = masterVisaData?.TransID;

            var paymentUpdateDatas = ClientFactory.SchoolServiceClient(CallContext).UpdateStudentsFeePaymentStatus(transactionNo);

            string paymentStatus;
            if (paymentUpdateDatas != null)
            {
                paymentStatus = "Successfully Updated";

                //Send Mail
                ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(paymentUpdateDatas);
            }
            else
            {
                paymentStatus = "";
            }

            return Json(paymentStatus);
        }

        [HttpGet]
        public JsonResult GetStudentsFeeCollectionHistory(byte? schoolID, int? academicYearID)
        {
            var dateFormat = new Domain.SettingBL().GetSettingValue<string>("DateFormat", 0 , "dd/MM/yyyy");

            var feeCollections = new List<FeeCollectionHistoryStudentViewModel>();

            var academicYearListData = new List<AcademicYearDTO>();

            if (academicYearID == 0)
            {
                academicYearListData = ClientFactory.SchoolServiceClient(CallContext).GetCurrentAcademicYearsData();
            }

            var studentDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            foreach (var studDet in studentDatas)
            {
                var currentSchoolID = schoolID;
                var currentAcademicYearID = academicYearID;

                if (currentSchoolID == 0 || studDet.SchoolID == currentSchoolID)
                {
                    if (currentSchoolID == 0)
                    {
                        currentSchoolID = studDet.SchoolID;
                    }

                    if (currentAcademicYearID == 0)
                    {
                        currentAcademicYearID = academicYearListData.Find(a => a.SchoolID == currentSchoolID).AcademicYearID;
                    }

                    var feeCollectionDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeCollectionsHistory(studDet, currentSchoolID, currentAcademicYearID);

                    var feeCollectionTypeHistoryList = new List<FeeCollectionHistoryFeeTypeViewModel>();

                    foreach (var feeCollection in feeCollectionDatas)
                    {
                        foreach (var feeType in feeCollection.FeeTypes)
                        {
                            var feeCollectionStatus = string.Empty;

                            if (feeCollection.FeeCollectionStatusID == feeCollection.FeeCollectionDraftStatusID)
                            {
                                feeCollectionStatus = "Pending";
                            }
                            else
                            {
                                feeCollectionStatus = "Paid";
                            }

                            feeCollectionTypeHistoryList.Add(new FeeCollectionHistoryFeeTypeViewModel()
                            {
                                IsExpand = false,
                                FeeReceiptNo = feeCollection.FeeReceiptNo,
                                CollectionDate = feeCollection.CollectionDate.HasValue ? Convert.ToDateTime(feeCollection.CollectionDate).ToString(dateFormat) : null,
                                Amount = feeType.Amount,
                                FeeMaster = new KeyValueViewModel { Key = feeType.FeeMasterID.ToString(), Value = feeType.FeeMaster },
                                FeePeriod = feeType.FeePeriodID.HasValue ? new KeyValueViewModel()
                                {
                                    Key = feeType.FeePeriodID.HasValue ? Convert.ToString(feeType.FeePeriodID) : null,
                                    Value = !feeType.FeePeriodID.HasValue ? null : feeType.FeePeriod
                                } : new KeyValueViewModel() { Key = null, Value = "NA" },

                                FeeCollectionStatusID = feeCollection.FeeCollectionStatusID,
                                FeeCollectionStatus = feeCollectionStatus,
                                FeeCollectionDraftStatusID = feeCollection.FeeCollectionDraftStatusID,
                                FeeCollectionCollectedStatusID = feeCollection.FeeCollectionCollectedStatusID,

                                FeeMonthly = (from split in feeType.MontlySplitMaps
                                              select new FeeAssignMonthlySplitViewModel()
                                              {
                                                  Amount = split.DueAmount,
                                                  MonthID = split.MonthID,
                                                  CreditNote = split.CreditNoteAmount,
                                                  Balance = split.Balance,
                                                  MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                              }).ToList(),
                            });
                        }
                    }

                    feeCollections.Add(new FeeCollectionHistoryStudentViewModel()
                    {
                        IsExpand = true,
                        StudentID = studDet.StudentIID,
                        StudentName = studDet.AdmissionNumber + " - " + studDet.FirstName + " " + studDet.MiddleName + " " + studDet.LastName,
                        ClassID = studDet.ClassID,
                        ClassName = studDet.ClassName,
                        SectionID = studDet.SectionID,
                        SectionName = studDet.SectionName,
                        SchoolID = studDet.SchoolID,
                        SchoolName = studDet.SchoolName,
                        AcademicYearID = studDet.AcademicYearID,
                        AcademicYear = studDet.AcademicYear,
                        FeeCollectionTypeHistories = feeCollectionTypeHistoryList,
                    });
                }

            }

            if (feeCollections == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeCollections });
            }
        }

        [HttpGet]
        public JsonResult GetFeeCollectionHistories(byte? schoolID, int? academicYearID)
        {
            var dateFormat = new Domain.SettingBL().GetSettingValue<string>("DateFormat", 0 , "dd/MM/yyyy");

            var studentDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            var feeCollectionDatas = ClientFactory.SchoolServiceClient(CallContext).GetFeeCollectionHistories(studentDatas, schoolID, academicYearID);

            var feeCollectionHistories = new List<FeeCollectionHistoryViewModel>();

            var studentFeeCollections = new List<FeeCollectionHistoryStudentViewModel>();

            var feeCollectionTypeHistoryList = new List<FeeCollectionHistoryFeeTypeViewModel>();

            var collectionGroupByDate = feeCollectionDatas.GroupBy(g => g.CollectionDate).ToList();

            foreach (var collectionGroup in collectionGroupByDate)
            {
                var feeCollectionGrouping = collectionGroup.GroupBy(g => g.GroupTransactionNumber).ToList();

                string transactionNo = string.Empty;
                foreach (var collection in feeCollectionGrouping)
                {
                    studentFeeCollections = new List<FeeCollectionHistoryStudentViewModel>();

                    transactionNo = string.IsNullOrEmpty(collection?.Key) ? "NA" : collection?.Key;

                    foreach (var feeCollection in collection)
                    {
                        feeCollectionTypeHistoryList = new List<FeeCollectionHistoryFeeTypeViewModel>();

                        foreach (var feeType in feeCollection.FeeTypes)
                        {
                            feeCollectionTypeHistoryList.Add(new FeeCollectionHistoryFeeTypeViewModel()
                            {
                                IsExpand = false,
                                Amount = feeType.Amount,
                                FeeMaster = new KeyValueViewModel { Key = feeType.FeeMasterID.ToString(), Value = feeType.FeeMaster },
                                FeePeriod = feeType.FeePeriodID.HasValue ? new KeyValueViewModel()
                                {
                                    Key = feeType.FeePeriodID.HasValue ? Convert.ToString(feeType.FeePeriodID) : null,
                                    Value = !feeType.FeePeriodID.HasValue ? null : feeType.FeePeriod
                                } : new KeyValueViewModel(),
                                FeeMonthly = (from split in feeType.MontlySplitMaps
                                              select new FeeAssignMonthlySplitViewModel()
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

                        studentFeeCollections.Add(new FeeCollectionHistoryStudentViewModel()
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
                            CollectionDate = feeCollection.CollectionDate.HasValue ? Convert.ToDateTime(feeCollection.CollectionDate).ToString(dateFormat) : null,
                            Amount = feeCollectionTypeHistoryList.Sum(t => t.Amount),
                            FeeCollectionTypeHistories = feeCollectionTypeHistoryList,
                        });
                    }

                    feeCollectionHistories.Add(new FeeCollectionHistoryViewModel()
                    {
                        IsExpand = false,
                        FeeCollectionStatusID = studentFeeCollections.Max(s => s.FeeCollectionStatusID),
                        FeeCollectionStatus = studentFeeCollections.Max(s => s.FeeCollectionStatus),
                        FeeCollectionDraftStatusID = studentFeeCollections.Max(s => s.FeeCollectionDraftStatusID),
                        FeeCollectionCollectedStatusID = studentFeeCollections.Max(s => s.FeeCollectionCollectedStatusID),
                        //TransactionNumber = string.IsNullOrEmpty(collectionGroup?.Key) ? "NA" : collectionGroup.Key,
                        TransactionNumber = transactionNo,
                        CollectionDate = studentFeeCollections.Max(s => s.CollectionDate),
                        Amount = studentFeeCollections.Sum(s => s.Amount),
                        StudentHistories = studentFeeCollections,
                        ParentEmailID = string.IsNullOrEmpty(studentDatas[0].ParentEmailID) ? CallContext.EmailID : studentDatas[0].ParentEmailID,
                    });
                }
            }

            if (feeCollectionHistories == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeCollectionHistories });
            }
        }

        [HttpGet]
        public JsonResult CheckFeeCollectionExistingStatus(string transactionNumber)
        {
            var feeCollectionStatus = ClientFactory.SchoolServiceClient(CallContext).CheckFeeCollectionStatusByTransactionNumber(transactionNumber);

            if (feeCollectionStatus != null)
            {
                return Json(new { IsError = true, Response = feeCollectionStatus });
            }
            else
            {
                return Json(new { IsError = false, Response = "" });
            }
        }

        public ActionResult ResendMailReceipt(string transactionNumber, string mailID)
        {
            string paymentStatus;

            try
            {
                var feeCollectionDatas = ClientFactory.SchoolServiceClient(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID);

                if (feeCollectionDatas != null && feeCollectionDatas.Count > 0)
                {
                    ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionDatas);

                    paymentStatus = "Successfully Sended";
                }
                else
                {
                    paymentStatus = "";
                }
            }
            catch (Exception ex)
            {
                var data = ex.Message;
                paymentStatus = "";
            }

            return Json(paymentStatus);
        }

    }
}