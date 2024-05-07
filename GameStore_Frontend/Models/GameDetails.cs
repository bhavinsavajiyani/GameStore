using System.ComponentModel.DataAnnotations;

namespace GameStore_Frontend.Models;

public class GameDetails
{
    public int ID {get; set;}
    
    [Required] [StringLength(50)]
    public required string Name {get; set;}
    
    [Required(ErrorMessage = "The Genre field is required.")]
    public string? GenreID {get; set;}
    
    [Range(0, 100)]
    public decimal Price {get; set;}
    
    public DateOnly ReleaseDate {get; set;}
}
