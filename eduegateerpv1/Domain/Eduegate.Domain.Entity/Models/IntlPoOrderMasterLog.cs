using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderMasterLog
    {
        [Key]
        public int IntlPoOrderMasterLogID { get; set; }
        public int RefIntlPoOrderMasterID { get; set; }
        public string Status { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public short UpdatedBy { get; set; }
        public virtual IntlPoOrderMaster IntlPoOrderMaster { get; set; }
    }
}
