using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Payment;
using System.Data.Entity.Validation;
using Eduegate.Framework.Helper;
using System.Data.Entity;
using Eduegate.Services.Contracts.Payments;

namespace Eduegate.Domain.Repository
{
    //TODO: Later this has to be moved to single table rather than having different tables for different payment gateways
    public class PaymentRepository
    {
        public bool MakeKNETPaymentEntry(PaymentDetail paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                    dbContext.PaymentDetails.Add(paymentDetail);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public bool UpdateKNETPaymentEntry(PaymentDetail paymentDetail)
        {
            bool status = false;
            try
            {

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    PaymentDetail paymentDetailUpdate = dbContext.PaymentDetails.Where(
                        x => x.TrackID.Equals(paymentDetail.TrackID)
                        && x.TrackKey.Equals(paymentDetail.TrackKey)
                        && x.RefCustomerID.Equals(paymentDetail.RefCustomerID)
                        && x.SessionID.Equals(paymentDetail.SessionID)
                        && x.PaymentID.Equals(paymentDetail.PaymentID)
                        && x.InitStatus.Equals("1")).FirstOrDefault();
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

        public bool MakePaymentErrorEntry(PaymentDetailsLog paymentDetailError)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.PaymentDetailsLogs.Add(paymentDetailError);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save Payment details. TrackId:" + paymentDetailError.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public bool MakeMIGSPaymentEntry(PaymentDetailsMasterVisa paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                    dbContext.PaymentDetailsMasterVisas.Add(paymentDetail);
                    dbContext.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to save MIGS payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public bool UpdateMIGSPaymentEntry(PaymentDetailsMasterVisa paymentDetail)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    PaymentDetailsMasterVisa paymentDetailUpdate = dbContext.PaymentDetailsMasterVisas.Where(
                         x => x.TrackID.Equals(paymentDetail.TrackID)
                         && x.TrackKey.Equals(paymentDetail.TrackKey)
                         && x.RefCustomerID.Equals(paymentDetail.RefCustomerID)
                         && x.SessionID.Equals(paymentDetail.SessionID)
                         && x.PaymentID.Equals(paymentDetail.PaymentID)
                         && x.InitStatus.Equals("1")).FirstOrDefault();
                    if (paymentDetailUpdate.IsNotNull())
                    {
                        paymentDetailUpdate.InitStatus = paymentDetail.InitStatus;
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
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update MIGS Payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public PaymentDTO GetPaymentDetailKNET(long orderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from payment in dbContext.PaymentDetailsKnets
                        where payment.OrderID == orderID
                        select new PaymentDTO
                        {
                            PaymentID = payment.PaymentID.ToString(),
                            Amount = payment.PaymentAmount.ToString(),
                            TrackID = payment.TrackID.ToString(),
                            InitiatedOn = payment.TransOn.ToString(),
                            // Fill other fields if necessary
                        }).Single();
            }
        }

        public PaymentDTO GetPaymentDetailMIGS(long orderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from payment in dbContext.PaymentMasterVisas
                        where payment.OrderID == orderID
                        select new PaymentDTO
                        {
                            PaymentID = payment.PaymentID.ToString(),
                            Amount = payment.PaymentAmount.ToString(),
                            TrackID = payment.TrackIID.ToString(),
                            InitiatedOn = payment.InitOn.ToString(),
                            // Fill other fields if necessary
                        }).Single();
            }
        }

        public bool MakePayPalPaymentEntry(PaymentDetailsPayPal paymentDetail)
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

