using System;
using System.ComponentModel.DataAnnotations;

namespace cityInfoApi.Models;
// use seperate dto for creating, updating and maintaing dtos
public class PointOfInterestForCreationDto
{
    // data annotations - rules for requests e.g., required
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

}
