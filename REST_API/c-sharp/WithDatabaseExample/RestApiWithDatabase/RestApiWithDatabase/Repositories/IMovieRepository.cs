using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiWithDatabase.Models;

namespace RestApiWithDatabase.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieModel> Create(MovieModel movie);
        Task<MovieModel> Update(MovieModel movie);
        Task<MovieModel> Get(int movieId);
        void Delete(int movieId);
        Task<IEnumerable<MovieModel>> All();
        Task<IEnumerable<MovieDetail>> AllDetail();


    }
}
