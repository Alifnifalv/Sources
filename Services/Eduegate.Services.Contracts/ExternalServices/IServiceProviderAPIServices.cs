using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.ExternalServices
{
    public interface IServiceProviderAPIServices
    {
        string GetTracking(string referenceID);

        string AddShipment(ServiceProviderShipmentDetailDTO dto);

        byte[] GenerateAWBPDF(string referenceID);

        List<KeyValueDTO> GetSMSACities();
    }
}