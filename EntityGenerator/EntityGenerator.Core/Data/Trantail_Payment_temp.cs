using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Trantail_Payment_temp
    {
        public int TL_Payment_ID { get; set; }
        public int Ref_TH_ID { get; set; }
        public int Ref_SlNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Cheque_No { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Cheque_No_Altered { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Cheque_Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Cheque_Date_Altered { get; set; }
        public bool? Cheque_Is_Cleared { get; set; }
        public int? Cheque_Cleared_Ref_TH_ID { get; set; }
        public int? Cheque_Cleared_Ref_SlNo { get; set; }
        public int? Card_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Card_No { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Card_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Card_ExpiryDate { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string Reference_No { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedDate { get; set; }
        public int? TenderTypeID { get; set; }
    }
}
