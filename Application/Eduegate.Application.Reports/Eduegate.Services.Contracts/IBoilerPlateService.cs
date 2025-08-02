using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework.Contracts.Common.BoilerPlates;
using Eduegate.Framework.Contracts.Common.PageRender;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBoilerPlateService" in both code and config file together.
    [ServiceContract]
    public interface IBoilerPlateService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBoilerPlates")]
        BoilerPlateDataSourceDTO GetBoilerPlates(BoilerPlateInfo boilerPlateInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveBoilerPlate")]
        BoilerPlateDTO SaveBoilerPlate(BoilerPlateDTO boilerPlateDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBoilerPlate?boilerPlateID={boilerPlateID}")]
        BoilerPlateDTO GetBoilerPlate(string boilerPlateID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBoilerPlateParameters?boilerPlateID={boilerPlateID}")]
        List<BoilerPlateParameterDTO> GetBoilerPlateParameters(long boilerPlateID);
    }
}
