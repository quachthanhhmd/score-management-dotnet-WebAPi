# Score management Server Web-API
A project for building RESTful APIs using Asp.Net Core, mySql.
By running a single command, you will get a production-ready Asp.Net core app installed and fully configured on your machine. The app comes with many built-in features, such as authentication using JWT, request validation, unit and integration tests, continuous integration, API documentation, pagination, etc. For more details, check the features list below.

## Quick Start


To create a project, clone my project:

```bash
git clone https://github.com/quachthanhhmd/score-management-dotnet-WebAPi.git
```

## Table of Contents

- [Features](#features)
- [Project Structure](#project-structure)

## Features

- **SQL database**: [MySql](https://www.mysql.com).
- **Entity Framework Core**: using [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) to build database.
- **Authentication and authorization**: using [Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio)
- **Validation**: request data validation using [fluent](https://fluentvalidation.net/).
- **Error handling**: centralized error handling mechanism.
- **API documentation**: using [swagger](https://swagger.io/) for Asp.Net core.

## Project Structure

```
qlsv\
 |--Assets\         # Teamplate used to display something like export some document for client.
 |--controllers\    # Route controllers (controller layer).
 |--Data\           # Script to build Db using entity framework core.
 |--Migrations\     # Contain versions of our Db as well as data changes
 |--Models\         # Contain model of each data fields, service and Interface.
 |--Pages\          # Pages to display for client (but for Api, it does not use).
 |--properties\     # Variable environment.
 |--Utilities\      # Utility classes and functions
 |--ViewModels\     # ViewModels for all models.
 |--wwwroot         # Contains all things to display for client like images, js,...
 |--Startup.cs      # Config of web.
 |--Program.cs      # Main funtion to build web
```
