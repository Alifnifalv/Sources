using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("WestbayLibraryBookMigrate27082024", Schema = "schools")]
    public partial class WestbayLibraryBookMigrate27082024
    {
        public string Book_Category { get; set; }
        public string Book_title { get; set; }
        public string Book_type { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Place_of_publication { get; set; }
        public string ISBN_Number { get; set; }
        public string Shelf_Number { get; set; }
        public string Book_condition { get; set; }
        public string Call_No { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string Acc_No { get; set; }
        public string Series { get; set; }
        public string Pages { get; set; }
        public string Year { get; set; }
        public string Edition { get; set; }
        public string Bill_No { get; set; }
        public string Book_price_Riyal { get; set; }
        public string Post_price { get; set; }
        [StringLength(50)]
        public string Active { get; set; }
        public string Language { get; set; }
    }
}
