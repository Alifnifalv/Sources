using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using nsPayment = Eduegate.Framework.Payment;
using System.Globalization;
using Eduegate.Framework.Helper;
using Eduegate.Framework;
using Eduegate.Domain.Mappers;

namespace Eduegate.Domain
{
    public class WalletBL
    {
        //private WalletRepository walletRepository = new WalletRepository();
        //private CustomerRepository customerRepository = new CustomerRepository();
        //private VoucherRepository voucherRepository = new VoucherRepository();

        private CallContext _callContext;

        public WalletBL(CallContext context)
        {
            _callContext = context;
        }
        public List<WalletTransactionDTO> GetWalletTransactionDetails(string CustomerId, int PageNumber, int PageSize, CallContext callContext)
        {
            var walletCustomerLog = new WalletRepository().GetWalletCustomer(CustomerId);
            List<WalletTransactionDTO> transactionList = new WalletRepository().GetWalletTransactionDetails(walletCustomerLog.CustomerId, PageNumber, PageSize, callContext);
            foreach (WalletTransactionDTO walleTransaction in transactionList)
            {
                if (walleTransaction.TransactionDateAndTime.IsNotNull())
                {
                    //setting date format
                    DateTime tranDateTime = Convert.ToDateTime(walleTransaction.TransactionDateAndTime, CultureInfo.InvariantCulture);
                    walleTransaction.TransactionDateAndTime = tranDateTime.ToString("dd-MMM-yyyy hh:mm");
                }
                //setting amount format
                walleTransaction.Amount = Utility.FormatDecimal(Convert.ToDecimal(walleTransaction.Amount), 3);
            }
            return transactionList;
        }

        public long CreateWalletEntry(nsPayment.PaymentDTO wallet)
        {
            var walletCustomerLog = new WalletRepository().GetWalletCustomer(wallet.Guid);
            WalletTransactionDetail walletTransaction = new WalletTransactionDetail()
            {
                RefTransactionRelationId = (short)nsPayment.WalletTransactionMaster.WalletTransaction,
                CustomerId = Convert.ToInt64(walletCustomerLog.CustomerId),
                Amount = Convert.ToDecimal(wallet.Amount),
                AdditionalDetails = wallet.AdditionalDetails,
                CreatedDateTime = DateTime.Now,
                StatusId = (short)nsPayment.WalletStatusMaster.Initiated,
                TrackId = Convert.ToInt64(wallet.TrackID), // convert from string to long
                RefOrderId = wallet.OrderID,
                PaymentMethod = wallet.PaymentGateway.ToString()
            };
            return new WalletRepository().MakeWalletEntry(walletTransaction);
        }

        public bool CreateWalletCustomerLog(WalletCustomerLogDTO log)
        {
            return new WalletRepository().CreateWalletCustomerLog(WalletCustomerLogMapper.Mapper().ToEntity(log));
        }

        public bool UpdateWalletEntry(nsPayment.PaymentDTO wallet)
        {

            WalletTransactionDetail walletTransaction = new WalletTransactionDetail()
            {
                TrackId = Convert.ToInt64(wallet.TrackID),
                CustomerId = Convert.ToInt64(wallet.CustomerID),
                AdditionalDetails = wallet.AdditionalDetails,
                ModifiedDateTime = DateTime.Now,
                StatusId = Convert.ToInt16(wallet.TransactionStatus),
                RefOrderId = wallet.OrderID,
            };
            return new WalletRepository().UpdateWalletEntry(walletTransaction);
        }

        public WalletCustomerDTO GetCustomerDetails(string walletGuid)
        {
            WalletCustomerDTO walletCustomerDTO = default(WalletCustomerDTO);
            var walletCustomerLog = new WalletRepository().GetWalletCustomer(walletGuid);


            if (walletCustomerLog.IsNotNull())
            {
                var customer = new CustomerRepository().GetWalletCustomerDetails(walletCustomerLog.CustomerId);

                if (customer.IsNotNull())
                {
                    walletCustomerDTO = new WalletCustomerDTO()
                    {
                        WalletGuid = walletGuid,
                        CustomerId = customer.CustomerIID.ToString(),
                        //CustomerSessionId = walletCustomerLog.CustomerSessionId,
                        CustomerFirstName = customer.FirstName,
                        CustomerLastName = customer.LastName,
                        CustomerEmailId = customer.Login.LoginEmailID,
                    };

                }

            }
            return walletCustomerDTO;
        }

        public decimal GetWalletBalance(string customerId)
        {
            decimal walletAmount = new WalletRepository().GetWalletBalance(customerId);
            return Convert.ToDecimal(Utility.FormatDecimal(walletAmount, 3));
        }

        public bool SignoutWallet(string customerId)
        {
            return new WalletRepository().SignoutWallet(customerId);
        }

        public long CreateVoucherWalletTransaction(Eduegate.Framework.Payment.VoucherWalletTransactionDTO voucherWalletTransaction)
        {
            //VoucherWalletTransactionDTO VoucherWalletTransactionDTO = default(VoucherWalletTransactionDTO);

            //mapping DTO to entity
            VoucherWalletTransaction VoucherWalletTransactionEntity = new VoucherWalletTransaction()
            {
                TransID = voucherWalletTransaction.TransID,
                VoucherNo = voucherWalletTransaction.VoucherNo,
                WalletTransactionID = voucherWalletTransaction.WalletTransactionID,
                //Amount = voucherWalletTransaction.Amount,
                
            };

            return new WalletRepository().CreateVoucherWalletTransaction(VoucherWalletTransactionEntity);
            
            //remap to DTO
            //if (VoucherWalletTransactionEntity.IsNotNull())
            //{
            //    VoucherWalletTransactionDTO.TransID = VoucherWalletTransactionEntity.TransID;
            //    VoucherWalletTransactionDTO.VoucherNo = VoucherWalletTransactionEntity.VoucherNo;
            //    VoucherWalletTransactionDTO.WalletTransactionID = VoucherWalletTransactionEntity.WalletTransactionID;
            //    VoucherWalletTransactionDTO.Amount = VoucherWalletTransactionEntity.Amount;
            //}

            
        }


        public bool UpdateWalletTransactionStatus(string walletTransactionId, string statusId)
        {
            return new WalletRepository().UpdateWalletTransactionStatus(Convert.ToInt64(walletTransactionId),Convert.ToInt16(statusId));
        }


        
       
    }
}
