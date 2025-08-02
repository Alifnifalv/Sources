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
    public class PaymentMasterVisaMapper : DTOEntityDynamicMapper
    {
        public static PaymentMasterVisaMapper Mapper(CallContext context)
        {
            var mapper = new PaymentMasterVisaMapper();
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

        public PaymentMasterVisaDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PaymentMasterVisas.Where(x => x.TrackIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var PaymentLogDTO = new PaymentMasterVisaDTO()
                {
                    TrackIID = entity.TrackIID,
                    TrackKey = entity.TrackKey,
                    CartID = entity.CartID,
                    CustomerID = entity.CustomerID,
                    PaymentAmount = entity.PaymentAmount,
                    SecureHash = entity.SecureHash,
                    InitOn = entity.InitOn,
                    TransID = entity.TransID,
                    Response = entity.Response,
                    LogType = entity.LogType,
                    CardType = entity.CardType,
                    CardTypeID = entity.CardTypeID,
                    LoginID = entity.LoginID,
                    SuccessStatus = entity.SuccessStatus,
                    MerchantID = entity.MerchantID,
                };

                return PaymentLogDTO;
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PaymentMasterVisaDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new PaymentMasterVisa()
                {
                    TrackIID = toDto.TrackIID,
                    TrackKey = toDto.TrackKey,
                    CartID = toDto.CartID,
                    CustomerID = toDto.CustomerID,
                    PaymentAmount = toDto.PaymentAmount,
                    SecureHash = toDto.SecureHash,
                    InitOn = toDto.InitOn,
                    TransID = toDto.TransID,
                    Response = toDto.Response,
                    LogType = toDto.LogType,
                    CardType = toDto.CardType,
                    CardTypeID = toDto.CardTypeID,
                    LoginID= toDto.LoginID,
                    SuccessStatus = toDto.SuccessStatus,
                    MerchantID = toDto.MerchantID,
                };
                if (entity.TrackIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.TrackIID));
            }
        }

        public PaymentMasterVisaDTO UpdatePaymentMasterVisa(PaymentMasterVisaDTO paymentMasterVisaDTO)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentMasterVisas = new PaymentMasterVisa();

                paymentMasterVisas = dbContext.PaymentMasterVisas.Where(x => x.TrackIID == paymentMasterVisaDTO.TrackIID).AsNoTracking().FirstOrDefault();
                if (paymentMasterVisas != null)
                {
                    if (!string.IsNullOrEmpty(paymentMasterVisaDTO.SecureHash))
                        paymentMasterVisas.SecureHash = paymentMasterVisaDTO.SecureHash;
                    if (!string.IsNullOrEmpty(paymentMasterVisaDTO.CardType))
                        paymentMasterVisas.CardType = paymentMasterVisaDTO.CardType;
                    if (paymentMasterVisaDTO.CardTypeID.HasValue && paymentMasterVisaDTO.CardTypeID != 0)
                        paymentMasterVisas.CardTypeID = paymentMasterVisaDTO.CardTypeID;
                    paymentMasterVisas.ResponseCode = paymentMasterVisaDTO.ResponseCode;
                    paymentMasterVisas.ResponseAcquirerID = paymentMasterVisaDTO.ResponseAcquirerID;
                    paymentMasterVisas.ResponseCardExpiryDate = paymentMasterVisaDTO.ResponseCardExpiryDate;
                    paymentMasterVisas.ResponseCardHolderName = paymentMasterVisaDTO.ResponseCardHolderName;
                    paymentMasterVisas.ResponseConfirmationID = paymentMasterVisaDTO.ResponseConfirmationID;
                    if (!string.IsNullOrEmpty(paymentMasterVisaDTO.ResponseStatus))
                        paymentMasterVisas.ResponseStatus = paymentMasterVisaDTO.ResponseStatus;
                    if (!string.IsNullOrEmpty(paymentMasterVisaDTO.ResponseStatusMessage))
                        paymentMasterVisas.ResponseStatusMessage = paymentMasterVisaDTO.ResponseStatusMessage;
                    paymentMasterVisas.ResponseSecureHash = paymentMasterVisaDTO.ResponseSecureHash;
                    if (!string.IsNullOrEmpty(paymentMasterVisaDTO.Response))
                    {
                        paymentMasterVisas.Response = paymentMasterVisaDTO.Response;
                    }

                    paymentMasterVisas.MerchantID = paymentMasterVisaDTO.MerchantID;
                    paymentMasterVisas.SuccessStatus = paymentMasterVisaDTO.SuccessStatus;

                    dbContext.Entry(paymentMasterVisas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                else if (!string.IsNullOrEmpty(paymentMasterVisaDTO.SecureHash))
                {
                    paymentMasterVisas = dbContext.PaymentMasterVisas.Where(x => x.SecureHash == paymentMasterVisaDTO.SecureHash).OrderByDescending(y => y.TrackIID).AsNoTracking().FirstOrDefault();
                    if (paymentMasterVisas != null)
                    {
                        if (!string.IsNullOrEmpty(paymentMasterVisaDTO.SecureHash))
                            paymentMasterVisas.SecureHash = paymentMasterVisaDTO.SecureHash;
                        if (!string.IsNullOrEmpty(paymentMasterVisaDTO.CardType))
                            paymentMasterVisas.CardType = paymentMasterVisaDTO.CardType;
                        if (paymentMasterVisaDTO.CardTypeID.HasValue && paymentMasterVisaDTO.CardTypeID != 0)
                            paymentMasterVisas.CardTypeID = paymentMasterVisaDTO.CardTypeID;
                        paymentMasterVisas.ResponseCode = paymentMasterVisaDTO.ResponseCode;
                        paymentMasterVisas.ResponseAcquirerID = paymentMasterVisaDTO.ResponseAcquirerID;
                        paymentMasterVisas.ResponseCardExpiryDate = paymentMasterVisaDTO.ResponseCardExpiryDate;
                        paymentMasterVisas.ResponseCardHolderName = paymentMasterVisaDTO.ResponseCardHolderName;
                        paymentMasterVisas.ResponseConfirmationID = paymentMasterVisaDTO.ResponseConfirmationID;
                        if (!string.IsNullOrEmpty(paymentMasterVisaDTO.ResponseStatus))
                            paymentMasterVisas.ResponseStatus = paymentMasterVisaDTO.ResponseStatus;
                        if (!string.IsNullOrEmpty(paymentMasterVisaDTO.ResponseStatusMessage))
                            paymentMasterVisas.ResponseStatusMessage = paymentMasterVisaDTO.ResponseStatusMessage;
                        paymentMasterVisas.ResponseSecureHash = paymentMasterVisaDTO.ResponseSecureHash;
                        if (!string.IsNullOrEmpty(paymentMasterVisaDTO.Response))
                            paymentMasterVisas.Response = paymentMasterVisaDTO.Response;

                        paymentMasterVisas.MerchantID = paymentMasterVisaDTO.MerchantID;
                        paymentMasterVisas.SuccessStatus = paymentMasterVisaDTO.SuccessStatus;

                        dbContext.Entry(paymentMasterVisas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        var paymentDetail = new PaymentMasterVisa()
                        {
                            LoginID = paymentMasterVisaDTO.LoginID == 0 ? (_context.LoginID.HasValue ? _context.LoginID.Value : 0) : paymentMasterVisaDTO.LoginID,
                            CustomerID = paymentMasterVisaDTO.CustomerID,
                            PaymentAmount = paymentMasterVisaDTO.PaymentAmount,
                            TransID = paymentMasterVisaDTO.TransID,
                            Response = paymentMasterVisaDTO.Response,
                            LogType = paymentMasterVisaDTO.LogType,
                            InitOn = paymentMasterVisaDTO.InitOn,
                            SecureHash = paymentMasterVisaDTO.SecureHash,
                            CardType = paymentMasterVisaDTO.CardType,
                            CardTypeID = paymentMasterVisaDTO.CardTypeID,
                            ResponseCode = paymentMasterVisaDTO.ResponseCode,
                            ResponseAcquirerID = paymentMasterVisaDTO.ResponseAcquirerID,
                            ResponseCardExpiryDate = paymentMasterVisaDTO.ResponseCardExpiryDate,
                            ResponseCardHolderName = paymentMasterVisaDTO.ResponseCardHolderName,

                            ResponseConfirmationID = paymentMasterVisaDTO.ResponseConfirmationID,
                            ResponseStatus = paymentMasterVisaDTO.ResponseStatus,

                            ResponseStatusMessage = paymentMasterVisaDTO.ResponseStatusMessage,
                            ResponseSecureHash = paymentMasterVisaDTO.ResponseSecureHash,
                            SuccessStatus = paymentMasterVisaDTO.SuccessStatus,
                            MerchantID = paymentMasterVisaDTO.MerchantID,
                        };

                        if (paymentDetail.InitLocation != null)
                        {
                            paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                        }

                        //dbContext.PaymentMasterVisas.Add(paymentDetail);
                        dbContext.Entry(paymentDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        dbContext.SaveChanges();
                    }
                }
            }
            return paymentMasterVisaDTO;
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.LoginID == loginID &&
                (x.InitOn.Value.Year == currentDate.Year && x.InitOn.Value.Month == currentDate.Month && x.InitOn.Value.Day == currentDate.Day) &&
                (x.PaymentAmount != null || x.PaymentAmount > 0))
                    .OrderByDescending(y => y.TrackIID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (data != null)
                {
                    masterVisaDTO = new PaymentMasterVisaDTO()
                    {
                        TrackIID = data.TrackIID,
                        TrackKey = data.TrackKey,
                        CustomerID = data.CustomerID,
                        InitOn = data.InitOn,
                        PaymentAmount = data.PaymentAmount,
                        TransID = data.TransID,
                        Response = data.Response,
                        LogType = data.LogType,
                        CartID = data.CartID,
                        SecureHash = data.SecureHash,
                        CardType = data.CardType,
                        CardTypeID = data.CardTypeID,
                        LoginID = data.LoginID,
                        SuccessStatus = data.SuccessStatus,
                        MerchantID = data.MerchantID,
                    };
                }

                return masterVisaDTO;
            }
        }

        public PaymentMasterVisaDTO MakePaymentEntryBySession(PaymentMasterVisaDTO paymentDetailDTO)
        {
            var paymentMasterVisDTO = new PaymentMasterVisaDTO();

            if (paymentDetailDTO == null || string.IsNullOrEmpty(paymentDetailDTO.TransID))
            {
                Eduegate.Logger.LogHelper<PaymentMasterVisaMapper>.Fatal("TransID",
                new Exception("TransID is not available.So data cannot be saved in PaymentMasterVisas"));
                return paymentMasterVisDTO;
            }

            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentDetail = new PaymentMasterVisa()
                {
                    LoginID = paymentDetailDTO.LoginID,
                    CustomerID = paymentDetailDTO.CustomerID,
                    PaymentAmount = paymentDetailDTO.PaymentAmount,
                    TransID = paymentDetailDTO.TransID,
                    CartID = paymentDetailDTO.CartID,
                    Response = paymentDetailDTO.Response,
                    LogType = paymentDetailDTO.LogType,
                    InitOn = paymentDetailDTO.InitOn,
                    SecureHash = paymentDetailDTO.SecureHash,
                    CardType = paymentDetailDTO.CardType,
                    CardTypeID = paymentDetailDTO.CardTypeID,
                    SuccessStatus = paymentDetailDTO.SuccessStatus,
                    MerchantID = paymentDetailDTO.MerchantID,
                };

                if (paymentDetail.InitLocation != null)
                {
                    paymentDetail.InitLocation = GetCountryNameByIP(paymentDetail.InitLocation, dbContext);
                }

                //dbContext.PaymentMasterVisas.Add(paymentDetail);
                dbContext.Entry(paymentDetail).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                dbContext.SaveChanges();

                paymentMasterVisDTO = new PaymentMasterVisaDTO()
                {
                    TrackIID = paymentDetail.TrackIID,
                    TransID = paymentDetail.TransID,
                    CustomerID = paymentDetail.CustomerID,
                    IsMasterVisaSaved = true,
                    InitOn = paymentDetail.InitOn,
                    PaymentAmount = paymentDetail.PaymentAmount,
                    Response = paymentDetail.Response,
                    LogType = paymentDetail.LogType,
                    CartID = paymentDetail.CartID,
                    SecureHash = paymentDetail.SecureHash,
                    CardType = paymentDetail.CardType,
                    CardTypeID = paymentDetail.CardTypeID,
                    LoginID = paymentDetail.LoginID,
                    SuccessStatus = paymentDetail.SuccessStatus,
                    MerchantID = paymentDetail.MerchantID,
                };
            }

            return paymentMasterVisDTO;
        }

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

        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByCartID(long cartID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.CartID == cartID).OrderByDescending(o => o.TrackIID).AsNoTracking().FirstOrDefault();

                if (data != null)
                {
                    masterVisaDTO = new PaymentMasterVisaDTO()
                    {
                        TrackIID = data.TrackIID,
                        TrackKey = data.TrackKey,
                        CustomerID = data.CustomerID,
                        InitOn = data.InitOn,
                        PaymentAmount = data.PaymentAmount,
                        TransID = data.TransID,
                        Response = data.Response,
                        LogType = data.LogType,
                        CartID = data.CartID,
                        SecureHash = data.SecureHash,
                        CardType = data.CardType,
                        CardTypeID = data.CardTypeID,
                        LoginID = data.LoginID,
                        SuccessStatus = data.SuccessStatus,
                        MerchantID = data.MerchantID,
                    };
                }

                return masterVisaDTO;
            }
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByTrackID(long trackID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.TrackIID == trackID).AsNoTracking().FirstOrDefault();

                if (data != null)
                {
                    masterVisaDTO = new PaymentMasterVisaDTO()
                    {
                        TrackIID = data.TrackIID,
                        TrackKey = data.TrackKey,
                        CustomerID = data.CustomerID,
                        InitOn = data.InitOn,
                        PaymentAmount = data.PaymentAmount,
                        TransID = data.TransID,
                        Response = data.Response,
                        LogType = data.LogType,
                        CartID = data.CartID,
                        SecureHash = data.SecureHash,
                        CardType = data.CardType,
                        CardTypeID = data.CardTypeID,
                        LoginID = data.LoginID,
                        SuccessStatus = data.SuccessStatus,
                        MerchantID = data.MerchantID,
                    };
                }

                return masterVisaDTO;
            }
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByTransID(string transID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.TransID == transID).AsNoTracking().FirstOrDefault();

                if (data != null)
                {
                    masterVisaDTO = new PaymentMasterVisaDTO()
                    {
                        TrackIID = data.TrackIID,
                        TrackKey = data.TrackKey,
                        CustomerID = data.CustomerID,
                        InitOn = data.InitOn,
                        PaymentAmount = data.PaymentAmount,
                        TransID = data.TransID,
                        Response = data.Response,
                        LogType = data.LogType,
                        CartID = data.CartID,
                        SecureHash = data.SecureHash,
                        CardType = data.CardType,
                        CardTypeID = data.CardTypeID,
                        LoginID = data.LoginID,
                        SuccessStatus = data.SuccessStatus,
                        MerchantID = data.MerchantID,
                    };
                }

                return masterVisaDTO;
            }
        }

    }
}