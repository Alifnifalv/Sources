using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductMapDTO
    {
        [DataMember]
        public long ProductToProductMapID { get; set; }
        [DataMember]
        public long ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int SalesRelationShipType { get; set; }
        [DataMember]
        public long ProductSKUMapID { get; set; }
    }


    //public static class ProductMapMapper
    //{
    //    public static ProductMapDTO ToSalesRelationshipTypeDTOMap(SalesRelationshipType obj)
    //    {
    //        return new ProductMapDTO()
    //        {
    //            SalesRelationTypeID = obj.SalesRelationTypeID,
    //            RelationName = obj.RelationName,


    //        };
    //    }
    //}
}
