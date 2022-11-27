using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.ServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.Controller.ServiceListing
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServiceListingsController : ControllerBase
    {
        private readonly IServiceListingService _repo;

        public ServiceListingsController(IServiceListingService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("getAllListings")]
        public async Task<IActionResult> GetAllListings()
        {
            try
            {
                var result = await _repo.GetAllListings();

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetByServiceById/{serviceListingId}")]
        public async Task<IActionResult> GetByServiceById(int serviceListingId)
        {
            try
            {
                var result = await _repo.GetByServiceById(serviceListingId);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("FilterApprovedListings/{category}/{location}")]
        public async Task<IActionResult> FilterApprovedListings(string? category, string? location)
        {
            try
            {
                var result = await _repo.FilterApprovedListings(category, location);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CreateServiceListing/{vendorId}")]
        public async Task<IActionResult> CreateServiceListing(CreateServiceListingRequestDto model, int vendorId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await _repo.CreateServiceListing(model, vendorId);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
