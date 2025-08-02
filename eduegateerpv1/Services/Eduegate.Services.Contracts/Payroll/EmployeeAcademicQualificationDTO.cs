using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    public class EmployeeAcademicQualificationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO

    {

        [DataMember]
        public long EmployeeQualificationMapIID { get; set; }
        [DataMember]

        public byte? QualificationID { get; set; }
        [DataMember]

        public long? EmployeeID { get; set; }

        [DataMember]
        public string TitleOfProgramme { get; set; }

        [DataMember]

        public string ModeOfProgramme { get; set; }

        [DataMember]

        public string University { get; set; }


        [DataMember]
        public int? GraduationMonth { get; set; }

        [DataMember]
        public int? GraduationYear { get; set; }

        [DataMember]

        public decimal? MarksInPercentage { get; set; }

        [DataMember]

        public string Subject { get; set; }


    }
}
