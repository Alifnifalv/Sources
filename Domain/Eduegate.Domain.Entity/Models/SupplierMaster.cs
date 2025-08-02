using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierMaster
    {
        public SupplierMaster()
        {
            this.TimeSlotMasters = new List<TimeSlotMaster>();
            this.TimeSlotOverRiders = new List<TimeSlotOverRider>();
        }

        [Key]
        public int SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public bool SupplierActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<long> CreatedByID { get; set; }
        public string SupplierAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail1 { get; set; }
        public string ContactEmail2 { get; set; }
        public string ContactPhone1 { get; set; }
        public string ContactPhone2 { get; set; }
        public long RefAreaID { get; set; }
        public string Website { get; set; }
        public Nullable<long> ProductManagerID { get; set; }
        public string ContactEmail3 { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string SwiftCode { get; set; }
        public Nullable<long> AccountNo { get; set; }
        public string AccountTitle { get; set; }
        public string ChequeName { get; set; }
        public string SupplierType { get; set; }
        public string ContactEmail4 { get; set; }
        public virtual ICollection<TimeSlotMaster> TimeSlotMasters { get; set; }
        public virtual ICollection<TimeSlotOverRider> TimeSlotOverRiders { get; set; }
    }
}
