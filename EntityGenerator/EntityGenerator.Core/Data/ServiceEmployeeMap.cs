using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceEmployeeMaps", Schema = "saloon")]
    public partial class ServiceEmployeeMap
    {
        [Key]
        public long ServiceEmployeeMapIID { get; set; }
        public long? ServiceID { get; set; }
        public long? EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("ServiceEmployeeMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("ServiceID")]
        [InverseProperty("ServiceEmployeeMaps")]
        public virtual Service Service { get; set; }
    }
}
