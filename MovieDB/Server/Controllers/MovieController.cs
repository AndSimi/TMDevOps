using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDB.Shared;

namespace MovieDB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly DataContext dataContext;

        public MovieController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<Movie>>> Get()
        {
            return Ok(await dataContext.Movies.ToListAsync());

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Movie>> GetSingle(int id)
        {
            var movie = await dataContext.Movies.SingleOrDefaultAsync(x => x.Id == id);
            if (movie == null)
            {
                return NotFound("Sorry, no movie with such ID");
            }
            return Ok(movie);   

        }   




        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
        {
            dataContext.Movies.Add(movie);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Movies.ToListAsync());

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(Movie request, int id)
        {
            var dbMovie = await dataContext.Movies.FindAsync(id);
            if (dbMovie == null)
            {
                return NotFound("Movie not found");
            }


            dbMovie.Title = request.Title;
            dbMovie.Description = request.Description;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Movies.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> DeleteMovie(int id)
        {
            var dbMovie = await dataContext.Movies.FindAsync(id);
            if (dbMovie == null)
            {
                return BadRequest("Movie not found");

            }

            dataContext.Movies.Remove(dbMovie);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Movies.ToListAsync());

        }



    }
}
