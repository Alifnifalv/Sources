using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Payments;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public PaymentLogDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PaymentLogs.Where(x => x.PaymentLogIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

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
                    LoginID = entity.LoginID,
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
                    LoginID = toDto.LoginID,
                    Amount = toDto.Amount,
                };
                if (entity.PaymentLogIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                        CustomerID = null,
                        Amount = totalAmount,
                        LoginID = _context.LoginID,
                    };

                    if (paymentLog.PaymentLogIID == 0)
                    {
                        dbContext.Entry(paymentLog).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
        public PaymentLogDTO GetLastQPAYLogData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var paymentLog = new PaymentLogDTO();


                var logData = (from p in dbContext.PaymentLogs
                               join sc in dbContext.ShoppingCarts on p.CartID equals sc.ShoppingCartIID
                               where p.ResponseLog != "SUCCESS" && sc.CartStatusID == (int)ShoppingCartStatus.InProcess
                               select p).OrderByDescending(a => a.PaymentLogIID).AsNoTracking().FirstOrDefault();

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
                        LoginID = logData.LoginID,
                        Amount = logData.Amount,
                        TransNo = logData.TransNo
                    };
                }

                return paymentLog;
            }
        }
        public List<PaymentLogDTO> GetPendingQPAYLogData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = _context.LoginID;
                DateTime currentDate = DateTime.Now;
                DateTime twentyFourHoursAgo = currentDate.AddHours(-24);
                var paymentLog = new List<PaymentLogDTO>();

                var cartData = dbContext.ShoppingCarts
                            .Where(sc => sc.PaymentMethod == PaymentGatewayType.QPAY.ToString() && sc.CartStatusID == (int)ShoppingCartStatus.InProcess)
                            .OrderByDescending(sc => sc.ShoppingCartIID)
                            .Select(sc => new { ShoppingCartIID = sc.ShoppingCartIID, StudentID = sc.StudentID })
                            .AsNoTracking()
                            .ToList();
                var cartIds = cartData.Select(x => x.ShoppingCartIID);
                var logDetails = dbContext.PaymentLogs
                  .Where(pl => cartIds.Contains(pl.CartID ?? 0) && pl.ResponseLog != "SUCCESS" && pl.CreatedDate.HasValue && pl.CreatedDate >= twentyFourHoursAgo &&
                   pl.CreatedDate < currentDate)
                  .GroupBy(pl => pl.CartID)
                  .Select(group => new PaymentLogDTO()
                  {
                      CartID = group.Key,
                      PaymentLogIID = group.Max(pl => pl.PaymentLogIID),
                      TrackID = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).TrackID,
                      TrackKey = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).TrackKey,
                      RequestLog = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).RequestLog,
                      ResponseLog = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).ResponseLog,
                      CreatedDate = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).CreatedDate,
                      CompanyID = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).CompanyID,
                      SiteID = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).SiteID,
                      RequestUrl = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).RequestUrl,
                      RequestType = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).RequestType,
                      CustomerID = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).CustomerID,
                      Amount = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).Amount,
                      TransNo = group.First(pl => pl.PaymentLogIID == group.Max(p => p.PaymentLogIID)).TransNo,
                      StudentID = cartData.Where(x => x.ShoppingCartIID == group.Key).Select(y => y.StudentID).FirstOrDefault(),
                      LoginID = loginID,
                  }).ToList();


                return logDetails;
            }
        }

        public PaymentLogDTO GetLastLogData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var paymentLog = new PaymentLogDTO();

                var logData = dbContext.PaymentLogs.Where(x => x.LoginID == loginID && (x.Amount != null || x.Amount > 0))
                    .OrderByDescending(y => y.PaymentLogIID).AsNoTracking().FirstOrDefault();

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
                        TransNo = logData.TransNo,
                        LoginID = logData.LoginID
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

                var logData = dbContext.PaymentLogs.Where(x => x.TransNo == transID && (x.Amount != null || x.Amount > 0)).OrderByDescending(o => o.PaymentLogIID).AsNoTracking().FirstOrDefault();

                if (logData != null)
                {
                    logData.PaymentLogIID = 0;
                    logData.RequestType = "Retry transaction";
                    logData.CreatedDate = DateTime.Now;

                    dbContext.Entry(logData).State = Microsoft.EntityFrameworkCore.EntityState.Added;

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
                        LoginID = logData.LoginID,
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
                var paymentLogData = dbContext.PaymentLogs.Where(x => x.LoginID == paymentMasterVisa.LoginID && (x.Amount != null || x.Amount > 0))
                    .OrderByDescending(y => y.PaymentLogIID).AsNoTracking().FirstOrDefault();

                //paymentLogData.TrackID = paymentMasterVisa.TrackIID;
                paymentLogData.TransNo = paymentMasterVisa.TransID;


                dbContext.Entry(paymentLogData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
                    CustomerID = paymentLog.CustomerID,
                    LoginID = paymentLog.LoginID.HasValue ? paymentLog.LoginID : _context?.LoginID,
                    Amount = paymentLog.Amount,
                    TransNo = paymentLog.TransNo,
                    ValidationResult = paymentLog.ValidationResult,
                    CardType=paymentLog.CardType,
                    CardTypeID=paymentLog.CardTypeID                    
                };

                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                dbContext.SaveChanges();
            }
        }

    }
}