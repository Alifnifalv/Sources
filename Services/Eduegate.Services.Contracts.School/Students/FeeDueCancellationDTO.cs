using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class FeeDueCancellationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeDueCancellationDTO()
        {
            StudentGroupFeeMaster = new StudentGroupFeeMasterDTO();
            //FeeDueDetails = new List<StudentFeeDueDTO>();
            FeeDueCancellation = new List<FeeDueCancellationDetailDTO>();
        }

        [DataMember]
        public long FeeDueCancellationIID { get; set; }

        [DataMember]
        public long StudentGroupFeeMasterID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public DateTime? CancelationDate { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public StudentGroupFeeMasterDTO StudentGroupFeeMaster { get; set; }

        //[DataMember]
        //public List<StudentFeeDueDTO> FeeDueDetails { get; set; }

        [DataMember]
        public List<FeeDueCancellationDetailDTO> FeeDueCancellation { get; set; }
    }
}
