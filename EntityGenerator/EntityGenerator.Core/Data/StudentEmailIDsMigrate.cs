using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentEmailIDsMigrate", Schema = "schools")]
    public partial class StudentEmailIDsMigrate
    {
        public string P { get; set; }
        public string User_Name { get; set; }
        public string Email_ID { get; set; }
    }
}
