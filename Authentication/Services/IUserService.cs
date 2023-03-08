using Models.CustomModels;
using Models.Data;

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
