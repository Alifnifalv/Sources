using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class EmploymentServiceDTO
    {
        [DataMember]
        public short CNTRYCD { get; set; }
        [DataMember]
        public Nullable<int> EMP_REQ_NO { get; set; }
        [DataMember]
        public Nullable<short> REQ_LINE_NO { get; set; }
        [DataMember]
        public string NAT_CODE { get; set; }
        [DataMember]
        public string PASSPORT_NO { get; set; }
        [DataMember]
        public Nullable<short> PAYCOMP { get; set; }
        [DataMember]
        public int EMPNO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> REQ_DT { get; set; }
        [DataMember]
        public string REQ_BATCH_IND { get; set; }
        [DataMember]
        public string NAME { get; set; }
        [DataMember]
        public Nullable<short> SHOP { get; set; }
        [DataMember]
        public string SHOP_DESC { get; set; }
        [DataMember]
        public Nullable<short> GROUP_CODE { get; set; }
        [DataMember]
        public string GROUP_DESC { get; set; }
        [DataMember]
        public Nullable<short> MAIN_DESIG_CODE { get; set; }
        [DataMember]
        public string MAIN_DESIG_DESC { get; set; }
        [DataMember]
        public Nullable<short> DESIGCD { get; set; }
        [DataMember]
        public Nullable<decimal> BASIC_SALARY { get; set; }
        [DataMember]
        public string REASON_BASIC { get; set; }
        [DataMember]
        public string PRODUCTIVE_TYPE { get; set; }
        [DataMember]
        public string EMP_TYPE { get; set; }
        [DataMember]
        public string REMARK_EMP_TYPE { get; set; }
        [DataMember]
        public Nullable<short> PROBATION { get; set; }
        [DataMember]
        public Nullable<decimal> BASIC_AFTER_PROBATION { get; set; }
        [DataMember]
        public Nullable<short> BASIC_PERIOD_AFT_PROB { get; set; }
        [DataMember]
        public string QUOTA_TYPE { get; set; }
        [DataMember]
        public Nullable<short> VISACOMPANY { get; set; }
        [DataMember]
        public Nullable<short> PRN_COUNT { get; set; }
        [DataMember]
        public Nullable<short> NOTICE_PER { get; set; }
        [DataMember]
        public string STAFF_CAR_IND { get; set; }
        [DataMember]
        public string PERS_REMARK { get; set; }
        [DataMember]
        public string DEPT_EMAIL { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public string CRE_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CRE_DT { get; set; }
        [DataMember]
        public string CRE_PROG_ID { get; set; }
        [DataMember]
        public string CRE_IP { get; set; }
        [DataMember]
        public string CRE_WEBUSER { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UPD_DT { get; set; }
        [DataMember]
        public string UPD_PROG_ID { get; set; }
        [DataMember]
        public string UPD_IP { get; set; }
        [DataMember]
        public string UPD_WEBUSER { get; set; }
        [DataMember]
        public string ALT_DEPT_EMAIL { get; set; }
        [DataMember]
        public string REMARK_RECRTMT { get; set; }
        [DataMember]
        public string APR_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> APR_DT { get; set; }
        [DataMember]
        public string APR_PROG_ID { get; set; }
        [DataMember]
        public string APR_IP { get; set; }
        [DataMember]
        public string VIS_UPD_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> VIS_UPD_DT { get; set; }
        [DataMember]
        public string VIS_UPD_PROG_ID { get; set; }
        [DataMember]
        public string VIS_UPD_IP { get; set; }
        [DataMember]
        public string RECRUITMENT_REMARK { get; set; }
        [DataMember]
        public string RECRUFRM { get; set; }
        [DataMember]
        public Nullable<short> AGNTCOD { get; set; }
        [DataMember]
        public string FLOW_IND { get; set; }
        [DataMember]
        public Nullable<short> CUR_LVL { get; set; }
        [DataMember]
        public Nullable<short> DEPT_WF_ID { get; set; }
        [DataMember]
        public Nullable<short> HR_WF_ID { get; set; }
        [DataMember]
        public string GENDER { get; set; }
        [DataMember]
        public Nullable<int> REPLACEMENT_ECNO { get; set; }
        [DataMember]
        public string BUDGET_TYPE { get; set; }
        [DataMember]
        public string BUDGET_REASON { get; set; }
        [DataMember]
        public string ORIG_EMP_TYPE { get; set; }
        [DataMember]
        public string OFFER_LETER_IND { get; set; }
        [DataMember]
        public string CIVILID { get; set; }
        [DataMember]
        public string F_NAME { get; set; }
        [DataMember]
        public string M_NAME { get; set; }
        [DataMember]
        public string L_NAME { get; set; }
        [DataMember]
        public string MARITAL_STATUS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        [DataMember]
        public string CONTRACT_TYPE { get; set; }
        [DataMember]
        public Nullable<short> CONTRACT_PERIOD { get; set; }
        [DataMember]
        public Nullable<short> AIR_TICKET_REFUND_PRD { get; set; }
        [DataMember]
        public Nullable<short> LOC_CODE { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PASSPORT_ISSUE { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PASSPORT_EXPIRY { get; set; }
        [DataMember]
        public Nullable<short> NO_OF_TICKETS { get; set; }
        [DataMember]
        public Nullable<short> DURATION { get; set; }
        [DataMember]
        public string TICKET_CLASS { get; set; }
        [DataMember]
        public string DESTINATION { get; set; }
        [DataMember]
        public string EMP_TYPE_SUB_OPT { get; set; }
        [DataMember]
        public string CONTRANO { get; set; }
        [DataMember]
        public string BUDYN { get; set; }
        [DataMember]
        public string CARLVL { get; set; }
        [DataMember]
        public string BULKYN { get; set; }
        [DataMember]
        public Nullable<int> BATCHNO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BATCHDT { get; set; }
        [DataMember]
        public string BULK_APP_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> BULK_APP_DT { get; set; }
        [DataMember]
        public string MOSA_FILENO { get; set; }
        [DataMember]
        public string PORT_OF_BOARD { get; set; }
    }
}
