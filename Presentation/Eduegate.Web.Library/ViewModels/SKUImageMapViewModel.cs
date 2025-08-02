using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class SKUImageMapViewModel
    {
        public string ImageName { get; set; }

        public int Sequence { get; set; }

        public string ImagePath { get; set; }

        public long ImageMapID { get; set; }

        public long SKUMapID { get; set; }

        public long ProductImageTypeID { get; set; }

        public long ProductID { get; set; }
    }
}