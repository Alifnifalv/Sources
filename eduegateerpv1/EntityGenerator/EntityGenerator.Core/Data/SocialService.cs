using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SocialServices", Schema = "communities")]
    public partial class SocialService
    {
        [Key]
        public long SocialServiceIID { get; set; }
        public long? MemberID { get; set; }
        public byte? OccupationTypeID { get; set; }
        [StringLength(500)]
        public string Domain { get; set; }
        [StringLength(500)]
        public string Designation { get; set; }
        [StringLength(500)]
        public string OccupationLocation { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? IncomePerMonth { get; set; }
        [StringLength(500)]
        public string Pensioner { get; set; }

        [ForeignKey("MemberID")]
        [InverseProperty("SocialServices")]
        public virtual Member Member { get; set; }
        [ForeignKey("OccupationTypeID")]
        [InverseProperty("SocialServices")]
        public virtual OccupationType OccupationType { get; set; }
    }
}
