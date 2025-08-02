namespace Eduegate.ERP.Admin.Areas.Reports.Models
{
    public class ReportRequestModel
    {
        public ReportRequestModel()
        {
            IsPrint = false;
        }
        public string ReportName { get; set; }

        public string Parameters { get; set; }

        public string Format { get; set; }

        public bool? IsPrint { get; set; }
    }
}