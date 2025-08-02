using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StaffPresentStatuses", Schema = "schools")]
    public partial class StaffPresentStatus
    {
        public StaffPresentStatus()
        {
            StaffAttendences = new HashSet<StaffAttendence>();
        }

        [Key]
        public byte StaffPresentStatusID { get; set; }
        [StringLength(50)]
        public string StatusDescription { get; set; }
        [StringLength(20)]
        public string StatusTitle { get; set; }

        [InverseProperty("PresentStatus")]
        public virtual ICollection<StaffAttendence> StaffAttendences { get; set; }
    }
}
