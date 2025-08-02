using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DataFeedTypesView
    {
        public int DataFeedTypeID { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string Name { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string TemplateName { get; set; }
        [StringLength(250)]
        [Unicode(false)]
        public string ProcessingSPName { get; set; }
        public byte? OperationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OperationName { get; set; }
        public int? NoOfColumns { get; set; }
    }
}
