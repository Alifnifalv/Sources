using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CommunicationTypes", Schema = "crm")]
    public partial class CommunicationType
    {
        public CommunicationType()
        {
            Communications = new HashSet<Communication>();
        }

        [Key]
        public byte CommunicationTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("CommunicationType")]
        public virtual ICollection<Communication> Communications { get; set; }
    }
}
