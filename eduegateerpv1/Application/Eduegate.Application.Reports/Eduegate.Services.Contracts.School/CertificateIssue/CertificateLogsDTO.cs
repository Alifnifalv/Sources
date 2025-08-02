using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.CertificateIssue
{
    [DataContract]
    public class CertificateLogsDTO : BaseMasterDTO
    {
        [DataMember]
        public long CertificateLogIID { get; set; }

        [DataMember]
        public long? CertificateTemplateParameterID { get; set; }

        [DataMember]
        public string ParameterValue { get; set; }

        [DataMember]
        public long? CertificateTemplateIID { get; set; }
    }
}


