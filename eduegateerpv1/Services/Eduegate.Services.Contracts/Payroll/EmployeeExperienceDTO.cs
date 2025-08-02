using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    public class EmployeeExperienceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO

    {

        [DataMember]
        public long EmployeeExperienceDetailIID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public DateTime? FromDate { get; set; }

        [DataMember]
        public DateTime? ToDate { get; set; }

        [DataMember]
        public string NameOfOraganizationtName { get; set; }

        [DataMember]
        public string CurriculamOrIndustry { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        public string SubjectTaught { get; set; }

        [DataMember]
        public string ClassTaught { get; set; }

        [DataMember]
        public string FromDateString { get; set; }

        [DataMember]
        public string ToDateString { get; set; }



    }
}
