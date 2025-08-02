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
    public class ROVehicleMapper : IDTOEntityMapper<RepairVehicleDTO, AS_VEHICLE>
    {
        private CallContext _context;

        public static ROVehicleMapper Mapper(CallContext context)
        {
            var mapper = new ROVehicleMapper();
            mapper._context = context;
            return mapper;
        }

        public AS_VEHICLE ToEntity(RepairVehicleDTO dto)
        {

            return new AS_VEHICLE()
            {
                ADESCRIPTION = dto.ADESCRIPTION,
                CHASSISNO = dto.CHASSISNO,
                CNTRYCD = dto.CNTRYCD,
                COLRCD = dto.COLRCD,
                CRODOCFYR = dto.CRODOCFYR,
                CROSHOP = dto.CROSHOP,
                CROTYPE = dto.CROTYPE,
                CURRONO = dto.CURRONO,
                CUSCODE = dto.CUSCODE,
                CYLCODE = dto.CYLCODE,
                DELDAT = dto.DELDAT,
                DESCRIPTION = dto.DESCRIPTION,
                ENGINENO = dto.ENGINENO,
                ENTRY = dto.ENTRY,
                FIRSTOWNER = dto.FIRSTOWNER,
                INVCOMP = dto.INVCOMP,
                INVDAT = dto.INVDAT,
                INVNUM = dto.INVNUM,
                INVSHOP = dto.INVSHOP,
                INVTYP = dto.INVTYP,
                KTNO = dto.KTNO,
                LABOUR = dto.LABOUR,
                LAST_SERV = dto.LAST_SERV,
                LASTDATE = dto.LASTDATE,
                LASTKM = dto.LASTKM,
                LASTRONO = dto.LASTRONO,
                LRODOCFYR = dto.LRODOCFYR,
                LROSHOP = dto.LROSHOP,
                LROTYPE = dto.LROTYPE,
                MAIN_VEHTYPE = dto.MAIN_VEHTYPE,
                MATERIAL = dto.MATERIAL,
                MDLCD = dto.MDLCD,
                MDLOPT = dto.MDLOPT,
                MODELYEAR = dto.MODELYEAR,
                NEXT_SERVICE = dto.NEXT_SERVICE,
                OBSERVATION = dto.OBSERVATION,
                PGMID = dto.PGMID,
                REGISTRATION = dto.REGISTRATION,
                SALCAT = dto.SALCAT,
                SECONDOWNER = dto.SECONDOWNER,
                SEL_MAIN_VEHTYPE = dto.SEL_MAIN_VEHTYPE,
                SEL_VEH_TYPE = dto.SEL_VEH_TYPE,
                STATUS = dto.STATUS,
                STOCKNO = dto.STOCKNO,
                TRIMCD = dto.TRIMCD,
                UPD_BY = dto.UPD_BY,
                UPGMID = dto.UPGMID,
                USERID = dto.USERID,
                VEH_TYPE = dto.VEH_TYPE,
                VEHMAKE = dto.VEHMAKE,
                VEHTYPE = dto.VEHTYPE,
                WARRANTYKM = dto.WARRANTYKM,
                WARRANTYMTH = dto.WARRANTYMTH,
            };
        }

        public RepairVehicleDTO ToDTO(AS_VEHICLE entity)
        {
            if (entity == null) return new RepairVehicleDTO();

            return new RepairVehicleDTO()
            {
                ADESCRIPTION = entity.ADESCRIPTION,
                CHASSISNO = entity.CHASSISNO,
                CNTRYCD = entity.CNTRYCD,
                COLRCD = entity.COLRCD,
                CRODOCFYR = entity.CRODOCFYR,
                CROSHOP = entity.CROSHOP,
                CROTYPE = entity.CROTYPE,
                CURRONO = entity.CURRONO,
                CUSCODE = entity.CUSCODE,
                CYLCODE = entity.CYLCODE,
                DELDAT = entity.DELDAT,
                DESCRIPTION = entity.DESCRIPTION,
                ENGINENO = entity.ENGINENO,
                ENTRY = entity.ENTRY,
                FIRSTOWNER = entity.FIRSTOWNER,
                INVCOMP = entity.INVCOMP,
                INVDAT = entity.INVDAT,
                INVNUM = entity.INVNUM,
                INVSHOP = entity.INVSHOP,
                INVTYP = entity.INVTYP,
                KTNO = entity.KTNO,
                LABOUR = entity.LABOUR,
                LAST_SERV = entity.LAST_SERV,
                LASTDATE = entity.LASTDATE,
                LASTKM = entity.LASTKM,
                LASTRONO = entity.LASTRONO,
                LRODOCFYR = entity.LRODOCFYR,
                LROSHOP = entity.LROSHOP,
                LROTYPE = entity.LROTYPE,
                MAIN_VEHTYPE = entity.MAIN_VEHTYPE,
                MATERIAL = entity.MATERIAL,
                MDLCD = entity.MDLCD,
                MDLOPT = entity.MDLOPT,
                MODELYEAR = entity.MODELYEAR,
                NEXT_SERVICE = entity.NEXT_SERVICE,
                OBSERVATION = entity.OBSERVATION,
                PGMID = entity.PGMID,
                REGISTRATION = entity.REGISTRATION,
                SALCAT = entity.SALCAT,
                SECONDOWNER = entity.SECONDOWNER,
                SEL_MAIN_VEHTYPE = entity.SEL_MAIN_VEHTYPE,
                SEL_VEH_TYPE = entity.SEL_VEH_TYPE,
                STATUS = entity.STATUS,
                STOCKNO = entity.STOCKNO,
                TRIMCD = entity.TRIMCD,
                UPD_BY = entity.UPD_BY,
                UPGMID = entity.UPGMID,
                USERID = entity.USERID,
                VEH_TYPE = entity.VEH_TYPE,
                VEHMAKE = entity.VEHMAKE,
                VEHTYPE = entity.VEHTYPE,
                WARRANTYKM = entity.WARRANTYKM,
                WARRANTYMTH = entity.WARRANTYMTH,
            };
        }

    }
}
