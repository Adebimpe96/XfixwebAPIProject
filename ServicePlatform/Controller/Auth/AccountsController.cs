using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicePlatform.DataLayer;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.DTO.ResponseDto;
using ServicePlatform.ServiceLayer.Contract;
using ServicePlatform.Utility.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicePlatform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _repo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        
        

        public AccountsController(IAccountService repo, 
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _repo = repo;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue; 
        }

        [HttpPost]
        [Route("registerCustomer")]
        [Produces(typeof(CustomerRegistrationRequestDto))]
        public async Task<IActionResult> RegisterCustomer(CustomerRegistrationRequestDto customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(customer.Email);

                    if (existingUser != null)
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = new List<string>
                             {
                                "Email Already in use.",
                            },
                            IsSuccess = false,
                        });
                    }

                    var newUser = new IdentityUser()
                    {
                        Email = customer.Email,
                        UserName = customer.Email
                    };

                    var IsCreated = await _userManager.CreateAsync(newUser, customer.Password);

                    if (IsCreated.Succeeded)
                    {
                        var result = await _repo.RegisterCustomer(customer);

                        if (result is not null)
                        {
                            var jwtToken = GenerateJwtToken(newUser);

                            return Ok(new CustomerRegistrationResponseDto()
                            {
                                IsSuccess = true,
                                Token = jwtToken
                            });
                        }
                        else
                        {
                            return BadRequest(new CustomerRegistrationResponseDto()
                            {
                                Errors = new List<string>
                                {
                                    "Invalid Payload",
                                },
                                IsSuccess = false,
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = IsCreated.Errors.Select(x => x.Description).ToList(),
                            IsSuccess = false,
                        });
                    }
                }

                return BadRequest(new CustomerRegistrationResponseDto()
                {
                    Errors = new List<string>
                    {
                        "Invalid Payload",
                    },
                    IsSuccess = false,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        [Produces(typeof(CustomerRegistrationResponseDto))]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new CustomerRegistrationResponseDto()
                    {
                        Errors = new List<string>
                             {
                                "Invalid Payload.",
                            },
                        IsSuccess = false,
                    });
                }

                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                var isPasswordMatch = await _userManager.CheckPasswordAsync(existingUser, model.Password);

                var result = await _repo.Login(model);

                


                var getUserType = await _repo.GetUserRoleByEmail(existingUser.Email);



                if (result == true & existingUser != null & isPasswordMatch == true)
                {
                    var token = GenerateJwtToken(existingUser);

                    return Ok(new CustomerRegistrationResponseDto()
                    {
                        Token= token,     
                        IsSuccess = true,
                        UserType = getUserType
                    });
                }
                else
                {
                    return BadRequest(new CustomerRegistrationResponseDto()
                    {
                        Errors = new List<string>()
                        {
                            "Login Failed"
                        },
                        IsSuccess = false,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("registerAdmin")]
        [Produces(typeof(CustomerRegistrationResponseDto))]
        public async Task<ActionResult> RegisterAdmin(AdminRegistrationRequestDto admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(admin.Email);

                    if (existingUser is not null)
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = new List<string>
                             {
                                "Email Already in use.",
                            },
                            IsSuccess = false,
                        });
                    }

                    var newUser = new IdentityUser()
                    {
                        Email = admin.Email,
                        UserName = admin.Email
                    };

                    var IsCreated = await _userManager.CreateAsync(newUser, admin.Password);



                    if (IsCreated.Succeeded)
                    {
                        var result = await _repo.RegisterAdmin(admin);

                        if (result is not null)
                        {
                            var jwtToken = GenerateJwtToken(newUser);

                            return Ok(new CustomerRegistrationResponseDto()
                            {
                                IsSuccess = true,
                                Token = jwtToken
                            });
                        }
                        else
                        {
                            return BadRequest(new CustomerRegistrationResponseDto()
                            {
                                Errors = new List<string>
                                {
                                    "Invalid Payload",
                                },
                                IsSuccess = false,
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = IsCreated.Errors.Select(x => x.Description).ToList(),
                            IsSuccess = false,
                        });
                    }
                }

                return BadRequest(new CustomerRegistrationResponseDto()
                {
                    Errors = new List<string>
                    {
                        "Invalid Payload",
                    },
                    IsSuccess = false,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("registerVendor")]
        [Produces(typeof(CustomerRegistrationResponseDto))]
        public async Task<IActionResult> RegisterVendor(VendorRegistrationRequestDto vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(vendor.Email);

                    if (existingUser != null)
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = new List<string>
                             {
                                "Email Already in use.",
                            },
                            IsSuccess = false,
                        });
                    }

                    var newUser = new IdentityUser()
                    {
                        Email = vendor.Email,
                        UserName = vendor.Email,
                       
                    };

                    var IsCreated = await _userManager.CreateAsync(newUser, vendor.Password);

                    if (IsCreated.Succeeded)
                    {
                        var result = await _repo.RegisterVendor(vendor);

                        if (result is not null)
                        {
                            var jwtToken = GenerateJwtToken(newUser);

                            return Ok(new CustomerRegistrationResponseDto()
                            {
                                IsSuccess = true,
                                Token = jwtToken
                            });
                        }
                        else
                        {
                            return BadRequest(new CustomerRegistrationResponseDto()
                            {
                                Errors = new List<string>
                                {
                                    "Invalid Payload",
                                },
                                IsSuccess = false,
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new CustomerRegistrationResponseDto()
                        {
                            Errors = IsCreated.Errors.Select(x => x.Description).ToList(),
                            IsSuccess = false,
                        });
                    }
                }

                return BadRequest(new CustomerRegistrationResponseDto()
                {
                    Errors = new List<string>
                    {
                        "Invalid Payload",
                    },
                    IsSuccess = false,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getCustomerById/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var result = await _repo.GetCustomerById(id);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("Customer does not exist");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("updateCustomer/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateCustomer(CustomerUpdateAccountRequestDto customer, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _repo.CustomerUpdateAccount(customer, id);

                if (result is true)
                {
                    return NoContent();
                }

                return BadRequest("Update failed");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("updateVendor/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateVendor(VendorUpdateAccountRequestDto vendor, string email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var vendorProfile = await _repo.GetVendorById(email);

                if (vendorProfile != null)
                {
                    vendorProfile.MiddleName = vendor.MiddleName;
                    vendorProfile.Address = vendor.Address;
                    vendorProfile.PhoneNumber = vendor.PhoneNumber;
                    vendorProfile.BusinessName = vendor.BusinessName;
                    vendorProfile.BusinessDescription = vendor.BusinessDescription;
                    vendorProfile.Expertise = vendor.Expertise;



                }

                var result = await _repo.VendorUpdateAccount(vendor, email);

                if (result is true)
                {
                    return NoContent();
                }

                return BadRequest("Update failed");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getVendorById/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetVendorById(int id)
        {
            try
            {
                var result = await _repo.GetVendorById(id);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("Vendor does not exist");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getAdminById/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAdminById(int id)
        {
            try
            {
                var result = await _repo.GetAdminById(id);

                if (result is not null)
                {
                    return Ok(result);
                }

                return NotFound("Admin does not exist");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
