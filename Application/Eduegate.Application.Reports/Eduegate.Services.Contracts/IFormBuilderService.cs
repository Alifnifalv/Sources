using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Forms;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFormBuilderService" in both code and config file together.
    [ServiceContract]
    public interface IFormBuilderService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveFormValues")]
        OperationResultDTO SaveFormValues(int formID, List<FormValueDTO> formValueDTOs);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetFormValuesByFormAndReferenceID?referenceID={referenceID}&formID={formID}")]
        FormValueDTO GetFormValuesByFormAndReferenceID(long? referenceID, int? formID);

    }
}