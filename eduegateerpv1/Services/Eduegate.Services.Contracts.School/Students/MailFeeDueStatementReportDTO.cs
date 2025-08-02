using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class MailFeeDueStatementReportDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public MailFeeDueStatementReportDTO()
        {

        }

        [DataMember]
        public bool? IsSelected { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public int? SchoolID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AdmissionNo { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string FeeDueDate { get; set; }

        [DataMember]
        public string AsOnDate { get; set; }

        [DataMember]
        public long? ClassID { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public string ParentContact { get; set; }

        [DataMember]
        public string ParentEmailID { get; set; }

        [DataMember]
        public long? ParentLoginID { get; set; }

        [DataMember]
        public string returnMessage { get; set; }

        [DataMember]
        public string ReportName { get; set; }

    }
}
