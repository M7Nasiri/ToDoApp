using App.Domain.Core.Common.Contracts.Services;
using App.Domain.Core.UserAgg.Contracts.Repositories;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using App.Infra.Data.Tools;
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
            model.Password = model.Password.ToMd5Hex();
            return _userRepository.Register(model);
        }
        public bool IsUserExist(string userName)
        {
            if (_userRepository.IsUserExist(userName))
            {
                return true;
            }
            return false;
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
            return _userRepository.Update(id, model);
        }

        public int FindIdByUserName(string userName)
        {
            return _userRepository.FindIdByUserName(userName);
        }
    }
}
