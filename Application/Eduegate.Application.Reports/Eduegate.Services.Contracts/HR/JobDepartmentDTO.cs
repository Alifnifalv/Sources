using Eduegate.Services.Contracts.Commons;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class JobDepartmentDTO : BaseMasterDTO
    {
        public JobDepartmentDTO()
        {
            Tags = new List<KeyValueDTO>();
        }

        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public byte StatusID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public List<KeyValueDTO> Tags { get; set; }
    }
}
