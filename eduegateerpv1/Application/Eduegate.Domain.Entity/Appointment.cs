namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.Appointments")]
    public partial class Appointment
    {
        [Key]
        public long AppointmentIID { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public long? CustomerID { get; set; }

        public int? FrequencyTypeID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? ServiceID { get; set; }

        public long? EmployeeID { get; set; }

        public decimal? Duration { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual FrequencyType FrequencyType { get; set; }

        public virtual Service Service { get; set; }
    }
}
