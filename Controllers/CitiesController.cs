// using System;
// using System.Reflection.Metadata.Ecma335;
// using cityInfoApi.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace cityInfoApi.Controllers;

// // JSON result is action result which formats given object as JSON 

// [ApiController] // requiring a certain type of routing 
// [Route("api/[controller]")] // matches first part of controller i.e., cities
// public class CitiesController : ControllerBase
// {
//     [HttpGet]
//     // public JsonResult GetCities()
//     // {
//     //     return new JsonResult(CitiesDataStore.Current.Cities);
//     // }

//     public ActionResult<IEnumerable<CityDto>> GetCities()
//     // result - of controller - action 
//     {
//         return Ok(CitiesDataStore.Current.Cities);
//     }
    
//     [HttpGet("{id}")]

//     // public JsonResult GetCity(int id)
//     // {
//     //     return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
//     // }

//     public ActionResult<CityDto> GetCity(int id)
//     {
//         // find the cirt
//         var cityToReturn = CitiesDataStore.Current.Cities
//             .FirstOrDefault(c => c.Id == id);

//         if (cityToReturn == null)
//         {
//             return NotFound();
//         }

//         return Ok(cityToReturn);
//     }
// }


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
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        var cities = await _context.Cities.ToListAsync();

        return Ok(cities);
    }

    // GET: api/cities/1
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var city = await _context.Cities.FindAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return Ok(city);
    }

    // POST: api/cities
    [HttpPost]
    public async Task<ActionResult<City>> CreateCity(City city)
    {
        city.Id = 0;

        _context.Cities.Add(city);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCity),
            new { id = city.Id },
            city);
    }
}