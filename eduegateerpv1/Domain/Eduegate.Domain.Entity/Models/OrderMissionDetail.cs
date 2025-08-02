using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMissionDetail
    {
        [Key]
        public long OrderMissionDetailID { get; set; }
        public long RefOrderMissionMasterID { get; set; }
        public long RefOrderID { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> UpdatedByTimeStamp { get; set; }
        public Nullable<long> UpdatedByID { get; set; }
    }
}
