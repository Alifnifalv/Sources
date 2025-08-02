using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CandidateRegisterView
    {
        public long? StudentID { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public long CandidateIID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [Required]
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(50)]
        public string CandidateName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string MobileNumber { get; set; }
        [StringLength(20)]
        public string Telephone { get; set; }
        public string Notes { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(500)]
        public string OnlineExam { get; set; }
        [StringLength(50)]
        public string OnlineExamStatus { get; set; }
        [StringLength(50)]
        public string OperationStatus { get; set; }
    }
}
