using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderTypes", Schema = "inventory")]
    public partial class TenderType1
    {
        public TenderType1()
        {
            Tenders = new HashSet<Tender>();
        }

        [Key]
        public long TenderTypeID { get; set; }
        public string TendorType { get; set; }
        public string Description { get; set; }

        [InverseProperty("TenderType")]
        public virtual ICollection<Tender> Tenders { get; set; }
    }
}
