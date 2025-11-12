using App.Domain.Core.UserAgg.Contracts.AppServices;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using App.Infra.Data.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.UserAgg
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public bool Delete(int id, DeleteUserDto model)
        {
            return _userService.Delete(id, model);  
        }

        public int FindIdByUserName(string userName)
        {
            return _userService.FindIdByUserName(userName);
        }

        public List<User> GetAll()
        {
            return _userService.GetAll();
        }

        public User? GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        public GetUserDto? Login(LoginUserDto login)
        {
            return _userService.Login(login);
        }

        public bool Register(RegisterUserDto register)
        {
            if (!_userService.IsUserExist(register.UserName))
            {
                return _userService.Register(register);
            }
            return false;
        }

        public bool Update(int id, UpdateUserDto model)
        {
            var user = GetUserById(id);

            var pass = string.IsNullOrEmpty(model.Password) ? user.Password : model.Password.ToMd5Hex();
            model.Password = pass;
            return _userService.Update(id, model);
        }
    }
}
