using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CustomerService
{
    [DataContract]
    public class RepairVehicleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public string CHASSISNO { get; set; }
        [DataMember]
        public string KTNO { get; set; }
        [DataMember]
        public Nullable<int> CUSCODE { get; set; }
        [DataMember]
        public string VEHMAKE { get; set; }
        [DataMember]
        public string VEHTYPE { get; set; }
        [DataMember]
        public Nullable<System.DateTime> MODELYEAR { get; set; }
        [DataMember]
        public string CYLCODE { get; set; }
        [DataMember]
        public string STOCKNO { get; set; }
        [DataMember]
        public string MDLCD { get; set; }
        [DataMember]
        public string MDLOPT { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public string ADESCRIPTION { get; set; }
        [DataMember]
        public string COLRCD { get; set; }
        [DataMember]
        public string TRIMCD { get; set; }
        [DataMember]
        public string ENGINENO { get; set; }
        [DataMember]
        public Nullable<int> FIRSTOWNER { get; set; }
        [DataMember]
        public Nullable<int> SECONDOWNER { get; set; }
        [DataMember]
        public Nullable<System.DateTime> REGISTRATION { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DELDAT { get; set; }
        [DataMember]
        public Nullable<int> WARRANTYKM { get; set; }
        [DataMember]
        public Nullable<short> WARRANTYMTH { get; set; }
        [DataMember]
        public Nullable<short> INVCOMP { get; set; }
        [DataMember]
        public Nullable<short> INVSHOP { get; set; }
        [DataMember]
        public Nullable<short> INVTYP { get; set; }
        [DataMember]
        public Nullable<int> INVNUM { get; set; }
        [DataMember]
        public Nullable<System.DateTime> INVDAT { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LAST_SERV { get; set; }
        [DataMember]
        public string SALCAT { get; set; }
        [DataMember]
        public Nullable<int> LASTKM { get; set; }
        [DataMember]
        public Nullable<short> LROSHOP { get; set; }

        [DataMember]
        public Nullable<short> LRODOCFYR { get; set; }
        [DataMember]
        public Nullable<short> LROTYPE { get; set; }
        [DataMember]
        public Nullable<int> LASTRONO { get; set; }
        [DataMember]
        public Nullable<short> CROSHOP { get; set; }
        [DataMember]
        public Nullable<short> CRODOCFYR { get; set; }
        [DataMember]
        public Nullable<short> CROTYPE { get; set; }
        [DataMember]
        public Nullable<int> CURRONO { get; set; }
        [DataMember]
        public Nullable<decimal> MATERIAL { get; set; }
        [DataMember]
        public Nullable<decimal> LABOUR { get; set; }
        [DataMember]
        public string STATUS { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ENTRY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LASTDATE { get; set; }
        [DataMember]
        public string PGMID { get; set; }
        [DataMember]
        public string USERID { get; set; }
        [DataMember]
        public string MAIN_VEHTYPE { get; set; }
        [DataMember]
        public string VEH_TYPE { get; set; }
        [DataMember]
        public Nullable<short> CNTRYCD { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public string UPGMID { get; set; }
        [DataMember]
        public string SEL_MAIN_VEHTYPE { get; set; }
        [DataMember]
        public string SEL_VEH_TYPE { get; set; }
        [DataMember]
        public Nullable<int> NEXT_SERVICE { get; set; }
        [DataMember]
        public string OBSERVATION { get; set; }
    }
}
