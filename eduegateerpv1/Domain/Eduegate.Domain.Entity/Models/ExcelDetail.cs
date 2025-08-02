using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class ExcelDetail
    {
        public string BatchNo { get; set; }
        public string RefCategoryColumnID { get; set; }
        public string ProductID { get; set; }
        public string Valen { get; set; }
        public string Valar { get; set; }
    }
}
