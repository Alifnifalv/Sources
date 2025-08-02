using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums.Schedulers;

namespace Eduegate.Services.Contracts.Schedulers
{
     [ServiceContract]
    public interface ISchedulerService
    {
         [OperationContract]
         [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetScheduler?schedulerType={schedulerType}&entityType={entityType}&entityID={entityID}")]
         SchedulerDTO GetScheduler(SchedulerTypes schedulerType, SchedulerEntityTypes entityType, string entityID);

         [OperationContract]
         [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSchedulerByType?schedulerType={schedulerType}&entityID={entityID}")]
         List<SchedulerDTO> GetSchedulerByType(SchedulerTypes schedulerType, string entityID);

         [OperationContract]
         [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSchedulers")]
         List<SchedulerDTO> SaveSchedulers(List<SchedulerDTO> user);
    }
}
