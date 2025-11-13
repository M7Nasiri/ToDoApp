using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;

namespace App.Domain.Core.UserAgg.Contracts.Services
{
    public interface IUserService
    {

        List<User> GetAll();
        User? GetUserById(int id);
        GetUserDto? Login(LoginUserDto login);
        bool Register(RegisterUserDto register);
        bool IsUserExist(string userName);

        bool Delete(int id, DeleteUserDto model);
        bool Update(int id, UpdateUserDto model);
        bool Update(int id, UserProfileDto model);
        int FindIdByUserName(string userName);
    }
}
