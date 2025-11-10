using App.Domain.Core.UserAgg.Contracts.Repositories;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;

using App.Framework.Tools;
using App.Infra.Data.FileStorageService.Contracts;
namespace App.Domain.Services.UserAgg
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        public UserService(IUserRepository userRepository, IFileService fileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
        }
        public bool Register(RegisterUserDto model)
        {
            if (_userRepository.IsUserExist(model.UserName))
            {
                return false;
            }

            model.Password = model.Password.ToMd5Hex();
            return _userRepository.Register(model);
        }

        public GetUserDto? Login(LoginUserDto dto)
        {
            dto.Password = dto.Password.ToMd5Hex();
            dto.RememberMe = false;
            return _userRepository.Login(dto);
        }

        public bool Delete(int id, DeleteUserDto model)
        {
            if (model.ImagePath != null)
            {
                _fileService.Delete(model.ImagePath);
            }
            return _userRepository.Delete(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User? GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }



        public bool Update(int id, UpdateUserDto model)
        {
            var user = GetUserById(id);
            
            var pass = string.IsNullOrEmpty(model.Password) ? user.Password : model.Password.ToMd5Hex();
            model.Password = pass;
            return _userRepository.Update(id, model);
        }

        public int FindIdByUserName(string userName)
        {
            return _userRepository.FindIdByUserName(userName);
        }
    }
}
