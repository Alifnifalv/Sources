using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ApplicationForms", Schema = "hr")]
    public partial class ApplicationForm
    {
        [Key]
        public long ApplicationFormIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ContactNo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string CountryOfResidence { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string YearsOfExperience { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string PositionAppliedFor { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string CV { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Nationality { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Gender { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Qualification { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Status { get; set; }
    }
}
