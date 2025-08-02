namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("communities.SocialServices")]
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

        public decimal? IncomePerMonth { get; set; }

        [StringLength(500)]
        public string Pensioner { get; set; }

        public virtual Member Member { get; set; }

        public virtual OccupationType OccupationType { get; set; }
    }
}
