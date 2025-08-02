using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    public partial class DoctorClinicDepartment
    {
        public int ID { get; set; }
        public Nullable<decimal> SLDoctorID { get; set; }
        public Nullable<decimal> SLClinicDepartment { get; set; }
    }
}
