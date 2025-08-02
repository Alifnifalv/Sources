using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Payroll;


namespace Eduegate.Domain.Mappers.EmploymentService
{
    public class HRV_CompanyMasterMapper : IDTOEntityMapper<HRV_CompanyMasterDTO, HRV_COMPANY_MASTER>
    {
        private CallContext _context;
        public static HRV_CompanyMasterMapper Mapper(CallContext context)
        {
            var mapper = new HRV_CompanyMasterMapper();
            mapper._context = context;
            return mapper;
        }
        public HRV_CompanyMasterDTO ToDTO(HRV_COMPANY_MASTER entity)
        {
            var dto = new HRV_CompanyMasterDTO()
            {
                COUNTRY_ID = entity.COUNTRY_ID,
                COMPANY_CODE = entity.COMPANY_CODE,
                COMPANY_NAME = entity.COMPANY_NAME,
                COMPANY_SHORT_NAME = entity.COMPANY_SHORT_NAME
            };
            return dto;
        }

        public HRV_COMPANY_MASTER ToEntity(HRV_CompanyMasterDTO employeeDTO)
        {
            return new HRV_COMPANY_MASTER();
        }
    }
}
