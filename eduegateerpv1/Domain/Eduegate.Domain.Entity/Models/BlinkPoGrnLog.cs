using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BlinkPoGrnLog
    {
        [Key]
        public int BlinkPoGrnLogID { get; set; }
        public int RefBlinkPoGrnMasterID { get; set; }
        public string GrnStatus { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual BlinkPoGrnMaster BlinkPoGrnMaster { get; set; }
    }
}
