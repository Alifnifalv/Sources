using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.EduegateModels
{
    public  class Country
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }
    }
}
