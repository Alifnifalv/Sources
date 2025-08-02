using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VWS_DX_STUDENTDATA
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FeeStartDate { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(4000)]
        public string ClassName { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ADMDate { get; set; }
        public int? SchoolAcademicyearID { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(20)]
        public string AcademicYear { get; set; }
        public long? ParentID { get; set; }
        [StringLength(10)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(302)]
        public string GaurdianName { get; set; }
        [StringLength(100)]
        public string BuildingNo { get; set; }
        [StringLength(100)]
        public string FlatNo { get; set; }
        [StringLength(100)]
        public string StreetNo { get; set; }
        [StringLength(100)]
        public string StreetName { get; set; }
        [StringLength(100)]
        public string LocationNo { get; set; }
        [StringLength(100)]
        public string LocationName { get; set; }
        [StringLength(100)]
        public string ZipNo { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string PostBoxNo { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(302)]
        public string MotherName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsUsingTransport { get; set; }
        public int? ORDERNO { get; set; }
    }
}
