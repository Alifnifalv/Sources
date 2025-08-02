using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Loan
{
    [DataContract]
    public partial class LoanRequestSearchDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LoanRequestIID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]       
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string DesignationName { get; set; }
    }
}
