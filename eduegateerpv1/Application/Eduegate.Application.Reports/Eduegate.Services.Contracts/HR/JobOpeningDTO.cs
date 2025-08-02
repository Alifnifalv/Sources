using Eduegate.Services.Contracts.Commons;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class JobOpeningDTO : BaseMasterDTO
    {
        public JobOpeningDTO()
        {
            CultureDatas = new List<JobOpeningCultureDataDTO>();
            Tags = new List<KeyValueDTO>();
        }

        [DataMember]
        public List<JobOpeningCultureDataDTO> CultureDatas { get; set; }

        [DataMember]
        public List<KeyValueDTO> Tags { get; set; }

        [DataMember]
        public long JobIID { get; set; }
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public string TypeOfJob { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string JobDetail { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string PageId { get; set; }
        [DataMember]
        public Nullable<int> DepartmentId { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public string JobStatus { get; set; }
        [DataMember]
        public string CreateDateAsString { get; set; }
    }
}
