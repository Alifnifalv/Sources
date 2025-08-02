using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LibraryBookMapDTO()
        {

        }

        [DataMember]
        public int LibraryBookMapIID { get; set; }

        [DataMember]
        public long? LibraryBookID { get; set; }

        [DataMember]
        public string Call_No { get; set; }

        [DataMember]
        public string Acc_No { get; set; }

        [DataMember]
        public int? S_No { get; set; }

    }
}


