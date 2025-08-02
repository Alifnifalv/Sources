using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public partial class EmployeeLSProvisionHeadDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeLSProvisionHeadDTO()
        {
          LSProvisionDetails = new List<EmployeeLSProvisionDetailDTO>();
        }

        [DataMember]
        public long EmployeeLSProvisionHeadIID { get; set; }
        [DataMember]
        public int? DocumentTypeID { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string EntryNumber { get; set; } 
        [DataMember]
        public List<KeyValueDTO> Department { get; set; }
        [DataMember]
        public List<KeyValueDTO> Employees { get; set; }
        [DataMember]
        public long? ReportContentID { get; set; }
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public long? BranchID { get; set; }
        [DataMember]
        public bool IsRegenerate { get; set; }
        [DataMember]
        public bool? IsaccountPosted { get; set; }
        [DataMember]
        public bool? IsOpening { get; set; }
        [DataMember]
        public string Branch { get; set; }
        [DataMember]
        public KeyValueDTO DocumentType { get; set; }     
        [DataMember]
        public  List<EmployeeLSProvisionDetailDTO> LSProvisionDetails { get; set; }
    }
}
