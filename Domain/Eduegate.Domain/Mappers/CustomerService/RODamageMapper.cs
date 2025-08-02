using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Domain.Mappers.CustomerService
{
    public class RODamageMapper : IDTOEntityMapper<RepairDefectDTO, AS_RODAMAGE>
    {
        private CallContext _context;

        public static RODamageMapper Mapper(CallContext context)
        {
            var mapper = new RODamageMapper();
            mapper._context = context;
            return mapper;
        }

        public RepairDefectDTO ToDTO(AS_RODAMAGE entity)
        {
            var dto = new RepairDefectDTO()
            {
                CNTRYCD = entity.CNTRYCD,
                DAMAGECODE = entity.DAMAGECODE,
                ENTRY = entity.ENTRY,
                LASTDATE = entity.LASTDATE,
                PGMID = entity.PGMID,
                RODOCFYR = entity.RODOCFYR,
                RONO = entity.RONO,
                ROSHOP = entity.ROSHOP,
                ROTYPE = entity.ROTYPE,
                SIDE = entity.SIDE,
                UPD_BY = entity.UPD_BY,
                UPGMID = entity.UPGMID,
                USERID = entity.USERID,
            };

            return dto;
        }
        public AS_RODAMAGE ToEntity(RepairDefectDTO dto)
        {
            return new AS_RODAMAGE()
            {
                CNTRYCD = dto.CNTRYCD,
                DAMAGECODE = dto.DAMAGECODE,
                ENTRY = dto.ENTRY,
                LASTDATE = dto.LASTDATE,
                PGMID = dto.PGMID,
                RODOCFYR = dto.RODOCFYR,
                RONO = dto.RONO,
                ROSHOP = dto.ROSHOP,
                ROTYPE = dto.ROTYPE,
                SIDE = dto.SIDE,
                UPD_BY = dto.UPD_BY,
                UPGMID = dto.UPGMID,
                USERID = dto.USERID,
            };
        }

        public List<AS_RODAMAGE> ToEntity(List<RepairDefectDTO> dtos)
        {
            var list = new List<AS_RODAMAGE>();
            foreach(var dto in dtos)
            {
                list.Add(ToEntity(dto));
            }

            return list;
        }

    }
}
