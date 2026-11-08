using cityInfoApi.Data;
using cityInfoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cityInfoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly CityInfoContext _context;

    public CitiesController(CityInfoContext context)
    {
        _context = context;
    }

    // GET: api/cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CityDetailsDto>>> GetCities()
    {
        var cities = await _context.Cities
            .Select(city => new CityDetailsDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                PointsOfInterest = city.PointsOfInterest
                    .Select(point => new PointOfInterestDto
                    {
                        Id = point.Id,
                        Name = point.Name,
                        Description = point.Description
                    }).ToList()
            })
            .ToListAsync();

        return Ok(cities);
    }

    // GET: api/cities/1
    [HttpGet("{id}")]
    public async Task<ActionResult<CityDetailsDto>> GetCity(int id)
    {
        var city = await _context.Cities
            .Where(c => c.Id == id)
            .Select(city => new CityDetailsDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                PointsOfInterest = city.PointsOfInterest
                    .Select(point => new PointOfInterestDto
                    {
                        Id = point.Id,
                        Name = point.Name,
                        Description = point.Description
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (city == null)
        {
            return NotFound();
        }

        return Ok(city);
    }

    // POST: api/cities
    [HttpPost]
public async Task<ActionResult<CityDetailsDto>> CreateCity(City city)    {
        city.Id = 0;

        // Do not allow clients to attach existing points of interest
        // while creating a city through this endpoint.
        city.PointsOfInterest = new List<PointOfInterest>();

        _context.Cities.Add(city);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCity),
            new { id = city.Id },
            new CityDetailsDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            });
    }
}