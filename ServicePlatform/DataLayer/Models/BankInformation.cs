
namespace ServicePlatform.DataLayer.Models
{
    public class BankInformation
    {
        public int BankInformationId { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }

        public int VendorId { get; set; }
        public AccountUser Vendor { get; set; }
    }
}
