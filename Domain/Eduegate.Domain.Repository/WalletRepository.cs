using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Extensions;
using System.Diagnostics;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class WalletRepository
    {

        public List<WalletTransactionDTO> GetWalletTransactionDetails(long CustomerId, int PageNumber, int PageSize, CallContext callContext)
        {
            List<WalletTransactionDTO> ret = new List<WalletTransactionDTO>();
            short success = (short)Eduegate.Framework.Payment.WalletStatusMaster.Success;
            short failed = (short)Eduegate.Framework.Payment.WalletStatusMaster.Failed;
            short cancelled = (short)Eduegate.Framework.Payment.WalletStatusMaster.Cancelled;
            

            try
            {
                int languageId = 0;
                if (callContext.IsNotNull() && callContext.LanguageCode.IsNotNullOrEmpty())
                {
                    var languageDTO = UtilityRepository.GetLanguageCultureId(callContext.LanguageCode); 
                    if(languageDTO!=null){
                        languageId = languageDTO.Culture.CultureID;
                    }
                    
                }
                    
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    long customerId = Convert.ToInt64(CustomerId);

                    List<Int16?> transactionStatusList = new List<Int16?>() { success, failed, cancelled };


                    var val = from wtd in db.WalletTransactionDetails
                              join wtm in db.WalletTransactionMasters on wtd.RefTransactionRelationId equals wtm.TransactionRelationId
                              join wsm in db.WalletStatusMasters on wtd.StatusId equals wsm.StatusId

                              //from pd in db.PaymentDetails.Where(x => x.TrackID == wtd.TrackId).DefaultIfEmpty()
                              from pd in db.PaymentDetailsKnets.Where(x => x.TrackID == wtd.TrackId).DefaultIfEmpty()

                              from vwt in db.VoucherWalletTransactions.Where(x => x.WalletTransactionID == wtd.WalletTransactionId).DefaultIfEmpty()

                              //join pd in db.PaymentDetails on wtd.TrackId equals pd.TrackID into rjn
                              //from subrjn in rjn.DefaultIfEmpty()

                              orderby wtd.CreatedDateTime descending

                              where wtd.CustomerId.Equals(customerId)
                              && transactionStatusList.Contains(wtd.StatusId)
                              && wtm.LanguageID.Equals(languageId)
                              && wsm.LanguageID.Equals(languageId)
                              //
                              select new WalletTransactionDTO
                              {
                                  TransactionDescription = wtm.Description,
                                  TransactionDateAndTime = wtd.CreatedDateTime.ToString(),
                                  Amount = wtd.Amount.ToString(),
                                  //TransactionReference = wtd.CustomerWalletTranRef,
                                  TransactionReference = wtd.RefOrderId,
                                  Status = wsm.Description,
                                  StatusId = wsm.StatusId,
                                  //PaymentId = subrjn.PaymentID,
                                  //TransId = subrjn.TransID,
                                  //PaymentOption = wtd.PaymentOption,
                                  //ReferenceID = subrjn.TransRef
                                  PaymentId = pd.PaymentID,
                                  TransId = pd.TransID,
                                  PaymentMethod = wtd.PaymentMethod,
                                  ReferenceID = pd.TransRef,
                                  VoucherNumber = vwt.VoucherNo

                              };
                    ret = val
                        .AsNoTracking().ToList();
                }

            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get Wallet transaction details", TrackingCode.Wallet);
            }
            return ret;
        }

        public bool CreateWalletCustomerLog(WalletCustomerLog log)
        {
            bool result = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //delete all the logs for this customer 
                var userLogs = dbContext.WalletCustomerLogs.Where(w => w.CustomerId == log.CustomerId)
                    .AsNoTracking()
                    .ToList();
                dbContext.WalletCustomerLogs.RemoveRange(userLogs);
                dbContext.WalletCustomerLogs.Add(log);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public long MakeWalletEntry(WalletTransactionDetail walletTransaction)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.WalletTransactionDetails.Add(walletTransaction);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to create new Wallet entry. TrackId:" + walletTransaction.TrackId.ToString(), TrackingCode.Wallet);
            }

            return walletTransaction.WalletTransactionId;
        }

        public bool UpdateWalletEntry(WalletTransactionDetail walletTransaction)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    WalletTransactionDetail updateWallet = dbContext.WalletTransactionDetails.Where(x => x.TrackId == walletTransaction.TrackId && x.CustomerId == walletTransaction.CustomerId)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (updateWallet.IsNotNull())
                    {
                        updateWallet.StatusId = walletTransaction.StatusId;
                        updateWallet.ModifiedDateTime = DateTime.UtcNow;
                        //updateWallet.Amount = walletTransaction.Amount;
                        updateWallet.AdditionalDetails = updateWallet.AdditionalDetails.IsNotNullOrEmpty() ? string.Concat(updateWallet.AdditionalDetails, ">", walletTransaction.AdditionalDetails) : walletTransaction.AdditionalDetails;
                        dbContext.SaveChanges();
                        status = true;
                    }
                }
            }
            //TODO: Need to handle this exception
            //catch (DbEntityValidationException entityEx)
            //{
            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = entityEx.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join("; ", errorMessages);
            //    SystemLog.Log("Application", EventLogEntryType.Error, "Not able to update Wallet entry. TrackId:" + walletTransaction.TrackId.ToString(), fullErrorMessage, TrackingCode.Wallet);
            //}
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update Wallet entry. TrackId:" + walletTransaction.TrackId.ToString(), TrackingCode.Wallet);
            }
            
            return status;
        }

        public WalletCustomerLog GetWalletCustomer(string walletGUID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.WalletCustomerLogs.Where(x => x.Guid.Equals(walletGUID))
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public decimal GetWalletBalance(string walletGUID)
        {
            decimal walletBalance = 0;
            short walletSuccessStatus = (short)Eduegate.Framework.Payment.WalletStatusMaster.Success; //success
            using (dbEduegateERPContext dbcontext = new dbEduegateERPContext())
            {
                var walletCustomer = dbcontext.WalletCustomerLogs.Where(x => x.Guid.Equals(walletGUID))
                    .AsNoTracking()
                    .FirstOrDefault();
                if (walletCustomer.IsNotNull())
                {
                    var wallet = dbcontext.WalletTransactionDetails.Where(x => x.CustomerId.Equals(walletCustomer.CustomerId) && x.StatusId == walletSuccessStatus);
                    if (wallet.Count() > 0)
                        walletBalance = wallet.Sum(x => x.Amount);
                }
            }
            return walletBalance;
        }

        public bool SignoutWallet(string walletGUID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var walletCustomer = dbContext.WalletCustomerLogs.Where(x => x.Guid.Equals(walletGUID))
                    .AsNoTracking()
                    .FirstOrDefault();
                if (walletCustomer.IsNotNull())
                {
                    var allWalletInstances = dbContext.WalletCustomerLogs.Where(x => x.CustomerId.Equals(walletCustomer.CustomerId));
                    dbContext.WalletCustomerLogs.RemoveRange(allWalletInstances);
                }
                dbContext.SaveChanges();
            }
            return true;
        }

        public long CreateVoucherWalletTransaction(VoucherWalletTransaction voucherDetail)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.VoucherWalletTransactions.Add(voucherDetail);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //to do
                //SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to create new Wallet entry. TrackId:" + walletTransaction.TrackId.ToString(), TrackingCode.Wallet);
            }
            return voucherDetail.TransID;
        }

        public bool UpdateWalletTransactionStatus(long walletTransactionId, short statusId)
        {
            bool status = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    WalletTransactionDetail updateWallet = dbContext.WalletTransactionDetails.Where(x => x.WalletTransactionId == walletTransactionId)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (updateWallet.IsNotNull())
                    {
                        updateWallet.StatusId = statusId;
                        updateWallet.ModifiedDateTime = DateTime.UtcNow;
                        dbContext.SaveChanges();
                        status = true;
                    }
                }
            }
            //TODO: Need to handle this exception
            //catch (DbEntityValidationException entityEx)
            //{
            //    // Retrieve the error messages as a list of strings.
            //    var errorMessages = entityEx.EntityValidationErrors
            //            .SelectMany(x => x.ValidationErrors)
            //            .Select(x => x.ErrorMessage);

            //    // Join the list to a single string.
            //    var fullErrorMessage = string.Join("; ", errorMessages);
            //    SystemLog.Log("Application", EventLogEntryType.Error, "Not able to update Wallet entry. WalletTransactionID:" + walletTransactionId.ToString(), fullErrorMessage, TrackingCode.Wallet);
            //}
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update Wallet entry. WalletTransactionID:" + walletTransactionId.ToString(), TrackingCode.Wallet);
            }

            return status;
        }
    }
}
