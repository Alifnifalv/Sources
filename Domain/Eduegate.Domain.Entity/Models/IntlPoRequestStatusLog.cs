using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoRequestStatusLog
    {
        [Key]
        public int IntlPoRequestStatusLogID { get; set; }
        public int RefIntlPoRequestID { get; set; }
        public string RequestStatus { get; set; }
        public long UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual IntlPoRequest IntlPoRequest { get; set; }
    }
}
