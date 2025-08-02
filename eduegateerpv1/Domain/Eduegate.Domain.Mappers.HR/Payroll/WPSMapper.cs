using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using System.Linq;
using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Repository.Frameworks;
using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Microsoft.EntityFrameworkCore;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Entity.School.Models.School;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class WPSMapper : DTOEntityDynamicMapper
    {
        public static WPSMapper Mapper(CallContext context)
        {
            var mapper = new WPSMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var slipdto = dto as SalarySlipDTO;
            string message = string.Empty;

            return message;
        }

        public bool SaveWPS(WPSDetailDTO wpsDTO)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = new WPSDetail();

                entity = new WPSDetail()
                {
                    WPSIID = wpsDTO.WPSIID,
                    PayerBankDetailIID = wpsDTO.PayerBankDetailIID,
                    SalaryYear = wpsDTO.SalaryYear,
                    SalaryMonth = wpsDTO.SalaryMonth,
                    TotalRecords = wpsDTO.TotalRecords,
                    TotalSalaries = wpsDTO.TotalSalaries,
                    ContentID  = wpsDTO.ContentID,
                    CreatedBy = (int?)_context.LoginID,
                    CreatedDate = DateTime.Now,
                };

                if(entity.WPSIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                    dbContext.SaveChanges();
                }
            }
            return true;
        }



    }
}