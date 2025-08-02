using Eduegate.Framework.Services;

namespace Eduegate.Services.ExternalServices
{
    //TODO: Not used
    //public class ServiceProviderAPIServices : BaseService, IServiceProviderAPIServices
    //{
    //    public string GetTracking(string referenceID)
    //    {
    //        try
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Info("Service Result : " + referenceID);
    //            return new ServiceProviderAPIBL(CallContext).GetTracking(referenceID);
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public string AddShipment(ServiceProviderShipmentDetailDTO dto)
    //    {
    //        try
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Info("Service Result : " + dto.ToString());
    //            return new ServiceProviderAPIBL(CallContext).AddShipment(dto);
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }   
    //    }

    //    public byte[] GenerateAWBPDF(string referenceID)
    //    {
    //        try
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Info("Service Result : " + referenceID);
    //            return new ServiceProviderAPIBL(CallContext).GenerateAWBPDF(referenceID);
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<MutualService>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public List<KeyValueDTO> GetSMSACities()
    //    {
    //        return new ServiceProviderAPIBL(CallContext).GetSMSACities();
    //    }
    //}
}
