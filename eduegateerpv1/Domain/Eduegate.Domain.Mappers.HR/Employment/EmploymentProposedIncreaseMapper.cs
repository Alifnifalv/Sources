using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Domain.Mappers.EmploymentService
{
    public class EmploymentProposedIncreaseMapper : IDTOEntityMapper<EmploymentProposedIncreaseDTO, HR_EMP_REQ_SALCHG>
    {
        private CallContext _context;
        public static EmploymentProposedIncreaseMapper Mapper(CallContext context)
        {
            var mapper = new EmploymentProposedIncreaseMapper();
            mapper._context = context;
            return mapper;
        }

        public HR_EMP_REQ_SALCHG ToEntity(EmploymentProposedIncreaseDTO dto)
        {

            return new HR_EMP_REQ_SALCHG()
            {
                REQ_LINE_NO = 0,
                AFT_PRDMTH = short.Parse(dto.SalaryChangeAfterPeriod.Key),
                BASIC_CHGAMT = dto.ProposedIncrease.HasValue ? dto.ProposedIncrease.Value : default(decimal),
                CNTRYCD = 1,
                REMARK = dto.Remarks,
                STATUS = dto.Status,
            };
        }

        public EmploymentProposedIncreaseDTO ToDTO(HR_EMP_REQ_SALCHG entity)
        {
            //var allowance = new EmploymentServiceRepository().GetAllowanceByCode(entity.PAYCOMP.Value, entity.ALLOWCD.Value);
            return new EmploymentProposedIncreaseDTO()
            {
                REQ_LINE_NO = entity.REQ_LINE_NO.HasValue ? entity.REQ_LINE_NO.Value : default(short),
                SalaryChangeAfterPeriod = new Services.Contracts.Commons.KeyValueDTO() { Key = entity.AFT_PRDMTH.ToString(), Value = entity.AFT_PRDMTH.ToString() },
                ProposedIncrease = entity.BASIC_CHGAMT,
                //BASIC_CHGAMT = dto.ProposedIncrease.HasValue ? dto.ProposedIncrease.Value : default(decimal),
                //CNTRYCD = 1,
                Remarks = entity.REMARK,
                Status = entity.STATUS,
            };
        }

        public List<EmploymentProposedIncreaseDTO> ToDTOList(List<HR_EMP_REQ_SALCHG> entitylist)
        {

            var list = new List<EmploymentProposedIncreaseDTO>();
            foreach (var entity in entitylist)
            {
                list.Add(ToDTO(entity));
            }
            return list;
        }
    }
}
