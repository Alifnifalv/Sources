using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;

namespace Eduegate.Web.Library.ViewModels
{
    public class PaymentViewModel
    {
        public decimal AmountReceived { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public string Guid { get; set; }
        public string Amount { get; set; }
        public string CustomerID { get; set; }
        public string EmailId { get; set; }
        public string PaymentID { get; set; }
        public string TrackKey { get; set; }
        public string TrackID { get; set; }
        public string SessionID { get; set; }
        public string TransactionStatus { get; set; }
        public Framework.Payment.TransactionStatus Status { get; set; }
        public string InitiatedOn { get; set; }
        public string InitiatedFromIP { get; set; }
        public string InitiatedLocation { get; set; }
        public string PaymentGatewayUrl { get; set; }
        public string SuccessReturnUrl { get; set; }
        public string CancelReturnUrl { get; set; }
        public string FailureReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderID { get; set; }
        public string AdditionalDetails { get; set; }
        public string PaymentGateway { get; set; }
        public string CustomAttributes { get; set; }
        public bool IsPaymentMocked { get; set; }
        public string AuthCode { get; set; }
        //public VoucherWalletTransactionDTO VoucherTransactionDetail { get; set; }


        public static PaymentDTO ToDTO(PaymentViewModel vm)
        {
            Mapper<PaymentViewModel, PaymentDTO>.CreateMap();
            return Mapper<PaymentViewModel, PaymentDTO>.Map(vm);
        }

        public static PaymentViewModel ToVM(PaymentDTO dto)
        {
            Mapper<PaymentDTO, PaymentViewModel>.CreateMap();
            return Mapper<PaymentDTO, PaymentViewModel>.Map(dto);
        }

    }
}