using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalesRelationshipType", Schema = "catalog")]
    public partial class SalesRelationshipType
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public byte SalesRelationTypeID { get; set; }
        [StringLength(50)]
        public string RelationName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("SalesRelationshipTypes")]
        public virtual Culture Culture { get; set; }
    }
}
