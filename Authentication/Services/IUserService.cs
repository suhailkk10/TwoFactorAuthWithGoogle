using Models.CustomModels;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        ResponseModel Login(string username, string password);
        ResponseModel SetupAuthentication(AuthenticationModel authentication);
        ResponseModel RegisterUser(UserModel userModel);
        ResponseModel GenerateQrCode(int userId);
    }
}
