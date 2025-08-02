using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SchoolsContentDetailsView
    {
        public byte SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(50)]
        public string RegistrationID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(50)]
        public string SchoolCode { get; set; }
        [StringLength(100)]
        public string Place { get; set; }
        public string EmployerEID { get; set; }
        public string PayerEID { get; set; }
        public string PayerQID { get; set; }
        [StringLength(10)]
        public string SchoolShortName { get; set; }
        public long? SchoolProfileID { get; set; }
        public long? SchoolSealID { get; set; }
        public byte[] SchoolSeal { get; set; }
    }
}
