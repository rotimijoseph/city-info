using System;
using Microsoft.EntityFrameworkCore;
using cityInfoApi.Models;

namespace cityInfoApi.Data;

public class CityInfoContext : DbContext
{
    public CityInfoContext(
        DbContextOptions<CityInfoContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities => Set<City>();

    public DbSet<PointOfInterest> PointsOfInterest
        => Set<PointOfInterest>();
}