using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BusinessTypes", Schema = "mutual")]
    public partial class BusinessType
    {
        public BusinessType()
        {
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public long BusinessTypeID { get; set; }
        [Column("BusinessType")]
        public string BusinessType1 { get; set; }

        [InverseProperty("BusinessType")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
