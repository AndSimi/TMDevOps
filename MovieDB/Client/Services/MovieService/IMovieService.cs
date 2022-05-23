namespace MovieDB.Client.Services.MovieService
{
    public interface IMovieService
    {
        List<Movie> movies { get; set; }
        Task GetMovies();
        Task<Movie> GetSingleMovie(int id);
       
        Task CreateMovie(Movie movie);
        Task DeleteMovie(int id);
        Task UpdateMovie(Movie movie);





    }
}
