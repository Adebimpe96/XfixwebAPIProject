using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Contract
{
    public interface IAccountService
    {
        Task<AccountUser> GetCustomerById(int customerId);
        Task<AccountUser> GetVendorById(int vendorId);
        Task<AccountUser> GetAdminById(int adminId);

        Task<AccountUser> RegisterCustomer(CustomerRegistrationRequestDto customer);
        Task<AccountUser> RegisterVendor(VendorRegistrationRequestDto vendor);
        Task<AccountUser> RegisterAdmin(AdminRegistrationRequestDto admin);

        Task<bool> VendorUpdateAccount(VendorUpdateAccountRequestDto vendor, int id);
        Task<bool> CustomerUpdateAccount(CustomerUpdateAccountRequestDto customer, int id);

        Task<bool> Login(LoginRequestDto model);
    }
}
