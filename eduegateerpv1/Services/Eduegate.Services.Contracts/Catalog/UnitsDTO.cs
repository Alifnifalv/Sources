using Eduegate.Framework.Contracts.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{

    [DataContract]
    public class UnitsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long UnitID { get; set; }

        [DataMember]
        public string UnitCode { get; set; }

        [DataMember]
        public string UnitName { get; set; }

        [DataMember]
        public double? Fraction { get; set; }

        [DataMember]
        public long? UnitGroupID { get; set; }
    }
}
