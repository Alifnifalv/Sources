using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentAssignmentMapSearch
    {
        public long StudentAssignmentMapIID { get; set; }
        public long? AssignmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfSubmission { get; set; }
        public byte? AssignmentStatusID { get; set; }
        public long? AttachmentReferenceId { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        public long? StudentId { get; set; }
        public string Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
