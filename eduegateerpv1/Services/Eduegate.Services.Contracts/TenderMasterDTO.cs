using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.School.Exams;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class TenderMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TenderMasterDTO() {
            TenderAuthenticationList = new List<TenderAuthenticationDTO>();
            TenderType = new KeyValueDTO();
            TenderStatus = new KeyValueDTO();
        }

        [DataMember]
        public long TenderIID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public long? LoginID { get; set; }

        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public long? TenderTypeID { get; set; }
        [DataMember]
        public long? TenderStatusID { get; set; }
        [DataMember]
        public bool? IsOpened { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public DateTime? SubmissionDate { get; set; }
        [DataMember]
        public DateTime? OpeningDate { get; set; }

        [DataMember]
        public string StartDateString { get; set; }
        [DataMember]
        public string EndDateString { get; set; }
        [DataMember]
        public string SubmissionDateString { get; set; }
        [DataMember]
        public string OpeningDateString { get; set; }

        [DataMember]
        public int? NumOfAuthorities { get; set; } 

        [DataMember]
        public KeyValueDTO TenderType { get; set; }

        [DataMember]
        public KeyValueDTO TenderStatus { get; set; } 

        [DataMember]
        public List<TenderAuthenticationDTO> TenderAuthenticationList { get; set; }
    }
}