using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Enums.Synchronizer
{
    [DataContract(Name = "Entities")]
    public enum Entities
    {
        [Description("Product Catalog")]
        [EnumMember]
        ProductCatalog = 1
    }
}
