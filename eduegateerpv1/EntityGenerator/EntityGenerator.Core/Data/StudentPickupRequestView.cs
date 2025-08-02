using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentPickupRequestView
    {
        public long StudentPickupRequestIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestDate { get; set; }
        public long? StudentID { get; set; }
        [StringLength(555)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public byte? PickedByID { get; set; }
        [StringLength(50)]
        public string PickedBY { get; set; }
        [StringLength(152)]
        [Unicode(false)]
        public string PickupPerson { get; set; }
        public byte? RequestStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public int? UpdatedBy { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
