using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RestApiWithDatabase.Exceptions;
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

        public async Task<MovieModel> Create(MovieModel movie)
        {
            await this.database.AddAsync(movie);
            return movie;
        }

        public void Delete(int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieModel> Get(int movieId)
        {
            if (movieId < 0)
                throw new UserErrorException(400, "Movie id must be a positive number!");
            
            MovieModel model = await this.database.Movies.FirstOrDefaultAsync(movie => movie.Id == movieId);

            if (model ==null)
                throw new UserErrorException(404, $"No movie with the {movieId} found!");

            return model;
        }

        public Task<MovieModel> Update(MovieModel movie)
        {
            throw new NotImplementedException();
        }
    }
}
