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
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Payment;

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
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

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
                        var balanceTypeAmountToPay = feeType.Amount - (feeType.CollectedAmount ?? 0) - (feeType.CreditNoteAmount ?? 0);

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
                                              //where split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0) != 0
                                              select new FeeAssignMonthlySplitViewModel()
                                              {
                                                  TotalAmount = split.Amount,
                                                  Amount = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
                                                  CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                                  Balance = 0,
                                                  NowPaying = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
                                                  OldNowPaying = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
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
                    NowPaying = feeTypeList.Sum(s => s.Amount),
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

        [HttpPost]
        public ActionResult InitiateFeeCollections([FromBody] List<FeeCollectionDTO> feeCollections)
        {
            var returnResult = ClientFactory.SchoolServiceClient(CallContext).FeeCollectionEntry(feeCollections);

            if (returnResult.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = returnResult.Message });
            }
            else
            {
                return Json(new { IsError = true, Response = returnResult.Message });
            }
        }

        public ActionResult UpdatePaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            if (string.IsNullOrEmpty(transactionNumber) || transactionNumber == "null")
            {
                var masterVisaData = ClientFactory.SchoolServiceClient(CallContext).GetPaymentMasterVisaData();

                transactionNumber = masterVisaData?.TransID;
            }
            else
            {
                if (paymentModeID.HasValue && paymentModeID == (byte)PaymentModes.QPAY)
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");

                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }
                else
                {
                    var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                    transactionNumber = transactionNumber.Replace(transactionPrefix, "");
                }
            }

            var parentLoginID = CallContext.LoginID;

            var paymentUpdateDatas = ClientFactory.SchoolServiceClient(CallContext).UpdateStudentsFeePaymentStatus(transactionNumber, parentLoginID);

            string paymentStatus;
            if (paymentUpdateDatas != null)
            {
                paymentStatus = "Successfully Updated";

                //Send Mail
                ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(paymentUpdateDatas, EmailTypes.AutoFeeReceipt);
            }
            else
            {
                paymentStatus = "";
            }

            return Json(paymentStatus);
        }

        [HttpGet]
        public JsonResult GetLastTenFeeCollectionHistories()
        {
            var feeCollectionDatas = new FeePaymentBL(CallContext).GetLastTenFeeCollectionHistories();

            if (feeCollectionDatas == null && feeCollectionDatas.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeCollectionDatas });
            }
        }

        [HttpGet]
        public JsonResult GetFeeCollectionHistories(byte? schoolID, int? academicYearID)
        {
            var feeCollectionDatas = new FeePaymentBL(CallContext).GetFeeCollectionHistoriesByFilter(schoolID, academicYearID);

            if (feeCollectionDatas == null && feeCollectionDatas.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = feeCollectionDatas });
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

        public ActionResult ResendMailReceipt(string transactionNumber, string mailID, string feeReceiptNo)
        {
            string paymentStatus;

            try
            {
                var feeCollectionDatas = ClientFactory.SchoolServiceClient(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID, feeReceiptNo);

                if (feeCollectionDatas != null && feeCollectionDatas.Count > 0)
                {
                    ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionDatas, EmailTypes.ResendFeeReceipt);

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

        [HttpGet]
        public JsonResult CheckTransactionPaymentStatus(string transactionNumber, byte? paymentModeID = null)
        {
            if (paymentModeID.HasValue && paymentModeID == (byte)PaymentModes.QPAY)
            {
                var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("QPAY_TRANSACTION_PREFIX");

                transactionNumber = transactionNumber.Replace(transactionPrefix, "");
            }
            else
            {
                var transactionPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                transactionNumber = transactionNumber.Replace(transactionPrefix, "");
            }

            var feeCollectionStatus = ClientFactory.PaymentGatewayServiceClient(CallContext).ValidatePaymentByTransaction(transactionNumber, paymentModeID);

            if (!string.IsNullOrEmpty(feeCollectionStatus))
            {
                if (feeCollectionStatus.ToLower() == "success")
                {
                    return Json(new { IsError = false, Response = feeCollectionStatus });
                }
                else
                {
                    return Json(new { IsError = true, Response = feeCollectionStatus });
                }
            }
            else
            {
                return Json(new { IsError = true, Response = "Not recived payment" });
            }
        }

    }
}