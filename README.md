# cyclone

API for enabling citizens to record weather forecasts in their locality.

## Design Goals

* fast
* maintainable

## Learning Goals

* Use .NET 8 (ran into problems with the Pomelo database adapter so downgraded to 6)
* CQRS and the mediator pattern
* microservice architecture
* SOLID principles
* Entity Framework
* Integration tests
* Options pattern

## Set-up

You will need MySQL or MariaDB (I got it via Docker-desktop)

Then run the EF commands to create migrations:

```
dotnet tool install --global dotnet-ef

dotnet ef migrations add InitialCreate --project cyclone.infrastructure --startup-project cyclone.api
```

More info on EF: https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-database/3-exercise-add-entity-framework-core

## Usage

Run the cyclone.api project and the Swagger GUI should start automatically.

Then start adding some forecasts!

## Useful Links

https://www.timeanddate.com/time/map
https://home.openweathermap.org/

## To Do

* create a front-end (React?) that uses the API