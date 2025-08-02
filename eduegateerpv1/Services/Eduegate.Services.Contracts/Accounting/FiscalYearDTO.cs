using System;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
   public  class FiscalYearDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int Company_ID { get; set; }

        [DataMember]
        public int FiscalYear_ID { get; set; }

        [DataMember]
        public string FiscalYear_Name { get; set; }

        [DataMember]
        public int? FiscalYear_Status { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public string CurrentYear { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsHide { get; set; }

        [DataMember]
        public int? AuditType { get; set; }

        [DataMember]
        public DateTime? AuditedDate { get; set; }

        [DataMember]
        public int? OrderNo { get; set; }

        [DataMember]
        public int Prv_FiscalYear_ID { get; set; }

        [DataMember]
        public string StatusName { get; set; }

        [DataMember]
        public string AuditTypeName { get; set; }
    }
}
