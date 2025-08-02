using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Logging
{
    [DataContract]
    public class DataHistoryEntityDTO
    {
        [DataMember]
        public int DataHistoryEntityID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string DBName { get; set; }
        [DataMember]
        public string SchemaName { get; set; }
        [DataMember]
        public string LoggerTableName { get; set; }
        [DataMember]
        public string LoggerDBName { get; set; }
        [DataMember]
        public string LoggerSchemaName { get; set; }
    }
}
