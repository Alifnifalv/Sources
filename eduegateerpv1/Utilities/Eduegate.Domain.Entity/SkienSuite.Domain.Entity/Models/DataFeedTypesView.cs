using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataFeedTypesView
    {
        public int DataFeedTypeID { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public string ProcessingSPName { get; set; }
        public Nullable<byte> OperationID { get; set; }
        public string OperationName { get; set; }
        public Nullable<int> NoOfColumns { get; set; }
    }
}
