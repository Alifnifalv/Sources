using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    public partial class Temp_LibraryBooks_WB
    {
        [Key]
        public long TempIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BookCategory { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BookTitle { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BookType { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Subject { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Author { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Publisher { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string PlaceOfPublication { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ISBNNumber { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string ShelfNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string BookCondition { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string CallNo { get; set; }
        [Unicode(false)]
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public int? AccNo { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Series { get; set; }
        public int? Pages { get; set; }
        public int? Year { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Edition { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string BillNo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BookPriceRiyal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PostPrice { get; set; }
        public bool? Active { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Language { get; set; }
    }
}
