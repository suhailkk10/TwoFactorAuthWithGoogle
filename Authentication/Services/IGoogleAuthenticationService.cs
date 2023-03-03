using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IGoogleAuthenticationService
    {
        string GenerateQrCode(string key, string username);
        bool VerifyAuthenticationCode(string key, string code);
    }
}
