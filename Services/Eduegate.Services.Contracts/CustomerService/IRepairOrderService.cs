using Eduegate.Services.Contracts.Search;

namespace Eduegate.Services.Contracts.CustomerService
{
    public interface IRepairOrderService
    {
        SearchResultDTO GetRepaidOrders(int currentPage, int pageSize, string orderBy);

        SearchResultDTO GetCustomers(int currentPage, int pageSize, string orderBy);

        RepairOrderDTO GetRepaidOrder(int orderID);

        RepairOrderDTO SaveRepairOrder(RepairOrderDTO orderDTO);

        SearchResultDTO GetRepairOrderSummary();

        RepairVehicleDTO GetVehcileDetails(string chasisNo, string regitrationNo);
    }
}