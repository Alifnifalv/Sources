using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OfflineCustomerMaster
    {
        [Key]
        public long OfflineCustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public int RefAreaID { get; set; }
        public string Block { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public string Floor { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDatetime { get; set; }
        public Nullable<int> UpdatedByID { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
    }
}
