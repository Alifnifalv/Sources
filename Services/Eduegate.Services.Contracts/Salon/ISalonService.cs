namespace Eduegate.Services.Contracts.Salon
{
    public interface ISalonService
    {
        Salon.ServiceDTO GetService(long serviceID);

        Salon.ServiceDTO SaveService(Salon.ServiceDTO service);
    }
}