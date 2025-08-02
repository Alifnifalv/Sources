using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Tution_Fee_OS_20221112
    {
        public double? DocNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DocDate { get; set; }
        [StringLength(255)]
        public string EnrollNo { get; set; }
        [StringLength(255)]
        public string StudentName { get; set; }
        public double? ReferenceDocNo { get; set; }
        [StringLength(255)]
        public string Grade { get; set; }
        [StringLength(255)]
        public string Section { get; set; }
        [StringLength(255)]
        public string PaymentMode { get; set; }
        [StringLength(255)]
        public string DocumentStatus { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Jan_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Jan_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Feb_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Feb_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Mar_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Mar_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Apr_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Apr_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_May_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_May_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Jun_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Jun_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Jul_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Jul_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Aug_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Aug_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Sep_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Sep_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Oct_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Oct_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Nov_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Nov_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total_Dec_2021 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Paid_Dec_2021 { get; set; }
    }
}
