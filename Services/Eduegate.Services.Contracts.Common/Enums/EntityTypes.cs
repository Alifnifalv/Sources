using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums
{
    // [mutual].[EntityTypes]
    [DataContract]
    public enum EntityTypes
    {
        [EnumMember]
        Supplier = 1,
        [EnumMember]
        Customer = 2,
        [EnumMember]
        Employee = 3,
        [EnumMember]
        Product = 4,
        [EnumMember]
        Transaction = 5,
        [EnumMember]
        Job = 6,
        [EnumMember]
        Ticket = 7,
        [EnumMember]
        DocumentType = 8,
        [EnumMember]
        Assets = 9,
        [EnumMember]
        OfflineCustomer = 10,
        [EnumMember]
        Others = 11,
        [EnumMember]
        DocumentFiles = 12,
        [EnumMember]
        Comments = 13,
        [EnumMember]
        Alerts = 14,
        [EnumMember]
        Journal = 15,

        [EnumMember]
        Student = 100,
        [EnumMember]
        StudentApplication = 101,
    }
}
