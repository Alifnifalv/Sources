using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LoginsProductSKUView
    {
        public long? LoginID { get; set; }
        public long? ProductSKUID { get; set; }
    }
}
