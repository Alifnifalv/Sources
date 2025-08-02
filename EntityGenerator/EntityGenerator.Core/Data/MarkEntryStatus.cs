using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkEntryStatuses", Schema = "schools")]
    public partial class MarkEntryStatus
    {
        public MarkEntryStatus()
        {
            MarkRegisterSkills = new HashSet<MarkRegisterSkill>();
            MarkRegisterSubjectMaps = new HashSet<MarkRegisterSubjectMap>();
            MarkRegisters = new HashSet<MarkRegister>();
        }

        [Key]
        public byte MarkEntryStatusID { get; set; }
        [StringLength(50)]
        public string MarkEntryStatusName { get; set; }
        [StringLength(10)]
        public string MarkEntryCode { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("MarkEntryStatus")]
        public virtual ICollection<MarkRegisterSkill> MarkRegisterSkills { get; set; }
        [InverseProperty("MarkEntryStatus")]
        public virtual ICollection<MarkRegisterSubjectMap> MarkRegisterSubjectMaps { get; set; }
        [InverseProperty("MarkEntryStatus")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
    }
}
