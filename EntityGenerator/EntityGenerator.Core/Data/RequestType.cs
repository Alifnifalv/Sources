using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RequestTypes", Schema = "crm")]
    public partial class RequestType
    {
        public RequestType()
        {
            Leads = new HashSet<Lead>();
        }

        [Key]
        public byte RequestTypeID { get; set; }
        [StringLength(50)]
        public string RequestTypeName { get; set; }

        [InverseProperty("RequestType")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}
