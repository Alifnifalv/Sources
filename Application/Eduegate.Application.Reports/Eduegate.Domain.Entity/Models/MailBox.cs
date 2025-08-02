using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("schools.MailBox")]
    public partial class MailBox
    {
        public long mailBoxID { get; set; }

        public long? fromID { get; set; }

        public long? toID { get; set; }

        [StringLength(255)]
        public string mailSubject { get; set; }

        public string mailBody { get; set; }

        [StringLength(255)]
        public string mailFolder { get; set; }

        public bool? viewStatus { get; set; }

        public bool? fromDelete { get; set; }

        public bool? toDelete { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? onDate { get; set; }
    }
}
