using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer;
using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.DTO.ResponseDto;
using ServicePlatform.ServiceLayer.Contract;
using ServicePlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Implementation
{
    public class BookService : IBookService
    {
        private readonly ApiContext _context;

        public BookService(ApiContext context)
        {
            _context = context;
        }
        public async Task<Service> CustomerBookService(BookServiceRequestDto model, int serviceListingID, int customerId)
        {
            try
            {
                var newService = new Service();

                var result = await _context.ServiceListings
                    .Where(x => x.ServiceListingId == serviceListingID)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    newService.ServiceLisitingId = result.ServiceListingId;
                    newService.ListingName = result.ListingName;
                    newService.Location = result.Location;
                    newService.CustomerName = model.CustomerName;
                    newService.ContactNumber = model.ContactNumber;
                    newService.Address = model.Address;
                    newService.Category = result.Category;
                    newService.DateCreated = DateTime.UtcNow;
                    newService.BookServiceStatus = ServiceStatus.PENDING;
                    newService.CustomerId = customerId;

                    await _context.AddAsync(newService);

                    await _context.SaveChangesAsync();

                    return newService;
                }

                return new Service() { };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Service>> GetAllApprovedServices()
        {
            try
            {
                var result = await _context.Services
                    .Where(x => x.BookServiceStatus == ServiceStatus.APPROVED)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Service>> GetAllCanceledServices()
        {
            try
            {
                var result = await _context.Services
                    .Where(x => x.BookServiceStatus == ServiceStatus.CANCELED)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Service>> GetAllPendingServices()
        {
            try
            {
                var result = await _context.Services
                    .Where(x => x.BookServiceStatus == ServiceStatus.PENDING)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Service>> GetAllServices()
        {
            try
            {
                var result = await _context.Services.ToListAsync();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Service> VendorApproveService(int serviceId)
        {
            try
            {
                var result = await _context.Services
                    .Where(x => x.ServiceId == serviceId)
                    .FirstOrDefaultAsync();

                if (result != null & result.BookServiceStatus == ServiceStatus.PENDING)
                {
                    result.BookServiceStatus = ServiceStatus.APPROVED;


                    _context.Services.Update(result);

                    await _context.SaveChangesAsync();

                    return result;
                }
                else
                {
                    return new Service() { };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Service> VendorCancelService(int serviceId)
        {
            try
            {
                var result = await _context.Services
                    .Where(x => x.ServiceId == serviceId)
                    .FirstOrDefaultAsync();

                if (result != null & result.BookServiceStatus == ServiceStatus.PENDING)
                {
                    result.BookServiceStatus = ServiceStatus.CANCELED;
                    
                    _context.Services.Update(result);

                    await _context.SaveChangesAsync();

                    return result;
                }
                else
                {
                    return new Service() { };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
