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
    public class RODetailMapper : IDTOEntityMapper<RepairDetailDTO, AS_RODETAIL>
    {
        private CallContext _context;

        public static RODetailMapper Mapper(CallContext context)
        {
            var mapper = new RODetailMapper();
            mapper._context = context;
            return mapper;
        }

        public RepairDetailDTO ToDTO(AS_RODETAIL entity)
        {
            return new RepairDetailDTO()
            {
                ADDIND = entity.ADDIND,
                ADDLREF = entity.ADDLREF,
                BALHRS = entity.BALHRS,
                BILLCD = entity.BILLCD,
                BILLFLAG = entity.BILLFLAG,
                CAUSE = entity.CAUSE,
                CBREMARKS = entity.CBREMARKS,
                CBRSNCD = entity.CBRSNCD,
                CLSIND = entity.CLSIND,
                CNTRYCD = entity.CNTRYCD,
                COMPLETED = entity.COMPLETED,
                CONSCHG = entity.CONSCHG,
                CONSFLAG = entity.CONSFLAG,
                CORRCT_ACTION = entity.CORRCT_ACTION,
                DEPENDENCE = entity.DEPENDENCE,
                DISALWD = entity.DISALWD,
                DOCDATE = entity.DOCDATE,
                ENTRY = entity.ENTRY,
                ESTDOCFYR = entity.ESTDOCFYR,
                ESTILABHRS = entity.ESTILABHRS,
                ESTNO = entity.ESTNO,
                ESTSHP = entity.ESTSHP,
                ESTTYP = entity.ESTTYP,
                FRT_MODIND = entity.FRT_MODIND,
                HRIND = entity.HRIND,
                INV_OPRN_ALABDIS = entity.INV_OPRN_ALABDIS,
                INV_OPRN_CONSAMT = entity.INV_OPRN_CONSAMT,
                INV_OPRN_GMATMAX = entity.INV_OPRN_GMATMAX,
                INV_OPRN_LABAMT = entity.INV_OPRN_LABAMT,
                INV_OPRN_LPOAMT = entity.INV_OPRN_LPOAMT,
                INV_OPRN_MATAMT = entity.INV_OPRN_MATAMT,
                INV_OPRN_MATDIS = entity.INV_OPRN_MATDIS,
                INVCOMP = entity.INVCOMP,
                INVDAT = entity.INVDAT,
                INVDOCFYR = entity.INVDOCFYR,
                INVNO = entity.INVNO,
                INVSHOP = entity.INVSHOP,
                INVTYPE = entity.INVTYPE,
                LABFLAG = entity.LABFLAG,
                LABRTOT = entity.LABRTOT,
                LASTDATE = entity.LASTDATE,
                MAJIND = entity.MAJIND,
                MATLFLAG = entity.MATLFLAG,
                MATRAT = entity.MATRAT,
                OCCURCODE = entity.OCCURCODE,
                OPGRPCODE = entity.OPGRPCODE,
                OPRNCODE = entity.OPRNCODE,
                PAYCODE = entity.PAYCODE,
                PERIND = entity.PERIND,
                PGMID = entity.PGMID,
                POSITION = entity.POSITION,
                PRFDOCFYR = entity.PRFDOCFYR,
                PRFIND = entity.PRFIND,
                PRFNO = entity.PRFNO,
                PRFSHOP = entity.PRFSHOP,
                PRFTYP = entity.PRFTYP,
                PRNFLAG = entity.PRNFLAG,
                RATEIND = entity.RATEIND,
                RECALL_IND = entity.RECALL_IND,
                RODOCFYR = entity.RODOCFYR,
                RONO = entity.RONO,
                ROTYPE = entity.ROTYPE,
                RSNCD = entity.RSNCD,
                RSNDT = entity.RSNDT,
                SEQUENCE = entity.SEQUENCE,
                SHOP = entity.SHOP,
                SRLNO = entity.SRLNO,
                STARTDATE = entity.STARTDATE,
                STATUS = entity.STATUS,
                STDLABHR = entity.STDLABHR,
                STDLABRATE = entity.STDLABRATE,
                SYMCODE = entity.SYMCODE,
                TCREF = entity.TCREF,
                TECHNICIAN = entity.TECHNICIAN,
                TOSHOP = entity.TOSHOP,
                UPD_BY = entity.UPD_BY,
                UPGMID = entity.UPGMID,
                USERID = entity.USERID
            };
        }

        public AS_RODETAIL ToEntity(RepairDetailDTO dto)
        {
            return new AS_RODETAIL()
            {
                ADDIND = dto.ADDIND,
                ADDLREF = dto.ADDLREF,
                BALHRS = dto.BALHRS,
                BILLCD = dto.BILLCD,
                BILLFLAG = dto.BILLFLAG,
                CAUSE = dto.CAUSE,
                CBREMARKS = dto.CBREMARKS,
                CBRSNCD = dto.CBRSNCD,
                CLSIND = dto.CLSIND,
                CNTRYCD = dto.CNTRYCD,
                COMPLETED = dto.COMPLETED,
                CONSCHG = dto.CONSCHG,
                CONSFLAG = dto.CONSFLAG,
                CORRCT_ACTION = dto.CORRCT_ACTION,
                DEPENDENCE = dto.DEPENDENCE,
                DISALWD = dto.DISALWD,
                DOCDATE = dto.DOCDATE,
                ENTRY = dto.ENTRY,
                ESTDOCFYR = dto.ESTDOCFYR,
                ESTILABHRS = dto.ESTILABHRS,
                ESTNO = dto.ESTNO,
                ESTSHP = dto.ESTSHP,
                ESTTYP = dto.ESTTYP,
                FRT_MODIND = dto.FRT_MODIND,
                HRIND = dto.HRIND,
                INV_OPRN_ALABDIS = dto.INV_OPRN_ALABDIS,
                INV_OPRN_CONSAMT = dto.INV_OPRN_CONSAMT,
                INV_OPRN_GMATMAX = dto.INV_OPRN_GMATMAX,
                INV_OPRN_LABAMT = dto.INV_OPRN_LABAMT,
                INV_OPRN_LPOAMT = dto.INV_OPRN_LPOAMT,
                INV_OPRN_MATAMT = dto.INV_OPRN_MATAMT,
                INV_OPRN_MATDIS = dto.INV_OPRN_MATDIS,
                INVCOMP = dto.INVCOMP,
                INVDAT = dto.INVDAT,
                INVDOCFYR = dto.INVDOCFYR,
                INVNO = dto.INVNO,
                INVSHOP = dto.INVSHOP,
                INVTYPE = dto.INVTYPE,
                LABFLAG = dto.LABFLAG,
                LABRTOT = dto.LABRTOT,
                LASTDATE = dto.LASTDATE,
                MAJIND = dto.MAJIND,
                MATLFLAG = dto.MATLFLAG,
                MATRAT = dto.MATRAT,
                OCCURCODE = dto.OCCURCODE,
                OPGRPCODE = dto.OPGRPCODE,
                OPRNCODE = dto.OPRNCODE,
                PAYCODE = dto.PAYCODE,
                PERIND = dto.PERIND,
                PGMID = dto.PGMID,
                POSITION = dto.POSITION,
                PRFDOCFYR = dto.PRFDOCFYR,
                PRFIND = dto.PRFIND,
                PRFNO = dto.PRFNO,
                PRFSHOP = dto.PRFSHOP,
                PRFTYP = dto.PRFTYP,
                PRNFLAG = dto.PRNFLAG,
                RATEIND = dto.RATEIND,
                RECALL_IND = dto.RECALL_IND,
                RODOCFYR = dto.RODOCFYR,
                RONO = dto.RONO,
                ROTYPE = dto.ROTYPE,
                RSNCD = dto.RSNCD,
                RSNDT = dto.RSNDT,
                SEQUENCE = dto.SEQUENCE,
                SHOP = dto.SHOP,
                SRLNO = dto.SRLNO,
                STARTDATE = dto.STARTDATE,
                STATUS = dto.STATUS,
                STDLABHR = dto.STDLABHR,
                STDLABRATE = dto.STDLABRATE,
                SYMCODE = dto.SYMCODE,
                TCREF = dto.TCREF,
                TECHNICIAN = dto.TECHNICIAN,
                TOSHOP = dto.TOSHOP,
                UPD_BY = dto.UPD_BY,
                UPGMID = dto.UPGMID,
                USERID = dto.USERID
            };
        }

        public List<AS_RODETAIL> ToEntity(List<RepairDetailDTO> dtoList)
        {

            var listRODetail = new List<AS_RODETAIL>();
            foreach(var dto in dtoList)
            {
                listRODetail.Add(ToEntity(dto));
            }
            return listRODetail;
        }
    }
}
