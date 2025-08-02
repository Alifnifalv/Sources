using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeTypeDTO : BaseMasterDTO
    {
         [DataMember]
        public int  FeeTypeID { get; set; }

        [DataMember]
        public string  FeeCode { get; set; }

        [DataMember]
        public string  TypeName { get; set; }

        [DataMember]
        public int? FeeGroupId { get; set; }
        
        [DataMember]
        public byte? FeeCycleId { get; set; }

        [DataMember]
        public bool? IsRefundable { get; set; }

        [DataMember]
        public string FeeGroupName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}