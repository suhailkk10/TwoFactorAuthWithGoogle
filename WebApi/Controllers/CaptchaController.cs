using Authentication.Services;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : Controller
    {
        private readonly ICaptchaServie _captchaServie;
        public CaptchaController(ICaptchaServie captchaServie)
        {
            _captchaServie = captchaServie;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetCaptcha()
        {
            string code = _captchaServie.GenerateCaptchaCode();
            string imagestring = _captchaServie.GenerateCaptchaImage(500, 200, code);
            var captcha = new EncryptAndDecrypt().Encrypt(code);
            var captchamodel = new CaptchaModel()
            {
                ImageBase64 = "data:image/png;base64," + imagestring,
                code = captcha,
            };
            return Ok(captchamodel);
        }
    }
}
