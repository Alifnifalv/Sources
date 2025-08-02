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
    public partial class ProductVideoMapDTO
    {
        [DataMember]
        public long ProductVideoMapID { get; set; }
        [DataMember]
        public Nullable<long> ProductID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public string VideoFile { get; set; }
        [DataMember]
        public Nullable<byte> Sequence { get; set; }
        [DataMember]
        public string VideoName { get; set; }
    }

    public class ProductVideoMapMapper
    {
        public static ProductVideoMap ToEntity(ProductVideoMapDTO dto)
        {
            if (dto != null)
            {
                return new ProductVideoMap {
                    ProductVideoMapIID = dto.ProductVideoMapID,
                    ProductID = dto.ProductID,
                    ProductSKUMapID = dto.ProductSKUMapID,
                    VideoFile = dto.VideoFile,
                    Sequence = dto.Sequence,
                   
                };
            }
            else
                return new ProductVideoMap();
        }

        public static ProductVideoMapDTO ToDto(ProductVideoMap entity)
        {
            if (entity != null)
            {
                return new ProductVideoMapDTO
                {
                    ProductVideoMapID = entity.ProductVideoMapIID,
                    ProductID = entity.ProductID,
                    ProductSKUMapID = entity.ProductSKUMapID,
                    VideoFile = entity.VideoFile,
                    Sequence = entity.Sequence,
                    VideoName = entity.VideoFile == null ? null : System.IO.Path.GetFileName(entity.VideoFile)
                };
            }
            else
                return new ProductVideoMapDTO();
        }
    }
}
