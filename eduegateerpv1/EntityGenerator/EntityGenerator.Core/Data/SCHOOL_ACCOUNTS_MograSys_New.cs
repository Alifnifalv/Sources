using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SCHOOL_ACCOUNTS_MograSys_New
    {
        [Required]
        [StringLength(50)]
        public string Root_Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Root_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string New_Mcode { get; set; }
        [Required]
        [StringLength(50)]
        public string glsg_group_code { get; set; }
        [Required]
        [StringLength(50)]
        public string glsg_group_description { get; set; }
        [Required]
        [StringLength(50)]
        public string glsg_group_type { get; set; }
        [Required]
        [StringLength(50)]
        public string glsc_sched_code { get; set; }
        [Required]
        [StringLength(50)]
        public string glsc_sched_name { get; set; }
        [Required]
        [StringLength(50)]
        public string New_Scode { get; set; }
    }
}
