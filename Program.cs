using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using cityInfoApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(); // sets up OpenAPI documentation 
builder.Services.AddControllers(options => // registers MVC/Web API controllers
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters(); 

builder.Services.AddAuthorization(); // registers authorization services
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions.Add("additionalInfo", "Additional info example");
        ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
    };
});

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddDbContext<CityInfoContext>(options =>
    options.UseSqlite("Data Source=cityinfo.db")); // tells api to use cityinfo.db
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection(); // redirects HTTP to HTTPS

app.UseRouting(); 

app.UseAuthorization(); // enables authorisation middleware

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // maps controller routes to endpoints
});

app.Run();

