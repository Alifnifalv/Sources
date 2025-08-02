using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.ViewModels
{
    public class ProductTypeViewModel
    {
        public long? ProductTypeID { get; set; }
       
        public string ProductTypeName { get; set; }
    }
}