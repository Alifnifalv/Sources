using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.Payroll;

namespace Eduegate.Services.Contracts.Communications
{
    [DataContract]
    public class StudentChatDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
       
        [DataMember]
        public long StudentID { get; set; }

        [DataMember]

        public List<EmployeeDTO> TeacherMessages { get; set; } = new List<EmployeeDTO>();

        [DataMember]

        public string StudentName { get; set; }


        [DataMember]

        public string AdmissionNumber { get; set; }
    }
}