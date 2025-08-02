using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DriverViolation
    {
        [Key]
        public int ViolationID { get; set; }
        public int RefDriverID { get; set; }
        public string ViolationRemark { get; set; }
        public System.DateTime ViolationHistoryDate { get; set; }
    }
}
