using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer;
using ServicePlatform.DataLayer.Models;
using ServicePlatform.DTO.RequestDto;
using ServicePlatform.ServiceLayer.Contract;
using ServicePlatform.Utility.Enums;
using ServicePlatform.Utility.PaswordHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly ApiContext _context;

        public AccountService(ApiContext context)
        {
            _context = context;
        }

        public async Task<bool> CustomerUpdateAccount(CustomerUpdateAccountRequestDto customer, int id)
        {
            try
            {
                var result = await GetCustomerById(id);

                if (result is not null)
                {
                    result.MiddleName = customer.MiddleName;
                    result.Address = customer.Address;
                    result.State = customer.State;
                    result.Country = customer.Country;
                    result.PhoneNumber = customer.PhoneNumber;
                    result.Gender = customer.Gender;

                    result.DateUpdated = DateTime.UtcNow;
                    result.AccountStatus = AccountStatus.ACTIVE;


                    _context.AccountUsers.Update(result);

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

        public async Task<AccountUser> GetAdminById(int adminId)
        {
            try
            {
                var admin = await _context.AccountUsers
                    .Where(x => x.UserId == adminId)
                    .FirstOrDefaultAsync();

                return admin;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AccountUser> GetCustomerById(int customerId)
        {
            try
            {
                var customer = await _context.AccountUsers
                    .Where(x => x.UserId == customerId)
                    .FirstOrDefaultAsync();

                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetUserRoleByEmail (string email)
        {
            var user = await _context.AccountUsers.FirstOrDefaultAsync(m => m.Email == email);

            return ((int)user.UserType);
        }

        public async Task<AccountUser> GetVendorById(string email)
        {
            try
            {
                var vendor = await _context.AccountUsers
                    .Where(x => x.Email == email)
                    .FirstOrDefaultAsync();

                return vendor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Login(LoginRequestDto model)
        {
            try
            {
                //Create a salt,
                //Create a hash password with salt
                //Save both hash and salt
                //decrypt with password and salt... so developers cant decrypt password

                var user = await _context.AccountUsers.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                

                if (user is not null)
                { 
                    var verifyResult = PasswordProcessor.AreEqual(model.Password, user.Password, user.Salt);

                    if (verifyResult == true)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Invalid Credentials");
                    }

                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AccountUser> RegisterAdmin(AdminRegistrationRequestDto admin)
        {
            try
            {
                var newAdmin = new AccountUser();

                var existingAdmin = await _context.AccountUsers
                    .Where(x => x.Email == admin.Email)
                    .FirstOrDefaultAsync();

                if (existingAdmin is not null)
                {
                    throw new Exception("Email is already in use.");
                };

                if (admin.ConfirmPassword != admin.Password)
                {
                    throw new Exception("Password and confirm Password do not match");
                };

                //Create a salt

                var salt = PasswordProcessor.CreateSalt(20);

                //Create a hash password with salt
                var hashPass = PasswordProcessor.GenerateHash(admin.Password, salt);               

                newAdmin.FirstName = admin.FirstName;
                newAdmin.AccountStatus = AccountStatus.ACTIVE;
                newAdmin.LastName = admin.LastName;
                newAdmin.UserType = AccountType.ADMIN;
                newAdmin.DateCreated = DateTime.UtcNow;
                newAdmin.Email = admin.Email;
                newAdmin.Password = hashPass;
                newAdmin.Salt = salt;

                await _context.AccountUsers.AddAsync(newAdmin);

                await _context.SaveChangesAsync();

                return newAdmin;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AccountUser> RegisterCustomer(CustomerRegistrationRequestDto customer)
        {
            try
            {
                var newCustomer = new AccountUser();

                var existingCustomer = await _context.AccountUsers
                    .Where(x => x.Email == customer.Email)
                    .FirstOrDefaultAsync();

                if (existingCustomer is not null)
                {
                    throw new Exception("Email is already in use.");
                };


                if (customer.ConfirmPassword != customer.Password)
                {
                    throw new Exception("Password and confirm Password do not match");
                };

                //Create a salt,

                var salt = PasswordProcessor.CreateSalt(20);

                //Create a hash password with salt
                var hashPass = PasswordProcessor.GenerateHash(customer.Password, salt);

                newCustomer.AccountStatus = AccountStatus.INACTIVE;
                newCustomer.FirstName = customer.FirstName;
                newCustomer.DateCreated = DateTime.UtcNow;
                newCustomer.UserType = AccountType.CUSTOMER;
                newCustomer.LastName = customer.LastName;
                newCustomer.Email = customer.Email;
                newCustomer.Salt = salt;
                newCustomer.Password = hashPass;

                await _context.AccountUsers.AddAsync(newCustomer);

                await _context.SaveChangesAsync();

                return newCustomer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AccountUser> RegisterVendor(VendorRegistrationRequestDto vendor)
        {
            try
            {
                var newVendor = new AccountUser();

                var existingVendor = await _context.AccountUsers
                    .Where(x => x.Email == vendor.Email)
                    .FirstOrDefaultAsync();

                if (existingVendor is not null)
                {
                    throw new Exception("Email is already in use.");
                };

                if (vendor.ConfirmPassword != vendor.Password)
                {
                    throw new Exception("Password and confirm Password do not match");
                };

                //Create a salt
                var salt = PasswordProcessor.CreateSalt(20);

                //Create a hash password with salt
                var hashPass = PasswordProcessor.GenerateHash(vendor.Password, salt);

                newVendor.FirstName = vendor.FirstName;
                newVendor.AccountStatus = AccountStatus.INACTIVE;
                newVendor.UserType = AccountType.VENDOR;
                newVendor.LastName = vendor.LastName;
                newVendor.Email = vendor.Email;
                newVendor.Expertise = vendor.Expertise;
                newVendor.Salt = salt;
                newVendor.Password = hashPass;
                newVendor.DateCreated = DateTime.UtcNow;

                await _context.AccountUsers.AddAsync(newVendor);

                await _context.SaveChangesAsync();

                return newVendor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> VendorUpdateAccount(VendorUpdateAccountRequestDto vendor, int id)
        {
            try
            {
                var result = await GetVendorById(id);

                if (result is not null)
                {
                    result.MiddleName = vendor.MiddleName;
                    result.Address = vendor.Address;
                    result.State = vendor.State;
                    //result.Country = vendor.Country;
                    result.PhoneNumber = vendor.PhoneNumber;
                    result.Gender = vendor.Gender;
                    result.BusinessName = vendor.BusinessName;
                    //result.Website = vendor.Website;
                    //result.BusinessEmail = vendor.BusinessEmail;
                    result.BusinessEmail = vendor.BusinessDescription;

                    result.DateUpdated = DateTime.UtcNow;
                    result.AccountStatus = AccountStatus.ACTIVE;

                    _context.AccountUsers.Update(result);

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
    }
}
