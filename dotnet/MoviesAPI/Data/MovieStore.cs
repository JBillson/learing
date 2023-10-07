using MoviesAPI.Models;
using MoviesAPI.Models.Dto;

namespace MoviesAPI.Store;

public static class MovieStore
{
    public static List<Movie> Movies { get; }

    static MovieStore()
    {
        Movies = new List<Movie>
        {
            new(id: 1, name: "The Godfather", rating: 10),
            new(id: 2, name: "The Dark Knight", rating: 9)
        };
    }
}