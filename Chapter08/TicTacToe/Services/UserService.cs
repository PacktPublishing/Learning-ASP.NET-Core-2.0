using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public class UserService : IUserService
    {
        private DbContextOptions<GameDbContext> _dbContextOptions;
        public UserService(DbContextOptions<GameDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task<bool> RegisterUser(UserModel userModel)
        {
            using (var db = new GameDbContext(_dbContextOptions))
            {
                db.UserModels.Add(userModel);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (var db = new GameDbContext(_dbContextOptions))
            {
                return await db.UserModels.FirstOrDefaultAsync(x => x.Email == email);
            }
        }

        public async Task UpdateUser(UserModel userModel)
        {
            using (var gameDbContext = new GameDbContext(_dbContextOptions))
            {
                gameDbContext.Update(userModel);
                await gameDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<UserModel>> GetTopUsers(int numberOfUsers)
        {
            using (var gameDbContext = new GameDbContext(_dbContextOptions))
            {
                return await gameDbContext.UserModels.OrderByDescending(x => x.Score).ToListAsync();
            }
        }

        public async Task<bool> IsUserExisting(string email)
        {
            using (var gameDbContext = new GameDbContext(_dbContextOptions))
            {
                return await gameDbContext.UserModels.AnyAsync(user => user.Email == email);
            }
        }
    }
}
