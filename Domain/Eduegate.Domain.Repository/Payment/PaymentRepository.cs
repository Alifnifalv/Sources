using System;
using System.Linq;
using System.Diagnostics;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Helper;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository.Payment
{
    public class PaymentRepository
    {

        #region KNET
        public bool MakeKNETPaymentEntry(PaymentDetailsKnet paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                    dbContext.PaymentDetailsKnets.Add(paymentDetail);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            //TODO: Need to handle this exception
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public bool UpdateKNETPaymentEntry(PaymentDetailsKnet paymentDetail)
        {
            bool status = false;
            try
            {
                var paymentStatus = (int)Eduegate.Framework.Payment.TransactionStatus.Initiated;

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    PaymentDetailsKnet paymentDetailUpdate = dbContext.PaymentDetailsKnets.Where(
                        x => x.TrackID.Equals(paymentDetail.TrackID)
                        && x.TrackKey.Equals(paymentDetail.TrackKey)
                        && x.CustomerID.Equals(paymentDetail.CustomerID)
                        && x.PaymentID.Equals(paymentDetail.PaymentID)
                        && x.InitStatus.Equals(paymentStatus.ToString()))
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (paymentDetailUpdate.IsNotNull())
                    {
                        paymentDetailUpdate.InitStatus = paymentDetail.InitStatus;
                        paymentDetailUpdate.TransID = paymentDetail.TransID;
                        paymentDetailUpdate.TransOn = DateTime.Now;
                        paymentDetailUpdate.TransResult = paymentDetail.TransResult;
                        paymentDetailUpdate.TransPostDate = paymentDetail.TransPostDate;
                        paymentDetailUpdate.TransAuth = paymentDetail.TransAuth;
                        paymentDetailUpdate.TransRef = paymentDetail.TransRef;
                        paymentDetailUpdate.TransIP = paymentDetail.TransIP;
                        paymentDetailUpdate.TransLocation = GetCountryNameByIP(paymentDetail.TransLocation, dbContext);
                        dbContext.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not ablet to update Payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public PaymentDetailsKnet GetKnetTrackID(long customerID, long paymentID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Eduegate.Logger.LogHelper<PaymentRepository>.Info("GetKnetTrackID payment repo called. orderID:" + paymentID + "customerID:" + customerID);
                if (paymentID > 0)
                {
                    Eduegate.Logger.LogHelper<PaymentRepository>.Info("fetching payment detail by paymentID:" + paymentID);
                    return dbContext.PaymentDetailsKnets.Where(x => x.PaymentID == paymentID && x.CustomerID == customerID)
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                else
                {
                    Eduegate.Logger.LogHelper<PaymentRepository>.Info("getting payment detail by customerID:" + customerID);

                    return (from pay in dbContext.PaymentDetailsKnets
                            where pay.CustomerID == customerID
                            orderby pay.InitOn descending
                            select pay)
                            .AsNoTracking()
                            .FirstOrDefault();
                }
            }
        }

        public PaymentDetailsKnet GetPaymentDetailKNET(long orderID, long trackID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Eduegate.Logger.LogHelper<PaymentRepository>.Info("GetPaymentDetailKNET payment repo called. orderID:" + orderID + "trackID:" + trackID);
                if (orderID > 0)
                {
                    Eduegate.Logger.LogHelper<PaymentRepository>.Info("fetching payment detail by orderid:" + orderID);
                    return dbContext.PaymentDetailsKnets.Where(x => x.OrderID == orderID).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    Eduegate.Logger.LogHelper<PaymentRepository>.Info("getting payment detail by trackid:" + trackID);
                    return dbContext.PaymentDetailsKnets.Where(x => x.TrackID == trackID).AsNoTracking().FirstOrDefault();
                }
            }
        }

        public long GetKnetDetails(long appKey)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentDetailsKnets.Where(a => a.AppKey == appKey).AsNoTracking().Select(a => a.TrackID).FirstOrDefault();
            }
        }

        #endregion

        #region  MIGS
        public bool MakeMIGSPaymentEntry(PaymentMasterVisa paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                    dbContext.PaymentMasterVisas.Add(paymentDetail);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save MIGS payment details. TrackId:" + paymentDetail.TrackIID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public bool UpdateMIGSPaymentEntry(PaymentMasterVisa paymentDetail)
        {
            bool status = false;
            try
            {
                var paymentStatus = (int)Eduegate.Framework.Payment.TransactionStatus.Initiated;

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    PaymentMasterVisa paymentDetailUpdate = dbContext.PaymentMasterVisas.Where(
                         x => x.TrackIID.Equals(paymentDetail.TrackIID)
                         && x.TrackKey.Equals(paymentDetail.TrackKey)
                         && x.CustomerID.Equals(paymentDetail.CustomerID)
                         && x.PaymentID.Equals(paymentDetail.PaymentID)
                         && x.InitStatus.Equals(paymentStatus.ToString())).AsNoTracking().FirstOrDefault();
                    if (paymentDetailUpdate.IsNotNull())
                    {
                        paymentDetailUpdate.InitStatus = paymentDetail.InitStatus;

                        //commented below as these request related fields
                        //paymentDetailUpdate.InitIP = paymentDetail.InitIP;
                        //paymentDetailUpdate.InitLocation = paymentDetail.InitLocation;
                        //paymentDetailUpdate.VpcURL = paymentDetail.VpcURL.IsNotNullOrEmpty() ? : paymentDetailUpdate.VpcURL;
                        //paymentDetailUpdate.VpcCommand = paymentDetail.VpcCommand;
                        //paymentDetailUpdate.MerchantID = paymentDetail.MerchantID;
                        //paymentDetailUpdate.VpcLocale = paymentDetail.VpcLocale;
                        //paymentDetailUpdate.PaymentAmount = paymentDetail.PaymentAmount;
                        //paymentDetailUpdate.VirtualAmount = paymentDetail.VirtualAmount;
                        //paymentDetailUpdate.PaymentCurrency = paymentDetail.PaymentCurrency;

                        paymentDetailUpdate.ResponseOn = DateTime.Now;
                        paymentDetailUpdate.ResponseCode = paymentDetail.ResponseCode;
                        paymentDetailUpdate.CodeDescription = paymentDetail.CodeDescription;
                        paymentDetailUpdate.Message = paymentDetail.Message;
                        paymentDetailUpdate.ReceiptNumber = paymentDetail.ReceiptNumber;
                        paymentDetailUpdate.TransID = paymentDetail.TransID;
                        paymentDetailUpdate.ResponseIP = paymentDetail.ResponseIP;
                        paymentDetailUpdate.AcquireResponseCode = paymentDetail.AcquireResponseCode;
                        paymentDetailUpdate.BankAuthorizationID = paymentDetail.BankAuthorizationID;
                        paymentDetailUpdate.BatchNo = paymentDetail.BatchNo;
                        paymentDetailUpdate.CardType = paymentDetail.CardType;
                        paymentDetailUpdate.ResponseAmount = paymentDetail.ResponseAmount;
                        paymentDetailUpdate.ResponseLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                        dbContext.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update MIGS Payment details. TrackId:" + paymentDetail.TrackIID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }



        public PaymentMasterVisa GetPaymentDetailMIGS(long orderID, long trackID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (trackID > 0)
                {
                    return dbContext.PaymentMasterVisas.Where(x => x.TrackIID == trackID).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return dbContext.PaymentMasterVisas.Where(x => x.OrderID == orderID).AsNoTracking().FirstOrDefault();
                }
            }
        }
        #endregion

        #region PayPal
        /// <summary>
        /// Log Paypal request before sending it to paypal page
        /// </summary>
        /// <param name="paymentDetail"></param>
        /// <returns></returns>
        public bool LogPayPalPaymentRequest(PaymentDetailsPayPal paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                    dbContext.PaymentDetailsPayPals.Add(paymentDetail);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Paypal payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        /// <summary>
        /// Get Paypal payment details
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="trackKey"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public PaymentDetailsPayPal GetPayPalPaymentDetail(long trackID, long trackKey, long customerID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Eduegate.Logger.LogHelper<PaymentRepository>.Info("GetPayPalPaymentDetail payment repo called. customerID:" + customerID + ", TrackID: " + trackID + ", TrackKey: " + trackKey);
                return dbContext.PaymentDetailsPayPals.Where(x => x.RefCustomerID == customerID && x.TrackID == trackID && x.TrackKey == trackKey).AsNoTracking().FirstOrDefault();
            }
        }

        public bool UpdatePayPalPaymentEntry(PaymentDetailsPayPal paypal)
        {
            bool exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var paymentStatus = Convert.ToString((int)Eduegate.Framework.Payment.TransactionStatus.Initiated);
                var paymentDetail = db.PaymentDetailsPayPals.Where(p => p.TrackID == paypal.TrackID
                    && p.TrackKey == paypal.TrackKey && p.PaymentID == paypal.PaymentID
                    && p.InitStatus == paymentStatus && p.RefCustomerID == paypal.RefCustomerID).SingleOrDefault();

                if (paymentDetail.IsNotNull())
                {
                    paymentDetail.TransMessage = paypal.TransMessage;
                    paymentDetail.TransDateTime = paypal.TransDateTime;
                    paymentDetail.TransStatus = paypal.TransStatus;

                    if (paypal.TransMessage.Trim().ToUpper() == "SUCCESS")
                    {
                        // Transaction properties
                        paymentDetail.TransID = paypal.TransID;
                        paymentDetail.TransAmount = paypal.TransAmount;
                        paymentDetail.TransCurrency = paypal.TransCurrency;
                        paymentDetail.TransPayerID = paypal.TransPayerID;
                        paymentDetail.TransPayerStatus = paypal.TransPayerStatus;
                        paymentDetail.TransPayerEmail = paypal.TransPayerEmail;
                        paymentDetail.TransPaymentType = paypal.TransPaymentType;
                        paymentDetail.TransReason = paypal.TransReason;
                        paymentDetail.TransNoOfCart = paypal.TransNoOfCart;
                        paymentDetail.TransAddressStatus = paypal.TransAddressStatus;
                        paymentDetail.TransAddressZip = paypal.TransAddressZip;
                        paymentDetail.TransAddressName = paypal.TransAddressName;
                        paymentDetail.TransAddressStreet = paypal.TransAddressStreet;
                        paymentDetail.TransAddressCountry = paypal.TransAddressCountry;
                        paymentDetail.TransAddressCity = paypal.TransAddressCity;
                        paymentDetail.TransAddressState = paypal.TransAddressState;
                        paymentDetail.TransResidenceCountry = paypal.TransResidenceCountry;
                        paymentDetail.TransAmountFee = paypal.TransAmountFee;
                        //paymentDetail.ExRateUSD = Convert.ToDecimal(paypal.ExRateUSD);
                    }

                    db.SaveChanges();
                    exit = true;
                }
            }
            return exit;
        }

        public PaymentDetailsPayPal GetPaymentDetailPaypal(long orderID, long trackID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (trackID > 0)
                {
                    return dbContext.PaymentDetailsPayPals.Where(x => x.TrackID == trackID).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return dbContext.PaymentDetailsPayPals.Where(x => x.OrderID == orderID).AsNoTracking().FirstOrDefault();
                }
            }
        }

        public bool UpdatePayPalIPNStatus(PaymentDetailsPayPal payment)
        {
            var exit = false;

            using (var db = new dbEduegateERPContext())
            {
                var paymentdetail = db.PaymentDetailsPayPals.FirstOrDefault(p => p.TrackID == payment.TrackID && p.TrackKey == payment.TrackKey && p.PaymentID == payment.PaymentID && p.RefCustomerID == payment.RefCustomerID);

                if (paymentdetail == null) return exit;
                paymentdetail.IpnHandlerVerified = payment.IpnHandlerVerified;
                paymentdetail.IpnHandlerTransID = payment.IpnHandlerTransID;
                paymentdetail.IpnHandlerUpdatedOn = payment.IpnHandlerUpdatedOn;
                db.SaveChanges();
                exit = true;
            }
            return exit;
        }

        public bool UpdatePDTData(PaymentDetailsPayPal payment)
        {
            var exit = false;

            using (var db = new dbEduegateERPContext())
            {
                var paymentdetail = db.PaymentDetailsPayPals.FirstOrDefault(p => p.TrackID == payment.TrackID && p.TrackKey == payment.TrackKey && p.PaymentID == payment.PaymentID && p.RefCustomerID == payment.RefCustomerID);

                if (paymentdetail == null) return exit;
                paymentdetail.PaypalDataTransferData = payment.PaypalDataTransferData;
                db.SaveChanges();
                exit = true;
            }
            return exit;
        }


        #endregion

        #region TheFort
        public bool LogTheFortPaymentRequest(PaymentDetailsTheFort fort)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    //fort.InitLocation = GetCountryNameByIP(fort.InitLocation, dbContext);
                    dbContext.PaymentDetailsTheForts.Add(fort);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Thefort payment details. TrackId:" + fort.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }
        public bool UpdateTheFortPaymentEntry(PaymentDetailsTheFort fort)
        {
            bool exit = false;
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    var paymentStatus = Convert.ToString((int)Eduegate.Framework.Payment.TransactionStatus.Initiated);
                    var paymentDetail = db.PaymentDetailsTheForts.Where(p =>
                        p.TrackKey == fort.TrackKey && p.InitIP == fort.PTransCustomerIP
                        && p.InitStatus == paymentStatus && p.PCustomerEmail == fort.PTransCustomerEmail).SingleOrDefault();

                    if (paymentDetail.IsNotNull())
                    {
                        paymentDetail.PTransMerchantReference = fort.PTransMerchantReference;
                        paymentDetail.PTransResponseMessage = fort.PTransResponseMessage;
                        paymentDetail.PTransCommand = fort.PTransCommand;
                        paymentDetail.PTransPaymentOption = fort.PTransPaymentOption;
                        paymentDetail.TLanguage = fort.TLanguage;
                        paymentDetail.PTransExpiryDate = fort.PTransExpiryDate;
                        paymentDetail.PTransAuthorizationCode = fort.PTransAuthorizationCode;
                        paymentDetail.PTransActualAmount = fort.PTransActualAmount;
                        paymentDetail.PTransCurrency = fort.PTransCurrency;
                        paymentDetail.PTransCustomerIP = fort.PTransCustomerIP;
                        paymentDetail.PTransResponseCode = fort.PTransResponseCode;
                        paymentDetail.PTransEci = fort.PTransEci;
                        paymentDetail.PTransStatus = fort.PTransStatus;
                        paymentDetail.PTransCardNumber = fort.PTransCardNumber;
                        paymentDetail.PTransAccessCode = fort.PTransAccessCode;
                        paymentDetail.PTransSignature = fort.PTransSignature;
                        paymentDetail.PTransMerchantIdentifier = fort.PTransMerchantIdentifier;
                        paymentDetail.TransID = fort.TransID;
                        paymentDetail.InitStatus = fort.InitStatus;
                        paymentDetail.AdditionalDetails= fort.AdditionalDetails;
                        paymentDetail.CardHolderName = fort.CardHolderName;

                        db.SaveChanges();
                        exit = true;
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save TheFort payment details. TrackId:" + fort.TrackID.ToString(), TrackingCode.PaymentGateway);
            }
            return exit;
        }

        public PaymentDetailsKnet GetPaymentKnetDetails(long orderID, long trackID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (trackID > 0)
                {
                    return dbContext.PaymentDetailsKnets.Where(x => x.TrackID == trackID).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return dbContext.PaymentDetailsKnets.Where(x => x.OrderID == trackID).AsNoTracking().FirstOrDefault();
                }
            }

        }

        public PaymentMasterVisa GetMasterVisaDetails(long orderID, long trackID = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (trackID > 0)
                {
                    return dbContext.PaymentMasterVisas.Where(x => x.TrackIID == trackID).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return dbContext.PaymentMasterVisas.Where(x => x.OrderID == trackID).AsNoTracking().FirstOrDefault();
                }
            }

        }


        public PaymentDetailsTheFort GetPaymentDetailTheFort(long orderID, long trackKey = default(long))
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (trackKey > 0)
                {
                    return dbContext.PaymentDetailsTheForts.Where(x => x.TrackKey == trackKey).AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return dbContext.PaymentDetailsTheForts.Where(x => x.OrderID == orderID).AsNoTracking().FirstOrDefault();
                }
            }
        }

        public long GetFortCustomerID(string trackKey, string email)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (dbContext.PaymentDetailsTheForts.Where(a => a.PMerchantReference == trackKey && a.PCustomerEmail == email).AsNoTracking().Any())
                {
                    return dbContext.PaymentDetailsTheForts.Where(a => a.PMerchantReference == trackKey && a.PCustomerEmail == email).AsNoTracking().Select(a => a.CustomerID).FirstOrDefault();
                }
                else
                {
                    return 0;
                }
            }
        }

        public PaymentDetailsTheFort GetPaymentDetailsTheFortByMerchantReference(string trackkey)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentDetailsTheForts.Where(a => a.PMerchantReference == trackkey).AsNoTracking().FirstOrDefault();
            }

        }
        #endregion


        private static string GetCountryNameByIP(string initLocation, dbEduegateERPContext dbContext)
        {
            long? dottedIP = Convert.ToInt64(initLocation);
            string countryName = string.Empty;
            IP2Country ip2Conutry = dbContext.IP2Country.Where(x => x.BeginningIP <= dottedIP && x.EndingIP >= dottedIP).AsNoTracking().FirstOrDefault();
            if (ip2Conutry.IsNotNull())
            {
                countryName = ip2Conutry.CountryName;
            }
            return countryName;
        }

        public bool MakePaymentErrorEntry(PaymentDetailsLog paymentErrorLog)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.PaymentDetailsLogs.Add(paymentErrorLog);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Payment details. TrackId:" + paymentErrorLog.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }



     

      
    }
}


