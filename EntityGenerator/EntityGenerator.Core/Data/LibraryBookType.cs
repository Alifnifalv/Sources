using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("LibraryBookTypes", Schema = "schedule")]
    public partial class LibraryBookType
    {
        public byte? LibraryBookTypeID { get; set; }
        [StringLength(50)]
        public string BookTypeName { get; set; }
    }
}
