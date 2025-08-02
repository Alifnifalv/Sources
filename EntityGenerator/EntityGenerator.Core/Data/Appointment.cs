using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Appointments", Schema = "saloon")]
    public partial class Appointment
    {
        [Key]
        public long AppointmentIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AppointmentDate { get; set; }
        public long? CustomerID { get; set; }
        public int? FrequencyTypeID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? ServiceID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Duration { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CustomerID")]
        [InverseProperty("Appointments")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("Appointments")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("FrequencyTypeID")]
        [InverseProperty("Appointments")]
        public virtual FrequencyType FrequencyType { get; set; }
        [ForeignKey("ServiceID")]
        [InverseProperty("Appointments")]
        public virtual Service Service { get; set; }
    }
}
