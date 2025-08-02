using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public partial class EmployeeESBProvisionHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeESBProvisionHeadDTO()
        {
            ESBProvisionDetails = new List<EmployeeESBProvisionDetailDTO>();
        }
        [DataMember]
        public long EmployeeESBProvisionHeadIID { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string EntryNumber { get; set; }
       
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public long? BranchID { get; set; }
        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public KeyValueDTO DocumentType { get; set; }
        [DataMember]
        public List<KeyValueDTO> Department { get; set; }

        [DataMember]
        public List<KeyValueDTO> Employees { get; set; }

        [DataMember]
        public bool IsRegenerate { get; set; }

        [DataMember]
        public bool? IsaccountPosted { get; set; }
        [DataMember]
        public bool? IsOpening { get; set; }

        [DataMember]
        public long? ReportContentID { get; set; }
        [DataMember]
        public virtual List<EmployeeESBProvisionDetailDTO> ESBProvisionDetails { get; set; }
    }
}
