using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Eduegate.Services.Inspectors
{
    public class EnableCrossOriginResourceSharingBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var requiredHeaders = new Dictionary<string, string>
            {
                { "Access-Control-Allow-Origin", "*" },
                { "Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS" },
                { "Access-Control-Allow-Headers", "X-Requested-With,Content-Type" }
            };

            //TODO: Uncomment this if you want to use this behavior in the config file
            //endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomHeaderMessageInspector(requiredHeaders));
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        //TODO: Uncomment this if you want to use this behavior in the config file
        //public override Type BehaviorType
        //{
        //    get { return typeof(EnableCrossOriginResourceSharingBehavior); }
        //}

        //protected override object CreateBehavior()
        //{
        //    return new EnableCrossOriginResourceSharingBehavior();
        //}

    }
}