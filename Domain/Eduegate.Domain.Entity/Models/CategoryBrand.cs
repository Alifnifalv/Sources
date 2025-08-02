using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{

    [Keyless]
    public partial class CategoryBrand
    {
        public string KeyCode { get; set; }
        public string KeyType { get; set; }
        public Nullable<byte> CategoryLevel { get; set; }
    }
}
