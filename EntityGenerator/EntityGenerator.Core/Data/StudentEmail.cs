using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentEmail")]
    public partial class StudentEmail
    {
        [Column("P#")]
        [StringLength(255)]
        public string P_ { get; set; }
        [Column("User Name")]
        [StringLength(255)]
        public string User_Name { get; set; }
        [Column("Email ID")]
        [StringLength(255)]
        public string Email_ID { get; set; }
    }
}
