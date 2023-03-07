using Authentication.Services;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using Models.Data;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Fill Madatory feilds.");
            }
            var response = _userService.Login(loginModel.Username, loginModel.Password);
            if (response != null && response.Status == true)
            {
                var auth = response.Data as AuthenticationModel;
                return Ok(auth);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    response.Message);
            }
        }

        [HttpPost]
        [Route("SetupAuthentication")]
        public IActionResult SetupAuthentication(AuthenticationModel authenticationModel)
        {
            var response = _userService.SetupAuthentication(authenticationModel);
            if (response != null && response.Status == true)
            {
                var auth = response.Data as AuthenticationModel;
                return Ok(auth);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    response.Message);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(UserModel userModel)
        {
            var response = _userService.RegisterUser(userModel);
            if (response != null && response.Status == true)
            {
                var auth = response.Data as AuthenticationModel;
                return Ok(auth);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    response.Message);
            }
        }

        [HttpGet]
        [Route("qrcode")]
        public IActionResult Getqrcode(int userId)
        {
            var image =_userService.GenerateQrCode(userId);
            return Ok(image);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("success");
        }
    }
}
