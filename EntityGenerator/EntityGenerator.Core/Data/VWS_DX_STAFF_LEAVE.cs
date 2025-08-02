using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STAFF_LEAVE
    {
        [Column(TypeName = "datetime")]
        public DateTime? NotificationDate { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        public long EmployeeIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(502)]
        public string EmployeeName { get; set; }
    }
}
