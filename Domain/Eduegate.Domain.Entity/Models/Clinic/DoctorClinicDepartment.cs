using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    public partial class DoctorClinicDepartment
    {
        [Key]
        public int ID { get; set; }
        public Nullable<decimal> SLDoctorID { get; set; }
        public Nullable<decimal> SLClinicDepartment { get; set; }
    }
}
