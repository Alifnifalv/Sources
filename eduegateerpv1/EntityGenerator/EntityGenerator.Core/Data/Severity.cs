using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Severities", Schema = "mutual")]
    public partial class Severity
    {
        public Severity()
        {
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
        }

        [Key]
        public byte SeverityID { get; set; }
        [StringLength(50)]
        public string SeverityName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Severity")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }
    }
}
