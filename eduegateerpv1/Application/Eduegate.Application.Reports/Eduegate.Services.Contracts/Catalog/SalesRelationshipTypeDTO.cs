using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public partial class SalesRelationshipTypeDTO
    {
        [DataMember]
        public byte SalesRelationTypeID { get; set; }
        [DataMember]
        public string RelationName { get; set; }

        [DataMember]
        public List<ProductMapDTO> ToProducts { get; set; }
    }


    public static class SalesRelationshipTypeMapper
    {
        public static SalesRelationshipTypeDTO ToSalesRelationshipTypeDTOMap(SalesRelationshipType obj)
        {
            return new SalesRelationshipTypeDTO()
            {
                SalesRelationTypeID = obj.SalesRelationTypeID,
                RelationName = obj.RelationName,
            };
        }
    }

}
