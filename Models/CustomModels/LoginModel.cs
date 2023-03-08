using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CaptchaCode { get; set; }
        public string SystemCaptcha { get; set; }
    }
}
