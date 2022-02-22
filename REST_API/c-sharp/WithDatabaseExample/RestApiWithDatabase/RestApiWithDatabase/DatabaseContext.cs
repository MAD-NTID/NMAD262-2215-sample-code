using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using RestApiWithDatabase.Models;

namespace RestApiWithDatabase
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            :base(options)
        {

        }

        public DbSet<MovieModel> Movies { get; set; }
    }
}
