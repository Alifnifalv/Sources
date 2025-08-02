using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPickupRequestStatuses", Schema = "schools")]
    public partial class StudentPickupRequestStatus
    {
        [Key]
        public byte StudentPickupRequestStatusID { get; set; }
        [StringLength(50)]
        public string StudentPickupRequestStatusName { get; set; }
    }
}
