using System.ComponentModel.DataAnnotations;

namespace Catering.API.Model;

public class Restaurant
{
    public int? Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set;}
    
    [Required]
    public int CatalogId { get; set; }
}
