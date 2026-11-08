## Prerequisites 

.NET SDK 9

## Local setup 

`dotnet build`

`dotnet run`


Building frontend - `dotnet build Cities.Client\Cities.Client.Web\Cities.Client.Web.csproj`

`dotnet run --project cityInfoApi.csproj` [might need to build everything else too?]
`dotnet run --project "Cities.Client\Cities.Client.Web\Cities.Client.Web.csproj"`

Cities API should be running on http://localhost:5275
Frontend should be running on http://localhost:5293

For hotreload:
`dotnet watch --project Cities.Client\Cities.Client.Web\Cities.Client.Web.csproj run`

## Database setup 
`dotnet tool install --global dotnet-ef`
`dotnet ef database update`

## Available endpoints 
http://localhost:5275/api/cities
http://localhost:5275/api/cities/{cityId}
http://localhost:5275/api/cities/{cityId}/pointOfInterests
http://localhost:5275/api/cities/{cityId}/pointOfInterests/{pointofInterestId}

## Project structure

Cities.Client - Native MAUI application
Cities.Client.Web - Browser-based application
Cities.Client.Shared - Shared Razor components