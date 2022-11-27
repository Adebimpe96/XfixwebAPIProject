using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Contract
{
    public interface IServiceListingService
    {
        Task<List<ServiceListing>> GetAllListings();

        Task<ServiceListing> GetByServiceById(int serviceListingId);

        Task<ServiceListing> CreateServiceListing(CreateServiceListingRequestDto model, int serviceListingId);
        
        Task<List<ServiceListing>> FilterApprovedListings(string? category, string? location);
    }
}
