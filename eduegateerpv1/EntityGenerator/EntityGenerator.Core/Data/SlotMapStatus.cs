using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SlotMapStatuses", Schema = "signup")]
    public partial class SlotMapStatus
    {
        public SlotMapStatus()
        {
            SignupSlotAllocationMaps = new HashSet<SignupSlotAllocationMap>();
            SignupSlotMaps = new HashSet<SignupSlotMap>();
        }

        [Key]
        public byte SlotMapStatusID { get; set; }
        [StringLength(100)]
        public string SlotMapStatusName { get; set; }
        public int? StatusOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SlotMapStatus")]
        public virtual ICollection<SignupSlotAllocationMap> SignupSlotAllocationMaps { get; set; }
        [InverseProperty("SlotMapStatus")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
    }
}
