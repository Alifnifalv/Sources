using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AppointmentSearch
    {
        public long AppointmentIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AppointmentDate { get; set; }
        public long? CustomerID { get; set; }
        [StringLength(255)]
        public string CustomerName { get; set; }
        public int? FrequencyTypeID { get; set; }
        [StringLength(200)]
        public string Frequency { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? ServiceID { get; set; }
        public string ServiceDescription { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Duration { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
