using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDesigner" in both code and config file together.
    [ServiceContract]
    public interface IDesigner
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDesignerDetail?designerIID={designerIID}")]
        DesignerOverviewDTO GetDesignerDetail(int designerIID);
    }
}
