using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    public interface IECommerceService
    {
        ShareHolderDTO GetShareHolderInfo(string emiratesID);

        void SaveCustomerCard(CustomerCardDTO card);
    }
}