using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Payments;
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
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public PaymentMasterVisaDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.PaymentMasterVisas.Where(x => x.TrackIID == IID).FirstOrDefault();

                var PaymentLogDTO = new PaymentMasterVisaDTO()
                {
                    TrackIID = entity.TrackIID,
                    TrackKey = entity.TrackKey,
                    CartID = entity.CartID,
                    CustomerID = entity.CustomerID,
                    PaymentAmount = entity.PaymentAmount,
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
                };
                if (entity.TrackIID == 0)
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.TrackIID));
            }
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaData()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.CustomerID == loginID &&
                (x.InitOn.Value.Year == currentDate.Year && x.InitOn.Value.Month == currentDate.Month && x.InitOn.Value.Day == currentDate.Day) &&
                (x.PaymentAmount != null || x.PaymentAmount > 0))
                    .OrderByDescending(y => y.TrackIID)
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
                        CartID = data.CartID
                    };
                }

                return masterVisaDTO;
            }
        }

        public PaymentMasterVisaDTO MakePaymentEntryBySession(PaymentMasterVisaDTO paymentDetailDTO)
        {
            var paymentMasterVisDTO = new PaymentMasterVisaDTO();

            using (var dbContext = new dbEduegateERPContext())
            {
                var paymentDetail = new PaymentMasterVisa()
                {
                    CustomerID = paymentDetailDTO.CustomerID,
                    PaymentAmount = paymentDetailDTO.PaymentAmount,
                    TransID = paymentDetailDTO.TransID,
                    Response = paymentDetailDTO.Response,
                    LogType = paymentDetailDTO.LogType,
                    InitOn = paymentDetailDTO.InitOn,
                };

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

        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByCartID(long cartID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var masterVisaDTO = new PaymentMasterVisaDTO();

                var loginID = _context.LoginID;
                var currentDate = DateTime.Now;

                var data = dbContext.PaymentMasterVisas.Where(x => x.CartID == cartID).OrderByDescending(o => o.TrackIID).FirstOrDefault();

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
                        CartID = data.CartID
                    };
                }

                return masterVisaDTO;
            }
        }

    }
}