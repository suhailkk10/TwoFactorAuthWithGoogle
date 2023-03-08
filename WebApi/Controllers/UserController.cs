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
        private readonly ICaptchaServie _captchaServie;
        public UserController(IUserService userService, ICaptchaServie captchaServie)
        {
            _userService = userService;
            _captchaServie = captchaServie;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password) || string.IsNullOrEmpty(loginModel.CaptchaCode))
            {
                return Ok(new ResponseModel(false, "Fill Madatory feilds.", null));
                //return StatusCode(StatusCodes.Status400BadRequest,
                //    "Fill Madatory feilds.");
            }
            string systemCaptcha = new EncryptAndDecrypt().Decrypt(loginModel.SystemCaptcha);
            if (systemCaptcha != loginModel.CaptchaCode)
            {
                return Ok(new ResponseModel(false, "Incorrect Captcha.", null));
            }
            var response = _userService.Login(loginModel.Username, loginModel.Password);
            
            if (response != null)
            {
                //var auth = response.Data as AuthenticationModel;
                return Ok(response);
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
            if (response != null )
            {
                var auth = response.Data as AuthenticationModel;
                return Ok(response);
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
            if (response != null)
            {
                //var auth = response.Data as AuthenticationModel;
                return Ok(response);
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

        //[HttpGet]
        //[Route("captcha")]
        //public IActionResult GetCaptcha()
        //{
        //    string code = _captchaServie.GenerateCaptchaCode();
        //    string imagestring = _captchaServie.GenerateCaptchaImage(500, 200, code);
        //    var captcha = new EncryptAndDecrypt().Encrypt(code);
        //    var captchamodel = new CaptchaModel()
        //    {
        //        ImageBase64 = "data:image/png;base64," + imagestring,
        //        code = captcha,
        //    };
        //    return Ok(captchamodel);
        //}

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("success");
        }
    }
}
