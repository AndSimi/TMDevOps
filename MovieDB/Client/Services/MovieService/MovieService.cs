using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MovieDB.Client.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;
        public List<Movie> movies { get; set; } = new List<Movie>();
        public MovieService(HttpClient http, NavigationManager navigationmanager)
        {
            this.http = http;
            this.navigationManager = navigationmanager;
        }


        public async Task GetMovies()
        {
            var result = await http.GetFromJsonAsync<List<Movie>>("api/Movie");
            if (result != null)
            {
                movies = result;
            }
        }

        public async Task<Movie> GetSingleMovie(int id)
        {
            var result = await http.GetFromJsonAsync<Movie>($"api/Movie/{id}");
            if (result != null)
                return result;
            throw new Exception("Movie with this ID does not exist");
        }

        public async Task CreateMovie(Movie movie)
        {

            var result = await http.PostAsJsonAsync("api/Movie", movie);
            var response = await result.Content.ReadFromJsonAsync<List<Movie>>();
            movies = response;
            navigationManager.NavigateTo("movies");

        }

        public async Task DeleteMovie(int id)
        {
            var result = await http.DeleteAsync($"api/Movie/{id}");
            var response = await result.Content.ReadFromJsonAsync<List<Movie>>();
            movies = response;
            navigationManager.NavigateTo("movies");
        }

        public async Task UpdateMovie(Movie movie)
        {
            var result = await http.PutAsJsonAsync($"api/Movie/{movie.Id}", movie);
            var response = await result.Content.ReadFromJsonAsync<List<Movie>>();
            movies = response;
            navigationManager.NavigateTo("movies");
        }
    }
}
