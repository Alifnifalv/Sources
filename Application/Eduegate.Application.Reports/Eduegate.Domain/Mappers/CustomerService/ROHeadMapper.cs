using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Oracle.Models;
using Eduegate.Domain.Repository.Oracle;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.CustomerService;

namespace Eduegate.Domain.Mappers.CustomerService
{
    public class ROHeadMapper : IDTOEntityMapper<RepairOrderDTO, AS_ROHEAD>
    {
        private CallContext _context;

        public static ROHeadMapper Mapper(CallContext context)
        {
            var mapper = new ROHeadMapper();
            mapper._context = context;
            return mapper;
        }

        public RepairOrderDTO ToDTO(AS_ROHEAD entity, List<AS_RODETAIL> roDetails, List<AS_RODAMAGE> roDamages)
        {
            var dto = new RepairOrderDTO()
            {
                ACTUALDEL = entity.ACTUALDEL,
                ADDCHG = entity.ADDCHG,
                ADDIND = entity.ADDIND,
                AMATDIS = entity.AMATDIS,
                BOOKED_DOCDAT = entity.BOOKED_DOCDAT,
                BOOKED_DOCNO = entity.BOOKED_DOCNO,
                BVEHTYPE = entity.BVEHTYPE,
                BYOWNER = entity.BYOWNER,
                CANCD = entity.CANCD,
                CANDT = entity.CANDT,
                CBEMPNO = entity.CBEMPNO,
                CHASSISNO = entity.CHASSISNO,
                CNTRYCD = entity.CNTRYCD,
                COMPDAT = entity.COMPDAT,
                CONTPER = entity.CONTPER,
                CONTTEL1 = entity.CONTTEL1,
                CONTTEL2 = entity.CONTTEL2,
                CUSCODE = entity.CUSCODE,
                CUST_ACKWD_TO_SA = entity.CUST_ACKWD_TO_SA,
                CUSTAPPR = entity.CUSTAPPR,
                DEPOSITS = entity.DEPOSITS,
                DISALOWD = entity.DISALOWD,
                DOCDATE = entity.DOCDATE,
                ENTRY = entity.ENTRY,
                ESTILABCOST = entity.ESTILABCOST,
                ESTILABHRS = entity.ESTILABHRS,
                FRROFYR = entity.FRROFYR,
                FRRONO = entity.FRRONO,
                FRROTYPE = entity.FRROTYPE,
                FRSHOP = entity.FRSHOP,
                FUEL = entity.FUEL,
                GPASSDATE = entity.GPASSDATE,
                GPASSNO = entity.GPASSNO,
                GPDOCFYR = entity.GPDOCFYR,
                GPRTIND = entity.GPRTIND,
                HISFLAG = entity.HISFLAG,
                INFORM_RECAL_TO_CUST = entity.INFORM_RECAL_TO_CUST,
                INSRIND = entity.INSRIND,
                INSRLAB = entity.INSRLAB,
                INSRPART = entity.INSRPART,
                JOB_ASGN = entity.JOB_ASGN,
                JOB_LOAD = entity.JOB_LOAD,
                JOBL_DATE = entity.JOBL_DATE,
                KMS = entity.KMS,
                KMSOUT = entity.KMSOUT,
                LABAMT = entity.LABAMT,
                LABDIS = entity.LABDIS,
                LABHRS = entity.LABHRS,
                LASTDATE = entity.LASTDATE,
                LOCCOD = entity.LOCCOD,
                MATDIS = entity.MATDIS,
                MOBILE = entity.MOBILE,
                NEXT_SERVICE = entity.NEXT_SERVICE,
                OBSERVATION = entity.OBSERVATION,
                OLD_PROMISED = entity.OLD_PROMISED,
                OLD_RONO = entity.OLD_RONO,
                OTHSHOP = entity.OTHSHOP,
                PARTSAMT = entity.PARTSAMT,
                PARTSCOST = entity.PARTSCOST,
                PAYCOMP = entity.PAYCOMP,
                PENDING_RECALLS = entity.PENDING_RECALLS,
                PGMID = entity.PGMID,
                PRIORITY = entity.PRIORITY,
                PROMISED = entity.PROMISED,
                PROMISED_CHGCD = entity.PROMISED_CHGCD,
                PRTIND = entity.PRTIND,
                PRV_UPD_DATE = entity.PRV_UPD_DATE,
                RECALL_REMARKS = entity.RECALL_REMARKS,
                RECSTS = entity.RECSTS,
                RELTCD = entity.RELTCD,
                RL_COUPON_NO = entity.RL_COUPON_NO,
                RL_WO_DT = entity.RL_WO_DT,
                RL_WO_NO = entity.RL_WO_NO,
                RODOCFYR = entity.RODOCFYR,
                RONO = entity.RONO,
                ROTYPE = entity.ROTYPE,
                RROFYR = entity.RROFYR,
                RRONO = entity.RRONO,
                RROTYPE = entity.RROTYPE,
                SERADV = entity.SERADV,
                SHOP = entity.SHOP,
                SMSDATE_FU = entity.SMSDATE_FU,
                SMSREF = entity.SMSREF,
                SMSREF_FU = entity.SMSREF_FU,
                SPOTTER = entity.SPOTTER,
                STATUS = entity.STATUS,
                UPD_BY = entity.UPD_BY,
                UPGMID = entity.UPGMID,
                USERID = entity.USERID,
                VEHINBY = entity.VEHINBY,
                VEHINBY_MOBILE = entity.VEHINBY_MOBILE,
                VRFD_CUST_INFO = entity.VRFD_CUST_INFO,
                WARRFLAG = entity.WARRFLAG,
                WARRIND = entity.WARRIND,
                WARRLAB = entity.WARRLAB,
                WARRPART = entity.WARRPART,
                WIPSTATUS = entity.WIPSTATUS,
                WO_FROM = entity.WO_FROM,
            };

            var repository = new RepairOrderRepository();
            var customerInfo = repository.GetCustomer(dto.CUSCODE);
            dto.Customer = new Services.Contracts.Commons.KeyValueDTO() { Key = customerInfo.CUSCODE.ToString(), Value = customerInfo.CUSNAME };
            dto.PhoneNumber = customerInfo.MOBILENO;
            dto.CivilID = customerInfo.CIVILID;

            var shopInfo = repository.GetROShop(dto.SHOP);
            dto.Shops = new Services.Contracts.Commons.KeyValueDTO() { Key = shopInfo.NO.ToString(), Value = shopInfo.NAME };
            var vehicleInfo = repository.GetVehcileInfoByChasisNo(dto.CHASSISNO);
            dto.KTNO = vehicleInfo.KTNO;
            dto.VehicleDescription = vehicleInfo.DESCRIPTION;
            dto.RegitrationDate = vehicleInfo.REGISTRATION;
            dto.BillVehicleType = vehicleInfo.MAIN_VEHTYPE;
            dto.WarrantyKMs = vehicleInfo.WARRANTYKM;
            dto.LastServiceKMs = vehicleInfo.LASTKM;

            var roType = repository.GetROType(dto.ROTYPE.ToString());
            dto.OrderType = new Services.Contracts.Commons.KeyValueDTO() { Key = roType.CODE.ToString(), Value = shopInfo.NAME };
            dto.Details = new List<RepairDetailDTO>();
            dto.Defects = new List<RepairDefectDTO>();

            foreach (var detail in roDetails)
            {
                var operation = repository.GetOperations(detail.OPRNCODE);
                var groupOperation = repository.GetOperationGroups(detail.OPGRPCODE);
                var symptoms = detail.SYMCODE.HasValue ? repository.GetSymptoms(detail.SYMCODE.Value) : null;
                dto.Details.Add(new RepairDetailDTO()
                {
                    OPGRPCODE = detail.OPGRPCODE,
                    OPGRPCODEDESCRIPTION = groupOperation.DESCRIPN,
                    OPRNCODE = detail.OPRNCODE,
                    OPRNCODEDESCRIPTION = operation.DESCRIPN,
                    SYMCODE = detail.SYMCODE,
                    SYMCODEDESCRIPTION = symptoms != null ? symptoms.DESCRIPN : string.Empty
                });
            }

            if (roDamages != null)
            {
                foreach (var damage in roDamages)
                {
                    dto.Defects.Add(new RepairDefectDTO()
                    {
                        DAMAGECODE = damage.DAMAGECODE,
                        DAMAMECODEDESC = repository.GetParameterName(damage.DAMAGECODE, "DC").NAME,
                        SIDE = damage.SIDE,
                        SIDEDESC = repository.GetParameterName(damage.SIDE,"VS").NAME
                    });
                }
            }

            return dto;
        }

