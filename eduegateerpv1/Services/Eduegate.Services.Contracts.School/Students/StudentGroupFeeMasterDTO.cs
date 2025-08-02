using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
   public class StudentGroupFeeMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentGroupFeeMasterDTO()
        {
            FeeMasterClassMaps = new List<StudentGroupFeeTypeMapDTO>();
        }
        [DataMember]
        public long StudentGroupFeeMasterIID { get; set; }


        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? StudentGroupID { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        

        [DataMember]
        public KeyValueDTO AcadamicYear { get; set; }

        [DataMember]
        public KeyValueDTO StudentGroup { get; set; }

        [DataMember]
        public List<StudentGroupFeeTypeMapDTO> FeeMasterClassMaps { get; set; }
    }
}