        public bool UpdateMIGSPaymentEntry(PaymentDetailsPayPal paymentDetail)
        {
            bool status = false;
            decimal amount = default(decimal);
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    PaymentDetailsPayPal paymentDetailUpdate = dbContext.PaymentDetailsPayPals.Where(
                         x => x.TrackID.Equals(paymentDetail.TrackID)
                         && x.TrackKey.Equals(paymentDetail.TrackKey)
                         && x.RefCustomerID.Equals(paymentDetail.RefCustomerID)
                         && x.PaymentID.Equals(paymentDetail.PaymentID)
                         && x.InitStatus.Equals("1")).FirstOrDefault();
                    if (paymentDetailUpdate.IsNotNull())
                    {
                        paymentDetailUpdate.InitStatus = paymentDetail.InitStatus;
                        paymentDetailUpdate.TransID = paymentDetail.TransID;
                        paymentDetailUpdate.TransAmount = paymentDetail.TransAmount;
                        paymentDetailUpdate.TransCurrency = paymentDetail.TransCurrency;
                        paymentDetailUpdate.TransStatus = paymentDetail.TransStatus;
                        paymentDetailUpdate.TransExchRateKWD = paymentDetail.TransExchRateKWD;
                        paymentDetailUpdate.TransPayerID = paymentDetail.TransPayerID;
                        paymentDetailUpdate.TransDateTime = paymentDetail.TransDateTime;
                        paymentDetailUpdate.TransAddressCountryCode = paymentDetail.TransAddressCountryCode;
                        paymentDetailUpdate.TransPayerStatus = paymentDetail.TransPayerStatus;
                        paymentDetailUpdate.TransPayerEmail = paymentDetail.TransPayerEmail;
                        paymentDetailUpdate.TransPaymentType = paymentDetail.TransPaymentType;
                        paymentDetailUpdate.IpnVerified = paymentDetail.IpnVerified;
                        paymentDetailUpdate.TransOn = DateTime.Now;
                        paymentDetailUpdate.TransMessage = paymentDetail.TransMessage;
                        paymentDetailUpdate.TransReason = paymentDetail.TransReason;
                        paymentDetailUpdate.TransNoOfCart = paymentDetail.TransNoOfCart;
                        paymentDetailUpdate.TransAddressStatus = paymentDetail.TransAddressStatus;
                        paymentDetailUpdate.TransAddressZip = paymentDetail.TransAddressZip;
                        paymentDetailUpdate.TransAddressName = paymentDetail.TransAddressName;
                        paymentDetailUpdate.TransAddressStreet = paymentDetail.TransAddressStreet;
                        paymentDetailUpdate.TransAddressCountry = paymentDetail.TransAddressCountry;
                        paymentDetailUpdate.TransAddressCity = paymentDetail.TransAddressCity;
                        paymentDetailUpdate.TransAddressState = paymentDetail.TransAddressState;
                        paymentDetailUpdate.TransResidenceCountry = paymentDetail.TransResidenceCountry;
                        paymentDetailUpdate.TransAmountFee = paymentDetail.TransAmountFee;
                        amount = paymentDetail.TransAmount ?? amount;
                        paymentDetailUpdate.TransAmountActual = amount - paymentDetail.TransAmountFee;
                        paymentDetailUpdate.TransAmountActualKWD = (amount - paymentDetail.TransAmountFee) * Convert.ToDecimal(paymentDetail.TransExchRateKWD);
                        paymentDetailUpdate.TransOn2 = DateTime.Now;
                        dbContext.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update PayPal Payment details. TrackId:" + paymentDetail.TrackID.ToString(), TrackingCode.PaymentGateway);
            }

            return status;
        }

        public string GetReturnUrl(string trackId, string gatewayType)
        {
            string returnUrl = string.Empty;
            long paymentTrackId = 0;

            if (trackId.IsNotNullOrEmpty())
                paymentTrackId = Convert.ToInt64(trackId);

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                switch (gatewayType.ParseEnum<PaymentGatewayType>())
                {
                    case PaymentGatewayType.KNET:
                        var paymentDetail = dbContext.PaymentDetails.Where(x => x.TrackID == paymentTrackId).FirstOrDefault();
                        if (returnUrl.IsNotNull())
                            returnUrl = paymentDetail.ReturnUrl;
                        break;
                    case PaymentGatewayType.MIGS:
                        var migsPaymentDetail = dbContext.PaymentDetailsMasterVisas.Where(x => x.TrackID == paymentTrackId).FirstOrDefault();
                        if (returnUrl.IsNotNull())
                            returnUrl = migsPaymentDetail.ReturnUrl;
                        break;
                    case PaymentGatewayType.PAYPAL:
                        var paypalPaymentDetail = dbContext.PaymentDetailsPayPals.Where(x => x.TrackID == paymentTrackId).FirstOrDefault();
                        if (returnUrl.IsNotNull())
                            returnUrl = paypalPaymentDetail.ReturnUrl;
                        break;

                }
            }
            return returnUrl;
        }

