using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    public class StudentApplicationDocumentsUploadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ApplicationDocumentIID { get; set; }
        [DataMember]
        public long? ApplicationID { get; set; }
        [DataMember]
        public long? BirthCertificateReferenceID { get; set; }

        [DataMember]
        public string BirthCertificateAttach { get; set; }
        [DataMember]
        public long? StudentPassportReferenceID { get; set; }

        [DataMember]
        public string StudentPassportAttach { get; set; }
        [DataMember]
        public long? TCReferenceID { get; set; }

        [DataMember]
        public string TCAttach { get; set; }
        [DataMember]
        public long? FatherQIDReferenceID { get; set; }

        [DataMember]
        public string FatherQIDAttach { get; set; }
        [DataMember]
        public long? MotherQIDReferenceID { get; set; }

        [DataMember]
        public string MotherQIDAttach { get; set; }
        [DataMember]
        public long? StudentQIDReferenceID { get; set; }

        [DataMember]
        public string StudentQIDAttach { get; set; }
        [DataMember]
        public string Notes { get; set; }

}
}
