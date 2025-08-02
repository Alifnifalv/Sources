using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Clinic
{
    [Keyless]
    public partial class SL_Clinic
    {
        public decimal SLClinicID { get; set; }
        public string SLClinicName { get; set; }
        public string SLClinicDescription { get; set; }
        public string SLClinicDescription2 { get; set; }
    }
}
