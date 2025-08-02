using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Payments;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.Payment
{
    public class PaymentLogMapper : DTOEntityDynamicMapper
    {
        public static PaymentLogMapper Mapper(CallContext context)
        {
            var mapper = new PaymentLogMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PaymentLogDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public PaymentLogDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PaymentLogs.Where(x => x.PaymentLogIID == IID).FirstOrDefault();

                var PaymentLogDTO = new PaymentLogDTO()
                {
                    PaymentLogIID = entity.PaymentLogIID,
                    TrackID = entity.TrackID,
                    TrackKey = entity.TrackKey,
                    RequestLog = entity.RequestLog,
                    ResponseLog = entity.ResponseLog,
                    CreatedDate = entity.CreatedDate,
                    CompanyID = entity.CompanyID,
                    SiteID = entity.SiteID,
                    RequestUrl = entity.RequestUrl,
                    RequestType = entity.RequestType,
                    CartID = entity.CartID,
                    CustomerID = entity.CustomerID,
                    Amount = entity.Amount,
                };

                return PaymentLogDTO;
            };
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PaymentLogDTO;

            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new PaymentLog()
                {
                    PaymentLogIID = toDto.PaymentLogIID,
                    TrackID = toDto.TrackID,
                    TrackKey = toDto.TrackKey,
                    RequestLog = toDto.RequestLog,
                    ResponseLog = toDto.ResponseLog,
                    CreatedDate = toDto.PaymentLogIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    CompanyID = toDto.CompanyID,
                    SiteID = toDto.SiteID,
                    RequestUrl = toDto.RequestUrl,
                    RequestType = toDto.RequestType,
                    CartID = toDto.CartID,
                    CustomerID = toDto.CustomerID,
                    Amount = toDto.Amount,
                };
                if (entity.PaymentLogIID == 0)
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.PaymentLogIID));
            }
        }

        public string SubmitAmountAsLog(decimal? totalAmount)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                try
                {
                    var paymentLog = new PaymentLog()
                    {
                        PaymentLogIID = 0,
                        CreatedDate = DateTime.Now,
                        RequestType = "New transaction",
                        CustomerID = _context.LoginID,
                        Amount = totalAmount
                    };

                    if (paymentLog.PaymentLogIID == 0)
                    {
                        dbContext.Entry(paymentLog).State = System.Data.Entity.EntityState.Added;
                    }
                    dbContext.SaveChanges();

                    return "Successfully Saved";
                }
                catch
                {
                    return null;
                }
            }
        }

        public PaymentLogDTO GetLastLogData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var paymentLog = new PaymentLogDTO();

                var logData = dbContext.PaymentLogs.Where(x => x.CustomerID == loginID && (x.Amount != null || x.Amount > 0))
                    .OrderByDescending(y => y.PaymentLogIID).FirstOrDefault();

                if (logData != null)
                {
                    paymentLog = new PaymentLogDTO()
                    {
                        PaymentLogIID = logData.PaymentLogIID,
                        TrackID = logData.TrackID,
                        TrackKey = logData.TrackKey,
                        RequestLog = logData.RequestLog,
                        ResponseLog = logData.ResponseLog,
                        CreatedDate = logData.CreatedDate,
                        CompanyID = logData.CompanyID,
                        SiteID = logData.SiteID,
                        RequestUrl = logData.RequestUrl,
                        RequestType = logData.RequestType,
                        CartID = logData.CartID,
                        CustomerID = logData.CustomerID,
                        Amount = logData.Amount,
                        TransNo = logData.TransNo
                    };
                }

                return paymentLog;
            }
        }

        public PaymentLogDTO GetAndInsertLogDataByTransactionID(string transID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentLog = new PaymentLogDTO();

                var logData = dbContext.PaymentLogs.Where(x => x.TransNo == transID && (x.Amount != null || x.Amount > 0)).OrderByDescending(o => o.PaymentLogIID).FirstOrDefault();

                if (logData != null)
                {
                    logData.RequestType = "Retry transaction";
                    logData.CreatedDate = DateTime.Now;

                    dbContext.Entry(logData).State = System.Data.Entity.EntityState.Added;

                    dbContext.SaveChanges();

                    paymentLog = new PaymentLogDTO()
                    {
                        PaymentLogIID = logData.PaymentLogIID,
                        TrackID = logData.TrackID,
                        TrackKey = logData.TrackKey,
                        RequestLog = logData.RequestLog,
                        ResponseLog = logData.ResponseLog,
                        CreatedDate = logData.CreatedDate,
                        CompanyID = logData.CompanyID,
                        SiteID = logData.SiteID,
                        RequestUrl = logData.RequestUrl,
                        RequestType = logData.RequestType,
                        CartID = logData.CartID,
                        CustomerID = logData.CustomerID,
                        Amount = logData.Amount,
                        TransNo = logData.TransNo
                    };
                }

                return paymentLog;
            }
        }

        public void UpdatePaymentLogsByMasterVisaData(PaymentMasterVisaDTO paymentMasterVisa)
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

        public void LogPaymentLog(PaymentLogDTO paymentLog)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = new PaymentLog()
                {
                    PaymentLogIID = 0,
                    TrackID = paymentLog.TrackID,
                    TrackKey = paymentLog.TrackKey,
                    RequestLog = paymentLog.RequestLog,
                    ResponseLog = paymentLog.ResponseLog,
                    CreatedDate = DateTime.Now,
                    CompanyID = paymentLog.CompanyID,
                    SiteID = paymentLog.SiteID,
                    RequestUrl = paymentLog.RequestUrl,
                    RequestType = paymentLog.RequestType,
                    CartID = paymentLog.CartID,
                    CustomerID = _context.LoginID,
                    Amount = paymentLog.Amount,
                    TransNo = paymentLog.TransNo,
                    ValidationResult = paymentLog.ValidationResult,
                };

                dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                dbContext.SaveChanges();
            }
        }

    }
}