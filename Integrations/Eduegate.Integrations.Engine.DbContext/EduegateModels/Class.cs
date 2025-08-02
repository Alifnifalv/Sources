using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.EduegateModels
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }    
        public string ClassDescription { get; set; }
    }
}
