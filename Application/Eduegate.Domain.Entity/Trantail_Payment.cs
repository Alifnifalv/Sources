namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Trantail_Payment")]
    public partial class Trantail_Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TL_Payment_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_TH_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ref_SlNo { get; set; }

        [StringLength(50)]
        public string Cheque_No { get; set; }

        [StringLength(50)]
        public string Cheque_No_Altered { get; set; }

        public DateTime? Cheque_Date { get; set; }

        public DateTime? Cheque_Date_Altered { get; set; }

        public bool? Cheque_Is_Cleared { get; set; }

        public int? Cheque_Cleared_Ref_TH_ID { get; set; }

        public int? Cheque_Cleared_Ref_SlNo { get; set; }

        public int? Card_ID { get; set; }

        [StringLength(50)]
        public string Card_No { get; set; }

        [StringLength(50)]
        public string Card_Name { get; set; }

        public DateTime? Card_ExpiryDate { get; set; }

        [StringLength(200)]
        public string Reference_No { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? TenderTypeID { get; set; }
    }
}
