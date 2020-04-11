using System;
using System.Linq;
using Xunit;
using FluentAssertions;
using SFF.Domain.Models;
using SFF.Domain.Controllers;

namespace SFF.Domain.Tests
{
    public class MovieTests {
        [Fact]
        public void ShouldBeAbleToAddMovie() {
            // arrange 
            var fake = new FakeMovieRepository();
            var sut = new MovieController(fake);

            // act
            sut.PostMovie("Banan", 1);

            // assert
            sut.GetMovie("Banan").Should().NotBeNull();
        }

        [Theory]
        [InlineData(10, -5, 5)]
        [InlineData(1, 5, 6)]
        [InlineData(1, -5, 0)]
        public void ShouldBeAbleToChangeAvailableAmountOfMovies(int amount, int change, int expected) {
            // arrange 
            var fake = new FakeMovieRepository();
            var sut = new MovieController(fake);
            sut.PostMovie("Banan", amount);

            // act
            sut.AdjustAmountOfMovies("Banan", change);

            // assert
            sut.GetMovies("Banan").Count().Should().Be(expected);
        }
    }
}
