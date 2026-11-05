using System;
using System.Drawing;
using cityInfoApi.Models;

namespace cityInfoApi;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        // initial dummy data
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1,
                Name = "New York City",
                Description = "The one with that big park",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Central Park",
                        Description = "The most visited urban park in the United States"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Empire State Building", 
                        Description = "A 102 story skyscraper located in Midtown Manhattan"
                    }
                }
            },
            new CityDto()
            {
                Id = 2,
                Name = "Antwerp",
                Description = "The one with the cathedral that was never really finished",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        Name = "Antwerp Central Station",
                        Description = "What it says on the tin"
                    }
                }
            },
            new CityDto()
            {
                Id = 3,
                Name = "Paris",
                Description = "The one with that big tower",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        Name = "Eiffel Tower",
                        Description = "Big tower thing"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 5,
                        Name = "The Louvre", 
                        Description = "That museum that got robbed in brought daylight. Also apparently the world's largest museum but I'm yet to fact check that"
                    }
                }
            }
        };
    }


}
