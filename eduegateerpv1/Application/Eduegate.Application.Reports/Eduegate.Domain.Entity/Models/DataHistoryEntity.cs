using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DataHistoryEntity
    {
        public int DataHistoryEntityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TableName { get; set; }
        public string DBName { get; set; }
        public string SchemaName { get; set; }
        public string LoggerTableName { get; set; }
        public string LoggerDBName { get; set; }
        public string LoggerSchemaName { get; set; }
    }
}
