using cityInfoApi.Data;
using cityInfoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cityInfoApi.Controllers;

[ApiController]
[Route("api/cities/{cityId}/[controller]")]
public class PointsOfInterestController : ControllerBase
{
    private readonly CityInfoContext _context;

    public PointsOfInterestController(CityInfoContext context)
    {
        _context = context;
    }

    // GET: api/cities/1/pointsOfInterest
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>>
        GetPointsOfInterest(int cityId)
    {
        var cityExists = await _context.Cities
            .AnyAsync(c => c.Id == cityId);

        if (!cityExists)
        {
            return NotFound();
        }

        var points = await _context.PointsOfInterest
            .Where(p => p.CityId == cityId)
            .Select(p => new PointOfInterestDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .ToListAsync();

        return Ok(points);
    }

    // GET: api/cities/1/pointsOfInterest/1
    [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public async Task<ActionResult<PointOfInterestDto>>
        GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        var point = await _context.PointsOfInterest
            .Where(p => p.CityId == cityId &&
                        p.Id == pointOfInterestId)
            .Select(p => new PointOfInterestDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .FirstOrDefaultAsync();

        if (point == null)
        {
            return NotFound();
        }

        return Ok(point);
    }

    // POST: api/cities/1/pointsOfInterest
    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>>
        CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
    {
        var cityExists = await _context.Cities
            .AnyAsync(c => c.Id == cityId);

        if (!cityExists)
        {
            return NotFound();
        }

        var finalPoint = new PointOfInterest
        {
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description,
            CityId = cityId
        };

        _context.PointsOfInterest.Add(finalPoint);

        await _context.SaveChangesAsync();

        var result = new PointOfInterestDto
        {
            Id = finalPoint.Id,
            Name = finalPoint.Name,
            Description = finalPoint.Description
        };

        return CreatedAtRoute(
            "GetPointOfInterest",
            new
            {
                cityId = cityId,
                pointOfInterestId = finalPoint.Id
            },
            result);
    }
}