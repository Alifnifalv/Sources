using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class EmployeeAdditionalInfoDTO : BaseMasterDTO
    {
        [DataMember]
        public long EmployeeAdditionalInfoIID {get;set;}
        [DataMember]
        public string HighestAcademicQualitication { get; set; }
        [DataMember]
        public string HighestPrefessionalQualitication { get; set; }
        [DataMember]
        public byte? TotalYearsofExperience { get; set; }
        [DataMember]
        public string ClassessTaught { get; set; }
        [DataMember]
        public string AppointedSubject { get; set; }
        [DataMember]
        public string MainSubjectTought { get; set; }
        [DataMember]
        public string AdditioanalSubjectTought { get; set; }
        [DataMember]
        public bool? IsComputerTrained { get; set; }
    }

}
