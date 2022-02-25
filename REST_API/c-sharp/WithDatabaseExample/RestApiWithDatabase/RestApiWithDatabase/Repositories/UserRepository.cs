using RestApiWithDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace RestApiWithDatabase.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSet<User> Users;

        public UserRepository(DatabaseContext database)
        {
            this.Users = database.Users;
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            //hash the password so we can compare against hash
            password = this.Hash(password);
            return await this.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }

        /**
         * This method takes a plaintext password and returns the hash
         * 
         */
        private string Hash(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] passwordHashedBytes = sha256.ComputeHash(passwordBytes);

            return BitConverter.ToString(passwordHashedBytes).ToLower().Replace("-", "");
        }
    }
}
