using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicePlatform.ServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.Controller.Assessment
{
    [Route("api/[controller]")]
    [ApiController]

    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentService _repo;

        public AssessmentsController(IAssessmentService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("getAllAssessments")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repo.GetAll();

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
        [Route("getByAssessmentById/{id}")]
        public async Task<IActionResult> GetByAssessmentById(int id)
        {
            try
            {
                var result = await _repo.GetByAssessmentById(id);

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
