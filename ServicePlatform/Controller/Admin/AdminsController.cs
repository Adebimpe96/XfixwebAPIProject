using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicePlatform.ServiceLayer.Contract;
using System;
using System.Threading.Tasks;

namespace ServicePlatform.Controller.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _repo;

        public AdminsController(IAdminService repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("approveListing/{id}")]
        public async Task<IActionResult> ApproveListing(int id)
        {
            try
            {
                var result = await _repo.ApproveListing(id);

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest("Approval failed.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("cancelListing/{id}")]
        public async Task<IActionResult> CancelListing(int id)
        {
            try
            {
                var result = await _repo.CancelListing(id);

                if (result is not null)
                {
                    return Ok(result);
                }

                return BadRequest("Cancel failed.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _repo.DeleteCustomer(id);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteAdmin/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                var result = await _repo.DeleteAdmin(id);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteVendor/{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                var result = await _repo.DeleteVendor(id);

                if (result)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var result = await _repo.GetAllCustomer();

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("No record found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllVendors")]
        public async Task<IActionResult> GetVendords()
        {
            try
            {
                var result = await _repo.GetAllVendor();

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("No record found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAllAdmins")]
        public async Task<IActionResult> GetAdmins()
        {
            try
            {
                var result = await _repo.GetAllAdmin();

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("No record found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
