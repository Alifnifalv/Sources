using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("CategoriesTableImport29042024", Schema = "schools")]
    public partial class CategoriesTableImport29042024
    {
        public double Category_Code { get; set; }
        public string Categories { get; set; }
    }
}
