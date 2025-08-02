using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CustomerService
{
    [DataContract]
    public class RepairDefectDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short ROSHOP { get; set; }
        [DataMember]
        public short RODOCFYR { get; set; }
        [DataMember]
        public short ROTYPE { get; set; }
        [DataMember]
        public int RONO { get; set; }
        [DataMember]
        public string SIDE { get; set; }
        [DataMember]
        public string SIDEDESC { get; set; }

        [DataMember]
        public string DAMAGECODE { get; set; }

        [DataMember]
        public string DAMAMECODEDESC { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ENTRY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LASTDATE { get; set; }
        [DataMember]
        public string PGMID { get; set; }
        [DataMember]
        public string USERID { get; set; }
        [DataMember]
        public Nullable<short> CNTRYCD { get; set; }
        [DataMember]
        public string UPD_BY { get; set; }
        [DataMember]
        public string UPGMID { get; set; }
    }
}
