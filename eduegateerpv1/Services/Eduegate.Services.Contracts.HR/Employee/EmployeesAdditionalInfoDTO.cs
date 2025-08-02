using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Employee
{
    [DataContract]
    public class EmployeesAdditionalInfoDTO : BaseMasterDTO
    {
        [DataMember]
        public long EmployeeAdditionalInfoIID {get;set;}
        [DataMember]
        public string HighestAcademicQualitication { get; set; }
        [DataMember]
        public string HighestPrefessionalQualitication { get; set; }
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
