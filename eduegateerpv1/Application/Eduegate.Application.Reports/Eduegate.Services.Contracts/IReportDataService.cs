using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportDataService" in both code and config file together.
    [ServiceContract]
    public interface IReportDataService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetReport")]
        byte[] GetReport(ReportDTO reportContract);
    }
}
