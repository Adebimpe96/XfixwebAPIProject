using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer;
using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.ServiceLayer.Contract;
using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Implementation
{
    public class ServiceListingService : IServiceListingService
    {
        private readonly ApiContext _context;
        private readonly IAccountService _account;

        public ServiceListingService(ApiContext context, IAccountService account)
        {
            _context = context;
            _account = account;
        }

        public async Task<List<ServiceListing>> GetAllListings()
        {
            try
            {
                var listings = await _context.ServiceListings
                    .Where(x => x.ListingApprovalStatus == AdminApprovalStatus.APPROVED & x.IsActive == true)
                    .ToListAsync();

                return listings;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ServiceListing> GetByServiceById(int serviceListingId)
        {
            try
            {
                var listing = await _context.ServiceListings
                    .Where(x => x.ServiceListingId == serviceListingId).FirstOrDefaultAsync();

                return listing;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceListing> CreateServiceListing(CreateServiceListingRequestDto model, int vendorId)
        {
            try
            {
                var vendor = await _account.GetVendorById(vendorId);

                if (vendor == null)
                {
                    throw new Exception("Failed");
                }

                var newServiceListing = new ServiceListing();
                
                newServiceListing.VendorId = vendorId;
                newServiceListing.ListingName = model.ListingName;
                newServiceListing.Description = model.Description;
                newServiceListing.Location = model.Location;
                newServiceListing.IsActive = false;
                newServiceListing.AssessmentFee = model.AssessmentFee;
                newServiceListing.ListingApprovalStatus = AdminApprovalStatus.UNDER_REVIEW;
                newServiceListing.DateCreated = DateTime.UtcNow;

                await _context.ServiceListings.AddAsync(newServiceListing);

                await _context.SaveChangesAsync();

                return newServiceListing;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ServiceListing>> FilterApprovedListings(string? category, string? location) 
        {
            try
            {
                if (string.IsNullOrEmpty(category) & string.IsNullOrEmpty(location))
                {
                    var listings = await _context.ServiceListings
                        .Where(x => x.IsActive == true & 
                        x.ListingApprovalStatus == AdminApprovalStatus.APPROVED)
                        .ToListAsync();

                    return listings;
                }


                if (!string.IsNullOrEmpty(category))
                {
                    var listings = await _context.ServiceListings
                        .Where(x => x.Category == category & x.IsActive == true 
                        & x.ListingApprovalStatus == AdminApprovalStatus.APPROVED)
                        .ToListAsync();

                    return listings;
                }

                if (!string.IsNullOrEmpty(category) & !string.IsNullOrEmpty(location))
                {
                    var listings = await _context.ServiceListings
                        .Where(x => x.Category == category & x.Location == location & x.IsActive == true 
                        & x.ListingApprovalStatus == AdminApprovalStatus.APPROVED)
                        .ToListAsync();

                    return listings;
                }

                return new List<ServiceListing>() { };
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
