using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryHomePage
    {
        [Key]
        public int RefCategoryID { get; set; }
    }
}
