using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eduegate.Integrations.Engine.DbContexts.EduegateModels
{
   public class Relegion
    {
        [Key]
        public byte RelegionID { get; set; }
        
        public string RelegionName { get; set; }

    }
}
