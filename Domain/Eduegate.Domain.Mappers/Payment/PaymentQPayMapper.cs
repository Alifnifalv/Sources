using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Payments;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.Payment
{
    public class PaymentQPayMapper : DTOEntityDynamicMapper
    {
        public static PaymentQPayMapper Mapper(CallContext context)
        {
            var mapper = new PaymentQPayMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PaymentLogDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public PaymentQPAYDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PaymentQPays.Where(x => x.PaymentQPayIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var PaymentLogDTO = new PaymentQPAYDTO()
                {
                    PaymentQPayIID = entity.PaymentQPayIID,
                    LoginID = entity.LoginID,
                    SecureKey = entity.SecureKey,
                    SecureHash = entity.SecureHash,
                    PaymentAmount = entity.PaymentAmount,
                    TransactionRequestDate = entity.TransactionRequestDate,
                    ActionID = entity.ActionID,

                    BankID = entity.BankID,
                    NationalID = entity.NationalID,
                    MerchantID = entity.MerchantID,
                    Lang = entity.Lang,


                    CurrencyCode = entity.CurrencyCode,
                    ExtraFields_f14 = entity.ExtraFields_f14,
                    Quantity = entity.Quantity,
                    PaymentDescription = entity.PaymentDescription,


                    MerchantGatewayUrl = entity.MerchantGatewayUrl,
                    ResponseAcquirerID = entity.ResponseAcquirerID,
                    ResponseAmount = entity.ResponseAmount,
                    ResponseBankID = entity.ResponseBankID,


                    ResponseCardExpiryDate = entity.ResponseCardExpiryDate,
                    ResponseCardHolderName = entity.ResponseCardHolderName,
                    ResponseCardNumber = entity.ResponseCardNumber,
                    ResponseConfirmationID = entity.ResponseConfirmationID,


                    ResponseCurrencyCode = entity.ResponseCurrencyCode,
                    ResponseEZConnectResponseDate = entity.ResponseEZConnectResponseDate,
                    ResponseLang = entity.ResponseLang,
                    ResponseMerchantID = entity.ResponseMerchantID,

                    ResponseMerchantModuleSessionID = entity.ResponseMerchantModuleSessionID,
                    ResponsePUN = entity.ResponsePUN,
                    ResponseStatus = entity.ResponseStatus,
                    ResponseStatusMessage = entity.ResponseStatusMessage,

                    ResponseSecureHash = entity.ResponseSecureHash,
                    LogType = entity.LogType

                };

                return PaymentLogDTO;
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PaymentQPAYDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new PaymentQPay()
                {
                    LoginID = toDto.LoginID,
                    SecureKey = toDto.SecureKey,
                    SecureHash = toDto.SecureHash,
                    PaymentAmount = toDto.PaymentAmount.Value,
                    TransactionRequestDate = toDto.TransactionRequestDate,
                    ActionID = toDto.ActionID,

                    BankID = toDto.BankID,
                    NationalID = toDto.NationalID,
                    MerchantID = toDto.MerchantID,
                    Lang = toDto.Lang,


                    CurrencyCode = toDto.CurrencyCode,
                    ExtraFields_f14 = toDto.ExtraFields_f14,
                    Quantity = toDto.Quantity,
                    PaymentDescription = toDto.PaymentDescription,


                    MerchantGatewayUrl = toDto.MerchantGatewayUrl,
                };
                if (entity.PaymentQPayIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.PaymentQPayIID));
            }
        }

        public PaymentQPAYDTO GetPaymentQpayData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentQPAYDTO = new PaymentQPAYDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentQPays.Where(x => x.LoginID == loginID &&
                (x.TransactionRequestDate.Value.Year == currentDate.Year && x.TransactionRequestDate.Value.Month == currentDate.Month && x.TransactionRequestDate.Value.Day == currentDate.Day) &&
                (x.PaymentAmount != null || x.PaymentAmount > 0))
                    .OrderByDescending(y => y.PaymentQPayIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (data != null)
                {
                    paymentQPAYDTO = new PaymentQPAYDTO()
                    {
                        SecureKey = data.SecureKey,
                        SecureHash = data.SecureHash,
                        PaymentAmount = data.PaymentAmount,
                        TransactionRequestDate = data.TransactionRequestDate,
                        ActionID = data.ActionID,
                        BankID = data.BankID,
                        NationalID = data.NationalID,
                        MerchantID = data.MerchantID,
                        Lang = data.Lang,
                    };
                }

                return paymentQPAYDTO;
            }
        }

        public PaymentQPAYDTO GetPaymentQPayData(string pun)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentQPAYDTO = new PaymentQPAYDTO();
                long iID = long.Parse(pun);
                var entity = dbContext.PaymentQPays.Where(x => x.PaymentQPayIID == iID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    paymentQPAYDTO = new PaymentQPAYDTO()
                    {
                        PaymentQPayIID = entity.PaymentQPayIID,
                        LoginID = entity.LoginID,
                        SecureKey = entity.SecureKey,
                        SecureHash = entity.SecureHash,
                        PaymentAmount = entity.PaymentAmount,
                        TransactionRequestDate = entity.TransactionRequestDate,
                        ActionID = entity.ActionID,

                        BankID = entity.BankID,
                        NationalID = entity.NationalID,
                        MerchantID = entity.MerchantID,
                        Lang = entity.Lang,


                        CurrencyCode = entity.CurrencyCode,
                        ExtraFields_f14 = entity.ExtraFields_f14,
                        Quantity = entity.Quantity,
                        PaymentDescription = entity.PaymentDescription,


                        MerchantGatewayUrl = entity.MerchantGatewayUrl,
                        ResponseAcquirerID = entity.ResponseAcquirerID,
                        ResponseAmount = entity.ResponseAmount,
                        ResponseBankID = entity.ResponseBankID,


                        ResponseCardExpiryDate = entity.ResponseCardExpiryDate,
                        ResponseCardHolderName = entity.ResponseCardHolderName,
                        ResponseCardNumber = entity.ResponseCardNumber,
                        ResponseConfirmationID = entity.ResponseConfirmationID,


                        ResponseCurrencyCode = entity.ResponseCurrencyCode,
                        ResponseEZConnectResponseDate = entity.ResponseEZConnectResponseDate,
                        ResponseLang = entity.ResponseLang,
                        ResponseMerchantID = entity.ResponseMerchantID,

                        ResponseMerchantModuleSessionID = entity.ResponseMerchantModuleSessionID,
                        ResponsePUN = entity.ResponsePUN,
                        ResponseStatus = entity.ResponseStatus,
                        ResponseStatusMessage = entity.ResponseStatusMessage,

                        ResponseSecureHash = entity.ResponseSecureHash,
                        LogType = entity.LogType
                    };
                }

                return paymentQPAYDTO;
            }
        }



    }
}