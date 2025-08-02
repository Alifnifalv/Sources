using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Sponsors", Schema = "payroll")]
    public partial class Sponsor
    {
        public Sponsor()
        {
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            Schools = new HashSet<School>();
        }

        [Key]
        public long SponsorIID { get; set; }
        [StringLength(500)]
        public string SponsorName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsVisible { get; set; }

        [InverseProperty("Sponsor")]
        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
        [InverseProperty("Sponsor")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }
        [InverseProperty("Sponsor")]
        public virtual ICollection<School> Schools { get; set; }
    }
}