        private static string GetCountryNameByIP(string initLocation, dbEduegateERPContext dbContext)
        {
            long? dottedIP = Convert.ToInt64(initLocation);
            string countryName = string.Empty;
            IP2Country ip2Conutry = dbContext.IP2Country.Where(x => x.BeginningIP <= dottedIP && x.EndingIP >= dottedIP).FirstOrDefault();
            if (ip2Conutry.IsNotNull())
            {
                countryName = ip2Conutry.CountryName;
            }
            return countryName;
        }


        public List<TransactionHeadShoppingCartMap> GetOrderByCartId(long cartId)
        {
            using (var db = new dbEduegateERPContext())
            {

                var order = (from thscm in db.TransactionHeadShoppingCartMaps
                             join ent in db.TransactionHeadEntitlementMaps
                             on thscm.TransactionHeadID equals ent.TransactionHeadID
                             where thscm.ShoppingCartID == cartId
                             select thscm).ToList();
                return order;
            }
        }

        public PaymentDetailsTheFort GetPaymentDetails(List<long> heads)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.PaymentDetailsTheForts.Where(x => heads.Contains((long)x.OrderID)).SingleOrDefault();

            }
        }

        public List<TransactionHeadEntitlementMap> GetEntitlementbyHeadID(List<long?> heads)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.TransactionHeadEntitlementMaps.Where(x => heads.Contains(x.TransactionHeadID)).Include(x => x.EntityTypeEntitlement).Include(x => x.TransactionHead).ToList();

            }
        }

        public PaymentMasterVisaDTO MakePaymentEntry(PaymentMasterVisa paymentDetail)
        {
            var paymentMasterVisDTO = new PaymentMasterVisaDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                if (paymentDetail.InitLocation != null)
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                }

                //dbContext.PaymentMasterVisas.Add(paymentDetail);
                dbContext.Entry(paymentDetail).State = System.Data.Entity.EntityState.Added;

                dbContext.SaveChanges();

                paymentMasterVisDTO = new PaymentMasterVisaDTO()
                {
                    TrackIID = paymentDetail.TrackIID,
                    TransID = paymentDetail.TransID,
                    CustomerID = paymentDetail.CustomerID,
                    IsMasterVisaSaved = true,
                };
            }

            return paymentMasterVisDTO;
        }

        public PaymentMasterVisa GetMasterVisaDetails(long cartID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMasterVisas.Where(x => x.CartID == cartID)
                   .OrderByDescending(x => x.TrackIID)
                   .FirstOrDefault();
            }
        }

        public List<PaymentMasterVisa> GetAllMasterVisaDetails(long cartID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentMasterVisas.Where(x => x.CartID == cartID)
                   .OrderByDescending(x => x.TrackIID)
                   .ToList();
            }
        }

        public PaymentLog GetEncriptedResponse(long orderID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentLogs.Where(x => x.TrackID == orderID).ToList().LastOrDefault();
            }
        }

        public void UpdatePaymentLogs(PaymentMasterVisaDTO paymentMasterVisa)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentLogData = dbContext.PaymentLogs.Where(x => x.CustomerID == paymentMasterVisa.CustomerID && (x.Amount != null || x.Amount > 0))
                    .OrderByDescending(y => y.PaymentLogIID).FirstOrDefault();

                //paymentLogData.TrackID = paymentMasterVisa.TrackIID;
                paymentLogData.TransNo = paymentMasterVisa.TransID;


                dbContext.Entry(paymentLogData).State = System.Data.Entity.EntityState.Modified;

                dbContext.SaveChanges();
            }
        }

    }
}