namespace MoviesAPI.Models;

public class Movie
{
    public Movie()
    {
    }
    
    public Movie(int id, string name, int? rating)
    {
        Id = id;
        Name = name;
        Rating = rating;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int? Rating { get; set; }
}