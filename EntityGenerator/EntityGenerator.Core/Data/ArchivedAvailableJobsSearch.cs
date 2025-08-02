using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ArchivedAvailableJobsSearch
    {
        public long JobIID { get; set; }
        public Guid? Id { get; set; }
        public int? DepartmentId { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(1000)]
        public string JobDescription { get; set; }
    }
}
