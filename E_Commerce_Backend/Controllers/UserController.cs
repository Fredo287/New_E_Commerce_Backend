using E_Commerce_Backend.Requests;
using E_Commerce_Backend.Services.ProductCategoryService;
using E_Commerce_Backend.Services.ProductService;
using E_Commerce_Backend.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userService;

        public UserController(IUserRepository repository)
        {
            _userService = repository;
        }


        [HttpPost("register")]
        public IActionResult Register(UserRequest request)
        {
            var Response = _userService.UserRegistration(request);

            if (Response.State == false)
            {
                return BadRequest(Response);
            }

            return Ok(Response);
        }


        [HttpPost("login")]
        public IActionResult Login(UserRequest request)
        {

            var response = _userService.UserLogIn(request);

            if (response.State == false)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}
