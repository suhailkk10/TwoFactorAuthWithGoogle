using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface ICaptchaServie
    {
        string GenerateCaptchaCode();
        string GenerateCaptchaImage(int width, int height, string captchaCode);
    }
}
