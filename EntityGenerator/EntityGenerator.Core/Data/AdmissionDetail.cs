using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("AdmissionDetail")]
    public partial class AdmissionDetail
    {
        public int? ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; }
        public int? AdmissionNo { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ContactNo { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ZipCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FatherName { get; set; }
    }
}
