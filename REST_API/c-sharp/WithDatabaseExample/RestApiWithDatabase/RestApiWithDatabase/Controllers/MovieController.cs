using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiWithDatabase.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace RestApiWithDatabase.Controllers
{
    [Authorize]
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository repository;
        public MovieController(IMovieRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult> ListMovies()
        {
            return Ok(await this.repository.All());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            return Ok(await this.repository.Get(id));
        }

        [AllowAnonymous]
        [HttpGet("showme")]
        public async Task<ActionResult> ShowMeEveryDetails()
        {
            return Ok(await this.repository.AllDetail());
        }
    }
}
