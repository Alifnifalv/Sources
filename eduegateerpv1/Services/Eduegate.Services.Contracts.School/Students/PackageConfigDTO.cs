using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class PackageConfigDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public PackageConfigDTO()
        {
            Class = new List<KeyValueDTO>();
            FeeStructure = new List<KeyValueDTO>();
            Student = new List<KeyValueDTO>();
            StudentGroup = new List<KeyValueDTO>();
            Academic = new KeyValueDTO();
            CreditNoteAccountID = new KeyValueDTO();
        }

        [DataMember]
        public long PackageConfigIID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public List<KeyValueDTO> Class { get; set; }

        [DataMember]
        public List<KeyValueDTO> FeeStructure { get; set; }

        [DataMember]
        public KeyValueDTO Academic { get; set; }

        [DataMember]
        public List<KeyValueDTO> Student { get; set; }
              
        [DataMember]
        public List<KeyValueDTO> StudentGroup { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public bool? IsAutoCreditNote { get; set; }

        [DataMember]
        public KeyValueDTO CreditNoteAccountID { get; set; }
    }
}
