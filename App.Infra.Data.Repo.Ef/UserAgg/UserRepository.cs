using App.Domain.Core.TaskAgg.DTOs;
using App.Domain.Core.UserAgg.Contracts.Repositories;
using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repo.Ef.UserAgg
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            return _context.Users.Where(u => u.Id == id).ExecuteDelete() > 0;
        }

        public int FindIdByUserName(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public bool IsUserExist(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public GetUserDto? Login(LoginUserDto login)
        {
            var user = _context.Users.Where(u => u.UserName.ToLower().Equals(login.UserName.ToLower()) && u.Password == login.Password).Select(u => new GetUserDto
            {
                Id = u.Id,
                UserName = login.UserName,
                Password = login.Password,
                FullName = u.FullName,
                ImagePath = u.ImagePath,
            }).FirstOrDefault();
            return user;
        }

        public bool Register(RegisterUserDto model)
        {
            var entity = new User
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Password = model.Password,
                ImagePath = model.ImagePath,
            };
            _context.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(int id, UpdateUserDto model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Password = model.Password;
                user.FullName = model.FullName;
                user.ImagePath = model.ImagePath;
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
