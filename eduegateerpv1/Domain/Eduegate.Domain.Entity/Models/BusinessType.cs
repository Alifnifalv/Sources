using Eduegate.Domain.Entity.Models.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BusinessTypes", Schema = "mutual")]
    public partial class BusinessTypes
    {
        public BusinessTypes() 
        {
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public long BusinessTypeID { get; set; }
        public string BusinessType { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
