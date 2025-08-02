using Eduegate.Application.Mvc;
using Eduegate.Web.Library.Budgeting.Budget;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.AdminCore.Areas.Accounts.Controllers
{
    [Area("Accounts")]
    public class ForecastingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Forecasting(int budgetID = 0)
        {
            var vm = new ForecastingViewModel();

            vm = FillDefaultGridValues();

            if (budgetID == 0)
            {
                vm.IsEdit = false;
            }
            else
            {
                vm.BudgetID = budgetID;
                vm.IsEdit = true;
            }

            return View(vm);
        }
        public ForecastingViewModel FillDefaultGridValues()
        {
            var vm = new ForecastingViewModel();

            vm.Allocations = new List<AllocationAmountViewModel>();
            vm.BudgetingAccountDetail = new BudgetingAccountDetailViewModel();

            DateTime fromDate = new DateTime(2024, 1, 1);
            DateTime endDate = new DateTime(2024, 12, 31);

            while (fromDate <= endDate)
            {
                var title = fromDate.ToString("MMM") + " - " + fromDate.Year;
                vm.HeaderTitles.Add(title);

                vm.Allocations.Add(new AllocationAmountViewModel()
                {
                    MonthName = fromDate.ToString("MMMM"),
                    MonthID = fromDate.Month,
                    Amount = null,
                    Year = fromDate.Year,
                });

                if (fromDate.Month == 12) // If the current month is December, move to the next year
                {
                    fromDate = new DateTime(fromDate.Year + 1, 1, 1);
                }
                else
                {
                    fromDate = fromDate.AddMonths(1);
                }
            }

            return vm;
        }
    }
}
