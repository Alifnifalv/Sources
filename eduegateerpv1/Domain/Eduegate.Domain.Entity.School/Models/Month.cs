namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Month")]
    public partial class Month
    {
        public byte MonthID { get; set; }

        [StringLength(15)]
        public string Description { get; set; }
    }
}
