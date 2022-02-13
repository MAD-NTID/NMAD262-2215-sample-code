using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MovieRestAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        
        //create a list of movie... for our purpose, this will mimic a database/collections
        private static int nextAvailableId = 102;
        private static List<Movie> movies = new List<Movie>()
        {
            new Movie {Id = 100, Title = "Star War", Rating = 10.0},
            new Movie {Id = 101, Title = "Iron Man", Rating = 20.0},
            
        };
        
        //this is the end point for when the user visit api/movies/
        //will return all movies in the collection
        [HttpGet]
        public List<Movie> Movies()
        {
            return movies;
        }
        
        //the user request the info of a specific movie based on the id
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            //go through the list and search for the movie based on the specific id and return it
            foreach (Movie movie in movies)
            {
                if (movie.Id == id)
                    return movie;
            }

            return NotFound($"No movie found for the movie id: {id}");
        }
        
        //add a new movie
        [HttpPost("add")]
        public ActionResult<Movie> AddNewMovie(Movie movie)
        {
            if (movie == null || string.IsNullOrEmpty(movie.Title))
                return Problem("Movie info cannot be null! Please supply the movie title and rating in the body!");

            movie.Id = nextAvailableId;
            movies.Add(movie);
            return movie;
        }
        
        
    }
}