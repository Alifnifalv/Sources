using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderOfflineMissionDetail
    {
        [Key]
        public long OrderOfflineMissionDetailID { get; set; }
        public long RefOrderOfflineMissionMasterID { get; set; }
        public long RefOrderOfflineID { get; set; }
        public long CreatedByID { get; set; }
        public System.DateTime CreatedDateTimeStamp { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> UpdatedByTimeStamp { get; set; }
        public Nullable<long> UpdatedByID { get; set; }
    }
}
