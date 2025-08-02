using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CBQ_Online_Log_20230307
    {
        [Required]
        [StringLength(50)]
        public string Merchant_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Order_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Order_Reference { get; set; }
        [Required]
        [StringLength(50)]
        public string Order_Status { get; set; }
        public DateTime Order_Date { get; set; }
        [Required]
        [StringLength(50)]
        public string Order_Amount_amount_only { get; set; }
        [Required]
        [StringLength(50)]
        public string Order_Amount_currency_only { get; set; }
        [Required]
        [StringLength(50)]
        public string Payment_Method { get; set; }
        [Required]
        [StringLength(50)]
        public string Account_Identifier { get; set; }
        [Required]
        [StringLength(50)]
        public string Account_Holder { get; set; }
        [StringLength(50)]
        public string Risk_Assessment_Result { get; set; }
        [StringLength(50)]
        public string Risk_Review_Decision_Status { get; set; }
        [Required]
        [StringLength(50)]
        public string Card_Funding_Method { get; set; }
    }
}
