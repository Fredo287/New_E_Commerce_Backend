using E_Commerce_Backend.DataAccess;
using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce_Backend.Services.UserService
{
    public class UserService : IUserRepository
    {
        private readonly DataContext _context = new DataContext();
        private UserErrorHandler response;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration= configuration;
        }



        // Creating a JWT token for Admin
        private string CreateAdminToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Key:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        // Creating a JWT token for Customer
        private string CreateUserToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Customer"),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Key:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



        //private static string CreateToken(string email, string password)
        //{
        //    var Secret = email + password;

        //    return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(Secret)));
        //}


        public List<User> AllUsers()
        {
            return _context.Users.ToList();
        }
        
        public User GetSingleUser(UserRequest request)
        {
            var user = _context.Users.Where(x => x.Email == request.Email).FirstOrDefault();

            return user;
        }

        
        public UserErrorHandler UserLogIn(UserRequest request)
        {
            //var AdminUser = _context.Users.Where(user => user.Email == request.Email && user.IsAdmin == true).FirstOrDefault();
            //var AdminToken = "";
            //var Token = CreateToken(request.Email, request.Password);

            //var user = _context.Users.Where(user => user.Token == Token).FirstOrDefault();

            //if (user == null)
            //{
            //    response = SetResponse(false, "User was not logged in!", "User", user);
            //    return response;
            //}


            //else if (user.Token == AdminToken)
            //{
            //    response = SetResponse(true, "Admin was logged in successfully!", "Admin", user);
            //    return response;
            //}

            //response = SetResponse(true, "Customer was logged in successfully!", "Customer", user);


            var User = _context.Users.Where(u => u.Email == request.Email && u.Password == request.Password).FirstOrDefault();
            var AdminUser = _context.Users.Where(u => u.Email == request.Email && u.Password == request.Password && u.IsAdmin == true).FirstOrDefault();
            var Customer = _context.Users.Where(u => u.Email == request.Email && u.Password == request.Password && u.IsAdmin == false).FirstOrDefault();

            if (User == null)
            {
                response = SetResponse(false, "User was not logged in!", "User", User);
                return response;
            }
            else if (Customer == null)
            {
                string adminToken = CreateAdminToken(AdminUser);
                var Admin = GetSingleUser(request);

                Admin.Token = adminToken;

                _context.Users.Update(Admin);

                _context.SaveChangesAsync();
                response = SetResponse(true, "Admin logged in successfully!", "Admin", AdminUser);
                return response;
            }

            string userToken = CreateUserToken(Customer);

            var customer = GetSingleUser(request);

            customer.Token = userToken;
            _context.Users.Update(customer);

            _context.SaveChangesAsync();
            response = SetResponse(true, "Customer logged in successfully!", "Customer", Customer);
            return response;

        }


        //public UserErrorHandler UserRegistration(UserRequest request)
        //{
        //    if (_context.Users.Any(user => user.Email == request.Email))
        //    {
        //        response = SetResponse(false, "Use another email address!", "Customer", null);
        //        return response;
        //    }

        //    try
        //    {
        //        var NewUser = new User
        //        {
        //            Email = request.Email,
        //            Password = request.Password,
        //            IsAdmin = false,
        //            Token = CreateToken(request.Email, request.Password)
        //        };

        //        _context.Users.Add(NewUser);
        //        _context.SaveChangesAsync();

        //        response = SetResponse(true, "User registration successful!", "Customer", NewUser);
        //    }
        //    catch (Exception ex)
        //    {
        //        response = SetResponse(false, "User could not be registered to the system. Try again later!", "Customer", null);
        //    }
        //    return response;
        //}




        // This method is not meant for registering admin users
        public UserErrorHandler UserRegistration(UserRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                response = SetResponse(false, "User already exists", "Customer", null);
                return response;
            }

            try
            {
                var user = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    IsAdmin = false,
                    Token = string.Empty,
                };

                _context.Users.Add(user);
                _context.SaveChangesAsync();

                response = SetResponse(true, "User added successfully!", "Customer", user);
            }
            catch (Exception ex)
            {
                response = SetResponse(false, "Something went wrong! Try again later.", "Customer", null);
            }

            return response;
        }


        private UserErrorHandler SetResponse(bool state, string message, string user, User obj)
        {
            UserErrorHandler response = new UserErrorHandler
            {
                State = state,
                User = user,
                Message = message,
                Object = obj
            };

            return response;
        }
    }
}
