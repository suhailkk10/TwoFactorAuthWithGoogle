﻿using Models.CustomModels;
using Models.Data;

namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly WebAppContext _context;
        private readonly IGoogleAuthenticationService _googleAuthService;
        public UserService(WebAppContext context, IGoogleAuthenticationService googleAuthService)
        {
            _context = context;
            _googleAuthService = googleAuthService;
        }

        public ResponseModel Login(string username, string password)
        {
            var result =  _context.User.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (result != null)
            {
                if (!result.IsAuthSet)
                {
                    AuthenticationModel authentication = new()
                    {
                        UserId = result.Id,
                    };
                    return new ResponseModel(true, "", authentication);
                }
                else
                {
                    AuthenticationModel authentication = new()
                    {
                        AuthKey = result.AuthKey,
                        UserId = result.Id,
                    };
                    return new ResponseModel(true, "", authentication);
                }
            }
            return new ResponseModel(false, "Invalid login details.", null);
        }

        public ResponseModel SetupAuthentication(AuthenticationModel authentication)
        {
            if (authentication == null)
                return new ResponseModel(false, "Enter the values.", null);
            UserModel? user = _context.User.FirstOrDefault(x => x.Id == authentication.UserId);
            if (user == null)
                return new ResponseModel(false, "No User exist.", null);
            bool isValid = _googleAuthService.VerifyAuthenticationCode(user.AuthKey, authentication.Code);
            if (isValid && user.IsAuthSet)
                return new ResponseModel(true, "Verified Successfully.", authentication);
            if (isValid)
            {
                user.AuthKey = user.AuthKey;
                user.IsAuthSet = true;
                _context.User.Update(user);
                _context.SaveChanges();
                return new ResponseModel(true, "Verified Successfully.", authentication);
            }
            return new ResponseModel(false, "Incorrect verification code.", null);
        }

        public ResponseModel RegisterUser(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.FullName) || string.IsNullOrEmpty(userModel.Username) || string.IsNullOrEmpty(userModel.Password))
                return new ResponseModel(false, "Fill mandatory fields.", userModel);
            bool isExist = _context.User.Any(x => x.Username == userModel.Username);
            if (isExist)
            {
                return new ResponseModel(false, "Username already exist",null);
            }
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            UserModel user = new()
            {
                FullName = userModel.FullName,
                Username = userModel.Username,
                Password = userModel.Password,
                AuthKey = key,
            };
            _context.User.Add(user);
            _context.SaveChanges();
            return new ResponseModel(true, "Registered successfully", user);
        }

        public ResponseModel GenerateQrCode(int userId)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == userId);
            if (user != null && user.IsAuthSet)
                return new ResponseModel(false, "", null);
            string imageBase64 = _googleAuthService.GenerateQrCode(user.AuthKey, user.Username);
            return new ResponseModel(true, "", imageBase64);
        }
    }
}
