using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderStatuses", Schema = "inventory")]
    public partial class TenderStatus
    {
        public TenderStatus()
        {
            Tenders = new HashSet<Tender>();
        }

        [Key]
        public long TenderStatusID { get; set; }
        public string Description { get; set; }

        [InverseProperty("TenderStatus")]
        public virtual ICollection<Tender> Tenders { get; set; }
    }
}
