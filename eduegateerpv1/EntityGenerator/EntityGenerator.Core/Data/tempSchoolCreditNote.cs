using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("tempSchoolCreditNote")]
    public partial class tempSchoolCreditNote
    {
        public long SchoolCreditNoteIID { get; set; }
        [StringLength(100)]
        public string CreditNoteNumber { get; set; }
        [StringLength(33)]
        [Unicode(false)]
        public string New_CreditNoteNumber { get; set; }
    }
}
