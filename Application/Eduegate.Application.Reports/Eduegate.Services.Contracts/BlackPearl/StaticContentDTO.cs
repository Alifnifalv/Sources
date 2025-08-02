using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class StaticContentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ContentDataIID { get; set; }
        [DataMember]
        public StaticContentTypes ContentTypeID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ImageFilePath { get; set; }
        [DataMember]        
        public List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO> AdditionalParameters { get; set; }


    }

    //public static class StaticContentMapper
    //{
    //    public static StaticContentDTO ToStaticContentDataDTOMap(StaticContentData staticContentData)
    //    {
    //        return new StaticContentDTO()
    //        {
    //            ContentDataIID = staticContentData.ContentDataIID,
    //            ContentTypeID = staticContentData.StaticContentType,
    //            Title = staticContentData.Title,
    //            Description = staticContentData.Description,
    //            ImageFilePath = staticContentData.ImageFilePath,
    //            AdditionalParameters = JsonConvert.DeserializeObject<List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>>(staticContentData.SerializedJsonParameters),
    //            CreatedDate = staticContentData.CreatedDate,
    //            UpdatedDate = staticContentData.UpdatedDate,
    //            CreatedBy = staticContentData.CreatedBy,
    //            UpdatedBy = staticContentData.UpdatedBy,
    //            TimeStamps = Convert.ToString(staticContentData.TimeStamps)
    //        };
    //    }
    //}

}
