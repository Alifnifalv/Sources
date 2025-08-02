namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Trantail_Narration")]
    public partial class Trantail_Narration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TL_Narration_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_TH_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_SlNo { get; set; }

        [StringLength(2000)]
        public string Narration { get; set; }

        [StringLength(2000)]
        public string Narration1 { get; set; }
    }
}
