using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models.Dto;

public class MovieDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public int? Rating { get; set; }
}