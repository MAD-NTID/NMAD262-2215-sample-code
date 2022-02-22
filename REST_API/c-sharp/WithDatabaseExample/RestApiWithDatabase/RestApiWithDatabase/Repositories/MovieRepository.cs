using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RestApiWithDatabase.Models;

namespace RestApiWithDatabase.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DatabaseContext database;
        public MovieRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public async Task<IEnumerable<MovieModel>> All()
        {
            return await this.database.Movies.ToListAsync();
        }

        public Task<MovieModel> Create(MovieModel movie)
        {
            throw new NotImplementedException();
        }

        public void Delete(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieModel> Get(int movieId)
        {
            return await this.database.Movies.FirstOrDefaultAsync(movie => movie.Id == movieId);
        }

        public Task<MovieModel> Update(MovieModel movie)
        {
            throw new NotImplementedException();
        }
    }
}
