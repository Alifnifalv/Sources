using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EducationTypes", Schema = "communities")]
    public partial class EducationType
    {
        public EducationType()
        {
            EducationDetails = new HashSet<EducationDetail>();
        }

        [Key]
        public byte EducationTypeID { get; set; }
        [StringLength(500)]
        public string EducationDescription { get; set; }

        [InverseProperty("EducationType")]
        public virtual ICollection<EducationDetail> EducationDetails { get; set; }
    }
}
