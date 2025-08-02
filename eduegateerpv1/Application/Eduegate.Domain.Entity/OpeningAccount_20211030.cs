namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.OpeningAccount_20211030")]
    public partial class OpeningAccount_20211030
    {
        [Key]
        [Column(Order = 0)]
        public int OP_Acc_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYear_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? OP_Date { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "money")]
        public decimal OP_Balance { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "money")]
        public decimal OP_Settled { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "money")]
        public decimal CL_Balance { get; set; }

        [StringLength(100)]
        public string InvoiceNo { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? OP_SlNo { get; set; }
    }
}
