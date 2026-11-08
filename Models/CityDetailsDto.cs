namespace cityInfoApi.Models;

public class CityDetailsDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<PointOfInterestDto> PointsOfInterest { get; set; }
        = new();
}