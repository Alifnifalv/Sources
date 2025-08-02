using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("FilterQueries_27032025", Schema = "setting")]
    public partial class FilterQueries_27032025
    {
        public int FilterQueriesID { get; set; }
        public string FilterQuery { get; set; }
    }
}
