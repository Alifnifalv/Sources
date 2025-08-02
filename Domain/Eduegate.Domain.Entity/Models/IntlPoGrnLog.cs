using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoGrnLog
    {
        [Key]
        public int IntlPoGrnLogID { get; set; }
        public int RefIntlPoGrnMasterID { get; set; }
        public string GrnStatus { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual IntlPoGrnMaster IntlPoGrnMaster { get; set; }
    }
}
