using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Streets", Schema = "mutual")]
    public partial class Street
    {
        public Street()
        {
            TransportApplications = new HashSet<TransportApplication>();
        }

        [Key]
        public short StreetID { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Streets")]
        public virtual Country Country { get; set; }
        [InverseProperty("Street")]
        public virtual ICollection<TransportApplication> TransportApplications { get; set; }
    }
}
