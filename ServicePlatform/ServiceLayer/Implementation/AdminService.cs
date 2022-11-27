using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer;
using ServicePlatform.DataLayer.Models;
using ServicePlatform.ServiceLayer.Contract;
using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Implementation
{ 
    public class AdminService : IAdminService
    {
        private readonly ApiContext _context;
        private readonly IAccountService _accountService;

        public AdminService(ApiContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<ServiceListing> ApproveListing(int serviceLisitingId)
        {
            try
            {
                var result = await _context.ServiceListings
                    .Where(x => x.ServiceListingId == serviceLisitingId)
                    .FirstOrDefaultAsync();

                if (result is not null)
                {
                    result.ListingApprovalStatus = AdminApprovalStatus.APPROVED;
                    result.IsActive = true;
                    result.DateApproved = DateTime.UtcNow;

                    _context.ServiceListings.Update(result);

                    await _context.SaveChangesAsync();

                    return result;
                }

                return new ServiceListing() { };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceListing> CancelListing(int serviceLisitingId)
        {
            try
            {
                var result = await _context.ServiceListings
                    .Where(x => x.ServiceListingId == serviceLisitingId)
                    .FirstOrDefaultAsync();

                if (result is not null)
                {
                    result.ListingApprovalStatus = AdminApprovalStatus.CANCELED;
                    result.IsActive = true;
                    result.DateApproved = DateTime.UtcNow;

                    _context.ServiceListings.Update(result);

                    await _context.SaveChangesAsync();

                    return result;
                }

                return new ServiceListing() { };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            try
            {
                var admin = await _accountService.GetAdminById(id);

                if (admin is not null)
                {
                    _context.AccountUsers.Remove(admin);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _accountService.GetCustomerById(id);

                if (customer is not null)
                {
                    _context.AccountUsers.Remove(customer);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteVendor(int id)
        {
            try
            {
                var vendor = await _accountService.GetVendorById(id);

                if (vendor is not null)
                {
                    _context.AccountUsers.Remove(vendor);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AccountUser>> GetAllAdmin()
        {
            try
            {
                var admins = await _context.AccountUsers.ToListAsync();

                return admins;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AccountUser>> GetAllCustomer()
        {
            try
            {
                var customers = await _context.AccountUsers.ToListAsync();

                return customers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<AccountUser>> GetAllVendor()
        {
            try
            {
                var vendors = await _context.AccountUsers.ToListAsync();

                return vendors;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
