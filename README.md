# Permisos-core

Short description or introduction of your project.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- SQL Server (if using a different database, adjust the connection string accordingly)

## Getting Started

To get a local copy up and running follow these simple steps.

### Clone the repository

```bash
git clone https://github.com/SirMaiquis/Permisos-core.git
cd your-project
```

### Setting up the Database

1. Navigate to the Data directory where your DbContext is located.
2. Open appsettings.json and configure your database connection string under "DefaultConnection".

### Running the Application

1. Set the startup project to API.
2. Press F5 or click on the "Play" button in Visual Studio to run the application.
3. Migrations will automatically run, creating the initial tables and data.

### Running the Application

To run unit tests, navigate to the Test project, right click it and then click "run tests".