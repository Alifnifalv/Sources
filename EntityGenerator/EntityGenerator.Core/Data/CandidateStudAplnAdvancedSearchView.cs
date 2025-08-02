using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CandidateStudAplnAdvancedSearchView
    {
        public long ApplicationIID { get; set; }
        [StringLength(50)]
        public string ApplicationNumber { get; set; }
        public long? LoginID { get; set; }
        public int? ClassID { get; set; }
        [StringLength(50)]
        public string ClassName { get; set; }
        public byte? GenderID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateOfBirth { get; set; }
        public byte? CastID { get; set; }
        public byte? RelegionID { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(50)]
        public string EmailID { get; set; }
        public long? ProfileContentID { get; set; }
        [StringLength(200)]
        public string ParmenentAddress { get; set; }
        [StringLength(100)]
        public string CurrentAddress { get; set; }
        [StringLength(25)]
        public string FatherOccupation { get; set; }
        [StringLength(152)]
        public string StudentName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string CastDescription { get; set; }
        [StringLength(50)]
        public string RelegionDescription { get; set; }
        [StringLength(50)]
        public string GenderDescription { get; set; }
        public byte? ApplicationStatusID { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public byte? SchoolID { get; set; }
    }
}