        public AS_ROHEAD ToEntity(RepairOrderDTO dto)
        {
            return new AS_ROHEAD()
            {
                ACTUALDEL = dto.ACTUALDEL,
                ADDCHG = dto.ADDCHG,
                ADDIND = dto.ADDIND,
                AMATDIS = dto.AMATDIS,
                BOOKED_DOCDAT = dto.BOOKED_DOCDAT,
                BOOKED_DOCNO = dto.BOOKED_DOCNO,
                BVEHTYPE = dto.BVEHTYPE,
                BYOWNER = dto.BYOWNER,
                CANCD = dto.CANCD,
                CANDT = dto.CANDT,
                CBEMPNO = dto.CBEMPNO,
                CHASSISNO = dto.CHASSISNO,
                CNTRYCD = dto.CNTRYCD,
                COMPDAT = dto.COMPDAT,
                CONTPER = dto.CONTPER,
                CONTTEL1 = dto.CONTTEL1,
                CONTTEL2 = dto.CONTTEL2,
                CUSCODE = dto.CUSCODE,
                CUST_ACKWD_TO_SA = dto.CUST_ACKWD_TO_SA,
                CUSTAPPR = dto.CUSTAPPR,
                DEPOSITS = dto.DEPOSITS,
                DISALOWD = dto.DISALOWD,
                DOCDATE = dto.DOCDATE,
                ENTRY = dto.ENTRY,
                ESTILABCOST = dto.ESTILABCOST,
                ESTILABHRS = dto.ESTILABHRS,
                FRROFYR = dto.FRROFYR,
                FRRONO = dto.FRRONO,
                FRROTYPE = dto.FRROTYPE,
                FRSHOP = dto.FRSHOP,
                FUEL = dto.FUEL,
                GPASSDATE = dto.GPASSDATE,
                GPASSNO = dto.GPASSNO,
                GPDOCFYR = dto.GPDOCFYR,
                GPRTIND = dto.GPRTIND,
                HISFLAG = dto.HISFLAG,
                INFORM_RECAL_TO_CUST = dto.INFORM_RECAL_TO_CUST,
                INSRIND = dto.INSRIND,
                INSRLAB = dto.INSRLAB,
                INSRPART = dto.INSRPART,
                JOB_ASGN = dto.JOB_ASGN,
                JOB_LOAD = dto.JOB_LOAD,
                JOBL_DATE = dto.JOBL_DATE,
                KMS = dto.KMS,
                KMSOUT = dto.KMSOUT,
                LABAMT = dto.LABAMT,
                LABDIS = dto.LABDIS,
                LABHRS = dto.LABHRS,
                LASTDATE = dto.LASTDATE,
                LOCCOD = dto.LOCCOD,
                MATDIS = dto.MATDIS,
                MOBILE = dto.MOBILE,
                NEXT_SERVICE = dto.NEXT_SERVICE,
                OBSERVATION = dto.OBSERVATION,
                OLD_PROMISED = dto.OLD_PROMISED,
                OLD_RONO = dto.OLD_RONO,
                OTHSHOP = dto.OTHSHOP,
                PARTSAMT = dto.PARTSAMT,
                PARTSCOST = dto.PARTSCOST,
                PAYCOMP = dto.PAYCOMP,
                PENDING_RECALLS = dto.PENDING_RECALLS,
                PGMID = dto.PGMID,
                PRIORITY = dto.PRIORITY,
                PROMISED = dto.PROMISED,
                PROMISED_CHGCD = dto.PROMISED_CHGCD,
                PRTIND = dto.PRTIND,
                PRV_UPD_DATE = dto.PRV_UPD_DATE,
                RECALL_REMARKS = dto.RECALL_REMARKS,
                RECSTS = dto.RECSTS,
                RELTCD = dto.RELTCD,
                RL_COUPON_NO = dto.RL_COUPON_NO,
                RL_WO_DT = dto.RL_WO_DT,
                RL_WO_NO = dto.RL_WO_NO,
                RODOCFYR = dto.RODOCFYR,
                RONO = dto.RONO,
                ROTYPE = dto.ROTYPE,
                RROFYR = dto.RROFYR,
                RRONO = dto.RRONO,
                RROTYPE = dto.RROTYPE,
                SERADV = dto.SERADV,
                SHOP = dto.SHOP,
                SMSDATE_FU = dto.SMSDATE_FU,
                SMSREF = dto.SMSREF,
                SMSREF_FU = dto.SMSREF_FU,
                SPOTTER = dto.SPOTTER,
                STATUS = dto.STATUS,
                UPD_BY = dto.UPD_BY,
                UPGMID = dto.UPGMID,
                USERID = dto.USERID,
                VEHINBY = dto.VEHINBY,
                VEHINBY_MOBILE = dto.VEHINBY_MOBILE,
                VRFD_CUST_INFO = dto.VRFD_CUST_INFO,
                WARRFLAG = dto.WARRFLAG,
                WARRIND = dto.WARRIND,
                WARRLAB = dto.WARRLAB,
                WARRPART = dto.WARRPART,
                WIPSTATUS = dto.WIPSTATUS,
                WO_FROM = dto.WO_FROM,
            };
        }

        public RepairOrderDTO ToDTO(AS_ROHEAD entity)
        {
            throw new NotImplementedException();
        }
    }
}
