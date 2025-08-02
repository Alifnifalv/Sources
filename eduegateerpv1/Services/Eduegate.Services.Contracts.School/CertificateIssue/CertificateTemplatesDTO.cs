using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.CertificateIssue
{
    [DataContract]
    public class CertificateTemplatesDTO : BaseMasterDTO
    {
        [DataMember]
        public long CertificateTemplateIID { get; set; }

        [DataMember]
        public string ReportName { get; set; }

        [DataMember]
        public string CertificateName { get; set; }

    }
}


