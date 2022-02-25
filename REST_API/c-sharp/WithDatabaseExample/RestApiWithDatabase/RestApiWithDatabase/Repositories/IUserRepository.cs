using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestApiWithDatabase.Models;

namespace RestApiWithDatabase.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
