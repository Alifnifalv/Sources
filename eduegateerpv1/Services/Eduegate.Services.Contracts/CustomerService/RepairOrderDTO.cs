using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.CustomerService
{
    [DataContract]
    public class RepairOrderDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public KeyValueDTO Shops { get; set; }
        [DataMember]
        public short SHOP { get; set; }
        [DataMember]
        public short RODOCFYR { get; set; }
        [DataMember]
        public short ROTYPE { get; set; }
        [DataMember]
        public KeyValueDTO OrderType { get; set; }
        [DataMember]
        public int RONO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DOCDATE { get; set; }
        [DataMember]
        public string CHASSISNO { get; set; }
        [DataMember]
        public string KTNO { get; set; }
        [DataMember]
        public string VehicleDescription { get; set; }
        [DataMember]
        public DateTime? RegitrationDate { get; set; }
        [DataMember]
        public string BillVehicleType { get; set; }
        [DataMember]
        public int? WarrantyKMs { get; set; }
        [DataMember]
        public int? LastServiceKMs { get; set; }
        [DataMember]
        public int CUSCODE { get; set; }
        [DataMember]
        public KeyValueDTO Customer { get; set; }
        [DataMember]
        public int? PhoneNumber { get; set; }
        [DataMember]
        public long? CivilID { get; set; }
        [DataMember]
        public string BVEHTYPE { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PROMISED { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ACTUALDEL { get; set; }
        [DataMember]
        public string PRIORITY { get; set; }
        [DataMember]
        public string SPOTTER { get; set; }
        [DataMember]
        public Nullable<short> PAYCOMP { get; set; }
        [DataMember]
        public Nullable<int> SERADV { get; set; }
        [DataMember]
        public Nullable<int> KMS { get; set; }
        [DataMember]
        public Nullable<int> KMSOUT { get; set; }
        [DataMember]
        public string FUEL { get; set; }
        [DataMember]
        public Nullable<short> GPDOCFYR { get; set; }
        [DataMember]
        public Nullable<int> GPASSNO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> GPASSDATE { get; set; }
        [DataMember]
        public Nullable<short> GPRTIND { get; set; }
        [DataMember]
        public Nullable<short> FRSHOP { get; set; }
        [DataMember]
        public Nullable<short> FRROFYR { get; set; }
        [DataMember]
        public Nullable<short> FRROTYPE { get; set; }
        [DataMember]
        public Nullable<int> FRRONO { get; set; }
        [DataMember]
        public string INSRIND { get; set; }
        [DataMember]
        public Nullable<decimal> INSRLAB { get; set; }
        [DataMember]
        public Nullable<decimal> INSRPART { get; set; }
        [DataMember]
        public string WARRIND { get; set; }
        [DataMember]
        public Nullable<decimal> WARRLAB { get; set; }
        [DataMember]
        public Nullable<decimal> WARRPART { get; set; }
        [DataMember]
        public string CUSTAPPR { get; set; }
        [DataMember]
        public string ADDIND { get; set; }
        [DataMember]
        public Nullable<decimal> ESTILABHRS { get; set; }
        [DataMember]
        public Nullable<decimal> ESTILABCOST { get; set; }
        [DataMember]
        public Nullable<decimal> ADDCHG { get; set; }
        [DataMember]
        public Nullable<decimal> LABAMT { get; set; }
        public Nullable<int> LABHRS { get; set; }
        public Nullable<decimal> PARTSAMT { get; set; }
        [DataMember]
        public Nullable<decimal> PARTSCOST { get; set; }
        [DataMember]
        public Nullable<decimal> DISALOWD { get; set; }
        [DataMember]
        public Nullable<decimal> LABDIS { get; set; }
        [DataMember]
        public Nullable<decimal> MATDIS { get; set; }
        [DataMember]
        public Nullable<decimal> AMATDIS { get; set; }
        [DataMember]
        public Nullable<decimal> DEPOSITS { get; set; }
        [DataMember]
        public Nullable<short> OTHSHOP { get; set; }
        [DataMember]
        public Nullable<System.DateTime> COMPDAT { get; set; }
        [DataMember]
        public Nullable<short> PRTIND { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public string HISFLAG { get; set; }
        [DataMember]
        public string WARRFLAG { get; set; }
        [DataMember]
        public string CANCD { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CANDT { get; set; }
        [DataMember]
        public string CONTPER { get; set; }
        [DataMember]
        public string CONTTEL1 { get; set; }
        [DataMember]
        public string CONTTEL2 { get; set; }
        [DataMember]
        public Nullable<short> LOCCOD { get; set; }
        [DataMember]
        public string RECSTS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ENTRY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LASTDATE { get; set; }
        [DataMember]
        public string PGMID { get; set; }
        [DataMember]
        public string USERID { get; set; }
        [DataMember]
        public Nullable<int> MOBILE { get; set; }
        [DataMember]
        public Nullable<int> OLD_RONO { get; set; }
        [DataMember]
        public Nullable<int> SMSREF { get; set; }
        [DataMember]
        public string BYOWNER { get; set; }
        [DataMember]
        public string VEHINBY { get; set; }
        [DataMember]
        public string RELTCD { get; set; }
        [DataMember]
        public Nullable<int> CBEMPNO { get; set; }
        [DataMember]
        public Nullable<short> CNTRYCD { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public string UPGMID { get; set; }
        [DataMember]
        public Nullable<int> RL_WO_NO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RL_WO_DT { get; set; }
        [DataMember]
        public Nullable<int> SMSREF_FU { get; set; }
        [DataMember]
        public Nullable<System.DateTime> SMSDATE_FU { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OLD_PROMISED { get; set; }
        [DataMember]
        public string PROMISED_CHGCD { get; set; }
        [DataMember]
        public Nullable<int> RL_COUPON_NO { get; set; }
        [DataMember]
        public string WIPSTATUS { get; set; }
        [DataMember]
        public string VRFD_CUST_INFO { get; set; }
        [DataMember]
        public Nullable<int> VEHINBY_MOBILE { get; set; }
        [DataMember]
        public Nullable<short> RROFYR { get; set; }
        [DataMember]
        public Nullable<short> RROTYPE { get; set; }
        [DataMember]
        public Nullable<int> RRONO { get; set; }
        [DataMember]
        public string PENDING_RECALLS { get; set; }
        [DataMember]
        public string INFORM_RECAL_TO_CUST { get; set; }
        [DataMember]
        public string CUST_ACKWD_TO_SA { get; set; }
        public string RECALL_REMARKS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PRV_UPD_DATE { get; set; }
        [DataMember]
        public string WO_FROM { get; set; }
        [DataMember]
        public Nullable<int> NEXT_SERVICE { get; set; }
        [DataMember]
        public string OBSERVATION { get; set; }
        [DataMember]
        public Nullable<decimal> BOOKED_DOCNO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BOOKED_DOCDAT { get; set; }
        [DataMember]
        public string JOB_LOAD { get; set; }
        [DataMember]
        public Nullable<System.DateTime> JOBL_DATE { get; set; }
        [DataMember]
        public Nullable<int> JOB_ASGN { get; set; }
        [DataMember]
        public List<RepairDetailDTO> Details { get; set; }
        [DataMember]
        public List<RepairDefectDTO> Defects { get; set; }


    }
}
