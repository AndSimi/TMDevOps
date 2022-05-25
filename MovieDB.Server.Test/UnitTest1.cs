using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDB.Server.Controllers;
using MovieDB.Server.Data;
using MovieDB.Shared;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MovieDB.Server.Test
{
    public class UnitTest1
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Movies.CountAsync() <= 0)
            {

                //adding dummy data
                databaseContext.Movies.Add(new Movie() { Id = 2, Title = "Spider Turtles", Description = "Movie" });
                databaseContext.Movies.Add(new Movie() { Id = 3, Title = "Spider Man", Description = "Movie about the Spider Man" });
                databaseContext.Movies.Add(new Movie() { Id = 4, Title = "Iron Man", Description = "Iron man movie" });
                databaseContext.Movies.Add(new Movie() { Id = 5, Title = "Testing Turtle", Description = "Movie testing" });
                databaseContext.Movies.Add(new Movie() { Id = 6, Title = "Amazing Movie", Description = "Awesome Movie" });

                await databaseContext.SaveChangesAsync();

            }

            return databaseContext;
        }




        [Fact]
        public async void GetMoviesTest()
        {
            //Should return a list of movies
            //Arrange

            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);

            //Act
            var result = movieController.Get();


            //Assert

            result.Should().NotBeNull();
            result.Should().NotBeOfType<User>();
            

        }



        [Fact]

        public async void GetSingleMovieTest()
        {
            //Get Movie with id:4
            //Arrange
            var movieID = 4;
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);


            //Act
            var result = await movieController.GetSingle(movieID);
            var okResult = result.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(okResult);
           


        }

        [Fact]

        public async void SingleMovieNotFoundTest()
        {

            //Arrange
            var movieID = 11;
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);


            //Act
            var result = await movieController.GetSingle(movieID);
            var okResult = result.Result as NotFoundObjectResult;

            //Assert
            Assert.IsType<NotFoundObjectResult>(okResult);

        }

        [Fact]
        public async void AddMovieTest()
        {
            //Arrange
            Movie newMovie = new Movie { Id = 11, Description = "Awesome movie 2", Title = "Awesome movie2" };
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);

            //Act
            var  result = await movieController.AddMovie(newMovie);
            var okResult = result.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(okResult);

        }


        [Fact]

        public async void UpdateMovieTest()
        {
            //Arrange
            Movie newMovie = new Movie {Description = "Awesome movie 2", Title = "Awesome movie2" };
            var movieID = 4;
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);

            //Act
            var result = await movieController.UpdateMovie(newMovie, movieID);
            var okResult = result.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(okResult);

        }



        [Fact]

        public async void UpdateMovieNotFoundTest()
        {
            //Arrange
            Movie newMovie = new Movie { Description = "Awesome movie 2", Title = "Awesome movie2" };
            var movieID = 14;
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);

            //Act
            var result = await movieController.UpdateMovie(newMovie, movieID);
            var okResult = result.Result as NotFoundObjectResult;

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<NotFoundObjectResult>(okResult);

        }


        [Fact]

        public async void DeleteMovieTest()
        {
            //Arrange
           
            var movieID = 4;
            var dbContext = await GetDatabaseContext();
            var movieController = new MovieController(dbContext);

            //Act
            var result = await movieController.DeleteMovie(movieID);
            var okResult = result.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<OkObjectResult>(okResult);

        }

    }
}