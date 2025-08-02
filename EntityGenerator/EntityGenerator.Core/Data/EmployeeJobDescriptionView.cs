using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeJobDescriptionView
    {
        public long JobDescriptionIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(250)]
        public string JDReference { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JDDate { get; set; }
        [Required]
        [StringLength(554)]
        public string Employee { get; set; }
        [Required]
        [StringLength(554)]
        public string ReportingToEmployee { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
