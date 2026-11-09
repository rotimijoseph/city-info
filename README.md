# City Explorer

A city information application built with ASP.NET Core Web API, Entity Framework Core, SQLite, Blazor Web and .NET MAUI.

The application allows users to browse cities, view individual city details, and manage points of interest.

## Prerequisites

- .NET 10 SDK
- .NET MAUI workload (for the native MAUI application)
- Visual Studio Code or another compatible code editor

## Local setup

Clone the repository and open the project folder in your terminal.

Build the API:

```powershell
dotnet build cityInfoApi.csproj
```

Build the Web frontend:

```powershell
dotnet build Cities.Client\Cities.Client.Web\Cities.Client.Web.csproj
```

### Running the application

The API and frontend run separately, so open two terminals.

**Terminal 1 – API**

```powershell
dotnet bin\Debug\net10.0\cityInfoApi.dll
```

Alternatively, you can try:

```powershell
dotnet run --project cityInfoApi.csproj
```

Use the DLL command if Windows Application Control blocks the generated executable.

**Terminal 2 – Web frontend**

```powershell
dotnet watch --project Cities.Client\Cities.Client.Web\Cities.Client.Web.csproj run
```

The `dotnet watch` command automatically rebuilds and refreshes the application when files change.

### Local URLs

- API: http://localhost:5000
- Web frontend: http://localhost:5293

The frontend port may differ depending on the launch settings. Use the URL printed in the terminal if it changes.

## Database setup

The application uses SQLite with Entity Framework Core.

Install the Entity Framework command-line tool (once):

```powershell
dotnet tool install --global dotnet-ef
```

Apply the existing database migrations:

```powershell
dotnet ef database update --project cityInfoApi.csproj --startup-project cityInfoApi.csproj
```

The database is stored in `cityinfo.db`.

Migrations are stored in the `Migrations` folder. They track changes to the database structure over time.

**Note:** The database file is local development data and should generally not be committed to Git.

## Available API endpoints

Base URL: `http://localhost:5000`

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/cities` | Get all cities |
| GET | `/api/cities/{cityId}` | Get one city and its points of interest |
| GET | `/api/cities/{cityId}/pointsOfInterest` | Get points of interest for a city |
| GET | `/api/cities/{cityId}/pointsOfInterest/{pointOfInterestId}` | Get one point of interest |
| POST | `/api/cities` | Create a city |
| POST | `/api/cities/{cityId}/pointsOfInterest` | Add a point of interest to a city |

## Project structure

The repository contains the API and frontend in one solution.

```text
city-info/
│
├── Controllers/
│   ├── CitiesController.cs
│   └── PointsOfInterestController.cs
│
├── Data/
│   └── CityInfoContext.cs
│
├── Models/
│   ├── City.cs
│   ├── PointOfInterest.cs
│   ├── CityDetailsDto.cs
│   └── Other DTOs...
│
├── Migrations/
│
├── Cities.Client/
│   │
│   ├── Cities.Client/
│   │   └── Native MAUI application
│   │
│   ├── Cities.Client.Shared/
│   │   ├── Components/
│   │   ├── Layout/
│   │   ├── Pages/
│   │   ├── wwwroot/
│   │   └── Routes.razor
│   │
│   └── Cities.Client.Web/
│       ├── Components/
│       ├── wwwroot/
│       └── Program.cs
│
├── cityInfoApi.csproj
├── CitiesProject.slnx
└── README.md
```

### Backend – API

The API is the backend of the application. It handles requests from the frontend, communicates with the database and returns data.

- **Controllers:** Define the API endpoints and handle incoming HTTP requests.
- **Models:** Represent the database entities, such as City and PointOfInterest.
- **DTOs:** Define the data transferred between the API and frontend.
- **Data:** Contains the Entity Framework database context, which connects the application to SQLite.
- **Migrations:** Track database structure changes.
- **Program.cs:** Configures services, database access and the API application.

### Frontend – Cities.Client

The frontend contains the user-facing applications.

**Cities.Client**

The native .NET MAUI application. It provides the foundation for running the application as a native mobile or desktop app.

**Cities.Client.Shared**

Contains Razor components and pages shared between the different frontend applications.

- `Components/`: Application-specific components, such as the cities list, add city form and individual city details.
- `Pages/`: Shared pages, including the default Home page.
- `Layout/`: Defines the overall application layout and navigation menu.
- `Routes.razor`: Handles page routing and determines which component is displayed for a URL.
- `wwwroot/`: Contains static frontend assets such as CSS, images, fonts and JavaScript.
- `app.css`: Controls the application's styling, colours, spacing and overall appearance.

**Cities.Client.Web**

The browser-based version of the application.

- `Program.cs`: Configures the Web application and its services, including the HTTP client used to communicate with the API.
- `Components/App.razor`: Defines the main HTML document and application entry point.
- `wwwroot/`: Contains static files specific to the Web project.

### What is wwwroot?

`wwwroot` is the standard folder used by ASP.NET Core and Blazor for static files.

It stands for the web root. Files inside it can be served directly to the browser.

For example:
- CSS files control the appearance of the application.
- Images and icons provide visual assets.
- JavaScript files provide browser-side functionality.

## Where to make changes

| If you want to... | Go to... |
|---|---|
| Change the navigation menu | `Cities.Client.Shared/Layout/NavMenu.razor` |
| Change the overall layout | `Cities.Client.Shared/Layout/MainLayout.razor` |
| Change colours, fonts or styling | `Cities.Client.Shared/wwwroot/app.css` |
| Change the Home page | `Cities.Client.Shared/Pages/Home.razor` |
| Change the Cities list | `Cities.Client.Shared/Components/CitiesList.razor` |
| Change the Add City form | `Cities.Client.Shared/Components/AddCity.razor` |
| Change individual city details | `Cities.Client.Shared/Components/CityDetails.razor` |
| Change API endpoints or request handling | `Controllers/` |
| Change database entities | `Models/` |
| Change database configuration | `Data/CityInfoContext.cs` and API `Program.cs` |
| Change frontend API connection | `Cities.Client.Web/Program.cs` |
| Change database structure | Create a new EF Core migration |

## Architecture overview

The general flow of the application is:

1. A user interacts with the Blazor frontend.
2. The frontend sends an HTTP request to the ASP.NET Core API.
3. The API controller handles the request.
4. Entity Framework Core communicates with the SQLite database.
5. The API returns the requested data.
6. The frontend displays the response to the user.

The shared frontend project allows the Web and MAUI applications to reuse the same Razor components instead of maintaining two separate user interfaces.

Features coming soon:
- Offline capability
- Search and filtering
- Favourites
- Images
- Maps
- User accounts