using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Domain.Mappers.EmploymentService
{
    public class EmploymentAllowanceMapper : IDTOEntityMapper<EmployementAllowanceDTO, HR_EMP_REQ_ALLOW>
    {
        private CallContext _context;
        public static EmploymentAllowanceMapper Mapper(CallContext context)
        {
            var mapper = new EmploymentAllowanceMapper();
            mapper._context = context;
            return mapper;
        }

        public HR_EMP_REQ_ALLOW ToEntity(EmployementAllowanceDTO dto)
        {

            return new HR_EMP_REQ_ALLOW()
            {
                REQ_LINE_NO = 0,
                REQ_DT = DateTime.Now,
                ALLOWCD = short.Parse(dto.Allowance.Key),
                ALLOW_DESC = "",
                ALLOW_AMT = dto.Amount,
                ALLOW_PROB_AMT = dto.AmountAfterProbation,
                REMARK = dto.Remark,
            };
        }

        public EmployementAllowanceDTO ToDTO(HR_EMP_REQ_ALLOW entity)
        {
            var allowance = new EmploymentServiceRepository().GetAllowanceByCode(entity.PAYCOMP.Value, entity.ALLOWCD.Value);
            return new EmployementAllowanceDTO()
            {
                Allowance = new Services.Contracts.Commons.KeyValueDTO() { Key = allowance.ALLOW_CODE.ToString(), Value = allowance.ALLOW_DESC },
                Amount = entity.ALLOW_AMT,
                AmountAfterProbation = entity.ALLOW_PROB_AMT,
                Remark = entity.REMARK,
                CRE_BY = entity.CRE_BY,
                CRE_DT = entity.CRE_DT,
                CRE_IP = entity.CRE_IP,
                CRE_WEBUSER = entity.CRE_WEBUSER,
                REQ_DT = entity.REQ_DT
            };
        }


        public List<EmployementAllowanceDTO> ToDTOList(List<HR_EMP_REQ_ALLOW> entitylist)
        {

            var list = new List<EmployementAllowanceDTO>();
            foreach (var entity in entitylist)
            {
                list.Add(ToDTO(entity));
            }
            return list;
        }

    }
}
