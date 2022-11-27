using ServicePlatform.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Contract
{
    public interface IAdminService
    {
        Task<ServiceListing> ApproveListing(int serviceLisitingId);

        Task<ServiceListing> CancelListing(int serviceLisitingId);

        Task<bool> DeleteCustomer(int id);

        Task<bool> DeleteAdmin(int id);
        
        Task<bool> DeleteVendor(int id);

        Task<List<AccountUser>> GetAllVendor();
        Task<List<AccountUser>> GetAllAdmin();
        Task<List<AccountUser>> GetAllCustomer();
    }
}
