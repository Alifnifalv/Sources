using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.EduegateModels
{
    public class Section
    {
        [Key]
        public int SectionID { get; set; }

        
        public string SectionName { get; set; }

    }
}
