using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.DTO.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Contract
{
    public interface IBookService
    {
        Task<Service> VendorCancelService(int serviceLisitingId);

        Task<Service> VendorApproveService(int serviceLisitingId);

        Task<List<Service>> GetAllServices();

        Task<List<Service>> GetAllPendingServices();

        Task<List<Service>> GetAllApprovedServices();

        Task<List<Service>> GetAllCanceledServices();

        Task<Service> CustomerBookService(BookServiceRequestDto dto, int serviceListingId, int customerId);
    }
}
