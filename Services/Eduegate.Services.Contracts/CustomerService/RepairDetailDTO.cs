using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CustomerService
{
    [DataContract]
    public class RepairDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short SHOP { get; set; }
        [DataMember]
        public short RODOCFYR { get; set; }
        [DataMember]
        public short ROTYPE { get; set; }
        [DataMember]
        public int RONO { get; set; }
        [DataMember]
        public short SRLNO { get; set; }
        [DataMember]
        public short OPGRPCODE { get; set; }
        [DataMember]
        public string OPGRPCODEDESCRIPTION { get; set; }
        [DataMember]
        public short OPRNCODE { get; set; }
        [DataMember]
        public string OPRNCODEDESCRIPTION { get; set; }
        [DataMember]
        public Nullable<short> SYMCODE { get; set; }
        [DataMember]
        public string SYMCODEDESCRIPTION { get; set; }
        [DataMember]
        public string OCCURCODE { get; set; }
        [DataMember]
        public string POSITION { get; set; }
        [DataMember]
        public Nullable<short> SEQUENCE { get; set; }
        [DataMember]
        public Nullable<short> DEPENDENCE { get; set; }
        [DataMember]
        public string BILLCD { get; set; }
        [DataMember]
        public string RATEIND { get; set; }
        [DataMember]
        public string HRIND { get; set; }
        [DataMember]
        public Nullable<decimal> DISALWD { get; set; }
        [DataMember]
        public Nullable<decimal> CONSCHG { get; set; }
        [DataMember]
        public string PERIND { get; set; }
        [DataMember]
        public string MAJIND { get; set; }
        [DataMember]
        public string BILLFLAG { get; set; }
        [DataMember]
        public string PRNFLAG { get; set; }
        [DataMember]
        public string MATLFLAG { get; set; }
        [DataMember]
        public string LABFLAG { get; set; }
        [DataMember]
        public string CONSFLAG { get; set; }
        [DataMember]
        public string RSNCD { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RSNDT { get; set; }
        [DataMember]
        public string PAYCODE { get; set; }
        [DataMember]
        public Nullable<short> ESTSHP { get; set; }
        [DataMember]
        public Nullable<short> ESTDOCFYR { get; set; }
        [DataMember]
        public Nullable<short> ESTTYP { get; set; }
        [DataMember]
        public Nullable<int> ESTNO { get; set; }
        [DataMember]
        public Nullable<short> TOSHOP { get; set; }
        [DataMember]
        public Nullable<short> INVCOMP { get; set; }
        [DataMember]
        public Nullable<short> INVSHOP { get; set; }
        [DataMember]
        public Nullable<short> INVDOCFYR { get; set; }
        [DataMember]
        public Nullable<short> INVTYPE { get; set; }
        [DataMember]
        public Nullable<int> INVNO { get; set; }
        [DataMember]
        public Nullable<System.DateTime> INVDAT { get; set; }
        [DataMember]
        public string ADDIND { get; set; }
        public Nullable<short> ESTILABHRS { get; set; }
        [DataMember]
        public Nullable<short> STDLABHR { get; set; }
        [DataMember]
        public Nullable<decimal> STDLABRATE { get; set; }
        [DataMember]
        public Nullable<decimal> MATRAT { get; set; }
        [DataMember]
        public Nullable<System.DateTime> STARTDATE { get; set; }
        [DataMember]
        public Nullable<System.DateTime> COMPLETED { get; set; }
        [DataMember]
        public Nullable<short> BALHRS { get; set; }
        [DataMember]
        public Nullable<short> LABRTOT { get; set; }
        [DataMember]
        public string ADDLREF { get; set; }
        [DataMember]
        public string TCREF { get; set; }
        [DataMember]
        public Nullable<short> PRFSHOP { get; set; }
        [DataMember]
        public Nullable<short> PRFDOCFYR { get; set; }
        [DataMember]
        public Nullable<short> PRFTYP { get; set; }
        [DataMember]
        public Nullable<int> PRFNO { get; set; }
        [DataMember]
        public string CLSIND { get; set; }
        [DataMember]
        public string PRFIND { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ENTRY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LASTDATE { get; set; }
        public string PGMID { get; set; }
        [DataMember]
        public string USERID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DOCDATE { get; set; }
        [DataMember]
        public string CBRSNCD { get; set; }
        [DataMember]
        public string CBREMARKS { get; set; }
        [DataMember]
        public Nullable<short> CNTRYCD { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public string UPGMID { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_LABAMT { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_LPOAMT { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_MATAMT { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_ALABDIS { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_MATDIS { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_GMATMAX { get; set; }
        [DataMember]
        public Nullable<decimal> INV_OPRN_CONSAMT { get; set; }
        [DataMember]
        public string FRT_MODIND { get; set; }
        [DataMember]
        public string RECALL_IND { get; set; }
        [DataMember]
        public string CAUSE { get; set; }
        [DataMember]
        public string CORRCT_ACTION { get; set; }
        [DataMember]
        public string TECHNICIAN { get; set; }
    }
}
