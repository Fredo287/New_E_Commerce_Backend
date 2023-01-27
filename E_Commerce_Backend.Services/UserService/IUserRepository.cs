using E_Commerce_Backend.ErrorHandler;
using E_Commerce_Backend.Models;
using E_Commerce_Backend.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Backend.Services.UserService
{
    public interface IUserRepository
    {
        public List<User> AllUsers();
        public User GetSingleUser(UserRequest request);
        public UserErrorHandler UserRegistration(UserRequest request);
        public UserErrorHandler UserLogIn(UserRequest request);
    }
}
