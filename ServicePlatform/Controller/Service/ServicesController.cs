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

namespace ServicePlatform.Controller.Service
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServicesController : ControllerBase
    {
        private readonly IBookService _repo;

        public ServicesController(IBookService repo)
        {
            _repo = repo;
        }

        [HttpPut]
        [Route("vendorCancelService/{serviceId}")]
        public async Task<IActionResult> VendorCancelService(int serviceId)
        {
            try
            {
                var result = await _repo.VendorCancelService(serviceId);

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("vendorApproveService/{serviceId}")]
        public async Task<IActionResult> VendorApproveService(int serviceId)
        {
            try
            {
                var result = await _repo.VendorApproveService(serviceId);

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                var result = await _repo.GetAllServices();

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllPendingServices")]
        public async Task<IActionResult> GetAllPendingServices()
        {
            try
            {
                var result = await _repo.GetAllPendingServices();

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllApprovedServices")]
        public async Task<IActionResult> GetAllApprovedServices()
        {
            try
            {
                var result = await _repo.GetAllApprovedServices();

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllCanceledServices")]
        public async Task<IActionResult> GetAllCanceledServices()
        {
            try
            {
                var result = await _repo.GetAllCanceledServices();

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CustomerBookService/{serviceListingId}/{customerId}")]
        public async Task<IActionResult> CustomerBookService(BookServiceRequestDto dto, int serviceListingId, int customerId)
        {
            try
            {
                var result = await _repo.CustomerBookService(dto, serviceListingId, customerId);

                if (result is not null)
                {
                    return Ok(result);
                    //return CreatedAtAction("GetServiceById", new { result.ServiceId}, result);
                }

                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
