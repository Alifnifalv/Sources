using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ClassTeacherMapSearch
    {
        public long ClassTeacherMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        public long? EmployeeID { get; set; }
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
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
    }
}
