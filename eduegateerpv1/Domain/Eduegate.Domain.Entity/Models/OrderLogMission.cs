using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderLogMission
    {
        [Key]
        public int OrderLogMissionID { get; set; }
        public long RefLogOrderID { get; set; }
        public byte OrderStatus { get; set; }
        public short RefDriverID { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
