using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PropertySearchView
    {
        public long PropertyIID { get; set; }
        [StringLength(50)]
        public string PropertyName { get; set; }
        [StringLength(100)]
        public string PropertyDescription { get; set; }
        [StringLength(50)]
        public string DefaultValue { get; set; }
        [StringLength(50)]
        public string PropertyTypeName { get; set; }
    }
}
