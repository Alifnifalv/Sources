using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.Common
{
  public  class BarChartViewModel
    {

        public BarChartViewModel()
        {
            labels = new List<BarChartLabelViewModel>() { new BarChartLabelViewModel() };
            datasets= new List<BarChartDatasetsViewModel>() { new BarChartDatasetsViewModel() };
        }
        public List<BarChartLabelViewModel> labels { get; set; }
        public List<BarChartDatasetsViewModel> datasets { get; set; }

    }

    public class BarChartLabelViewModel
    {
        public string MonthName { get; set; } 
        
    }

    public class BarChartDatasetsViewModel
    {

        public BarChartDatasetsViewModel()
        {
            data = new List<BarChartDataViewModel>() { new BarChartDataViewModel() };
        }
        public string fillColor { get; set; }
        public string strokeColor { get; set; }

        public string highlightFill { get; set; }

        public string highlightStroke { get; set; }

        public List<BarChartDataViewModel> data { get; set; }

    }

    public class BarChartDataViewModel
    {
        public decimal data { get; set; }
    

    }
}
