using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TreatmentTypes", Schema = "saloon")]
    public partial class TreatmentType
    {
        public TreatmentType()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public int TreatmentTypeID { get; set; }
        [StringLength(200)]
        public string TreatmentName { get; set; }
        public int TreatmentGroupID { get; set; }

        [ForeignKey("TreatmentGroupID")]
        [InverseProperty("TreatmentTypes")]
        public virtual TreatmentGroup TreatmentGroup { get; set; }
        [InverseProperty("TreatmentType")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
