using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.Models
{
    public class Parent
    {
        [Key]
        public string LoginUserID { get; set; }
    }
}
