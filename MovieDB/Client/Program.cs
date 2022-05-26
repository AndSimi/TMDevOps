global using MovieDB.Client.Services.MovieService;
global using MovieDB.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieDB.Client;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://185.51.76.155:8091/") });
builder.Services.AddScoped<IMovieService, MovieService>();

await builder.Build().RunAsync();
