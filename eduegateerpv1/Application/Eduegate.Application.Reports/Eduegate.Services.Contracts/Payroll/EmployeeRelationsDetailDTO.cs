using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    public class EmployeeRelationsDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO

    {

        [DataMember]
        public long EmployeeRelationsDetailIID { get; set; }
        [DataMember]

        public byte? EmployeeRelationTypeID { get; set; }
        [DataMember]

        public long? EmployeeID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]

        public string MiddleName { get; set; }

        [DataMember]

        public string LastName { get; set; }

        [DataMember]

        public string PassportNo { get; set; }

        [DataMember]
        public int? CountryofIssueID { get; set; }

        [DataMember]
        public string PlaceOfIssue { get; set; }

        [DataMember]

        public DateTime? PassportNoIssueDate { get; set; }

        [DataMember]

        public DateTime? PassportNoExpiry { get; set; }

        [DataMember]
        public string VisaNo { get; set; }

        [DataMember]

        public DateTime? VisaExpiry { get; set; }

        [DataMember]
        public string NationalIDNo { get; set; }

        [DataMember]

        public DateTime? NationalIDNoIssueDate { get; set; }
        [DataMember]

        public DateTime? NationalIDNoExpiry { get; set; }

        [DataMember]  
        public string ContactNo { get; set; }

        [DataMember]
        public KeyValueDTO EmployeeRelationType { get; set; }
    }
}
