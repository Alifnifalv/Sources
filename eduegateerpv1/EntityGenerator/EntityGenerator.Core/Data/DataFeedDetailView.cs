using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DataFeedDetailView
    {
        public long DataFeedLogDetailIID { get; set; }
        public string ErrorMessage { get; set; }
        public long? DataFeedLogIID { get; set; }
        public int? CompanyID { get; set; }
    }
}
