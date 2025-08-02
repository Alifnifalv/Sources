using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers.Payments
{
    class PaypalMapper : IDTOEntityMapper<PaypalPaymentDTO, PaymentDetailsPayPal>
    {
        private CallContext _context;
        public static PaypalMapper Mapper(CallContext context)
        {
            var mapper = new PaypalMapper();
            mapper._context = context;
            return mapper;
        }
        public PaypalPaymentDTO ToDTO(PaymentDetailsPayPal entity)
        {
            return new PaypalPaymentDTO()
            {
                TrackID = entity.TrackID,
                TrackKey = entity.TrackKey,
                RefCustomerID = entity.RefCustomerID,
                BusinessEmail = entity.BusinessEmail,
                PaymentID = entity.PaymentID,
                InitOn = entity.InitOn,
                InitStatus = entity.InitStatus,
                InitIP = entity.InitIP,
                InitLocation = entity.InitLocation,
                InitAmount = entity.InitAmount,
                InitCurrency = entity.InitCurrency,
                IpnVerified = entity.IpnVerified,
                TransID = entity.TransID,
                TransAmount = entity.TransAmount,
                TransCurrency = entity.TransCurrency,
                TransStatus = entity.TransStatus,
                TransPayerID = entity.TransPayerID,
                TransDateTime = entity.TransDateTime,
                TransPayerStatus = entity.TransPayerStatus,
                TransPayerEmail = entity.TransPayerEmail,
                TransPaymentType = entity.TransPaymentType,
                TransMessage = entity.TransMessage,
                TransOn = Convert.ToString(entity.TransOn),
                TransReason = entity.TransReason,
                TransAddressStatus = entity.TransAddressStatus,
                TransAddressCountryCode = entity.TransAddressCountryCode,
                TransAddressZip = entity.TransAddressZip,
                TransAddressName = entity.TransAddressName,
                TransAddressStreet = entity.TransAddressStreet,
                TransAddressCountry = entity.TransAddressCountry,
                TransAddressCity = entity.TransAddressCity,
                TransAddressState = entity.TransAddressState,
                TransResidenceCountry = entity.TransResidenceCountry,
                OrderID = entity.OrderID,
                IpnHandlerVerified = entity.IpnHandlerVerified,
                IpnHandlerTransID = entity.IpnHandlerTransID,
                IpnHandlerUpdatedOn = entity.IpnHandlerUpdatedOn,
                ExRateUSD = entity.ExRateUSD,
                InitAmountUSDActual = entity.InitAmountUSDActual,
                InitAmountUSD = entity.InitAmountUSD,
                IpnVerificationRequired = entity.IpnVerificationRequired,
                InitCartTotalUSD = entity.InitCartTotalUSD,
                TransAmountActual = entity.TransAmountActual,
                TransAmountFee = entity.TransAmountFee,
                TransExchRateKWD = entity.TransExchRateKWD,
                TransAmountActualKWD = entity.TransAmountActualKWD,
                TransOn2 = entity.TransOn2,
                CartID = entity.CartID,
            };
        }

        public PaymentDetailsPayPal ToEntity(PaypalPaymentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
