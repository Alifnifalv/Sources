namespace Eduegate.Vendor.PortalCore.Models
{
    public class ErrorViewModel
    { 
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
