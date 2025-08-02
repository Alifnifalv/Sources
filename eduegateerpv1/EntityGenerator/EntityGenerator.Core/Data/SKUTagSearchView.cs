using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SKUTagSearchView
    {
        public long ProductSKUTagIID { get; set; }
        [StringLength(50)]
        public string TagName { get; set; }
    }
}
