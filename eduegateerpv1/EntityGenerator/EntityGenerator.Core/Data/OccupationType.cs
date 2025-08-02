using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OccupationTypes", Schema = "communities")]
    public partial class OccupationType
    {
        public OccupationType()
        {
            SocialServices = new HashSet<SocialService>();
        }

        [Key]
        public byte OccupationTypeID { get; set; }
        [StringLength(100)]
        public string OccupationDescription { get; set; }

        [InverseProperty("OccupationType")]
        public virtual ICollection<SocialService> SocialServices { get; set; }
    }
}
