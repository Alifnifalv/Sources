using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PropertyTypeSearchView
    {
        public byte PropertyTypeID { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
    }
}
