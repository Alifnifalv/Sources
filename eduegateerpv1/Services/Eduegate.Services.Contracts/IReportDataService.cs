namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportDataService" in both code and config file together.
    public interface IReportDataService
    {
        byte[] GetReport(ReportDTO reportContract);
    }
}